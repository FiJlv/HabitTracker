using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.Application.Habits.Queries.GetHabitDetails
{
    public class GetHabitDetailsValidator: AbstractValidator<GetHabitDetailsQuery>
    {
        public GetHabitDetailsValidator()
        {
            RuleFor(habit => habit.Id).NotEqual(Guid.Empty);
            RuleFor(habit => habit.UserId).NotEqual(Guid.Empty);
        }
    }
}
