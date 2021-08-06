using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CluedIn.Core.Installers;
using CluedIn.RelatedEntities;

namespace CluedIn.RudiDoes.RelatedEntities.Installers
{
    public class RelatedEntitiesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.Register(Component.For<RelatedEntitiesQueries>().Instance(new RelatedEntitiesQueries()));
            container.Register(CluedInTypes.FromCluedInAssembliesWithServiceFromInterface<IRelatedEntitiesProvider>());
        }
    }
}