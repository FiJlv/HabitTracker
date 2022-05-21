using Microsoft.EntityFrameworkCore;
using HabitTracker.Application.Interfaces;
using HabitTracker.Domain;
using HabitTracker.Persistence.EntityTypeConfigurations;

namespace HabitTracker.Persistence
{
    public class HabitTrackerDbContext : DbContext, IHabitTrackerDbContext
    {
        public DbSet<Habit> Habits { get; set; }

        public HabitTrackerDbContext(DbContextOptions<HabitTrackerDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new HabitConfiguration());
            base.OnModelCreating(builder);
        }

    }
}
