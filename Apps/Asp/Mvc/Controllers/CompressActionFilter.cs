using System.IO.Compression;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Controllers
{
    public class CompressActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var response = filterContext.HttpContext.Response;
            
            //already compressed, return
            if (response.Filter is GZipStream || response.Filter is DeflateStream) return;

            var request = filterContext.HttpContext.Request;

            var acceptEncoding = request.Headers["Accept-Encoding"];

            //dosen't support compress, return
            if (string.IsNullOrEmpty(acceptEncoding)) return;

            acceptEncoding = acceptEncoding.ToUpperInvariant();

            if (acceptEncoding.Contains("GZIP"))
            {
                response.AppendHeader("Content-encoding", "gzip");
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }
            else if (acceptEncoding.Contains("DEFLATE"))
            {
                response.AppendHeader("Content-encoding", "deflate");
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            }
        }
    }
}
