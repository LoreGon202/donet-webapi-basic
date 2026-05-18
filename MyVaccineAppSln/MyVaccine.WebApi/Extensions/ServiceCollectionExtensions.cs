using Microsoft.Extensions.DependencyInjection;

namespace MyVaccine.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // Placeholder extension to satisfy the call in Program.cs.
        // Move actual DB-related registrations or initializers here if needed.
        public static IServiceCollection SetDatabaseConfiguration(this IServiceCollection services)
        {
            return services;
        }
    }
}