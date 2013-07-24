using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Agents.Applications.Queries
{
    public class WebSiteApplications
    {
        private readonly Guid _verticalId;
        private readonly IDictionary<WebSite, Application> _applications = new Dictionary<WebSite, Application>();

        public WebSiteApplications(Guid verticalId)
        {
            _verticalId = verticalId;
        }

        internal Guid VerticalId
        {
            get { return _verticalId; }
        }

        internal void Add(WebSite webSite, string host, int port, string applicationPath)
        {
            _applications[webSite] = new Application(host, port, applicationPath);
        }

        public Application this[WebSite webSite]
        {
            get
            {
                Application application;
                if (!_applications.TryGetValue(webSite, out application))
                    throw new ApplicationException("Cannot get the '" + webSite + "' application because it has not been created in IIS.");
                return application;
            }
        }
    }

    internal class VerticalWebSiteApplications
    {
        private readonly IDictionary<Guid, WebSiteApplications> _webSiteApplications = new Dictionary<Guid, WebSiteApplications>();

        public VerticalWebSiteApplications(IVerticalsQuery verticalsQuery, Func<WebSite, string, string> getHost, Func<WebSite, int> getPort, Func<WebSite, string> getApplicationPath)
        {
            // Add a default.

            var webSiteApplications = new WebSiteApplications(Guid.Empty);
            foreach (WebSite webSite in Enum.GetValues(typeof(WebSite)))
            {
                var host = getHost(webSite, null);
                var port = getPort(webSite);
                var applicationPath = getApplicationPath(webSite);
                if (port != Url.InsecurePort || applicationPath != null)
                    webSiteApplications.Add(webSite, host, port, applicationPath);
            }

            Add(webSiteApplications);

            // Set up a collection for each vertical that has its own host defined.

            foreach (var vertical in verticalsQuery.GetVerticals())
            {
                if (!string.IsNullOrEmpty(vertical.Host))
                {
                    webSiteApplications = new WebSiteApplications(vertical.Id);
                    foreach (WebSite webSite in Enum.GetValues(typeof(WebSite)))
                    {
                        var host = getHost(webSite, vertical.Host);
                        var port = getPort(webSite);
                        var applicationPath = getApplicationPath(webSite);
                        if (port != Url.InsecurePort || applicationPath != null)
                            webSiteApplications.Add(webSite, host, port, applicationPath);
                    }

                    Add(webSiteApplications);
                }
            }
        }

        public WebSiteApplications this[Guid? verticalId]
        {
            get
            {
                if (verticalId == null)
                    verticalId = Guid.Empty;
                WebSiteApplications applications;
                _webSiteApplications.TryGetValue(verticalId.Value, out applications);
                return applications;
            }
        }

        private void Add(WebSiteApplications applications)
        {
            _webSiteApplications[applications.VerticalId] = applications;
        }
    }

    public class WebSiteQuery
        : IWebSiteQuery
    {
        private readonly IVerticalsQuery _verticalsQuery;
        private volatile VerticalWebSiteApplications _verticalWebSiteApplications;

        public WebSiteQuery(IVerticalsQuery verticalsQuery)
        {
            _verticalsQuery = verticalsQuery;
            
        }

        private void EnsureInitialised()
        {
            if (_verticalWebSiteApplications == null)
                Initialise();
        }

        private void Initialise()
        {
            _verticalWebSiteApplications = new VerticalWebSiteApplications(_verticalsQuery, ApplicationSetUp.GetHost, ApplicationSetUp.GetPort, ApplicationSetUp.GetApplicationPath);
        }

        ReadOnlyUrl IWebSiteQuery.GetUrl(WebSite webSite, Guid? verticalId, bool secure, string applicationPath)
        {
            return GetUrl(webSite, verticalId, secure, applicationPath, null);
        }

        ReadOnlyUrl IWebSiteQuery.GetUrl(WebSite webSite, Guid? verticalId, bool secure, string applicationPath, ReadOnlyQueryString queryString)
        {
            return GetUrl(webSite, verticalId, secure, applicationPath, queryString);
        }

        ReadOnlyUrl IWebSiteQuery.GetLoginUrl(WebSite webSite, Guid? verticalId, string loginApplicationPath, string applicationPath)
        {
            return GetLoginUrl(webSite, verticalId, loginApplicationPath, applicationPath, null);
        }

        ReadOnlyUrl IWebSiteQuery.GetLoginUrl(WebSite webSite, Guid? verticalId, string loginApplicationPath, string applicationPath, ReadOnlyQueryString queryString)
        {
            return GetLoginUrl(webSite, verticalId, loginApplicationPath, applicationPath, queryString);
        }

        private ReadOnlyUrl GetUrl(WebSite webSite, Guid? verticalId, bool secure, string applicationPath, ReadOnlyQueryString queryString)
        {
            EnsureInitialised();
            var webSiteApplications = GetWebSiteApplications(verticalId);
            return new ReadOnlyApplicationUrl(webSiteApplications[webSite], secure, applicationPath, queryString);
        }

        private ReadOnlyUrl GetLoginUrl(WebSite webSite, Guid? verticalId, string loginApplicationPath, string applicationPath, ReadOnlyQueryString queryString)
        {
            EnsureInitialised();
            var webSiteApplications = GetWebSiteApplications(verticalId);

            var url = new ReadOnlyApplicationUrl(webSiteApplications[webSite], null, applicationPath, queryString);
            return new ReadOnlyApplicationUrl(webSiteApplications[webSite], true, loginApplicationPath, new ReadOnlyQueryString("returnUrl", url.PathAndQuery));
        }

        private WebSiteApplications GetWebSiteApplications(Guid? verticalId)
        {
            if (verticalId == null || verticalId.Value == Guid.Empty)
                return _verticalWebSiteApplications[verticalId];

            var applications = _verticalWebSiteApplications[verticalId];
            if (applications != null)
                return applications;

            // Asked for a vertical by id but it has not been cached.
            // May have been added so refresh and try again.

            _verticalWebSiteApplications = null;
            EnsureInitialised();

            applications = _verticalWebSiteApplications[verticalId];
            if (applications != null)
                return applications;

            return _verticalWebSiteApplications[Guid.Empty];
        }
    }
}