using System.Linq;
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

            // TODO: need to figure out exact method for listing all the derived classes without using windsor
            // {
            //     // log all the derived classes - that is the intent of our registration below
            //     // unfortunately castle windsor doesn't offer debug options around a IRegistration object
            //     var classes = System.AppDomain.CurrentDomain.GetAssemblies()
            //                .SelectMany(assembly => assembly.GetTypes())
            //                .Where(type => type.IsSubclassOf(typeof(IRelatedEntitiesProvider)));
            //     foreach (var derivedClass in classes)
            //     {
            //         Log.LogInformation($"[RudiDoes] Registering {derivedClass}");
            //     }
            // }

            // register all the IRelatedEntitiesProvider classes
            container.Register(CluedInTypes.FromCluedInAssembliesWithServiceFromInterface<IRelatedEntitiesProvider>());

            Log.LogInformation("[RudiDoes] End CluedIn.RudiDoes.RelatedEntities.Installers.Install()");
        }
    }
}
