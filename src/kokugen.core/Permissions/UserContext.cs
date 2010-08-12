using System.Collections.Generic;
using Kokugen.Core.Domain;

namespace Kokugen.Core.Permissions
{
    public class UserContext
    {
        public User User { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }
    }
}