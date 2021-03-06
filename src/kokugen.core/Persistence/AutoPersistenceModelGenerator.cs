using System;
using System.Collections.Generic;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using Kokugen.Core.Domain;
using Kokugen.Core.Persistence.Conventions;

namespace Kokugen.Core.Persistence
{
    public class AutoPersistenceModelGenerator : IAutoPersistenceModelGenerator
    {
        public AutoPersistenceModel Generate()
        {
            setupComponentTypes();
            var mappings = new AutoPersistenceModel();

            mappings = AutoMap.AssemblyOf<Domain.Entity>();
            mappings.Where(GetAutoMappingFilter);
            mappings.Conventions.Setup(GetConventions());
            mappings.UseOverridesFromAssemblyOf<AutoPersistenceModelGenerator>();
            mappings.Setup(GetSetup());
            mappings.OverrideAll(x => x.IgnoreProperties(z => z.PropertyType.IsSubclassOf(typeof(Enumeration))));
            
            mappings.IgnoreBase<Domain.Entity>();
            mappings.UseOverridesFromAssemblyOf<AutoPersistenceModelGenerator>();

            return mappings;
        }

        private void setupComponentTypes()
        {
            componentTypes.Add<Address>();
        }

        private Action<AutoMappingExpressions> GetSetup()
        {
            return c =>
            {
                c.IsComponentType = type => componentTypes.Contains(type); 
                
                c.SubclassStrategy = t => SubclassStrategy.JoinedSubclass;
                c.FindIdentity = property => property.Name == "Id";
                c.GetComponentColumnPrefix = name => "";
                
            };
        }

        private List<Type> componentTypes = new List<Type>();

        private Action<IConventionFinder> GetConventions()
        {
            return c =>
            {
                c.Add<PrimaryKeyConvention>();
                c.Add<CustomManyToManyTableNameConvention>();
                c.Add<CustomReferencesConvention>();
                c.Add<CustomJoinedSubclassConvention>();

                AddValidation(c);

                // Keep these last
                c.Add<DefaultStringLengthConvention>();
                c.Add<CustomForeignKeyConvention>();
                c.Add<TableNameConvention>();
                
                //c.Add(ConventionBuilder.Property.Always(s => s.Column("[" + s.Property.Name + "]")));
                //c.Add(ConventionBuilder.Class.Always(s => s.Table("[" + s.EntityType.Name + "]"))); 
            };
        }

        private void AddValidation(IConventionFinder finder)
        {
            finder.Add<RequiredAttributeConvention>();
            finder.Add<MaximumStingLengthConvention>();
        }

        private static bool GetAutoMappingFilter(Type arg)
        {
            if(arg.IsSubclassOf(typeof(Domain.Entity)))
            {
                if(arg == typeof (SiteConfiguration) || arg == typeof (Alias))
                    return false;

                return true;
            }

            return false;
        }
    }

    public interface IAutoPersistenceModelGenerator
    {
        AutoPersistenceModel Generate();
    }


}