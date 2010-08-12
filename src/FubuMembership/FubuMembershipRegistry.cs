
using System;
using FubuMembership.Membership.Services;
using FubuMVC.Core.Configuration;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace FubuMembership
{
    public class FubuMembershipRegistry : Registry
    {
        public FubuMembershipRegistry()
        {
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.WithDefaultConventions();
                         x.Convention<SettingsScanner>();
                     });
            For<IPasswordHelperService>()
                .Use<HashedPasswordHelperService>();
        }
    }

    public class SettingsScanner : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if (type.Name.EndsWith("Settings"))
            {
                registry.For(type).Use(c =>
                {
                    var settingsProvider = c.GetInstance<ISettingsProvider>();
                    return settingsProvider.SettingsFor(type);
                });
            }
        }
    }
}