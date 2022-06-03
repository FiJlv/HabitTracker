using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HabitTracker.Application.Interfaces;
using MediatR;
using HabitTracker.Application.Common.Exceptions;

namespace HabitTracker.Application.Habits.Commands.UpdateHabit
{
    public class UpdateHabitCommandHandler : IRequest<UpdateHabitCommand>
    {
        private readonly IHabitTrackerDbContext _dbContext;
        public UpdateHabitCommandHandler(IHabitTrackerDbContext dbContext) => _dbContext = dbContext;
        public async Task<Unit> Handle(UpdateHabitCommand request, CancellationToken cancellationToken)
        {
            var entity = 
                await _dbContext.Habits.FirstOrDefaultAsync(habit => habit.Id == request.Id, cancellationToken);

            if(entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Domain.Habit), request.Id);
            }

            entity.Instruction = request.Instruction;
            entity.Title = request.Title;
            entity.HabitDays = request.HabitDays;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;  
        }
    }
}
