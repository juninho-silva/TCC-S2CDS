using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using S2CDS.Api.Infrastructure.Repositories.User;

namespace S2CDS.Specs.StepDefinitions
{
    public class WebAppFactory : WebApplicationFactory<Program>
    {
        public readonly InMemoryUserRepository UserRepository = new();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IUserRepository>(x =>
                {
                    return UserRepository;
                });
            });

            builder.UseEnvironment("Development");
        }
    }
}
