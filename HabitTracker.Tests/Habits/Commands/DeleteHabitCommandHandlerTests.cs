using HabitTracker.Application.Common.Exceptions;
using HabitTracker.Application.Habits.Commands.CreateHabit;
using HabitTracker.Application.Habits.Commands.DeleteHabit;
using HabitTracker.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HabitTracker.Tests.Habits.Commands
{
    public class DeleteHabitCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteHabitCommandHandler_Success()
        {
            // Arrange
            var handler = new DeleteHabitCommandHandler(Context);

            // Act
            await handler.Handle(new DeleteHabitCommand
            {
                Id = HabitTrackerContextFactory.HabitIdForDelete,
                UserId = HabitTrackerContextFactory.UserAId
            }, CancellationToken.None);

            // Assert
            Assert.Null(Context.Habits.SingleOrDefault(habit =>
                habit.Id == HabitTrackerContextFactory.HabitIdForDelete));
        }

        [Fact]
        public async Task DeleteHabitCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new DeleteHabitCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteHabitCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = HabitTrackerContextFactory.UserAId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteHabitCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var deleteHandler = new DeleteHabitCommandHandler(Context);
            var createHandler = new CreateHabitCommandHandler(Context);
            var habitId = await createHandler.Handle(
                new CreateHabitCommand
                {
                    Title = "HabitTitle",
                    UserId = HabitTrackerContextFactory.UserAId
                }, CancellationToken.None);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await deleteHandler.Handle(
                    new DeleteHabitCommand
                    {
                        Id = habitId,
                        UserId = HabitTrackerContextFactory.UserBId
                    }, CancellationToken.None));
        }
    }
}
