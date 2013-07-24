using System.Web.Mvc;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Agents.Applications.Commands;
using LinkMe.Apps.Agents.Applications.Queries;
using LinkMe.Apps.Management.Areas.Communications.Models;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Urls;
using Microsoft.Practices.Unity;

namespace LinkMe.Apps.Management.Areas.Communications.Views
{
    public abstract class CommunicationsViewPage<TModel>
        : Asp.Mvc.Views.ViewPage<TModel>
        where TModel : CommunicationsModel
    {
        private static EventSource _eventSource;

        [Dependency]
        public IWebSiteQuery WebSiteQuery { get; set; }
        [Dependency]
        public ITinyUrlCommand TinyUrlCommand { get; set; }

        protected CommunicationsViewPage(EventSource eventSource)
        {
            _eventSource = eventSource;
        }

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            var model = (CommunicationsModel)ViewContext.ViewData.Model;
            ViewContext.TempData["TinyUrlMappings"] = new TinyUrlMappings(WebSiteQuery, model.ContextId, model.Definition, ContentType, WebSite.LinkMe, model.UserId, null);
        }

        protected override void OnUnload(System.EventArgs e)
        {
            const string method = "OnUnload";

            base.OnUnload(e);

            var mappings = ViewContext.TempData["TinyUrlMappings"] as TinyUrlMappings;
            if (mappings != null)
            {
                TinyUrlCommand.CreateMappings(mappings);

                // Need to log the urls for tracking.

                _eventSource.Raise(Event.CommunicationTracking,
                    method,
                    Model.Definition + " communication links.",
                    Event.Arg(typeof(CommunicationTrackingType).Name, CommunicationTrackingType.Links),
                    Event.Arg("ContextId", Model.ContextId),
                    Event.Arg("TinyUrls", mappings));
            }
        }

        protected string GetActivationUrl(HtmlHelper html, string activationCode, bool secure, string applicationPath, params string[] parameters)
        {
            if (string.IsNullOrEmpty(activationCode))
                return html.TinyUrl(secure, applicationPath, parameters);

            var webSiteUrl = WebSiteQuery.GetUrl(WebSite.LinkMe, null, secure, applicationPath, new ReadOnlyQueryString(parameters));
            return html.TinyUrl(false, "~/accounts/activation", "activationCode", activationCode, "returnUrl", webSiteUrl.PathAndQuery);
        }

        protected string GetLogoutLoginUrl(HtmlHelper html, string loginApplicationPath, string applicationPath, params string[] parameters)
        {
            var webSiteUrl = WebSiteQuery.GetLoginUrl(WebSite.LinkMe, null, loginApplicationPath, applicationPath, new ReadOnlyQueryString(parameters));
            return html.TinyUrl(true, "~/logout", "returnUrl", webSiteUrl.PathAndQuery);
        }

        protected string GetLogoutUrl(HtmlHelper html, bool secure, string applicationPath, params string[] parameters)
        {
            var webSiteUrl = WebSiteQuery.GetUrl(WebSite.LinkMe, null, secure, applicationPath, new ReadOnlyQueryString(parameters));
            return html.TinyUrl(true, "~/logout", "returnUrl", webSiteUrl.PathAndQuery);
        }
    }
}