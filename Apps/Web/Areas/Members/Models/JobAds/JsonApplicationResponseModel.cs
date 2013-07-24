using System;
using LinkMe.Apps.Asp.Json.Models;

namespace LinkMe.Web.Areas.Members.Models.JobAds
{
    public class JsonApplicationResponseModel
        : JsonResponseModel
    {
        public Guid Id { get; set; }
    }
}
