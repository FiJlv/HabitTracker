using AutoMapper;
using HabitTracker.Application.Habits.Commands.CreateHabit;
using HabitTracker.Application.Habits.Commands.DeleteHabit;
using HabitTracker.Application.Habits.Commands.UpdateHabit;
using HabitTracker.Application.Habits.Queries.GetHabitDetails;
using HabitTracker.Application.Habits.Queries.GetHabitList;
using HabitTracker.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class HabitController : BaseController
    {
        private readonly IMapper _mapper;
        public HabitController(IMapper mapper) => _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<HabitListVm>> GetAll()
        {
            var query = new GetHabitListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HabitInstructionsVm>> Get(Guid id)
        {
            var query = new GetHabitInstructionsQuery
            {
                UserId = UserId,
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateHabitDto createHabitDto)
        {
            var command = _mapper.Map<CreateHabitCommand>(createHabitDto);
            command.UserId = UserId;
            var noteId = await Mediator.Send(command);
            return Ok(noteId);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateHabitDto updateHabitDto)
        {
            var command = _mapper.Map<UpdateHabitCommand>(updateHabitDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteHabitCommand
            {
                Id = id,
                UserId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
