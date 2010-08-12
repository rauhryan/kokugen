using System;

namespace FubuMembership.Membership.Services
{
    public class ClearTextPasswordHelper : IPasswordHelperService
    {
        public string CreatePassword(string password)
        {
            return password;
        }

        public bool ComparePassword(string password, string passwordHash)
        {
            return password == passwordHash;
        }

        public string RandomPassword(int length, int nonAlphaNumberic)
        {
            return new HashedPasswordHelperService().RandomPassword(length, nonAlphaNumberic);
        }
    }
}