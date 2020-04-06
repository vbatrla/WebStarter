namespace VB.WebStarter.Helpers
{
    using System.Data.SqlClient;
    using Abstractions;

    public class SqlHelper : ISqlHelper
    {
        private static ISqlHelper privateInstance = new SqlHelper();

        public static ISqlHelper Instance
        {
            get
            {
                return privateInstance;
            }

            // Use only in Tests
            internal set
            {
                privateInstance = value;
            }
        }

        public SqlCommand GetSqlCommand(string sqlQuery, SqlConnection sqlConnection)
        {
            return new SqlCommand(sqlQuery, sqlConnection);
        }

        public SqlConnection GetSqlConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}