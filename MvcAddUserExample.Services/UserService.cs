using System.Threading.Tasks;
using MvcAddUserExample.Core.Interfaces;

namespace MvcAddUserExample.Services
{
    /// <inheritdoc/>
    public class UserService : IUserService
    {
        /// <inheritdoc/>
        public Task AddUserAsync(string email, string password)
        {
            return Task.CompletedTask;
        }
    }
}
