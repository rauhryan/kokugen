using System;
using System.Linq.Expressions;
using Kokugen.Core.Membership;
using Kokugen.Web.Actions.Board;
using Kokugen.Web.Actions.Project;

namespace Kokugen.Web
{
    public class KokugenSecurityRegistry : SecurityRegistry
    {
        public KokugenSecurityRegistry()
        {
            For<ListAction>(c => c.Query(null))
                .RequirePermission(Permission.CanListProjects);

            For<BoardAction>(c => c.Configure(null))
                .RequirePermission(Permission.CanConfigureProcess);
        }

        
    }
}