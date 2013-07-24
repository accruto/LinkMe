using System;

namespace LinkMe.Web.Areas.Errors.Models.Errors
{
    public class ErrorReport
    {
        public Exception Exception { get; set; }
        public string RequestUrl { get; set; }
        public string ReferrerUrl { get; set; }
        public string UserAgent { get; set; }
        public string UserHostAddress { get; set; }
        public string RequestType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string RequestCookies { get; set; }
        public string ResponseCookies { get; set; }
    }
}