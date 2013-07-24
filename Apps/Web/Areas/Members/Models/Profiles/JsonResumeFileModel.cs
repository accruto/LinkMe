using System;
using LinkMe.Apps.Asp.Json.Models;

namespace LinkMe.Web.Areas.Members.Models.Profiles
{
    public class JsonResumeFileModel
        : JsonResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
    }
}
