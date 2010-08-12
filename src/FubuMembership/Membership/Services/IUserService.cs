#region

using System.Linq;
using FubuMembership.Membership.Security;

#endregion

namespace FubuMembership.Membership.Services
{

    public interface IUserService<USER> where USER : IUser
    {
        INotification Update(USER user);
        void Delete(USER user);
        USER Retrieve(object id);
        INotification Create(USER user);

        USER GetUserByLogin(string name);
        USER GetUserByEmail(string email);
        IQueryable<USER> FindAll();

        int TotalUsers { get; }
        
    }
}