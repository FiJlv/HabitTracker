using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HabitTracker.Persistence;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HabitTracker.Domain;

namespace HabitTracker.Tests.Common
{
    public class HabitTrackerContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid HabitIdForDelete = Guid.NewGuid();
        public static Guid HabitIdForUpdate = Guid.NewGuid();

        public static HabitTrackerDbContext Create()
        {
            var options = new DbContextOptionsBuilder<HabitTrackerDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new HabitTrackerDbContext(options);
            context.Database.EnsureCreated();
            context.Habits.AddRange(
                new Habit
                {
                    Id = Guid.Parse("{B68ECE43-20BE-4470-9584-41C8167E147B}"),
                    UserId = UserAId,
                    Title = "Title1",
                    Instruction = "Some instruction1",
                    HabitDays = "Monday, Tuesday, Friday",
                    CreationDate = DateTime.Today                 
                },
                 new Habit
                 {
                     Id = Guid.Parse("{9663F95D-49D2-44B6-8C7B-5A3B8BFF0960}"),
                     UserId = UserBId,
                     Title = "Title2",
                     Instruction = "Some instruction2",
                     HabitDays = "Monday, Friday",
                     CreationDate = DateTime.Today
                 },

                 new Habit
                 {
                     Id = HabitIdForDelete,
                     UserId = UserAId,
                     Title = "Title3",
                     Instruction = "Some instruction2",
                     HabitDays = "Monday",
                     CreationDate = DateTime.Today
                 },

                 new Habit
                 {
                     Id = HabitIdForUpdate,
                     UserId = UserBId,
                     Title = "Title4",
                     Instruction = "Some instruction4",
                     HabitDays = "Friday",
                     CreationDate = DateTime.Today
                 }
            );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(HabitTrackerDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
