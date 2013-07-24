namespace LinkMe.Domain.Files
{
    // The names of these enum values are used as folder names and the numeric values are stored in the
    // database and domain model. Do not change either the names or the values!
    public enum FileType : byte
    {
        CompanyLogo = 1,
        Resume = 2,
        ProfilePhoto = 3,
        Attachment = 7,
    }
}