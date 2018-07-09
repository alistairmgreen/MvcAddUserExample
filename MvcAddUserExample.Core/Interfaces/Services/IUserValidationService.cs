using MvcAddUserExample.Core.Models;

namespace MvcAddUserExample.Core.Interfaces.Services
{
    /// <summary>
    /// A service to check that new users are valid before they are created.
    /// </summary>
    public interface IUserValidationService
    {
        /// <summary>
        /// Ensures that a user has a valid email address and password.
        /// </summary>
        /// <param name="user">The new user to be validated.</param>
        void ValidateUserToCreate(UserToCreate user);
    }
}
