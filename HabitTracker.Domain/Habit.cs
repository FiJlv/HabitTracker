using System;

namespace HabitTracker.Domain
{
    public class Habit
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Instruction { get; set; }
        public string HabitDays { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
