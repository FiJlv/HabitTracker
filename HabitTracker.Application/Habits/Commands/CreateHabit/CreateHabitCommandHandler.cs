using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using HabitTracker.Domain;
using HabitTracker.Application.Interfaces;
using System.Threading;

namespace HabitTracker.Application.Habits.Commands.CreateHabit
{
    public class CreateHabitCommandHandler : IRequestHandler<CreateHabitCommand,Guid>
    {
        private readonly IHabitTrackerDbContext _dbContext;
        public CreateHabitCommandHandler(IHabitTrackerDbContext dbContext) => _dbContext = dbContext;
        public async Task<Guid> Handle(CreateHabitCommand request, CancellationToken cancellationToken) 
        {
           
            var habit = new Habit
            {
                UserId = request.UserId,
                Title = request.Title,
                Instruction = request.Instruction,
                Id = Guid.NewGuid(),
                HabitDays = request.HabitDays,
                CreationDate = DateTime.Now
            };

            await _dbContext.Habits.AddAsync(habit, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return habit.Id;
        }
    }
}
