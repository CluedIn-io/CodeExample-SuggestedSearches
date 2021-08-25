using Castle.MicroKernel.Registration;

using CluedIn.Core;
using CluedIn.Core.Providers;
// 
using CluedIn.Crawling.RudiDoes.Core;
using CluedIn.Crawling.RudiDoes.Infrastructure.Installers;
// 
using ComponentHost;
using CluedIn.Core.Server;
using CluedIn.RelatedEntities;
using CluedIn.RudiDoes.RelatedEntities.Installers;
using Microsoft.Extensions.Logging;

namespace CluedIn.Provider.RudiDoes
{
    [Component(RudiDoesConstants.ProviderName, "Providers", ComponentType.Service, ServerComponents.ProviderWebApi, Components.Server, Components.DataStores, Isolation = ComponentIsolation.NotIsolated)]
    public sealed class RudiDoesProviderComponent : ServiceApplicationComponent<IBusServer>
    {
        public RudiDoesProviderComponent(ComponentInfo componentInfo)
            : base(componentInfo)
        {
            // Dev. Note: Potential for compiler warning here ... CA2214: Do not call overridable methods in constructors
            //   this class has been sealed to prevent the CA2214 waring being raised by the compiler
            Container.Register(Component.For<RudiDoesProviderComponent>().Instance(this));
        }

        public override void Start()
        {
            Container.Install(new InstallComponents());
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            Container.Register(Types.FromAssembly(asm).BasedOn<IProvider>().WithServiceFromInterface().If(t => !t.IsAbstract).LifestyleSingleton());
            Container.Register(Types.FromAssembly(asm).BasedOn<IEntityActionBuilder>().WithServiceFromInterface().If(t => !t.IsAbstract).LifestyleSingleton());
            Container.Register(Types.FromAssembly(asm).BasedOn<IRelatedEntitiesProvider>().WithServiceFromInterface().If(t => !t.IsAbstract).LifestyleSingleton());

            Log.LogInformation("[RudiDoes] Begin CluedIn.Provider.RudiDoes.Start()");
            Container.Install(new RelatedEntitiesInstaller(Log));
            State = ServiceState.Started;
            Log.LogInformation("[RudiDoes] End CluedIn.Provider.RudiDoes.Start()");
        }

        public override void Stop()
        {
            if (State == ServiceState.Stopped)
                return;

            State = ServiceState.Stopped;
        }
    }
}
