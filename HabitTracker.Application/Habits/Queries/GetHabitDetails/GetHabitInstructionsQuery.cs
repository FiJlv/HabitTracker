using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.Application.Habits.Queries.GetHabitDetails
{
    public class GetHabitInstructionsQuery : IRequest<HabitInstructionsVm>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
