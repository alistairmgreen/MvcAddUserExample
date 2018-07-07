using System.Diagnostics.CodeAnalysis;
using MvcAddUserExample.Core.Interfaces.Services;
using static Sodium.PasswordHash;

namespace MvcAddUserExample.Services
{
    /// <inheritdoc/>
    public class PasswordService : IPasswordService
    {
        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        // Inherently not unit testable because this is a facade in front of an external library.
        // Also, the output is random.
        public string SaltAndHashPassword(string plainTextPassword)
        {
            // The libsodium documentation recommends Medium strength for most use cases:
            // https://bitbeans.gitbooks.io/libsodium-net/content/password_hashing/index.html
            // This will consume 128 Mb of RAM.
            return ScryptHashString(plainTextPassword, Strength.Medium);
        }
    }
}
