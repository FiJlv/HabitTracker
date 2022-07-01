using HabitTracker.Application.Habits.Commands.UpdateHabit;
using HabitTracker.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using HabitTracker.Application.Common.Exceptions;

namespace HabitTracker.Tests.Habits.Commands
{
    public class UpdateHabitCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateHabitCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateHabitCommandHandler(Context);
            var updatedTitle = "new title";

            // Act
            await handler.Handle(new UpdateHabitCommand
            {
                Id = HabitTrackerContextFactory.HabitIdForUpdate,
                UserId = HabitTrackerContextFactory.UserBId,
                Title = updatedTitle
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(await Context.Habits.SingleOrDefaultAsync(habit =>
                habit.Id == HabitTrackerContextFactory.HabitIdForUpdate &&
                habit.Title == updatedTitle));
        }

        [Fact]
        public async Task UpdateHabitCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new UpdateHabitCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateHabitCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = HabitTrackerContextFactory.UserAId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateHabitCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new UpdateHabitCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(
                    new UpdateHabitCommand
                    {
                        Id = HabitTrackerContextFactory.HabitIdForUpdate,
                        UserId = HabitTrackerContextFactory.UserAId
                    },
                    CancellationToken.None);
            });
        }
    }
}
