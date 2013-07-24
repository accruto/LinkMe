using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using LinkMe.Apps.Asp.Json;
using LinkMe.Apps.Asp.Json.Models;

namespace LinkMe.Apps.Asp.Routing
{
    public abstract class JsonHttpHandler
        : IHttpHandler
    {
        private readonly JsonResponseModel _model;

        protected JsonHttpHandler(JsonResponseModel model)
        {
            _model = model;
        }

        public void ProcessRequest(HttpContext context)
        {
            var response = context.Response;

            // Set the status code.

            var statusCode = _model.GetStatusCode();

            // Unauthorized is a special case because simply setting it results in ASP.NET forms authentication kicking in which redirects to a login page,
            // which we do not want to happen.

            switch (statusCode)
            {
                case HttpStatusCode.Unauthorized:
                    context.SkipAuthorization = true;
                    response.Clear();
                    response.ContentType = ContentTypes.Json;
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Write(new JavaScriptSerializer().Serialize(_model));

                    // End it all here.

                    response.End();
                    break;

                default:
                    response.ContentType = ContentTypes.Json;
                    response.StatusCode = (int)statusCode;
                    response.Write(new JavaScriptSerializer().Serialize(_model));
                    break;
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}
