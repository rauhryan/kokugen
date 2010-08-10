using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Runtime;
using StructureMap;

namespace Kokugen.Core.Permissions.Handlers
{
    public abstract class AbstractPermissionHandler<TInput> : IPermissionHandler<TInput>
    {
        public virtual bool Execute(TInput input)
        {
            return true;
        }
    }

    public interface IPermissionHandler<T>
    {
        bool Execute(T input);
    }

    public class PermissionExecuter<T> : IPermissionExecuter<T> where T : class
    {
        private readonly IEnumerable<IPermissionHandler<T>> _handlers;
        private readonly IFubuRequest _fubuRequest;

        public PermissionExecuter(IEnumerable<IPermissionHandler<T>> handlers, IFubuRequest fubuRequest)
        {
            _handlers = handlers;
            _fubuRequest = fubuRequest;
        }

        public bool Handle(T input)
        {
            return _handlers.All(permissionHandler => permissionHandler.Execute(input));
        }
    }

    public interface IPermissionExecuter<T>
    {
        bool Handle(T input);
    }

    public class PermissionHandlerBehavior<TInput> : BasicBehavior where TInput : class
    {
        private readonly IPermissionExecuter<TInput> _handler;
        private readonly IFubuRequest _fubuRequest;
        private readonly HttpContextBase _httpContextBase;

        public PermissionHandlerBehavior(IPermissionExecuter<TInput> handler, IFubuRequest fubuRequest, HttpContextBase httpContextBase)
            : base(PartialBehavior.Executes)
        {
            _handler = handler;
            _fubuRequest = fubuRequest;
            _httpContextBase = httpContextBase;
        }

        protected override FubuMVC.Core.DoNext performInvoke()
        {
            if (_handler.Handle(_fubuRequest.Get<TInput>()))
                return base.performInvoke();

            _httpContextBase.Response.StatusCode = 403;

            return DoNext.Stop;
        }

    }

    public class PermissionHandlerNode : Wrapper
    {
        public PermissionHandlerNode(Type behaviorType) : base(behaviorType)
        {
        }

       
    }

    public class PermissionPolicy : IConfigurationAction
    {
       

        public void Configure(BehaviorGraph graph)
        {
            graph.Behaviors.Where(x => x.HasInput()).Each(x => x.FirstCall().WrapWith(
                typeof (PermissionHandlerBehavior<>).
                    MakeGenericType(x.ActionInputType())));
        }
    } 
}