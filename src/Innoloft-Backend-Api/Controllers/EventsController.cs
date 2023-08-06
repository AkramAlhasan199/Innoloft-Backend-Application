using Innoloft_Backend_Application.DataTransferObjects;
using Innoloft_Backend_Application.DataTransferObjects.Events;
using Innoloft_Backend_Application.DataTransferObjects.Users;
using Innoloft_Backend_Domain.Entities;
using Innoloft_Backend_Domain.Services.Events;
using Innoloft_Backend_Domain.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text;

namespace Innoloft_Backend_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IUserService _userService;

        public EventsController(IEventService eventService,
            IUserService userService)
        {
            _eventService = eventService;
            _userService = userService;
        }

        [HttpGet]
        [Route("ListUserEvents")]
        public async Task<IActionResult> ListUserEvents()
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _eventService.ReadListAsync(e => e.CreatedById == userId, e => new EventDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                CreatedById = e.CreatedById,
                CreatedOn = e.CreatedOn,
                UpdatedOn = e.UpdatedOn
            });

            if(!result.Succeeded)
            {
                return BadRequest(result.ErrorOutcome);
            }

            return Ok(result.Outcome);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _eventService.ReadAsync(e => e.Id == id, e => new EventDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                CreatedById = e.CreatedById,
                CreatedOn = e.CreatedOn,
                UpdatedOn = e.UpdatedOn,
                Participants = e.Participants.Select(ue => new UserDto
                {
                    Id = ue.Id,
                    UserName = ue.UserName,
                }).ToList()
            });
            if(!result.Succeeded)
            {
                return BadRequest(result.ErrorOutcome);
            }

            return Ok(result.Outcome);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEventDto @event)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _eventService.CreateAsync(new Event
            {
                Title = @event.Title,
                Description = @event.Description,
                CreatedById = userId,
            });

            if(!result.Succeeded)
            {
                return BadRequest(result.ErrorOutcome);
            }

            return Ok();
        }

        [HttpPost]
        [Route("AddParticipant")]
        public async Task<IActionResult> AddParticipant(string eventId)
        {
            var eventResult = await _eventService.ReadAsync(e => e.Id == eventId);
            if (!eventResult.Succeeded)
            {
                return BadRequest(eventResult.ErrorOutcome);
            }

            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var userResult = await _userService.ReadAsync(u => u.Id == userId);
            if (!eventResult.Succeeded)
            {
                return BadRequest(eventResult.ErrorOutcome);
            }

            eventResult.Outcome.Participants.Add(userResult.Outcome);
            var result = await _eventService.UpdateAsync(eventResult.Outcome);
            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorOutcome);
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateEventDto @event)
        {
            var eventResult = await _eventService.ReadAsync(e => e.Id == @event.Id);

            if(!eventResult.Succeeded)
            {
                return BadRequest(eventResult.ErrorOutcome);
            }

            eventResult.Outcome.Title = @event.Title;
            eventResult.Outcome.Description = @event.Description;

            var result = await _eventService.UpdateAsync(eventResult.Outcome);

            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorOutcome);
            }

            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var eventResult = await _eventService.ReadAsync(e => e.Id == id);

            if (!eventResult.Succeeded)
            {
                return BadRequest(eventResult.ErrorOutcome);
            }

            var result = await _eventService.DeleteAsync(eventResult.Outcome);

            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorOutcome);
            }

            return Ok();
        }
    }
}
