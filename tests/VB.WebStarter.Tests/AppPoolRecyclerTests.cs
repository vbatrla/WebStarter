namespace VB.WebStarter.Tests
{
    using Abstractions;
    using Common;
    using Configs;
    using Features;
    using Helpers;
    using Microsoft.Web.Administration;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class AppPoolRecyclerTests
    {
        private AppPoolRecycler appPoolRecycler;
        private IApplicationPoolWrapper applicationPool;

        [SetUp]
        public void Setup()
        {
            appPoolRecycler = new AppPoolRecycler();
            Logger.Instance = Substitute.For<ILogger>();
            SqlHelper.Instance = Substitute.For<ISqlHelper>();
            var serverManager = Substitute.For<IServerManagerHelper>();
            applicationPool = Substitute.For<IApplicationPoolWrapper>();
            serverManager.GetApplicationPool(Arg.Any<string>()).Returns(applicationPool);
            ServerManagerHelper.Instance = serverManager;
        }

        [Test]
        public void Pool_Is_Started_Should_Be_Recycled()
        {
            // Arrange
            applicationPool.GetState().Returns(ObjectState.Started);

            // Act
            appPoolRecycler.RecycleOrRestartAppPool(new AppPoolConfig { Name = "AppPool" });

            // Asserts
            Logger.Instance.LogWarning(Arg.Is<string>(a => a.Contains("recycled")), Arg.Any<string>());
        }

        [Test]
        public void Pool_Is_Stopped_Should_Be_Started()
        {
            // Arrange
            applicationPool.GetState().Returns(ObjectState.Stopped);

            // Act
            appPoolRecycler.RecycleOrRestartAppPool(new AppPoolConfig { Name = "AppPool" });

            // Asserts
            Logger.Instance.LogWarning(Arg.Is<string>(a => a.Contains("started")), Arg.Any<string>());
        }
    }
}