namespace LinkMe.Common
{
    /// <summary>
    /// Defines the maximum lengths of string types used in the data model. Keep this in sync with the model
    /// and the user-defined types in the database.
    /// </summary>
    public static class DomainConstants
    {
        public const int FilenameMaxLength = 260;
        public const int JobTitleMaxLength = 100;
        public const int LocationDisplayNameMaxLength = 100;
        public const int AdminPasswordMinLength = 8;
        public const int PersonNameMaxLength = 30;
        public const int PhoneNumberMaxLength = 20;
        public const int PostcodeMinLength = 3;
        public const int PostcodeMaxLength = 8;
        public const int UrlMaxLength = 1000; // This refers to the "Url" user type in the database.
        public const int UserEventExtraDataMaxLength = 1000;
        public const int UserToUserRequestMessageMaxLength = 2000;
        public const int WhiteboardMessageMaxLength = 400;

        public const int GroupNameMinLength = 2;
        public const int GroupNameMaxLength = 75;
        public const int GroupDescriptionMaxLength = 500;
        public const int GroupEventDescriptionMaxLength = 500;
    }
}
