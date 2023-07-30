using Innoloft_Backend_Persistence.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySql.Data;
using System.Reflection;

namespace Innoloft_Backend_Persistence
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddEntityFrameworkLayer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
                    sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)));

            services.AddScoped(typeof(AppDbContext), typeof(AppDbContext));
            return services;
        }
    }
}
