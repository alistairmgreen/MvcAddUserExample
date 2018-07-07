using System.Threading.Tasks;

namespace MvcAddUserExample.Core.Interfaces.Services
{
    /// <summary>
    /// A service that adds users to the database.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>An awaitable task.</returns>
        Task AddUserAsync(string email, string password);
    }
}
