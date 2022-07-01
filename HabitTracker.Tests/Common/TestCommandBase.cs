using HabitTracker.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly HabitTrackerDbContext Context;

        public TestCommandBase()
        {
            Context = HabitTrackerContextFactory.Create();
        }

        public void Dispose()
        {
            HabitTrackerContextFactory.Destroy(Context);
        }
    }
}
