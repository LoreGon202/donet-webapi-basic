using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Configurations;

namespace MyVaccine.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers
            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
                                                               http://localhost:5237/swagger
            // Database
            builder.Services.SetDatabaseConfiguration(builder.Configuration);

            // Authentication JWT
            builder.Services.SetMyVaccioneAuthConfiguration();

            var app = builder.Build();

            // Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Authentication
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection SetDatabaseConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString =
                Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                ?? configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' not found.");
            }

            services.AddDbContext<MyVaccineAppDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}