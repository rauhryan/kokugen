using System;
using System.Linq;
using System.Web;
using FubuCore;
using Kokugen.Core;
using Kokugen.Core.Permissions.Handlers;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Kokugen.Web
{
    public class KokugenWebRegistry : Registry
    {
        public KokugenWebRegistry()
        {
         

            For<HttpContextBase>().Use(ctx => new HttpContextWrapper(HttpContext.Current));
            For<HttpRequestBase>().Use(ctx => new HttpRequestWrapper(HttpContext.Current.Request));
           
            For(typeof (IAuthorizationHandler<>)).Use(typeof (AuthorizationHandler<>));
            For(typeof (IAuthorize<>)).Use(typeof (AuthorizePermissions<>));

            Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();

                x.AddAllTypesOf<IStartable>();
                x.Convention<PermissionHandlerConvention>();
                x.ConnectImplementationsToTypesClosing(typeof(IAuthorize<>));

            }
             );
        }
    }

    public class PermissionHandlerConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if(type.Closes(typeof(IAuthorize<>)))
            {
                var selectedInterface = type.FindInterfaceThatCloses(typeof(IAuthorize<>));
          
                registry.Configure(graph =>
                                       {

                                           var family = graph.FindFamily(typeof (IAuthorize<>).MakeGenericType(selectedInterface.GetGenericArguments()[0]));
                                               
                                           // logic here needs some work but for us we only want one custom auth but we are open for debate.
                                           if (family != null && family.InstanceCount > 1)
                                           {
                                               throw new StructureMap.Exceptions.StructureMapConfigurationException(
                                                   string.Format("There are too many implementations of IAuthorize<{0}>", selectedInterface.GetGenericArguments()[0].Name));
                                           }
                                       });
                registry.For(selectedInterface).Add(
                    typeof (AuthorizePermissions<>).MakeGenericType(selectedInterface.GetGenericArguments()[0]));
            }
        }
    }

}