using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Apps.Agents.Applications.Queries;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Apps.Agents.Applications.Data
{
    public class ApplicationsRepository
        : IApplicationsRepository
    {
        private static readonly EventSource EventSource = new EventSource<ApplicationsRepository>();

        private readonly IWebSiteQuery _webSiteQuery;
        private readonly IDbConnectionFactory _connectionFactory;

        private class MappingCriteria
        {
            public WebSite WebSite;
            public Guid? VerticalId;
            public bool Secure;
            public string LongUrl;
            public Guid ContextId;
            public string MimeType;
            public int Instance;
        }

        private static readonly Func<ApplicationsDataContext, Guid, IWebSiteQuery, TinyUrlMapping> GetMapping
            = CompiledQuery.Compile((ApplicationsDataContext dc, Guid tinyId, IWebSiteQuery webSiteQuery)
                => (from m in dc.TinyUrlMappingEntities
                    where m.tinyId == tinyId
                    select m.Map(webSiteQuery)).SingleOrDefault());

        private static readonly Func<ApplicationsDataContext, MappingCriteria, IWebSiteQuery, IQueryable<TinyUrlMapping>> GetMappingsByCriteria
            = CompiledQuery.Compile((ApplicationsDataContext dc, MappingCriteria criteria, IWebSiteQuery webSiteQuery)
                => from m in dc.TinyUrlMappingEntities
                   where m.webSite == (byte)criteria.WebSite
                   && m.verticalId == (criteria.VerticalId ?? Guid.Empty)
                   && m.secure == criteria.Secure
                   && m.longUrl == criteria.LongUrl
                   && m.contextId == criteria.ContextId
                   && m.mimeType == criteria.MimeType
                   && m.instance == criteria.Instance
                   select m.Map(webSiteQuery));

        public ApplicationsRepository(IDbConnectionFactory connectionFactory, IWebSiteQuery webSiteQuery)
        {
            _connectionFactory = connectionFactory;
            _webSiteQuery = webSiteQuery;
        }

        void IApplicationsRepository.CreateMappings(IEnumerable<TinyUrlMapping> mappings)
        {
            const string method = "CreateMappings";

            try
            {
                using (var dc = new ApplicationsDataContext(_connectionFactory.CreateConnection()))
                {
                    foreach (var mapping in mappings)
                    {
                        string longUrl;
                        bool secure;
                        GetLongUrlDetails(mapping, out longUrl, out secure);
                        dc.TinyUrlMappingEntities.InsertOnSubmit(mapping.Map(longUrl, secure));
                    }

                    dc.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                // Temporary log.

                foreach (var mapping in mappings)
                    EventSource.Raise(Event.Error, method, ex, new StandardErrorHandler(), Event.Arg("mapping", mapping));
                throw;
            }
        }

        TinyUrlMapping IApplicationsRepository.GetMapping(Guid tinyId)
        {
            using (var dc = new ApplicationsDataContext(_connectionFactory.CreateConnection()).AsReadOnly())
            {
                return GetMapping(dc, tinyId, _webSiteQuery);
            }
        }

        IList<TinyUrlMapping> IApplicationsRepository.GetMappings(WebSite webSite, Guid? verticalId, bool secure, string longUrl, Guid contextId, string mimeType, int instance)
        {
            using (var dc = new ApplicationsDataContext(_connectionFactory.CreateConnection()).AsReadOnly())
            {
                var criteria = new MappingCriteria
                {
                    WebSite = webSite,
                    VerticalId = verticalId,
                    Secure = secure,
                    LongUrl = longUrl,
                    ContextId = contextId,
                    MimeType = mimeType,
                    Instance = instance,
                };

                return GetMappingsByCriteria(dc, criteria, _webSiteQuery).ToList();
            }
        }

        private void GetLongUrlDetails(TinyUrlMapping mapping, out string longUrl, out bool secure)
        {
            secure = mapping.LongUrl.Scheme == Url.SecureScheme;

            // If the long url is not an application url then just return the absolute values.

            if (!(mapping.LongUrl is ApplicationUrl || mapping.LongUrl is ReadOnlyApplicationUrl))
            {
                longUrl = mapping.LongUrl.AbsoluteUri;
                return;
            }

            // Compare the url against the root url to determine whether the long url can be stored as am application relative value.

            var applicationRootUrl = _webSiteQuery.GetUrl(mapping.WebSite, mapping.VerticalId, secure, "~/");
            if (!mapping.LongUrl.AbsoluteUri.StartsWith(applicationRootUrl.AbsoluteUri, StringComparison.InvariantCultureIgnoreCase))
            {
                longUrl = mapping.LongUrl.AbsoluteUri;
            }
            else
            {
                // Use the application relative path.

                string appRelativePathAndQuery;
                if (mapping.LongUrl is ApplicationUrl)
                    appRelativePathAndQuery = ((ApplicationUrl)mapping.LongUrl).AppRelativePathAndQuery;
                else
                    appRelativePathAndQuery = ((ReadOnlyApplicationUrl)mapping.LongUrl).AppRelativePathAndQuery;

                longUrl = "~" + appRelativePathAndQuery;
            }
        }
    }
}