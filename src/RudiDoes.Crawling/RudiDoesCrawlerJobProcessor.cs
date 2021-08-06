using CluedIn.Crawling.RudiDoes.Core;

namespace CluedIn.Crawling.RudiDoes
{
    public class RudiDoesCrawlerJobProcessor : GenericCrawlerTemplateJobProcessor<RudiDoesCrawlJobData>
    {
        public RudiDoesCrawlerJobProcessor(RudiDoesCrawlerComponent component) : base(component)
        {
        }
    }
}
