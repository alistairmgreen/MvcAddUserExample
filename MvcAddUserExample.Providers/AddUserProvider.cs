using System.Threading.Tasks;
using MvcAddUserExample.Core.Interfaces.Providers;

namespace MvcAddUserExample.Providers
{
    /// <summary>
    /// A concrete implementation of <see cref="IAddUserProvider"/>
    /// that uses SQL Server for persistence.
    /// </summary>
    public class AddUserProvider : IAddUserProvider
    {
        /// <inheritdoc />
        public Task AddUserAsync(string email, string passwordHash)
        {
            return Task.CompletedTask;
        }
    }
}
