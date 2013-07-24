using System;
using LinkMe.Apps.Asp.Json.Models;

namespace LinkMe.Web.Areas.Employers.Models.Candidates
{
    public class JsonFileModel
        : JsonResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
    }
}
