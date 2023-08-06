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
using Innoloft_Backend_Domain.Repositories.Events;
using Innoloft_Backend_Persistence.Repositories.Events;
using Innoloft_Backend_Domain.Repositories;
using Innoloft_Backend_Persistence.Repositories;
using Innoloft_Backend_Domain.Repositories.Users;
using Microsoft.AspNetCore.Builder;

namespace Innoloft_Backend_Persistence
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("Default"), sqlOptions =>
                    sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)));

            services.AddScoped(typeof(AppDbContext), typeof(AppDbContext));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        public static void MigrateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
            }
        }
    }
}
