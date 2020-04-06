namespace VB.WebStarter.Common
{
    using Microsoft.Web.Administration;

    public class ApplicationPoolWrapper : IApplicationPoolWrapper
    {
        private readonly ApplicationPool appPool;

        public ApplicationPoolWrapper(ApplicationPool appPool)
        {
            this.appPool = appPool;
        }

        public ObjectState GetState()
        {
            return appPool.State;
        }

        public void Recycle()
        {
            appPool.Recycle();
        }

        public void Start()
        {
            appPool.Start();
        }
    }
}