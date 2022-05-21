using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HabitTracker.Domain;

namespace HabitTracker.Persistence.EntityTypeConfigurations
{
    public class HabitConfiguration : IEntityTypeConfiguration<Habit>
    {
        public void Configure(EntityTypeBuilder<Habit> builder)
        {
            builder.HasKey(habit => habit.Id);
            builder.HasIndex(habit => habit.Id).IsUnique();
            builder.Property(habit => habit.Title).HasMaxLength(250);  
        }
    }
}
