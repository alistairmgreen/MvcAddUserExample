using MvcAddUserExample.Core.Exceptions;
using MvcAddUserExample.Core.Interfaces.Services;
using MvcAddUserExample.Core.Models;

namespace MvcAddUserExample.Services
{
    /// <inheritdoc />
    public class UserValidationService : IUserValidationService
    {
        /// <inheritdoc />
        /// <exception cref="InvalidEmailException">Thrown if the user does not have a valid email address.</exception>
        /// <remarks>We do not attempt to enforce the full (very complicated) validity rules for email addresses,
        /// because a real application would send a message to confirm that the address exists.
        /// Instead, we simply reject addresses that are blank or contain no '@' symbol.</remarks>
        public void ValidateUserToCreate(UserToCreate user)
        {
            CheckEmailAddress(user.Email);
        }

        private static void CheckEmailAddress(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidEmailException("The email address is required.");
            }

            if (!email.Contains("@"))
            {
                throw new InvalidEmailException("The email address is not valid.");
            }
        }
    }
}
