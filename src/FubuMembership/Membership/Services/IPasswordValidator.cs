namespace FubuMembership.Membership.Services
{
    public interface IPasswordValidator
    {
        bool ValidatePassword(string password);
    }
}