using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.Application.Habits.Queries.GetHabitList
{
    public class HabitListVm
    {
        public IList<HabitLookupDto> Habits { get; set; }
    }
}
