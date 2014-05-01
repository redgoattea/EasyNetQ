using EasyNetQ.Tests.Mocking;
using NUnit.Framework;
using SimpleInjector;

namespace EasyNetQ.DI.Tests
{
    [TestFixture]
    [Explicit("Starts a connection to localhost")]
    public class SimpleInjectorAdapterTests
    {
        private Container container;
        private IBus bus;

        [SetUp]
        public void SetUp()
        {
            container = new Container();
            container.RegisterAsEasyNetQContainerFactory();

            bus = new MockBuilder().Bus;
        }

        [Test]
        public void Should_create_bus_with_simple_injector_adapter()
        {
            Assert.IsNotNull(bus);
        }

        [TearDown]
        public void TearDown()
        {
            if (bus != null)
            {
                bus.Dispose();
            }
            RabbitHutch.SetContainerFactory(() => new DefaultServiceProvider());
        }
    }
}
