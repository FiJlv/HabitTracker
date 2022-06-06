using FluentValidation;
using System;

namespace HabitTracker.Application.Habits.Commands.CreateHabit
{
    public class CreateHabitCommandValidator : AbstractValidator<CreateHabitCommand>
    {
      
        public CreateHabitCommandValidator()
        {
            RuleFor(createHabitCommand =>
                createHabitCommand.Title).NotEmpty().MaximumLength(250);
            RuleFor(createHabitCommand =>
                createHabitCommand.UserId).NotEqual(Guid.Empty);
        }
    }
}
