using System.Threading.Tasks;
using MvcAddUserExample.Core.Interfaces.Services;

namespace MvcAddUserExample.Services
{
    /// <inheritdoc/>
    public class UserService : IUserService
    {
        private readonly IPasswordService passwordService;

        public UserService(IPasswordService passwordService)
        {
            this.passwordService = passwordService;
        }

        /// <inheritdoc/>
        public Task AddUserAsync(string email, string password)
        {
            string hash = passwordService.SaltAndHashPassword(password);
            return Task.CompletedTask;
        }
    }
}
