using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MvcAddUserExample.Providers
{
    /// <inheritdoc />
    public class ConnectionFactory : IConnectionFactory
    {
        /// <inheritdoc />
        public async Task<SqlConnection> ConnectAsync()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["UserDatabase"].ConnectionString;
            var connection = new SqlConnection(connectionString);

            try
            {
                await connection.OpenAsync();
                return connection;
            }
            catch (Exception)
            {
                connection.Dispose();
                throw;
            }
        }
    }
}
