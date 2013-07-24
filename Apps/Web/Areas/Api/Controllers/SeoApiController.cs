using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Navigation;

namespace LinkMe.Web.Areas.Api.Controllers
{
    public class SeoApiController
        : ApiController
    {
        public ActionResult SiteMap()
        {
            // Use the query string to determine format.

            var stream = new MemoryStream();
            var settings = new XmlWriterSettings { Encoding = Encoding.UTF8, Indent = true };
            var writer = XmlWriter.Create(stream, settings);
            var server = HttpContext.Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.UriEscaped);

            // Process from the root node down.

            WriteStartDocument(writer);
            var node = System.Web.SiteMap.RootNode as NavigationSiteMapNode;
            if (node != null)
                ProcessNode(writer, server, node);
            WriteEndDocument(writer);

            stream.Seek(0, SeekOrigin.Begin);
            return Xml(stream);
        }

        private static void WriteStartDocument(XmlWriter writer)
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("urlset", "http://www.google.com/schemas/sitemap/0.84");
        }

        private static void WriteEndDocument(XmlWriter writer)
        {
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
        }

        private static void WriteNode(XmlWriter writer, string server, SiteMapNode node)
        {
            writer.WriteStartElement("url");
            AddLoc(writer, server, node);
            AddChangeFreq(writer, node);
            AddPriority(writer, node);
            writer.WriteEndElement();
        }

        private static void AddLoc(XmlWriter writer, string server, SiteMapNode node)
        {
            writer.WriteStartElement("loc");
            writer.WriteString(server + node.Url);
            writer.WriteEndElement();
        }

        private static void AddChangeFreq(XmlWriter writer, SiteMapNode node)
        {
            var changeFreq = node["changefreq"];
            if (!string.IsNullOrEmpty(changeFreq))
            {
                writer.WriteStartElement("changefreq");
                writer.WriteString(changeFreq);
                writer.WriteEndElement();
            }
        }

        private static void AddPriority(XmlWriter writer, SiteMapNode node)
        {
            var priority = node["priority"];
            if (!string.IsNullOrEmpty(priority))
            {
                writer.WriteStartElement("priority");
                writer.WriteString(priority);
                writer.WriteEndElement();
            }
        }

        private static void ProcessNode(XmlWriter writer, string server, NavigationSiteMapNode node)
        {
            // Check that the node should be added.

            if (CanAddNode(node))
                WriteNode(writer, server, node);

            // Iterate over children.

            foreach (NavigationSiteMapNode childNode in node.ChildNodes)
                ProcessNode(writer, server, childNode);
        }

        private static bool CanAddNode(NavigationSiteMapNode node)
        {
            // If the url is not specified or it is external then don't add.

            if (string.IsNullOrEmpty(node.Url) || node.Url.StartsWith("http"))
                return false;

            // If it is a redirect node then don't add.

            if (node.Redirect)
                return false;

            // If the node itself indicates that it is not crawlable then don't add.

            return node.Crawlable;
        }
    }
}
