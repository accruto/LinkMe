namespace LinkMe.Domain.Users.Employers.Contacts
{
    public static class Constants
    {
        public static readonly string[] ValidFileExtensions = new[] { ".doc", ".docx", ".rtf", ".txt", ".pdf", ".xls", ".xlsx", ".ppt", ".pptx", ".ppsx", ".zip", ".rar", ".vsd" };
        public const int MaxAttachmentFileSize = 2 * 1024 * 1024; // 2 MB
        public const int MaxAttachmentTotalFileSize = 5 * 1024 * 1024; // 5 MB
    }
}
