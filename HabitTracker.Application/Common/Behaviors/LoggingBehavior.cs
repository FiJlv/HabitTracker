using HabitTracker.Application.Interfaces;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HabitTracker.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse> where TRequest
        : IRequest<TResponse>
    {
        ICurrentUserService currentUserService;
        public LoggingBehavior(ICurrentUserService currentUserService)
        {
            this.currentUserService = currentUserService;
        }
        public async Task<TResponse> Handle(TRequest request, 
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var requestName = typeof(TRequest).Name;
            var userId = currentUserService.UserId;

            Log.Information("Habits Request: {Name} {@UserId} {@Request}",
                requestName, userId, request);

            var responce = await next();
            return responce; 

        }
    }
}
