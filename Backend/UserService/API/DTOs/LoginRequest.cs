namespace Eventify.Backend.UserService.API.DTOs
{
    // Login model
    public class LoginRequest
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}