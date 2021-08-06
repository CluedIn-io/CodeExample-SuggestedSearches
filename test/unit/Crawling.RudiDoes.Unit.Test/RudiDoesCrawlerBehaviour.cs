using CluedIn.Core.Crawling;
using CluedIn.Crawling;
using CluedIn.Crawling.RudiDoes;
using CluedIn.Crawling.RudiDoes.Infrastructure.Factories;
using Moq;
using Shouldly;
using Xunit;

namespace Crawling.RudiDoes.Unit.Test
{
    public class RudiDoesCrawlerBehaviour
    {
        private readonly ICrawlerDataGenerator _sut;

        public RudiDoesCrawlerBehaviour()
        {
            var nameClientFactory = new Mock<IRudiDoesClientFactory>();

            _sut = new RudiDoesCrawler(nameClientFactory.Object);
        }

        [Fact]
        public void GetDataReturnsData()
        {
            var jobData = new CrawlJobData();

            _sut.GetData(jobData)
                .ShouldNotBeNull();
        }
    }
}
