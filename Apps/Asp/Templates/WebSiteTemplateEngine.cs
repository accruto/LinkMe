using System;
using System.Net.Mime;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Agents.Applications.Commands;
using LinkMe.Apps.Agents.Applications.Queries;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Content;
using LinkMe.Framework.Content.Templates;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Reports.Roles.Orders;
using LinkMe.Query.Reports.Roles.Orders.Queries;

namespace LinkMe.Apps.Asp.Templates
{
    public class WebSiteTemplateEngine
        : TemplateEngine
    {
        private MergeSettings _settings;
        private readonly IWebSiteQuery _webSiteQuery;
        private readonly ITinyUrlCommand _tinyUrlCommand;

        public WebSiteTemplateEngine(IContentEngine contentEngine, IWebSiteQuery webSiteQuery, ITinyUrlCommand tinyUrlCommand)
            : base(contentEngine)
        {
            _webSiteQuery = webSiteQuery;
            _tinyUrlCommand = tinyUrlCommand;
        }

        protected override ITextTemplateEngine CreateTextTemplateEngine(Guid? verticalId, TemplateContentItem contentItem)
        {
            if (_settings == null)
                _settings = CreateSettings(verticalId);
            var settings = CreateSettings(_settings);
            return new WebSiteTextTemplateEngine(verticalId, MediaTypeNames.Text.Plain, contentItem.Subject, settings, WebSite.LinkMe, _webSiteQuery, _tinyUrlCommand);
        }

        protected override ITextTemplateEngine CreateTextTemplateEngine(Guid? verticalId, ViewContentItem contentItem)
        {
            if (_settings == null)
                _settings = CreateSettings(verticalId);
            var settings = CreateSettings(_settings);

            return new WebSiteTextTemplateEngine(verticalId, contentItem.MimeType, contentItem.Parts[0].Text, settings, WebSite.LinkMe, _webSiteQuery, _tinyUrlCommand);
        }

        private static MergeSettings CreateSettings(MergeSettings parentSettings)
        {
            return parentSettings.Clone();
        }

        private MergeSettings CreateSettings(Guid? verticalId)
        {
            var settings = new MergeSettings();

            // Add references to some needed assemblies, i.e. Core and Common.

            settings.References.Add(typeof(NamesExtensions).Assembly);
            settings.References.Add(typeof(EmailsExtensions).Assembly);
            settings.References.Add(typeof(TextUtil).Assembly);
            settings.References.Add(typeof(IUser).Assembly);
            settings.References.Add(typeof(Url).Assembly);
            settings.References.Add(typeof(WebSite).Assembly);
            settings.References.Add(typeof(IInstrumentable).Assembly);
            settings.References.Add(typeof(ICommunicationRecipient).Assembly);
            settings.References.Add(typeof(Definition).Assembly);
            settings.References.Add(typeof(OrderReport).Assembly);
            settings.References.Add(typeof(JobAd).Assembly);

            // Add a namespace for the EmailManager.

            settings.Usings.Add(typeof(NamesExtensions));
            settings.Usings.Add(typeof(EmailsExtensions));
            settings.Usings.Add(typeof(TextUtil));
            settings.Usings.Add(typeof(UrlExtensions));
            settings.Usings.Add(typeof(WebSite));
            settings.Usings.Add(typeof(IInstrumentable));
            settings.Usings.Add(typeof(ICommunicationRecipient));

            // Add some useful methods.

            settings.Fields.Add(new MergeField("RootPath", _webSiteQuery.GetUrl(WebSite.LinkMe, null, false, "~/").AbsoluteUri.ToLower()));
            settings.Fields.Add(new MergeField("SecureRootPath", _webSiteQuery.GetUrl(WebSite.LinkMe, null, true, "~/").AbsoluteUri.ToLower()));
            settings.Fields.Add(new MergeField("VerticalRootPath", _webSiteQuery.GetUrl(WebSite.LinkMe, verticalId, false, "~/").AbsoluteUri.ToLower()));

            settings.Methods.Add(
                @"        private static string MakeNamePossessive(string name)
        {
            return NamesExtensions.MakeNamePossessive(name);
        }");

            settings.Methods.Add(
                @"        private static string GetPrice(decimal price, LinkMe.Domain.Currency currency)
        {
            return price.ToString(" + "\"C\"" + @", currency.CultureInfo);
        }");

            settings.Methods.Add(@"        private static string GetUrl(bool secure, string applicationPath)
        {
            if (applicationPath.Length > 0 && applicationPath[0] == '~')
                applicationPath = applicationPath.Substring(1);
            if (secure)
                return UrlExtensions.AddUrlSegments(SecureRootPath, applicationPath).ToLower();
            else
                return UrlExtensions.AddUrlSegments(RootPath, applicationPath).ToLower();
        }");

            settings.Methods.Add(@"        private static string GetTrackingPixelUrl(Guid id)
        {
            string applicationPath = ""/url/"" + id.ToString(""n"") + "".aspx"";
            return UrlExtensions.AddUrlSegments(VerticalRootPath, applicationPath).ToLower();
        }");

            settings.Methods.Add(@"        private static string DoNotEncode(string text)
        {
            return text;
        }");

            settings.Methods.Add(@"        private static string ConvertHtmlToPlainText(string text)
        {
            return EmailsExtensions.ConvertHtmlToPlainText(text);
        }");

            return settings;
        }
    }
}
