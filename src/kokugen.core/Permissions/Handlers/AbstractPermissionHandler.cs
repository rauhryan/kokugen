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
    public interface IAuthorize<TInput, TContext>
    {
        bool Authorize(TInput input, TContext context);
    }

    public abstract class BasicAuthorizer<TInput> : IAuthorize<TInput>
    {
        public virtual bool Authorize(TInput input, UserContext context)
        {
            return true;
        }
    }

    public class AuthorizePermissions<TInput>  : IAuthorize<TInput>
    {
        public bool Authorize(TInput input, UserContext context)
        {
            //Todo: logic here

            return true;
        }
    }

    public interface IAuthorize<TInput> : IAuthorize<TInput, UserContext>
    {
    }


    public class AuthorizationHandler<T> : IAuthorizationHandler<T> where T : class
    {
        private readonly IEnumerable<IAuthorize<T>> _handlers;
        private readonly UserContext _context;

        public AuthorizationHandler(IEnumerable<IAuthorize<T>> handlers, UserContext context)
        {
            _handlers = handlers;
            _context = context;
        }

        public FubuContinuation Handle(T input)
        {
            if (_handlers.Count() == 0)
                return FubuContinuation.NextBehavior();

            return _handlers.All(permissionHandler => permissionHandler.Authorize(input, _context))
                ? FubuContinuation.NextBehavior()
                : FubuContinuation.RedirectTo("/Home");
        }
    }

    public interface IAuthorizationHandler<T>
    {
        FubuContinuation Handle(T input);
    }

    public class AuthorizationContinuationBehavior<TInput> : ContinuationHandler where TInput : class
    {
        private readonly IAuthorizationHandler<TInput> _handler;
        private readonly IFubuRequest _request;

        public AuthorizationContinuationBehavior(IAuthorizationHandler<TInput> handler, IUrlRegistry registry, IOutputWriter writer, IFubuRequest request, IPartialFactory factory) : base(registry, writer, request, factory)
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

    public class AuthorizationPolicy : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            graph.Behaviors
                .Where(x => x.HasInput())
                .Each(x => x.FirstCall().WrapWith(
                typeof (AuthorizationContinuationBehavior<>).
                    MakeGenericType(x.ActionInputType())));
        }
    } 
}