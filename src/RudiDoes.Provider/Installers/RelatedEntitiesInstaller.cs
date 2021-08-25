using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CluedIn.Core.Installers;
using CluedIn.RelatedEntities;
using Microsoft.Extensions.Logging;

namespace CluedIn.RudiDoes.RelatedEntities.Installers
{
    public class RelatedEntitiesInstaller : IWindsorInstaller
    {
        private ILogger Log;

        public RelatedEntitiesInstaller(ILogger log)
        {
            Log = log;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Log.LogInformation("[RudiDoes] Begin CluedIn.RudiDoes.RelatedEntities.Installers.Install()");

            // RelatedEntitiesUtility.CypherFluentQueriesCount() requires RelatedEntitiesQueries to be registered
            container.Register(Component.For<RelatedEntitiesQueries>().Instance(new RelatedEntitiesQueries()));

            // register all the IRelatedEntitiesProvider classes
            var providers = CluedInTypes.FromCluedInAssembliesWithServiceFromInterface<IRelatedEntitiesProvider>();
            foreach (var provider in providers)
            {
                Log.LogInformation($"[RudiDoes] Registering {provider.GetType()}");
            }
            container.Register(providers);

            Log.LogInformation("[RudiDoes] End CluedIn.RudiDoes.RelatedEntities.Installers.Install()");
        }
    }
}
