using Castle.Windsor;
using CluedIn.Core;
using CluedIn.Core.Providers;
using CluedIn.Crawling.RudiDoes.Infrastructure.Factories;
using Moq;

namespace CluedIn.Provider.RudiDoes.Unit.Test.RudiDoesProvider
{
    public abstract class RudiDoesProviderTest
    {
        protected readonly ProviderBase Sut;

        protected Mock<IRudiDoesClientFactory> NameClientFactory;
        protected Mock<IWindsorContainer> Container;

        protected RudiDoesProviderTest()
        {
            Container = new Mock<IWindsorContainer>();
            NameClientFactory = new Mock<IRudiDoesClientFactory>();
            var applicationContext = new ApplicationContext(Container.Object);
            Sut = new RudiDoes.RudiDoesProvider(applicationContext, NameClientFactory.Object);
        }
    }
}
