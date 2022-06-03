using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HabitTracker.Application.Common.Mappings;
using HabitTracker.Domain;
using MediatR;

namespace HabitTracker.Application.Habits.Queries.GetHabitList
{
    public class HabitLookupDto : IMapWith<Habit>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
         
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Habit, HabitLookupDto>()
                .ForMember(habitDto => habitDto.Id,
                opt => opt.MapFrom(habit => habit.Id))
                .ForMember(habitDto => habitDto.Title,
                opt => opt.MapFrom(habit => habit.Title));
        }      
    }
}
