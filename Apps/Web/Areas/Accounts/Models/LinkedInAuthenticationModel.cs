using LinkMe.Apps.Asp.Json.Models;

namespace LinkMe.Web.Areas.Accounts.Models
{
    public class LinkedInAuthenticationModel
        : JsonResponseModel
    {
        public string Status { get; set; }
    }
}