using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HabitTracker.Application.Common.Exceptions;
using HabitTracker.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Application.Habits.Queries.GetHabitDetails
{
    public class GetHabitInstructionsQueryHandler
        :IRequestHandler<GetHabitInstructionsQuery, HabitInstructionsVm> 
    {
        private readonly IHabitTrackerDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetHabitInstructionsQueryHandler(IHabitTrackerDbContext dbContext, 
            IMapper mapper) => ( _dbContext, _mapper) = (dbContext, mapper);

        public async Task <HabitInstructionsVm> Handle(GetHabitInstructionsQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Habits
                .FirstOrDefaultAsync(habit =>
                habit.Id == request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Domain.Habit), request.Id);
            }

            return _mapper.Map<HabitInstructionsVm>(entity); 
        }
    }
}
