using AutoMapper;
using HabitTracker.Application.Common.Mappings;
using HabitTracker.Application.Habits.Commands.UpdateHabit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.WebApi.Models
{
    public class UpdateHabitDto : IMapWith<UpdateHabitCommand>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Instruction { get; set; }

        public string HabitDays { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateHabitDto, UpdateHabitCommand>()
                .ForMember(habitCommand => habitCommand.Id,
                    opt => opt.MapFrom(habitDto => habitDto.Id))
                .ForMember(habitCommand => habitCommand.Title,
                    opt => opt.MapFrom(habitDto => habitDto.Title))
                .ForMember(habitCommand => habitCommand.Instruction,
                    opt => opt.MapFrom(habitDto => habitDto.Instruction))
                .ForMember(habitCommand => habitCommand.HabitDays,
                    opt => opt.MapFrom(habitDto => habitDto.HabitDays));
        }
    }
}
