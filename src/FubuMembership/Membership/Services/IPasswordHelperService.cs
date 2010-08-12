namespace FubuMembership.Membership.Services
{
    public interface IPasswordHelperService
    {
        string CreatePassword(string password);
        bool ComparePassword(string password, string passwordHash);
        string RandomPassword(int length, int nonAlphaNumberic);
    }
}