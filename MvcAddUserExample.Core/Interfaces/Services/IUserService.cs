using System.Threading.Tasks;
using MvcAddUserExample.Core.Models;

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
        /// <param name="user">The user to be added.</param>
        /// <returns>An awaitable task.</returns>
        Task AddUserAsync(UserToCreate user);
    }
}
