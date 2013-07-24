using System;
using System.IO;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Results
{
    public class TextResult
        : ActionResult
    {
        private readonly string _text;

        public TextResult(string text)
        {
            _text = text;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var response = context.HttpContext.Response;
            response.ContentType = "text/rtf";
            response.CacheControl = "no-cache";
            response.AddHeader("Pragma", "no-cache");
            response.Expires = -1;

            var outputStream = response.OutputStream;
            using (var writer = new StreamWriter(outputStream))
            {
                writer.Write(_text);
            }
        }
    }
}