using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace HabitTracker.Application.Habits.Commands.DeleteHabit
{
    public class DeleteHabitCommandValidator : AbstractValidator<DeleteHabitCommand>
    {
        public DeleteHabitCommandValidator()
        {
            RuleFor(deleteHabitCommand =>
              deleteHabitCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(deleteHabitCommand =>
             deleteHabitCommand.Id).NotEqual(Guid.Empty);
        }
    }
}
