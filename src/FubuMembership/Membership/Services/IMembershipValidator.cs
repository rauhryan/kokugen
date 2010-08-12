using System;
using FubuMembership.Membership.Security;

namespace FubuMembership.Membership.Services
{
    public interface IMembershipValidator
    {
        bool ValidateUser(IUser user, string password);
    }

    public class MembershipValidator : IMembershipValidator
    {
        private readonly IPasswordHelperService _passwordHelperService;

        public MembershipValidator(IPasswordHelperService passwordHelperService)
        {
            _passwordHelperService = passwordHelperService;
        }


        public bool ValidateUser(IUser user, string password)
        {
            return _passwordHelperService.ComparePassword(password, user.Password);
        }
    }
}