using System;

namespace MvcAddUserExample.Core.Exceptions
{
    /// <summary>
    /// Exception indicating that an email address is invalid.
    /// </summary>
    public class InvalidEmailException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEmailException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public InvalidEmailException(string message)
            : base(message)
        {
        }
    }
}
