namespace Eventify.Backend.UserService.API.DTOs
{
    // Registration model
    public class RegisterRequest
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}