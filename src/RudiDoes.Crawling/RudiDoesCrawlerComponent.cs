using CluedIn.Core;
using CluedIn.Crawling.RudiDoes.Core;

using ComponentHost;

namespace CluedIn.Crawling.RudiDoes
{
    [Component(RudiDoesConstants.CrawlerComponentName, "Crawlers", ComponentType.Service, Components.Server, Components.ContentExtractors, Isolation = ComponentIsolation.NotIsolated)]
    public class RudiDoesCrawlerComponent : CrawlerComponentBase
    {
        public RudiDoesCrawlerComponent([NotNull] ComponentInfo componentInfo)
            : base(componentInfo)
        {
        }
    }
}

