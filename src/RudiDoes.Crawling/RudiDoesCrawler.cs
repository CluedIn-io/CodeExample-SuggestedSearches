using System.Collections.Generic;

using CluedIn.Core.Crawling;
using CluedIn.Crawling.RudiDoes.Core;
using CluedIn.Crawling.RudiDoes.Infrastructure.Factories;

namespace CluedIn.Crawling.RudiDoes
{
    public class RudiDoesCrawler : ICrawlerDataGenerator
    {
        private readonly IRudiDoesClientFactory clientFactory;
        public RudiDoesCrawler(IRudiDoesClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public IEnumerable<object> GetData(CrawlJobData jobData)
        {
            if (!(jobData is RudiDoesCrawlJobData rudidoescrawlJobData))
            {
                yield break;
            }

            var client = clientFactory.CreateNew(rudidoescrawlJobData);

            //retrieve data from provider and yield objects
            
        }       
    }
}
