using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HabitTracker.Application.Habits.Commands.CreateHabit
{
    public class CreateHabitCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Instruction { get; set; }
        public string HabitDays { get; set; }

    }
}
