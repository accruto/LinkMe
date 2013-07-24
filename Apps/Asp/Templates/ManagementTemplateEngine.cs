using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mime;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Agents.Applications.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Content.Templates;
using LinkMe.Framework.Utility.Files;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Templates
{
    public class ManagementTemplateEngine
        : ITemplateEngine
    {
        private const string EmailMetaTag = "<meta name=\"LMEP_OK\" content=\"LMEP_OK\" />";
        private readonly IWebSiteQuery _webSiteQuery;
        private readonly int _timeout;

        public ManagementTemplateEngine(IWebSiteQuery webSiteQuery, int timeout)
        {
            _webSiteQuery = webSiteQuery;
            _timeout = timeout;
        }

        CopyItem ITemplateEngine.GetCopyItem(TemplateContext context, TemplateProperties properties, string[] mimeTypes)
        {
            return GetCopyItem(context, properties);
        }

        CopyItemEngine ITemplateEngine.GetCopyItemEngine(TemplateContentItem templateContentItem)
        {
            throw new NotSupportedException();
        }

        CopyItem ITemplateEngine.GetCopyItem(CopyItemEngine copyItemEngine, TemplateContext context, TemplateProperties properties, string[] mimeTypes)
        {
            throw new NotImplementedException();
        }

        private CopyItem GetCopyItem(TemplateContext context, TemplateProperties properties)
        {
            var url = GetUrl(context, properties);
            var htmlText = GetCopyText(url, MediaType.Html);
            var plainText = GetCopyText(url, MediaType.Text);
            return CreateCopyItem(htmlText, plainText);
        }

        private ReadOnlyUrl GetUrl(TemplateContext context, TemplateProperties properties)
        {
            // Get the user id.

            var user = properties["To"] as IUser;
            if (user == null)
                throw new ApplicationException("No user has been supplied.");

            // Get the application path.

            var applicationPath = "~/communications/definitions/" + context.Definition;
            var queryString = new QueryString(
                "userId", user.Id.ToString(),
                "contextId", context.Id.ToString(),
                "definition", context.Definition,
                "category", context.Category);
            if (context.VerticalId != null)
                queryString.Add("verticalId", context.VerticalId.Value.ToString());

            return _webSiteQuery.GetUrl(WebSite.Management, null, false, applicationPath, queryString);
        }

        private string GetCopyText(ReadOnlyUrl url, string contentType)
        {
            // Create the request.

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url.AbsoluteUri);
                request.Method = "GET";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
                request.Timeout = _timeout;
                request.ContentType = contentType;

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    string copy;
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        copy = reader.ReadToEnd();
                    }

                    // Check for errors.

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        if (contentType == MediaType.Html)
                            throw new ApplicationException("Status code '" + (int)response.StatusCode + "' returned from url '" + url.AbsoluteUri + "'. Copy: " + copy);
                        return null;
                    }

                    // Actually look for the following meta tag to ensure that this is in fact an email page rather than an error page etc.

                    if (contentType == MediaType.Html && !copy.Contains(EmailMetaTag))
                        throw new ApplicationException("The copy returned from url '" + url.AbsoluteUri + "' does not contain the email meta tag '" + EmailMetaTag + "'. Copy: " + copy);

                    return copy;
                }
            }
            catch (Exception ex)
            {
                if (contentType == MediaType.Html)
                    throw new ApplicationException("Cannot get the email using the url '" + url.AbsoluteUri + "'.", ex);
                return null;
            }
        }

        private CopyItem CreateCopyItem(string htmlText, string plainText)
        {
            var copyItem = new CopyItem(GetSubject(htmlText));

            // Add the html view.

            var viewItem = new ViewItem(MediaTypeNames.Text.Html, htmlText);
            AddResourceItems(htmlText, viewItem);
            copyItem.ViewItems.Add(viewItem);

            // Add the plain text view.

            if (!string.IsNullOrEmpty(plainText))
            {
                viewItem = new ViewItem(MediaTypeNames.Text.Plain, plainText);
                copyItem.ViewItems.Add(viewItem);
            }

            return copyItem;
        }

        private void AddResourceItems(string text, ViewItem viewItem)
        {
            // Look for images.

            var links = GetResourceLinks(text);
            if (links != null && links.Count > 0)
                AddResourceItems(links, viewItem);
        }

        private static IList<string> GetResourceLinks(string text)
        {
            IList<string> links = null;

            // Do it by hand for this email.

            var pos = text.IndexOf(" src=\"");
            while (pos != -1)
            {
                var start = pos + " src=\"".Length;
                var end = text.IndexOf("\"", start);
                if (end == -1)
                    break;

                var link = text.Substring(start, end - start);
                if (link.StartsWith("cid:"))
                {
                    if (links == null)
                        links = new List<string>();
                    if (!links.Contains(link))
                        links.Add(link);
                }

                pos = text.IndexOf(" src=\"", end);
            }

            return links;
        }

        private void AddResourceItems(IEnumerable<string> links, ViewItem viewItem)
        {
            foreach (var link in links)
                viewItem.ResourceItems.Add(GetResourceItem(link));
        }

        private ResourceItem GetResourceItem(string link)
        {
            var id = link.Substring("cid:".Length);

            // Get the resource from the web site.

            var url = _webSiteQuery.GetUrl(WebSite.Management, null, false, "~/communications/members/photo/" + id.Replace('.', '/'));

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url.AbsoluteUri);
                request.Method = "GET";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var stream = new MemoryStream();
                    CopyStream(response.GetResponseStream(), stream);
                    stream.Seek(0, SeekOrigin.Begin);

                    return new ResourceItem(id, stream, MediaTypeNames.Image.Jpeg);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Cannot get the resource using the url '" + url.AbsoluteUri + "'.", ex);
            }
        }

        private static void CopyStream(Stream source, Stream destination)
        {
            var buffer = new byte[65536];
            int read;
            do
            {
                read = source.Read(buffer, 0, buffer.Length);
                destination.Write(buffer, 0, read);
            } while (read != 0);
        }

        private static string GetSubject(string text)
        {
            // The subject of the email is the title of the page (for performance just do a string lookup).

            var start = text.IndexOf("<title>");
            if (start != -1)
            {
                start += "<title>".Length;
                var end = text.IndexOf("</title>", start);
                if (end != -1)
                    return text.Substring(start, end - start).Trim();
            }

            return string.Empty;
        }
    }
}
