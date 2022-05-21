namespace HabitTracker.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(HabitTrackerDbContext context)
        {
            context.Database.EnsureCreated();
        }

    }
}
