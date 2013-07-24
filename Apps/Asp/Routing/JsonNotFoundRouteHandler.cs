using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Asp.Routing
{
    public class JsonNotFoundHttpHandler
        : JsonHttpHandler
    {
        public JsonNotFoundHttpHandler()
            : base(CreateJsonNotFoundModel())
        {
        }

        private static JsonResponseModel CreateJsonNotFoundModel()
        {
            // Need to populate errors.

            var exception = new NotFoundException("page");
            var errorHandler = (IErrorHandler)new StandardErrorHandler();
            var message = errorHandler.FormatErrorMessage(exception);
            var errorCode = errorHandler.GetErrorCode(exception);

            return new JsonResponseModel
            {
                Success = false,
                Errors = new List<JsonError> { new JsonError { Code = errorCode, Message = message } }
            };
        }
    }

    public class JsonNotFoundRouteHandler
        : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new JsonNotFoundHttpHandler();
        }
    }
}
