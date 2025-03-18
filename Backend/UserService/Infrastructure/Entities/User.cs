using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eventify.Backend.UserService.Infrastructure.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Email { get; set; }

        [Required]
        public required string PasswordHash { get; set; }

        [Required]
        public Role Role { get; set; } = Role.User;
    }     
}