using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyVaccine.WebApi.Models;

namespace MyVaccine.WebApi
{
    // Builds and returns the configured WebApplication for testing or hosting.
    public static class Program
    {
        public static WebApplication CreateApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure database using environment variable or appsettings.
            builder.Services.SetDatabaseConfiguration(builder.Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            return app;
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection SetDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                                   ?? configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found in environment variables or configuration.");

            services.AddDbContext<MyVaccineAppDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}