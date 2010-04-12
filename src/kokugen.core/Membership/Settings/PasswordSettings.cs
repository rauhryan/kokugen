﻿using System.Web.Security;

namespace Kokugen.Core.Membership.Settings
{
    public class PasswordSettings
    {
        public PasswordSettings()
        {
            MinRequiredPasswordLength = 8;
            MinRequiredNonAlphanumericCharacters = 1;
            PasswordFormat = PasswordFormat.Hashed;
            PasswordStrengthRegularExpression = "";
        }

        public int MinRequiredPasswordLength { get; private set; }
        public int MinRequiredNonAlphanumericCharacters { get; private set; }
        public string PasswordStrengthRegularExpression { get; private set; }
        public PasswordFormat PasswordFormat { get; private set; }

    }

    public enum PasswordFormat
    {
        Hashed,
        Clear,
        Encrypted
    }
}