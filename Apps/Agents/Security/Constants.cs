namespace LinkMe.Apps.Agents.Security
{
    public static class Constants
    {
        // For members the email address and the login id are the same so keep them in sync.

        public const int LoginIdMaxLength = Framework.Utility.Validation.Constants.EmailAddressMaxLength;
        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 50;
        public const int AdministratorPasswordMinLength = 8;
    }
}