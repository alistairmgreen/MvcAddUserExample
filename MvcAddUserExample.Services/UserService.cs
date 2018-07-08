using System.Threading.Tasks;
using MvcAddUserExample.Core.Interfaces.Providers;
using MvcAddUserExample.Core.Interfaces.Services;

namespace MvcAddUserExample.Services
{
    /// <inheritdoc/>
    public class UserService : IUserService
    {
        private readonly IPasswordService passwordService;
        private readonly IAddUserProvider addUserProvider;

        public UserService(IPasswordService passwordService, IAddUserProvider addUserProvider)
        {
            this.passwordService = passwordService;
            this.addUserProvider = addUserProvider;
        }

        /// <inheritdoc/>
        public async Task AddUserAsync(string email, string password)
        {
            string hash = passwordService.SaltAndHashPassword(password);
            await addUserProvider.AddUserAsync(email, hash);
        }
    }
}
