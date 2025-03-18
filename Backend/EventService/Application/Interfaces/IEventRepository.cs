using Eventify.Backend.EventService.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventify.Backend.EventService.Application.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event?> GetEventByIdAsync(Guid eventId);
        Task<Event> AddEventAsync(Event newEvent);
        Task<bool> UpdateEventAsync(Event updatedEvent);
        Task<bool> DeleteEventAsync(Guid eventId);
        Task<bool> RegisterUserAsync(Guid eventId, Guid userId);
        Task<bool> DeregisterUserAsync(Guid eventId, Guid userId);
    }
}
