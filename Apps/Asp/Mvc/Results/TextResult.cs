using System;
using System.IO;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Results
{
    public class XmlResult
        : ActionResult
    {
        private const int BufferSize = 0x1000;
        private readonly Stream _stream;

        public XmlResult(Stream stream)
        {
            _stream = stream;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var response = context.HttpContext.Response;
            response.ContentType = "text/xml";
            response.CacheControl = "no-cache";
            response.AddHeader("Pragma", "no-cache");
            response.Expires = -1;

            var outputStream = response.OutputStream;
            using (_stream)
            {
                var buffer = new byte[BufferSize];
                while (true)
                {
                    var bytesRead = _stream.Read(buffer, 0, BufferSize);
                    if (bytesRead == 0)
                        break;
                    outputStream.Write(buffer, 0, bytesRead);
                }
            }
        }
    }
}