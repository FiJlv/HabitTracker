using FluentValidation;
using HabitTracker.Application.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services
                .AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            services.AddTransient
                (typeof(IPipelineBehavior<,>), 
                typeof(ValidationBehavior<,>));
            services.AddTransient
             (typeof(IPipelineBehavior<,>),
             typeof(LoggingBehavior<,>));
            return services;
        }
    }
}
