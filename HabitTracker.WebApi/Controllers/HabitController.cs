using AutoMapper;
using HabitTracker.Application.Habits.Commands.CreateHabit;
using HabitTracker.Application.Habits.Commands.DeleteHabit;
using HabitTracker.Application.Habits.Commands.UpdateHabit;
using HabitTracker.Application.Habits.Queries.GetHabitDetails;
using HabitTracker.Application.Habits.Queries.GetHabitList;
using HabitTracker.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/[controller]")]
    public class HabitController : BaseController
    {
        private readonly IMapper _mapper;
        public HabitController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Gets the list of habits
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /habit
        /// </remarks>
        /// <returns>Returns HabitListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<HabitListVm>> GetAll()
        {   
            var query = new GetHabitListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the habit by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /habit/599539B4-A59C-4C27-8FEB-5D18CA9804A0
        /// </remarks>
        /// <param name="id">Habit id (guid)</param>
        /// <returns>Returns HabitDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user in unauthorized</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<HabitDetailsVm>> Get(Guid id)
        {
            var query = new GetHabitDetailsQuery
            {
                UserId = UserId,
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the habit
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /habit
        /// {
        ///     title: "habit title",
        ///     details: "habit details"
        /// }
        /// </remarks>
        /// <param name="createHabitDto">CreateHabitDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateHabitDto createHabitDto)
        {
            var command = _mapper.Map<CreateHabitCommand>(createHabitDto);
            command.UserId = UserId;
            var habitId = await Mediator.Send(command);
            return Ok(habitId);
        }

        /// <summary>
        /// Updates the habit
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /habit
        /// {
        ///     title: "updated habit title"
        /// }
        /// </remarks>
        /// <param name="updateHabitDto">UpdateHabitDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] UpdateHabitDto updateHabitDto)
        {
            var command = _mapper.Map<UpdateHabitCommand>(updateHabitDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes the habit by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /habit/130E03BA-E76C-4170-992D-5577FAE10E83
        /// </remarks>
        /// <param name="id">Id of the habit (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
