using Eventify.Backend.UserService.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventify.Backend.UserService.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<User?> GetUserByUserNameAsync(string userName);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task PatchUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<bool> UserNameExistsAsync(string userName);
        Task<bool> EmailExistsAsync(string email);
    }
}
