using System;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Agents.Applications.Queries;
using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Public.Routes;
using LinkMe.Web.Context;

namespace LinkMe.Web.Areas.Public.Controllers.Support
{
    public class TinyUrlsController
        : ViewController
    {
        private static readonly EventSource EventSource = new EventSource<TinyUrlsController>();

        private readonly ITinyUrlQuery _tinyUrlQuery;
        private readonly IVerticalsQuery _verticalsQuery;

        public TinyUrlsController(ITinyUrlQuery tinyUrlQuery, IVerticalsQuery verticalsQuery)
        {
            _tinyUrlQuery = tinyUrlQuery;
            _verticalsQuery = verticalsQuery;
        }

        public ActionResult Track(Guid id)
        {
            const string method = "Track";

            // Track.

            EventSource.Raise(
                Event.CommunicationTracking,
                method,
                "Communication opened.",
                Event.Arg(typeof(CommunicationTrackingType).Name, CommunicationTrackingType.Opened),
                Event.Arg("Id", id));

            HttpContext.Response.End();
            return null;
        }

        public ActionResult TinyUrl(Guid id)
        {
            const string method = "TinyUrl";

            // Check the mapping.

            var mapping = _tinyUrlQuery.GetMapping(id);
            if (mapping == null || mapping.LongUrl == null)
                return RedirectToRoute(HomeRoutes.Home);

            // Track.

            EventSource.Raise(
                Event.CommunicationTracking,
                method,
                "Communication link clicked for '" + mapping.LongUrl.AbsoluteUri + "'.",
                Event.Arg(typeof(CommunicationTrackingType).Name, CommunicationTrackingType.LinkClicked),
                Event.Arg("TinyId", mapping.TinyId),
                Event.Arg("WebSite", mapping.WebSite),
                Event.Arg("VerticalId", mapping.VerticalId),
                Event.Arg("LongUrl", mapping.LongUrl),
                Event.Arg("ContextId", mapping.ContextId),
                Event.Arg("Definition", mapping.Definition),
                Event.Arg("MimeType", mapping.MimeType),
                Event.Arg("Instance", mapping.Instance));

            // Need to set up some stuff.

            SetUser(mapping.UserId);
            SetVertical(mapping.VerticalId);

            // Redirect.

            return RedirectToUrl(GetUrl(mapping));
        }

        private static ReadOnlyUrl GetUrl(TinyUrlMapping mapping)
        {
            if (string.IsNullOrEmpty(mapping.Definition))
                return mapping.LongUrl;

            // Add in tracking parameters.

            var url = mapping.LongUrl.AsNonReadOnly();

            // If it is a login or activation page then change the returnUrl parameter.

            if (url.Path.EndsWith("/login") || url.Path.EndsWith("/activation"))
            {
                var queryStringReturnUrl = url.QueryString["returnUrl"];
                if (queryStringReturnUrl != null)
                {
                    var returnUrl = new ApplicationUrl(queryStringReturnUrl);
                    AddTracking(mapping.Definition, returnUrl);
                    url.QueryString["returnUrl"] = returnUrl.PathAndQuery;
                    return url;
                }
            }

            AddTracking(mapping.Definition, url);
            return url;
        }

        private static void AddTracking(string definition, Url url)
        {
            url.QueryString["utm_source"] = "linkme";
            url.QueryString["utm_medium"] = "email";
            url.QueryString["utm_campaign"] = definition.ToLower();
        }

        private void SetUser(Guid? userId)
        {
            if (userId != null)
                new AnonymousUserContext(HttpContext).RequestUserId = userId;
        }

        private void SetVertical(Guid? verticalId)
        {
            if (verticalId == null)
                return;

            // Look for a vertical.

            var vertical = _verticalsQuery.GetVertical(verticalId.Value);
            if (vertical != null)
                ActivityContext.Current.Set(vertical);
        }
    }
}
