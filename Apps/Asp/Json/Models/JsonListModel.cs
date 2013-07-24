using System;

namespace LinkMe.Apps.Asp.Json.Models
{
    public class JsonCountModel
        : JsonResponseModel
    {
        public int Count { get; set; }
    }

    public class JsonListCountModel
        : JsonCountModel
    {
        public Guid Id { get; set; }
    }
}