using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Model;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddServiceApplication(this IServiceCollection services,IConfiguration config){
            services.AddDbContext<DataContext>(options => {
                 options.UseSqlite(config.GetConnectionString("DefaultConnection"));//Default connection string came from appSettings.Dev
            }
            );

            services.AddScoped<ITokenService,TokenService>();
            return services;
        }
    }
}