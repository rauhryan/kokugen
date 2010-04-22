using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Routing;
using AutoMapper;
using FubuCore;
using FubuMVC.Core;
using FubuMVC.StructureMap;
using Kokugen.Core;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.Board;
using Kokugen.Web.Actions.Card;
using Kokugen.Web.Actions.DTO;
using Kokugen.Web.Actions.TimeRecord.WidgetLog;
using Kokugen.Web.Behaviors;
using StructureMap;

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

            var pluginDir = UrlContext.ToPhysicalPath("Plugins");
            var kokugenPluginRegistry = new KokugenPluginRegistry(pluginDir);

            ObjectFactory.Initialize(x =>
                                         {
                                             x.AddRegistry(new KokugenCoreRegistry());
                                             x.AddRegistry(new KokugenWebRegistry());
                                             x.AddRegistry(kokugenPluginRegistry);
                                         });

            kokugenPluginRegistry.Assemblies.Each(x => fubuRegistry.Applies.ToAssembly(x));

            var fubuBootstrapper = new StructureMapBootstrapper(ObjectFactory.Container, fubuRegistry);
            fubuBootstrapper.Builder = (c, args, id) =>
                                           {
                                               return new TransactionalContainerBehavior(c, args, id);
                                           };
            fubuBootstrapper.Bootstrap(_routes);

            ObjectFactory.Container.StartStartables();

            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            
            

            ConfigureAutoMapper();
        }

        private void ConfigureAutoMapper()
        {
            Mapper.CreateMap<Card, CardViewDTO>()
                .ForMember(a => a.Status, b=> b.MapFrom(c => c.Status.DisplayName))
                .ForMember(a => a.GravatarHash, b=> b.MapFrom(c => c.AssignedTo.GravatarHash))
                .ForMember(a => a.UserDisplay, b=> b.MapFrom(c => c.AssignedTo.DisplayName()));
            Mapper.CreateMap<Card, CardDetailModel>()
                .ForMember(a => a.Status, b => b.MapFrom(c => c.Status.DisplayName))
                .ForMember(a => a.GravatarHash, b=> b.MapFrom(c => c.AssignedTo.GravatarHash))
                .ForMember(a => a.UserDisplay, b=> b.MapFrom(c => c.AssignedTo.DisplayName()));
            Mapper.CreateMap<BoardColumn, BoardColumnDTO>()
                .ForMember(a => a.CardLimit, b=> b.UseValue(0));
            Mapper.CreateMap<CustomBoardColumn, BoardColumnDTO>()
                .ForMember(a => a.CardLimit, b=> b.NullSubstitute(0));

            Mapper.CreateMap<TimeRecord, TimeLogItem>();
            Mapper.CreateMap<Task, TaskDTO>();
//            Mapper.CreateMap<TimeRecord, TimeRecordDTO>()
//                .ForMember(a => a.User, b => b.NullSubstitute(null));


            Mapper.AssertConfigurationIsValid();
        }
    }
}