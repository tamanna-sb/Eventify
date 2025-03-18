using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eventify.Backend.EventService.Infrastructure.Entities
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required DateTime StartDate { get; set; }

        [Required]
        public required DateTime EndDate { get; set; }

        [Required]
        public required string Location { get; set; }

        [Required]
        public required int Capacity { get; set; }

        public List<EventRegistration> Registrations { get; set; } = new List<EventRegistration>();
    }
}
