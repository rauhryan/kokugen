using System.Web.Routing;
using FubuMVC.Core;
using FubuMVC.Core.Runtime;
using FubuMVC.StructureMap;
using Kokugen.Core;
using Kokugen.Core.Persistence;
using Kokugen.Web.Behaviors;
using Kokugen.Web.Conventions;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Kokugen.Web
{
    public class KokugenStructureMapBootstrapper
    {
        private readonly RouteCollection _routes;

        private KokugenStructureMapBootstrapper(RouteCollection routes)
        {
            _routes = routes;
        }

        public static void Bootstrap(RouteCollection routes, FubuRegistry fubuRegistry)
        {
            new KokugenStructureMapBootstrapper(routes).BootstrapStructureMap(fubuRegistry);
        }

        private void BootstrapStructureMap(FubuRegistry fubuRegistry)
        {
            UrlContext.Reset();

            ObjectFactory.Initialize(x =>
                                         {
                                             x.AddRegistry(new KokugenCoreRegistry());
                                             x.AddRegistry(new KokugenWebRegistry());
                                         });

            //var transProcessor = new TransactionProcessor(ObjectFactory.Container);

            //ObjectFactory.Container.ExecuteInTransaction(x =>
            //     {
            //         var startables =ObjectFactory.Container.Model.GetAllPossible<IStartable>();
            //         foreach (var startable in startables)
            //         {

            //             startable.Start();
            //         }
            //     });


            


            var fubuBootstrapper = new StructureMapBootstrapper(ObjectFactory.Container, fubuRegistry);
            fubuBootstrapper.Builder = (c, args, id) =>
                                           {
                                               return new TransactionalContainerBehavior(c, args, id);
                                           };
            fubuBootstrapper.Bootstrap(_routes);
        }
    }

    public class KokugenWebRegistry : Registry
    {
        public KokugenWebRegistry()
        {
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.WithDefaultConventions();
                     }
                );
        }
    }
}