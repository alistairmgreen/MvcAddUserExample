namespace MvcAddUserExample.Core.Interfaces.Services
{
    /// <summary>
    /// A service for hashing passwords.
    /// </summary>
    public interface IPasswordService
    {
        /// <summary>
        /// Generates a random salt and uses it to hash the supplied password.
        /// </summary>
        /// <param name="plainTextPassword">The password to be hashed.</param>
        /// <returns>A string containing both the salt and the hash.</returns>
        string SaltAndHashPassword(string plainTextPassword);
    }
}
