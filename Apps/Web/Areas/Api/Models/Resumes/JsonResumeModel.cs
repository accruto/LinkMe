using System;
using LinkMe.Apps.Asp.Json.Models;

namespace LinkMe.Web.Areas.Api.Models.Resumes
{
    public class JsonResumeModel
        : JsonResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
    }

    public class JsonParsedResumeModel
        : JsonResponseModel
    {
        public Guid? Id { get; set; }
    }
}