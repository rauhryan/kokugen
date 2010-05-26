using Kokugen.Core.Membership.Settings;
using Kokugen.Tests.Core.Membership.Services;
using Machine.Specifications;
using Rhino.Mocks;

namespace Kokugen.MSpec.Tests.Core.Membership.Services
{
    //these are dup tests used to get the hang of mspec

    [Subject("Password Validator")]
    public class when_given_a_weak_password_with_a_minimum_non_alphanumberic_constrant
    {
        Establish context = () =>
                                {
                                    var settings = MockRepository.GenerateStub<PasswordSettings>();
                                    settings.MinRequiredNonAlphanumericCharacters = 1;

                                    passwordService = new PasswordValidator(settings); 
                                };

        Because of = () => expected = passwordService.ValidatePassword("weakasspassword");

        private It should_return_false = () => { expected.ShouldBeFalse(); };

        private static PasswordValidator passwordService;
        private static bool expected;
    }

    [Subject("Password Validator")]
    public class when_given_a_strong_password_with_a_minimum_non_alphanumberic_constrant
    {
        Establish context = () =>
        {
            var settings = MockRepository.GenerateStub<PasswordSettings>();
            settings.MinRequiredNonAlphanumericCharacters = 1;

            passwordService = new PasswordValidator(settings);
        };

        Because of = () => expected = passwordService.ValidatePassword("strong@sspassword");

        private It should_return_true = () => { expected.ShouldBeTrue(); };

        private static PasswordValidator passwordService;
        private static bool expected;
    }

    [Subject("Password Validator")]
    public class when_given_a_weak_password_without_a_minimum_non_alphanumberic_constrant
    {
        Establish context = () =>
        {
            var settings = MockRepository.GenerateStub<PasswordSettings>();
            settings.MinRequiredNonAlphanumericCharacters = 0;
            passwordService = new PasswordValidator(settings);
        };

        Because of = () => expected = passwordService.ValidatePassword("weakasspassword");

        private It should_return_true = () => { expected.ShouldBeTrue(); };

        private static PasswordValidator passwordService;
        private static bool expected;
    }
}