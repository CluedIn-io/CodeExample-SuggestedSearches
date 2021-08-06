using CluedIn.Crawling.RudiDoes.Core;

namespace CluedIn.Crawling.RudiDoes.Infrastructure.Factories
{
    public interface IRudiDoesClientFactory
    {
        RudiDoesClient CreateNew(RudiDoesCrawlJobData rudidoesCrawlJobData);
    }
}
