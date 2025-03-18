using System.Collections.Generic;
using Eventify.Backend.UserService.Application.Validator;

namespace Eventify.Backend.UserService.Common.Exceptions
{
    public class UserValidationException : Exception
    {
        public ValidationResult ValidationErrors { get; }

        public UserValidationException(ValidationResult validationErrors)
            : base(validationErrors.ErrorMessage)
        {
            ValidationErrors = validationErrors;
        }

        // Override ToString to include validation details in the exception message
        public override string ToString()
        {
            return $"Exception: {base.ToString()}\n" +
                   $"Validation Errors: {ValidationErrors?.ErrorMessage ?? "No details available."}\n" +
                   $"Field: {ValidationErrors?.Field ?? "Unknown field"}";
        }
    }

    public class UserNotFoundException : Exception
    {
        public Guid? UserId { get; }
        public string? UserName { get; }

        // Default constructor
        public UserNotFoundException() { }

        // Constructor with specific message
        public UserNotFoundException(string message)
            : base(message) { }

        public override string ToString()
        {
            return $"Exception: {base.ToString()}";
        }
    }
}
