using AutoMapper;
using AutoMapper.QueryableExtensions;
using HabitTracker.Application.Common.Exceptions;
using HabitTracker.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HabitTracker.Application.Habits.Queries.GetHabitList
{
    public class GetHabitListQueryHandler
        : IRequestHandler<GetHabitListQuery, HabitListVm>
    {
        private readonly IHabitTrackerDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetHabitListQueryHandler(IHabitTrackerDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<HabitListVm> Handle(GetHabitListQuery request,
            CancellationToken cancellationToken)
        {
            var habitsQuery = await _dbContext.Habits
                .Where(habit => habit.UserId == request.UserId)
                .ProjectTo<HabitLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken); 

            return new HabitListVm { Habits = habitsQuery };            
        }
    }
}
