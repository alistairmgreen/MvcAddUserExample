using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MvcAddUserExample.Providers
{
    /// <summary>
    /// Extension methods on SqlConnection to reduce boilerplate code
    /// for creating and executing commands.
    /// </summary>
    internal static class SqlCommandHelper
    {
        /// <summary>
        /// Asynchronously executes an SQL statement that does not return any values.
        /// </summary>
        /// <param name="connection">The SQL connection.</param>
        /// <param name="sql">The SQL statement to be executed.</param>
        /// <param name="parameters">A dictionary of parameter values, keyed on the parameter names.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public static async Task ExecuteSqlStatementAsync(
            this SqlConnection connection,
            string sql,
            Dictionary<string, object> parameters)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = sql;

                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    command.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
                }

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
