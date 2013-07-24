using LinkMe.Apps.Asp.Json;
using LinkMe.Apps.Asp.Json.Models;

namespace LinkMe.Web.Areas.Members.Models.Profiles
{
    public class VisibilityModel
        : JsonRequestModel
    {
        public bool ShowResume { get; set; }
        public bool ShowName { get; set; }
        public bool ShowPhoneNumbers { get; set; }
        public bool ShowProfilePhoto { get; set; }
        public bool ShowRecentEmployers { get; set; }
    }
}