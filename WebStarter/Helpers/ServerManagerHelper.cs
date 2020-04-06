namespace VB.WebStarter.Helpers
{
    using Abstractions;
    using Common;
    using Microsoft.Web.Administration;

    public class ServerManagerHelper : IServerManagerHelper
    {
        private static IServerManagerHelper privateInstance = new ServerManagerHelper();

        public static IServerManagerHelper Instance
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

        public IApplicationPoolWrapper GetApplicationPool(string name)
        {
            return new ApplicationPoolWrapper(GetServerManager().ApplicationPools[name]);
        }

        private ServerManager GetServerManager()
        {
            return new ServerManager();
        }
    }
}