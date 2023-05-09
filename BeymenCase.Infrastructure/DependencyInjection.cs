using BeymenCase.Application.Interfaces;
using BeymenCase.Application.Repositories;
using BeymenCase.Infrastructure.Persistence;
using BeymenCase.Infrastructure.Services;
using BeymenCase.Library;
using BeymenCase.Library.Entities;
using BeymenCase.Library.Interfaces;
using BeymenCase.Library.Repositories;
using BeymenCase.Library.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeymenCase.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
        {
            ConfigureDatabase(services, configuration);
            ConfigureRedis(services, configuration);
            ConfigureServices(services);
            ConfigureRader(services, configuration);

            return services;
        }
        private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BeymenDbContext>(options =>
                   options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void Seed(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<BeymenDbContext>();
            context.Database.Migrate();

            if (context.AppData.Count() == 0)
            {
                context.AppData.AddRange(
                    new List<AppData>() {
                         new AppData() { Id = Guid.NewGuid(), Name = "SiteName", Type = "String", Value = "Boyner.com.tr", IsActive = true, ApplicationName = "BeymenCase"},
                         new AppData() { Id=Guid.NewGuid(),Name = "IsBasketEnabled", Type = "Boolean", Value = "True", IsActive = true, ApplicationName = "BeymenCaseStudy" },
                         new AppData() {Id = Guid.NewGuid(), Name = "MaxItemCount", Type = "Int", Value = "50", IsActive = false, ApplicationName = "BeymenCase"},
                        }
                    );
            }

            context.SaveChanges();

        }
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<ICacheService, CacheService>();
            services.AddScoped<IConfigurationService, ConfigurationService>();
            
        }
        private static void ConfigureRader(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfigurationReader>(x => new ConfigurationReader("BeymenCase", configuration.GetConnectionString("DefaultConnection"), 10000));
        }
        private static void ConfigureRedis(IServiceCollection services, IConfiguration configuration)
        {
            var options = ConfigurationOptions.Parse(configuration.GetConnectionString("RedisConfig"));
            options.ConnectRetry = 5;
            options.AbortOnConnectFail = false;
            var multiplexer = ConnectionMultiplexer.Connect(options);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);
        }
    }
}
