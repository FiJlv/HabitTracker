using AutoMapper;
using HabitTracker.Application.Habits.Queries.GetHabitDetails;
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
    public class GetHabitDetailsQueryHandlerTests
    {
        private readonly HabitTrackerDbContext Context;
        private readonly IMapper Mapper;

        public GetHabitDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetHabitDetailsQueryHandler_Success()
        {
            // Arrange
            var handler = new GetHabitDetailsQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetHabitDetailsQuery
                {
                    UserId = HabitTrackerContextFactory.UserBId,
                    Id = Guid.Parse("9663F95D-49D2-44B6-8C7B-5A3B8BFF0960")
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<HabitDetailsVm>();
            result.Title.ShouldBe("Title2");
            result.CreationDate.ShouldBe(DateTime.Today);
        }
    }
}
