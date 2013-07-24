using System;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using LinkMe.Apps.Asp.Json;
using LinkMe.Apps.Asp.Json.Models;

namespace LinkMe.Apps.Asp.Mvc.Controllers
{
    public class JsonResponseResult
        : ActionResult
    {
        private readonly object _data;
        private readonly JavaScriptConverter[] _converters;
        private readonly JsonRequestBehavior _behavior = JsonRequestBehavior.DenyGet;
        private readonly string _contentType;
        private readonly Encoding _contentEncoding;

        public JsonResponseResult(Encoding contentEncoding, string contentType, object data, JavaScriptConverter[] converters)
        {
            _contentEncoding = contentEncoding;
            _converters = converters;
            _data = data;
            _contentType = contentType;
        }

        public JsonResponseResult(object data, string contentType, JavaScriptConverter[] converters)
        {
            _data = data;
            _contentType = contentType;
            _converters = converters;
        }

        public JsonResponseResult(object data, JsonRequestBehavior behavior, JavaScriptConverter[] converters)
        {
            _data = data;
            _behavior = behavior;
            _converters = converters;
        }

        public JsonResponseResult(object data, JavaScriptConverter[] converters)
        {
            _data = data;
            _converters = converters;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (_behavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Get request not allowed.");

            var response = context.HttpContext.Response;
            
            // Set the status code.

            var model = _data as JsonResponseModel;
            var statusCode = model.GetStatusCode();

            // Unauthorized is a special case because simply setting it results in ASP.NET forms authentication kicking in which redirects to a login page,
            // which we do not want to happen.

            switch (statusCode)
            {
                case HttpStatusCode.Unauthorized:
                    context.HttpContext.SkipAuthorization = true;
                    response.Clear();
                    response.ContentType = !string.IsNullOrEmpty(_contentType) ? _contentType : ContentTypes.Json;
                    if (_contentEncoding != null)
                        response.ContentEncoding = _contentEncoding;
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    Write(response);

                    // End it all here.

                    response.End();
                    break;

                default:
                    response.ContentType = !string.IsNullOrEmpty(_contentType) ? _contentType : ContentTypes.Json;
                    if (_contentEncoding != null)
                        response.ContentEncoding = _contentEncoding;
                    response.StatusCode = (int)statusCode;
                    Write(response);
                    break;
            }
        }

        private void Write(HttpResponseBase response)
        {
            if (_data != null)
            {
                var serializer = new JavaScriptSerializer();
                if (_converters != null && _converters.Length > 0)
                    serializer.RegisterConverters(_converters);
                response.Write(serializer.Serialize(_data));
            }
        }
    }
}
