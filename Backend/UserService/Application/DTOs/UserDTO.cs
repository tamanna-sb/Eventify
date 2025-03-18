using System;
using Eventify.Backend.UserService.Infrastructure.Entities;
public class UserDto
{
    public Guid Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required Role Role { get; set; }
}
