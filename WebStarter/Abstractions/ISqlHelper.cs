namespace VB.WebStarter.Abstractions
{
    using System.Data.SqlClient;

    public interface ISqlHelper
    {
        SqlConnection GetSqlConnection(string connectionString);

        SqlCommand GetSqlCommand(string sqlQuery, SqlConnection sqlConnection);
    }
}