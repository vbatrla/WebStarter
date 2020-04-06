namespace VB.WebStarter.Common
{
    using Microsoft.Web.Administration;

    public interface IApplicationPoolWrapper
    {
        ObjectState GetState();

        void Recycle();

        void Start();
    }
}