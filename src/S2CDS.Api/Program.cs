using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using S2CDS.Api.Configurations;
using S2CDS.Api.Dtos.v1.Donor;
using S2CDS.Api.Dtos.v1.Organization;
using S2CDS.Api.Infrastructure.Repositories.Campaign;
using S2CDS.Api.Infrastructure.Repositories.Donor;
using S2CDS.Api.Infrastructure.Repositories.Organization;
using S2CDS.Api.Infrastructure.Repositories.User;
using S2CDS.Api.Infrastructure.Services.Authentication;
using S2CDS.Api.Infrastructure.Services.Smtp;
using S2CDS.Api.Services.v1;
using S2CDS.Api.Services.v1.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var secret = builder.Configuration.GetValue<string>("JwtSettings:Secret");
var key = Encoding.ASCII.GetBytes(secret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});


// Telemetria
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// AutoMapper
builder.Services.AddAutoMapper(
    typeof(OrganizationProfile).Assembly,
    typeof(DonorProfile).Assembly
);

builder.Services.AddSingleton(s => new TokenService(secret));
builder.Services.AddTransient<EmailService>();
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));

builder.Services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(builder.Configuration.GetValue<string>("MongoDB:ConnectionString")));

builder.Services.AddScoped(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});

// Added Repositories
builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDonorRepository, DonorRepository>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();

// Added Services
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IDonorService, DonorService>();
builder.Services.AddScoped<ICampaignService, CampaignService>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }