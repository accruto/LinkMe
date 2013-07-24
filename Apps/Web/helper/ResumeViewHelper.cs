namespace LinkMe.Web.Helper
{
    public static class ResumeViewHelper
    {
        public static string GetOptionalListItem(string text)
        {
            return (string.IsNullOrEmpty(text) ? "" : "<li>" + text + "</li>");
        }
    }
}
