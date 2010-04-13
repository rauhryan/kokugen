using System;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Login
{
    public class LoginFormAction
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginFormAction(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public LoginModel Query(LoginFormModel inModel)
        {
            return new LoginModel();
        }
    }


    public class LoginFormModel
    {
        public string ReturnUrl { get; set; }
    }

    public class LoginModel
    {
        public virtual string Password { get; set; }
        public virtual string Login { get; set; }
        public virtual bool RememberMe { get; set; }
        
    }
}