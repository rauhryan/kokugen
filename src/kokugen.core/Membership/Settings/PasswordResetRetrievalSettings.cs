﻿
namespace Kokugen.Core.Membership.Settings
{
    public class PasswordResetRetrievalSettings
    {
        public bool EnablePasswordReset { get; private set; }
        public bool EnablePasswordRetrieval { get; private set; }
        public bool RequiresQuestionAndAnswer { get; private set; }
    }
}