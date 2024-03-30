using Microsoft.EntityFrameworkCore;
using S2CDS.Api.Infrastruture.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionStringMySql = builder.Configuration.GetConnectionString("MySqlConnection");

//builder.Services.AddDbContext<ApiDbContext>(options =>
//    options.UseMySql(connectionStringMySql, ServerVersion.Parse("10.4.22-MariaDB"))
//);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
