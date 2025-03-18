using Eventify.Backend.EventService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Backend.EventService.Infrastructure.Data
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options) { }

        // DbSets for each entity
        public DbSet<Event> Events { get; set; }
        public DbSet<EventRegistration> EventRegistrations { get; set; }

        // Configure the model (relationships, keys, etc.)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Event-EventRegistration relationship
            modelBuilder.Entity<Event>()
                .HasMany(e => e.Registrations)
                .WithOne(r => r.Event)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Composite Key for EventRegistration
            modelBuilder.Entity<EventRegistration>()
                .HasKey(r => new { r.EventId, r.UserId });

            // You can add any further custom configurations here as needed
        }
    }
}
