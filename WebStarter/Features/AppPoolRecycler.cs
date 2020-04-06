namespace VB.WebStarter.Features
{
    using System;
    using Assets;
    using Common;
    using Configs;
    using Helpers;
    using Microsoft.Web.Administration;

    public class AppPoolRecycler
    {
        public void Execute()
        {
            var config = ConfigurationHelper.ReadConfig();
            foreach (var appPoolConfig in config.AppPools)
            {
                if (!CheckIfPassSqlCondition(appPoolConfig.Query, appPoolConfig.ConnectionString))
                {                    
                    return;
                }

                RecycleOrRestartAppPool(appPoolConfig);
            }
        }

        public void RecycleOrRestartAppPool(AppPoolConfig appPoolConfigConfig)
        {
            var appPool = ServerManagerHelper.Instance.GetApplicationPool(appPoolConfigConfig.Name);
            if (appPool == null)
            {
                return;
            }

            switch (appPool.GetState())
            {
                case ObjectState.Started:
                    appPool.Recycle();
                    Logger.Instance.LogWarning(WebStarterConstants.ServiceName, $"Application pool ({appPoolConfigConfig.Name}) was recycled.");
                    break;
                case ObjectState.Stopped:
                    Logger.Instance.LogWarning(WebStarterConstants.ServiceName, $"Application pool ({appPoolConfigConfig.Name}) was started.");
                    appPool.Start();
                    break;
            }
        }

        public bool CheckIfPassSqlCondition(string sqlQuery, string connectionString)
        {
            var passSqlCondition = false;
            var sqlConnection = SqlHelper.Instance.GetSqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                var command = SqlHelper.Instance.GetSqlCommand(sqlQuery, sqlConnection);
                var dataReader = command.ExecuteReader();
                
                if (!dataReader.HasRows)
                {
                    passSqlCondition = true;
                }

                dataReader.Close();
                command.Dispose();
                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Logger.Instance.LogError(WebStarterConstants.ServiceName, $"{exception.Message}");
            }

            return passSqlCondition;
        }
    }
}
