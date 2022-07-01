using AutoMapper;
using HabitTracker.Application.Common.Mappings;
using HabitTracker.Application.Interfaces;
using HabitTracker.Persistence;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HabitTracker.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public HabitTrackerDbContext Context;
        public IMapper Mapper;

        public QueryTestFixture()
        {
            Context = HabitTrackerContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(IHabitTrackerDbContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            HabitTrackerContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
