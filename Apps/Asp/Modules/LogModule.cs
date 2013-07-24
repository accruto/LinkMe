using System.IO;
using System.Text;
using System.Web;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Apps.Asp.Modules
{
    public class LogModule
        : HttpModule
    {
        private static readonly EventSource EventSource = new EventSource<LogModule>();

        protected override void OnBeginRequest()
        {
            const string method = "OnBeginRequest";

            var clientUrl = GetClientUrl();
            var request = HttpContext.Current.Request;

            var sb = new StringBuilder();
            sb.Append(request.HttpMethod).Append(" ").Append(request.RawUrl).Append(" ").AppendLine(request.ServerVariables["SERVER_PROTOCOL"]);
            foreach (var key in request.Headers.AllKeys)
            {
                if (key != "X-REWRITE-URL")
                {
                    var values = request.Headers.GetValues(key);
                    if (values != null)
                    {
                        foreach (var value in values)
                            sb.Append(key).Append(": ").AppendLine(value);
                    }
                }
            }

            sb.AppendLine();
            using (var stream = new MemoryStream())
            {
                request.InputStream.CopyTo(stream);
                request.InputStream.Position = 0;
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    using (var writer = new StringWriter(sb))
                    {
                        writer.Write(reader.ReadToEnd());
                    }
                }
            }

            if (EventSource.IsEnabled(Event.Information))
                EventSource.Raise(Event.Information, method, "Request begun for url: " + clientUrl, Event.Arg("clientUrl", clientUrl), Event.Arg("requestText", sb.ToString()));
        }
    }
}
