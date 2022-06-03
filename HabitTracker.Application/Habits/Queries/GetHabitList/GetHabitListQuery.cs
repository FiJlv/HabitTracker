 using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.Application.Habits.Queries.GetHabitList
{
    public class GetHabitListQuery : IRequest<HabitListVm>
    {
        public Guid UserId { get; set; } 
    }
}
