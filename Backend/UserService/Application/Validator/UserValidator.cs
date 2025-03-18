using System.Text.RegularExpressions;
using Eventify.Backend.UserService.Infrastructure.Entities; 

namespace Eventify.Backend.UserService.Application.Validator
{
    public class ValidationResult
    {
        public required string Field { get; set; }
        public required string ErrorMessage { get; set; }
    }

    public class UserValidator
    {
        // Validate username
        public ValidationResult? ValidateUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName) || userName.Length < 6 || userName.Length > 50)
            {
                return new ValidationResult
                {
                    Field = "UserName",
                    ErrorMessage = "Username must be between 6 and 50 characters."
                };
            }

            return null;
        }

        // Validate email format
        public ValidationResult? ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                return new ValidationResult
                {
                    Field = "Email",
                    ErrorMessage = "Invalid email format."
                };
            }

            return null;
        }

        // Validate password strength
        public ValidationResult? ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8 || password.Length > 100)
            {
                return new ValidationResult
                {
                    Field = "Password",
                    ErrorMessage = "Password must be between 8 and 100 characters."
                };
            }

            if (!IsPasswordStrong(password))
            {
                return new ValidationResult
                {
                    Field = "Password",
                    ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character."
                };
            }

            return null;
        }

        // Check if password is strong
        private bool IsPasswordStrong(string password)
        {
            return password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit) &&
                   password.Any(ch => !char.IsLetterOrDigit(ch));
        }

        // Validate email using Regex
        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        public ValidationResult ValidateUser(User user)
        {
            // Check for username validation error
            var usernameError = ValidateUserName(user.UserName);
            if (usernameError != null)
                return usernameError;  // Return the first validation error

            // Check for email validation error
            var emailError = ValidateEmail(user.Email);
            if (emailError != null)
                return emailError;  // Return the first validation error

            // Check for password validation error
            var passwordError = ValidatePassword(user.PasswordHash);
            if (passwordError != null)
                return passwordError;  // Return the first validation error

            // If no validation errors, return null
            return null;
        }



    }
}