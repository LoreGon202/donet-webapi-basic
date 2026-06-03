using Microsoft.Extensions.DependencyInjection;

namespace MyVaccine.WebApi.Configurations
{
    public static class DbConfigurations
    {
        public static IServiceCollection SetDatabaseConfiguration(this IServiceCollection services)
        {
            // Configure your database services here, for example:
            // var connection = configuration.GetConnectionString("DefaultConnection");
            // services.AddDbContext<MyDbContext>(options => options.UseSqlServer(connection));
            return services;
        }
    }
}
