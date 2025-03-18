using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eventify.Backend.EventService.Application.Interfaces;
using Eventify.Backend.EventService.Infrastructure.Entities;

namespace Eventify.Backend.EventService.Application.Services
{
    public class EventManagementService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventManagementService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync() => await _eventRepository.GetAllEventsAsync();

        public async Task<Event?> GetEventByIdAsync(Guid eventId) => await _eventRepository.GetEventByIdAsync(eventId);

        public async Task<Event> CreateEventAsync(Event newEvent) => await _eventRepository.AddEventAsync(newEvent);

        public async Task<bool> UpdateEventAsync(Event updatedEvent) => await _eventRepository.UpdateEventAsync(updatedEvent);

        public async Task<bool> DeleteEventAsync(Guid eventId) => await _eventRepository.DeleteEventAsync(eventId);

        public async Task<bool> RegisterUserAsync(Guid eventId, Guid userId) => await _eventRepository.RegisterUserAsync(eventId, userId);

        public async Task<bool> DeregisterUserAsync(Guid eventId, Guid userId) => await _eventRepository.DeregisterUserAsync(eventId, userId);
    }
}
