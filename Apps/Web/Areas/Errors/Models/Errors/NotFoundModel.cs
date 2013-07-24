using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Areas.Errors.Models.Errors
{
    public class ObjectNotFoundModel
    {
        public string Type { get; set; }
        public string PropertyName { get; set; }
        public object PropertyValue { get; set; }
    }

    public class NotFoundModel
    {
        public ReadOnlyUrl RequestedUrl { get; set; }
        public ReadOnlyUrl ReferrerUrl { get; set; }
    }
}