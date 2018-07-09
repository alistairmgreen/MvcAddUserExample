namespace MvcAddUserExample.Core.Models
{
    /// <summary>
    /// Domain model representing a new user to be created.
    /// </summary>
    /// <remarks>This is not the same as an existing user because it has no ID.</remarks>
    public class UserToCreate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserToCreate"/> class.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        public UserToCreate(string email, string password)
        {
            Email = email;
            Password = password;
        }

        /// <summary>
        /// Gets the user's email address.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Gets the user's password.
        /// </summary>
        public string Password { get; }
    }
}
