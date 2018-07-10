using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MvcAddUserExample.Providers
{
    /// <summary>
    /// A provider that creates connections to the SQL Server database.
    /// </summary>
    public interface IConnectionFactory
    {
        /// <summary>
        /// Opens a connection to the SQL Server.
        /// </summary>
        /// <returns>An open <see cref="SqlConnection">.</see></returns>
        Task<SqlConnection> ConnectAsync();
    }
}