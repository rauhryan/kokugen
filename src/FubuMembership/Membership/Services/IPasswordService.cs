#region

using FubuMembership.Membership.Security;

#endregion

namespace FubuMembership.Membership.Services
{
    public interface IPasswordService<USER> where USER : IUser
    {
        void Unlock(USER user);
        void ChangePassword(USER user, string oldPassword, string newPassword);
        void ChangePassword(USER user, string oldPassword, string newPassword, string answer);
        string ResetPassword(USER user, string passwordAnswer);
        string ResetPassword(USER user);


        string GetPassword(USER user, string passwordAnswer);
        string GetPassword(USER user);
    }
}