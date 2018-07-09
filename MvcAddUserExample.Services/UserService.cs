using System.Threading.Tasks;
using MvcAddUserExample.Core.Interfaces.Providers;
using MvcAddUserExample.Core.Interfaces.Services;
using MvcAddUserExample.Core.Models;

namespace MvcAddUserExample.Services
{
    /// <inheritdoc/>
    public class UserService : IUserService
    {
        private readonly IUserValidationService userValidationService;
        private readonly IPasswordService passwordService;
        private readonly IAddUserProvider addUserProvider;

        public UserService(
            IUserValidationService userValidationService,
            IPasswordService passwordService,
            IAddUserProvider addUserProvider)
        {
            this.userValidationService = userValidationService;
            this.passwordService = passwordService;
            this.addUserProvider = addUserProvider;
        }

        /// <inheritdoc/>
        /// <exception cref="InvalidEmailException">Thrown if the user's email address is invalid or already exists in the database.</exception>
        public async Task AddUserAsync(UserToCreate user)
        {
            userValidationService.ValidateUserToCreate(user);
            string hash = passwordService.SaltAndHashPassword(user.Password);
            await addUserProvider.AddUserAsync(user.Email, hash);
        }
    }
}
