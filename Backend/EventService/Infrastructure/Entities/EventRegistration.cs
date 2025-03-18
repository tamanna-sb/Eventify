using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Eventify.Backend.EventService.Infrastructure.Entities
{
    public class EventRegistration
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required Guid EventId { get; set; }

        [Required]
        public required Guid UserId { get; set; }

        [Required]
        public required DateTime RegisteredAt { get; set; }

        [JsonIgnore]
        public Event Event { get; set; } = null!;
    }
}
