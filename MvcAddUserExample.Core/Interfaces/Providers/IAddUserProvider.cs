using System.Threading.Tasks;

namespace MvcAddUserExample.Core.Interfaces.Providers
{
    /// <summary>
    /// A provider for persisting users to the database.
    /// </summary>
    public interface IAddUserProvider
    {
        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="passwordHash">A salted and hashed password.</param>
        /// <returns>An awaitable Task.</returns>
        Task AddUserAsync(string email, string passwordHash);
    }
}
