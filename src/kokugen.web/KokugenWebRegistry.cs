using System;
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
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.WithDefaultConventions();

                         x.AddAllTypesOf<IStartable>();
                         x.Convention<PermissionHanderConvention>();
                         x.ConnectImplementationsToTypesClosing(typeof (IPermissionHandler<>));
                         
                     }
                );

            For<HttpContextBase>().Use(ctx => new HttpContextWrapper(HttpContext.Current));
            For<HttpRequestBase>().Use(ctx => new HttpRequestWrapper(HttpContext.Current.Request));
            For(typeof (IPermissionExecuter<>)).Use(typeof (PermissionExecuter<>));
        }
    }

    public class PermissionHanderConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            
        }
    }
}