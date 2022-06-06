using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace HabitTracker.Application.Habits.Commands.UpdateHabit
{
    public class UpdateHabitCommandValidator : AbstractValidator<UpdateHabitCommand> 
    {
        public UpdateHabitCommandValidator()
        {
            RuleFor(updateHabitCommand =>
                updateHabitCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(updateHabitCommand =>
             updateHabitCommand.Id).NotEqual(Guid.Empty);
            RuleFor(updateHabitCommand =>
             updateHabitCommand.Title).NotEmpty().MaximumLength(250);
        }
    }
}
