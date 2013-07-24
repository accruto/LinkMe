using System.Linq;
using System.Net;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Asp.Json
{
    public static class JsonHttpExtensions
    {
        public static HttpStatusCode GetStatusCode(this JsonResponseModel model)
        {
            if (model == null || model.Success)
                return HttpStatusCode.OK;

            // Look for any specific error codes.

            if (model.Errors != null)
            {
                if (model.Errors.Any(e => ErrorCodes.GetErrorCodeClass(e.Code) == ErrorCodeClass.NotFound))
                    return HttpStatusCode.NotFound;
                if (model.Errors.Any(e => ErrorCodes.GetErrorCodeClass(e.Code) == ErrorCodeClass.ServerError))
                    return HttpStatusCode.InternalServerError;
            }

            // All else are forbidden. Somewhat of a catch all.
            // Did distinguish HttpStatusCode.Unauthorized at one stage
            // but that caused grief in some situations as various combinations of browsers/IIS/ASP.NET
            // got involved, causing e.g. authentication pop-ups.

            return HttpStatusCode.Forbidden;
        }
    }
}
