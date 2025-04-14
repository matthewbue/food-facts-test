using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessFoods.Application.Interfaces;
using FitnessFoods.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using FitnessFoods.Domain.Interfaces;
using FitnessFoods.Application.Services;
using FitnessFoods.Application;

namespace FitnessFoods.Infrastructure
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IImportHistoryRepository, ImportHistoryRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductImportService, ProductImportService>();
            return services;
        }
    }
}
