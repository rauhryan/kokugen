using System.Collections.Generic;
using Kokugen.Core.Domain;

namespace Kokugen.Core.Permissions
{

    // just a class to hold context about our current user.
    public class UserContext
    {
        public User User { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }
    }
}