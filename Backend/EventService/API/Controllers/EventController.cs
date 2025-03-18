using Microsoft.AspNetCore.Mvc;
using Eventify.Backend.EventService.Application.Interfaces;
using Eventify.Backend.EventService.Infrastructure.Entities;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Eventify.Backend.EventService.API.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEvent([FromBody] Event newEvent)
        {
            var createdEvent = await _eventService.CreateEventAsync(newEvent);
            return CreatedAtAction(nameof(GetAllEvents), new { id = createdEvent.Id }, createdEvent);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] Event updatedEvent)
        {
            if (id != updatedEvent.Id)
                return BadRequest();

            var result = await _eventService.UpdateEventAsync(updatedEvent);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var result = await _eventService.DeleteEventAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpPost("{id}/register")]
        public async Task<IActionResult> RegisterUser(Guid id, [FromBody] Guid userId)
        {
            var result = await _eventService.RegisterUserAsync(id, userId);
            return result ? Ok() : BadRequest("Unable to register");
        }

        [HttpPost("{id}/deregister")]
        public async Task<IActionResult> DeregisterUser(Guid id, [FromBody] Guid userId)
        {
            var result = await _eventService.DeregisterUserAsync(id, userId);
            return result ? Ok() : BadRequest("Unable to deregister");
        }
    }
}
