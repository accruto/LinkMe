using System.Web;

namespace LinkMe.Apps.Asp.Mvc
{
    public static class HttpPostedFileBaseExtensions
    {
        public static bool IsUploaded(this HttpPostedFileBase file)
        {
            return file != null && file.ContentLength > 0;
        }
    }
}
