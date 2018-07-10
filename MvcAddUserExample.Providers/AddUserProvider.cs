using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MvcAddUserExample.Core.Exceptions;
using MvcAddUserExample.Core.Interfaces.Providers;

namespace MvcAddUserExample.Providers
{
    /// <summary>
    /// A concrete implementation of <see cref="IAddUserProvider"/>
    /// that uses SQL Server for persistence.
    /// </summary>
    public class AddUserProvider : IAddUserProvider
    {
        private readonly IConnectionFactory connectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddUserProvider"/> class.
        /// </summary>
        /// <param name="factory">A provider that creates connections to the SQL database.</param>
        public AddUserProvider(IConnectionFactory factory)
        {
            connectionFactory = factory;
        }

        /// <inheritdoc />
        public async Task AddUserAsync(string email, string passwordHash)
        {
            using (var connection = await connectionFactory.ConnectAsync())
            {
                try
                {
                    await connection.ExecuteSqlStatementAsync(
                     "INSERT INTO [dbo].[Users] (Email, PasswordHash) VALUES (@email, @passwordhash)",
                      new Dictionary<string, object>
                      {
                        { "@email", email },
                        { "@passwordhash", passwordHash }
                      });
                }
                catch (SqlException e) when (e.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    throw new InvalidEmailException($"Email address {email} is already associated with a user account.");
                }
            }
        }
    }
}
