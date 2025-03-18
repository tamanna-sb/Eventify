using Eventify.Backend.UserService.Infrastructure.Entities;

namespace Eventify.Backend.UserService.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        bool ValidateToken(string token);
    }

}

