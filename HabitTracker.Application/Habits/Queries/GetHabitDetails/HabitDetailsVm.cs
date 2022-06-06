using AutoMapper;
using HabitTracker.Application.Common.Mappings;
using HabitTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.Application.Habits.Queries.GetHabitDetails
{
    public class HabitDetailsVm : IMapWith<Habit>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Instruction { get; set; }
        public string HabitDays { get; set; }
        public DateTime CreationDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Habit, HabitDetailsVm>()
                .ForMember(habitVm => habitVm.Id,
                opt => opt.MapFrom(habit => habit.Id))
                .ForMember(habitVm => habitVm.Title,
                opt => opt.MapFrom(habit => habit.Title))
                .ForMember(habitVm => habitVm.Instruction,
                opt => opt.MapFrom(habit => habit.Instruction))
                .ForMember(habitVm => habitVm.HabitDays,
                opt => opt.MapFrom(habit => habit.HabitDays))
                .ForMember(habitVm => habitVm.CreationDate,
                opt => opt.MapFrom(habit => habit.CreationDate));
        }
    }
}
