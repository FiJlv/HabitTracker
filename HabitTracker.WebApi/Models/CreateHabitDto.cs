    using AutoMapper;
using HabitTracker.Application.Common.Mappings;
using HabitTracker.Application.Habits.Commands.CreateHabit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.WebApi.Models
{
    public class CreateHabitDto : IMapWith<CreateHabitCommand>
    {
        [Required]
        public string Title { get; set; }
        public string Instruction { get; set; }
        public string HabitDays { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateHabitDto, CreateHabitCommand>()
                .ForMember(habitCommand => habitCommand.Title,
                    opt => opt.MapFrom(habitDto => habitDto.Title))
                .ForMember(habitCommand => habitCommand.Instruction,
                    opt => opt.MapFrom(habitDto => habitDto.Instruction))
                .ForMember(habitCommand => habitCommand.HabitDays,
                    opt => opt.MapFrom(habitDto => habitDto.HabitDays));
        }
    }
}
