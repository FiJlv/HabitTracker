using HabitTracker.Application.Habits.Commands.CreateHabit;
using HabitTracker.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HabitTracker.Tests.Habits.Commands
{
    public class CreateHabitCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateHabitCommandHandler_Success()
        {
            // Arrange
            var handler = new CreateHabitCommandHandler(Context);
            var habitTitle = "habit title";
            var habitInstruction = "habit instruction";
            var habitDays = "habit days";

            //Act
            var habitId = await handler.Handle(
                new CreateHabitCommand { 
                    Title = habitTitle,
                    Instruction = habitInstruction,
                    HabitDays = habitDays,
                    UserId = HabitTrackerContextFactory.UserAId
                },
                CancellationToken.None);

            //Assert
            Assert.NotNull(
                await Context.Habits.SingleOrDefaultAsync(habit =>
                    habit.Id == habitId && habit.Title == habitTitle &&
                    habit.Instruction == habitInstruction && habit.HabitDays == habitDays));
        }
    }
}
