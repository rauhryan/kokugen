using System.Collections.Generic;
using System.Reflection;
using Kokugen.Core;
using StructureMap.Configuration.DSL;

namespace Kokugen.Web
{
    public class KokugenPluginRegistry : Registry
    {
        public KokugenPluginRegistry(string pluginDir)
        {
            Assemblies = new List<Assembly>();
            Scan(c =>
                     {
                         c.AssembliesFromPath(pluginDir, a =>
                                                             {
                                                                 if (a.FullName.Contains("Plugin"))
                                                                 {
                                                                     Assemblies.Add(a);
                                                                     return true;
                                                                 }

                                                                 return false;
                                                             });
                       
                         c.LookForRegistries();
                         c.AddAllTypesOf<IStartable>();
                     });
            
        }

       public IList<Assembly> Assemblies { get; private set; }
    }
}