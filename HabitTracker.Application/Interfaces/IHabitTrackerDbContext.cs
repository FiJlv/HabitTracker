using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HabitTracker.Domain;

namespace HabitTracker.Application.Interfaces
{
    public interface IHabitTrackerDbContext
    {
        DbSet<Habit> Habits { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
