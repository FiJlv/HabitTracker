using HabitTracker.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, 
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<HabitTrackerDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddScoped<IHabitTrackerDbContext>(provider =>
                provider.GetService<HabitTrackerDbContext>());
            return services; 

        }
    }   
}
