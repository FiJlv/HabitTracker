using HabitTracker.Application.Common.Exceptions;
using HabitTracker.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HabitTracker.Application.Habits.Commands.DeleteHabit
{
    public class DeleteHabitCommandHandler: 
        IRequestHandler<DeleteHabitCommand>
    {
        private readonly IHabitTrackerDbContext _dbContext;

        public DeleteHabitCommandHandler(IHabitTrackerDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle (DeleteHabitCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Habits.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Domain.Habit), request.Id);
            }

            _dbContext.Habits.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
