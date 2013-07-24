using System;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Agents.Applications.Commands;
using LinkMe.Apps.Agents.Applications.Queries;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Asp.Templates
{
    internal class WebSiteTextTemplateEngine
        : MergeTextTemplateEngine
    {
        private readonly WebSite _webSite;
        private readonly IWebSiteQuery _webSiteQuery;
        private readonly ITinyUrlCommand _tinyUrlCommand;

        public WebSiteTextTemplateEngine(Guid? verticalId, string mimeType, string text, MergeSettings settings, WebSite webSite, IWebSiteQuery webSiteQuery, ITinyUrlCommand tinyUrlCommand)
            : base(verticalId, mimeType, text, settings)
        {
            _webSite = webSite;
            _webSiteQuery = webSiteQuery;
            _tinyUrlCommand = tinyUrlCommand;
        }

        protected override string GetCopyText(TemplateContext context, TemplateProperties properties)
        {
            // Create the object to keep track of all mappings used when generating the text.

            var mappings = new TinyUrlMappings(_webSiteQuery, context.Id, context.Definition, MimeType, _webSite, context.UserId, VerticalId);
            properties.Add("TinyUrls", mappings);

            // Get the text.

            var text = base.GetCopyText(context, properties);

            // Register those mappings.

            _tinyUrlCommand.CreateMappings(mappings);
            return text;
        }
    }
}
