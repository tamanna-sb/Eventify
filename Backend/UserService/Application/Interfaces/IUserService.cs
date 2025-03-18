using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eventify.Backend.UserService.Infrastructure.Entities;

namespace Eventify.Backend.UserService.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<IEnumerable<User?>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task PatchUserAsync(Guid userId, Dictionary<string, object> updates);
        Task DeleteUserAsync(Guid userId);
        Task<User?> ValidateSignInAsync(string userName, string password);

    }
}
