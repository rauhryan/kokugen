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
  
    public abstract class BasicAuthorizer<TInput> : IAuthorize<TInput>
    {
        public virtual bool Authorize(TInput input)
        {
            return true;
        }
    }

    public class AuthorizePermissions<TInput>  : IAuthorize<TInput>
    {
        private readonly UserContext _userContext;

        public AuthorizePermissions(UserContext userContext)
        {
            _userContext = userContext;
        }

        public bool Authorize(TInput input)
        {
            //Todo: logic here to check if user has permission by convention

            return true;
        }
    }

    public interface IAuthorize<TInput> 
    {
        bool Authorize(TInput input);
    }


    public class AuthorizationHandler<T> : IAuthorizationHandler<T> where T : class
    {
        private readonly IEnumerable<IAuthorize<T>> _handlers;

        public AuthorizationHandler(IEnumerable<IAuthorize<T>> handlers)
        {
            _handlers = handlers;
        }

        public FubuContinuation Handle(T input)
        {
            if (_handlers.Count() == 0)
                return FubuContinuation.NextBehavior();

            return _handlers.All(permissionHandler => permissionHandler.Authorize(input))
                ? FubuContinuation.NextBehavior()
                // this is not right maybe 403 here or something
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