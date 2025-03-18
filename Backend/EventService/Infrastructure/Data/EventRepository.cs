using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventify.Backend.EventService.Application.Interfaces;
using Eventify.Backend.EventService.Infrastructure.Data;
using Eventify.Backend.EventService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Backend.EventService.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventDbContext _context;

        public EventRepository(EventDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events
                .Include(e => e.Registrations)
                .ToListAsync();
        }

        public async Task<Event?> GetEventByIdAsync(Guid eventId)
        {
            return await _context.Events
                .Include(e => e.Registrations)
                .FirstOrDefaultAsync(e => e.Id == eventId);
        }

        public async Task<Event> AddEventAsync(Event newEvent)
        {
            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
            return newEvent;
        }

        public async Task<bool> UpdateEventAsync(Event updatedEvent)
        {
            var existingEvent = await _context.Events.FindAsync(updatedEvent.Id);
            if (existingEvent == null)
                return false;

            _context.Entry(existingEvent).CurrentValues.SetValues(updatedEvent);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEventAsync(Guid eventId)
        {
            var eventToDelete = await _context.Events
                .Include(e => e.Registrations)
                .FirstOrDefaultAsync(e => e.Id == eventId);
            
            if (eventToDelete == null)
                return false;

            _context.EventRegistrations.RemoveRange(eventToDelete.Registrations);
            _context.Events.Remove(eventToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RegisterUserAsync(Guid eventId, Guid userId)
        {
            var existingEvent = await _context.Events
                .Include(e => e.Registrations)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (existingEvent == null || existingEvent.Registrations.Count >= existingEvent.Capacity)
                return false;

            if (existingEvent.Registrations.Any(r => r.UserId == userId))
                return false; // User already registered

            _context.EventRegistrations.Add(new EventRegistration
            {
                Id = Guid.NewGuid(),
                EventId = eventId,
                UserId = userId,
                RegisteredAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeregisterUserAsync(Guid eventId, Guid userId)
        {
            var registration = await _context.EventRegistrations
                .FirstOrDefaultAsync(r => r.EventId == eventId && r.UserId == userId);

            if (registration == null)
                return false;

            _context.EventRegistrations.Remove(registration);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
