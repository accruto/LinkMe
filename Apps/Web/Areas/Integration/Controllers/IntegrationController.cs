using System.IO;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Web.Areas.Integration.Models;
using LinkMe.Web.Service;

namespace LinkMe.Web.Areas.Integration.Controllers
{
    public abstract class IntegrationController
        : ApiController
    {
        protected string GetRequestXml()
        {
            using (var reader = new StreamReader(HttpContext.Request.InputStream))
            {
                return reader.ReadToEnd();
            }
        }

        protected ActionResult Xml(XmlResponse response)
        {
            var stream = new MemoryStream();
            response.WriteXml(stream);
            stream.Position = 0;
            return Xml(stream);
        }

        protected ActionResult Xml(WebServiceResponse response)
        {
            var stream = new MemoryStream();
            response.WriteXml(stream);
            stream.Position = 0;
            return Xml(stream);
        }
    }
}
