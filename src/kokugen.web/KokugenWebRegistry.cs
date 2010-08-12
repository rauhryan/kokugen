using System;
using System.Linq;
using System.Web;
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
            var selectedInterface =
                (from @interface in type.GetInterfaces()
                 where @interface.IsGenericType &&
                       @interface.GetGenericTypeDefinition()
                           .IsAssignableFrom(typeof (IAuthorize<>))
                 select @interface).SingleOrDefault();

            if (selectedInterface != null)
            {
                registry.Configure(graph =>
                                       {

                                           var family = graph.FindFamily(typeof (IAuthorize<>).MakeGenericType(
                                               selectedInterface.GetGenericArguments()[0]));
                                               
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