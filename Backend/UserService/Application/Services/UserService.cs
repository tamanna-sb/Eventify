// Application/Services/UserManagementService.cs
using System;
using Eventify.Backend.UserService.Common.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Eventify.Backend.UserService.Application.Interfaces;
using Eventify.Backend.UserService.Infrastructure.Entities;
using Eventify.Backend.UserService.Core.Services;
using Eventify.Backend.UserService.Application.Validator;


using System;

namespace Eventify.Backend.UserService.Application.Services
{
    public class UserManagementService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHelper _passwordHelper;
        private readonly UserValidator _userValidator;

        // Inject PasswordHelper
        public UserManagementService(IUserRepository userRepository, PasswordHelper passwordHelper)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
            _userValidator = new UserValidator();
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<IEnumerable<User?>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users;
        }

        public async Task AddUserAsync(User user)
        {
            var validationError = _userValidator.ValidateUser(user);
            validationError = validationError ?? await UserNameExistsAsync(user.UserName);   
            validationError = validationError ?? await EmailExistsAsync(user.Email);
            if (validationError != null){
                throw new UserValidationException(validationError);
            }
            user.PasswordHash = HashPassword(user.PasswordHash);
            await _userRepository.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            var tempUser = await _userRepository.GetUserByIdAsync(user.Id);
            if (tempUser == null)
            {
                throw new UserNotFoundException($"User ID {user.Id} not found");
            }

            var validationError = _userValidator.ValidateUser(user);
            validationError = validationError ?? await UserNameExistsAsync(user.UserName);   
            validationError = validationError ?? await EmailExistsAsync(user.Email);
            if (validationError != null){
                throw new UserValidationException(validationError);
            }

            user.PasswordHash = HashPassword(user.PasswordHash);
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task PatchUserAsync(Guid userId, Dictionary<string, object> updates)
            {
                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    throw new UserNotFoundException($"User ID {userId} not found");
                }

                foreach (var update in updates)
                {
                    switch (update.Key.ToLower())
                    {
                        case "username":
                            string newUserName = update.Value.ToString()!;
                            var userNameValidationError = _userValidator.ValidateUserName(newUserName);
                            userNameValidationError = userNameValidationError ?? await UserNameExistsAsync(newUserName);   
                            if (userNameValidationError != null)  throw new UserValidationException(userNameValidationError);
                            user.UserName = newUserName;
                            break;

                        case "email":
                            string newEmail = update.Value.ToString()!;
                            var emailValidationError = _userValidator.ValidateEmail(newEmail);
                            emailValidationError = emailValidationError ?? await EmailExistsAsync(newEmail);   
                            if (emailValidationError != null)  throw new UserValidationException(emailValidationError);
                            user.Email = newEmail;
                            break;

                        case "passwordhash":
                            string newPassword = update.Value.ToString()!;
                            var passwordValidation = _userValidator.ValidatePassword(newPassword);
                            if (passwordValidation != null) throw new UserValidationException(passwordValidation);
                            user.PasswordHash = HashPassword(newPassword);
                            break;

                        default:
                            break;
                    }

                    await _userRepository.PatchUserAsync(user);
                }

            }
        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new UserNotFoundException($"User ID {userId} not found");
            }
            await _userRepository.DeleteUserAsync(user);
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            var user = await _userRepository.GetUserByUserNameAsync(username);
            return user != null;
        }

        public async Task<User?> GetUserByUserNameAsync(string userName)
        {
            return await _userRepository.GetUserByUserNameAsync(userName);
        }

        // Use PasswordHelper to hash passwords when creating new users
        private string HashPassword(string password)
        {
            return _passwordHelper.HashPassword(password);
        }

        public async Task<User?> ValidateSignInAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUserNameAsync(username);
            if (user == null || !_passwordHelper.VerifyPassword(password, user.PasswordHash))
            {
                return null;
            }

            return user;
        }

        private async Task<ValidationResult?> UserNameExistsAsync(string userName)
        {
            if (await _userRepository.UserNameExistsAsync(userName))
            {
                return new ValidationResult
                {
                    Field = "UserName",
                    ErrorMessage = "UserName is already taken."
                };
            }
            return null;
        }

        private async Task<ValidationResult?> EmailExistsAsync(string email)
        {

            if (await _userRepository.EmailExistsAsync(email))
            {
                return new ValidationResult
                {
                    Field = "Email",
                    ErrorMessage = "Email is already registered."
                };

            }
            return null;
        }

    }
}
