using AutoMapper;
using HabitTracker.Application.Habits.Queries.GetHabitList;
using HabitTracker.Persistence;
using HabitTracker.Tests.Common;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HabitTracker.Tests.Habits.Queries
{
    [Collection("QueryCollection")]
    public class GetHabitListQueryHandlerTests
    {
        private readonly HabitTrackerDbContext Context;
        private readonly IMapper Mapper;

        public GetHabitListQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetHabitListQueryHandler_Success()
        {
            // Arrange
            var handler = new GetHabitListQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetHabitListQuery
                {
                    UserId = HabitTrackerContextFactory.UserBId
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<HabitListVm>();
            result.Habits.Count.ShouldBe(2);
        }
    }
}
