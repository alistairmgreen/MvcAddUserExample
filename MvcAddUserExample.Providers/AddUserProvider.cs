using System.Configuration;
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
        /// <inheritdoc />
        public async Task AddUserAsync(string email, string passwordHash)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["UserDatabase"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "INSERT INTO [dbo].[Users] (Email, PasswordHash) VALUES (@email, @passwordhash)";
                    command.Parameters.Add(new SqlParameter("@email", email));
                    command.Parameters.Add(new SqlParameter("@passwordhash", passwordHash));

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (SqlException e) when (e.Message.Contains("Violation of UNIQUE KEY constraint"))
                    {
                        throw new DuplicateEmailException(email);
                    }
                }
            }
        }
    }
}
