using System;
using System.Collections.Generic;

namespace LinkMe.Apps.Asp.Json.Models
{
    public class JsonError
    {
        public string Key { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    public class JsonResponseModel
    {
        public bool Success { get; set; }
        public IList<JsonError> Errors { get; set; }
    }

    public class JsonConfirmationModel
        : JsonResponseModel
    {
        public Guid Id { get; set; }
    }
}