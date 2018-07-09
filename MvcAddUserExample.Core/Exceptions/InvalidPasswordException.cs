using System;
using static MvcAddUserExample.Core.Constants.PasswordRestrictions;

namespace MvcAddUserExample.Core.Exceptions
{
    /// <summary>
    /// An exception indicating that a password is missing or too weak.
    /// </summary>
    public class InvalidPasswordException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPasswordException"/> class.
        /// </summary>
        public InvalidPasswordException()
            : base($"The password is required and must contain at least {MINIMUM_LENGTH} characters.")
        {
        }
    }
}
