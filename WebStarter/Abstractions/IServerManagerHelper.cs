namespace VB.WebStarter.Abstractions
{
    using Common;

    public interface IServerManagerHelper
    {
        IApplicationPoolWrapper GetApplicationPool(string name);
    }
}