using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Urls;
using StructureMap;

namespace Kokugen.Core.Permissions.Handlers
{
    public interface IPermissionHandler<TInput, TContext>
    {
        bool Execute(TInput input, TContext context);
    }

    public abstract class AbstractAuthorize<TInput> : IAuthorize<TInput>
    {
        public virtual bool Execute(TInput input, UserContext context)
        {
            return true;
        }
    }

    public class AuthorizePermissions<TInput>  : IAuthorize<TInput>
    {
        public bool Execute(TInput input, UserContext context)
        {
            return true;
        }
    }

    public interface IAuthorize<TInput> : IPermissionHandler<TInput, UserContext>
    {
    }


    public class PermissionExecuter<T> : IPermissionExecuter<T> where T : class
    {
        private readonly IEnumerable<IAuthorize<T>> _handlers;
        private readonly IFubuRequest _fubuRequest;
        private readonly UserContext _context;

        public PermissionExecuter(IEnumerable<IAuthorize<T>> handlers, IFubuRequest fubuRequest, UserContext context)
        {
            _handlers = handlers;
            _fubuRequest = fubuRequest;
            _context = context;
        }

        public FubuContinuation Handle(T input)
        {
            if (_handlers.Count() == 0)
                return FubuContinuation.NextBehavior();

            return _handlers.All(permissionHandler => permissionHandler.Execute(input, _context))
                ? FubuContinuation.NextBehavior()
                : FubuContinuation.RedirectTo("/Home");
        }
    }

    public interface IPermissionExecuter<T>
    {
        FubuContinuation Handle(T input);
    }

    public class PermissionContinuationBehavior<TInput> : ContinuationHandler where TInput : class
    {
        private readonly IPermissionExecuter<TInput> _handler;
        private readonly IFubuRequest _request;

        public PermissionContinuationBehavior(IPermissionExecuter<TInput> handler, IUrlRegistry registry, IOutputWriter writer, IFubuRequest request, IPartialFactory factory) : base(registry, writer, request, factory)
        {
            _handler = handler;
            _request = request;
        }

        protected override DoNext performInvoke()
        {
            var continuation = _handler.Handle(_request.Get<TInput>());
            continuation.Process(this);
            return DoNext.Stop;
        }
    }

    //public class PermissionHandlerBehavior<TInput> : BasicBehavior where TInput : class
    //{
    //    private readonly IPermissionExecuter<TInput> _handler;
    //    private readonly IFubuRequest _fubuRequest;
    //    private readonly HttpContextBase _httpContextBase;

    //    public PermissionHandlerBehavior(IPermissionExecuter<TInput> handler, IFubuRequest fubuRequest, HttpContextBase httpContextBase)
    //        : base(PartialBehavior.Executes)
    //    {
    //        _handler = handler;
    //        _fubuRequest = fubuRequest;
    //        _httpContextBase = httpContextBase;
    //    }

    //    protected override FubuMVC.Core.DoNext performInvoke()
    //    {
    //        if (_handler.Handle(_fubuRequest.Get<TInput>()))
    //            return base.performInvoke();

    //        _httpContextBase.Response.StatusCode = 403;

    //        return DoNext.Stop;
    //    }

    //}

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
            graph.Behaviors
                .Where(x => x.HasInput())
                .Each(x => x.FirstCall().WrapWith(
                typeof (PermissionContinuationBehavior<>).
                    MakeGenericType(x.ActionInputType())));
        }
    } 
}