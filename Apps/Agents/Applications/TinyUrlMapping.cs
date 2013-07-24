using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using LinkMe.Apps.Agents.Applications.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Agents.Applications
{
    public class TinyUrlMapping
        : IInstrumentable
    {
        [DefaultNow]
        public DateTime CreatedTime { get; set; }

        public Guid TinyId { get; set; }
        public WebSite WebSite { get; set; }
        public Guid? VerticalId { get; set; }
        public ReadOnlyUrl LongUrl { get; set; }
        public Guid ContextId { get; set; }
        public int Instance { get; set; }
        public Guid? UserId { get; set; }
        public string Definition { get; set; }
        public string MimeType { get; set; }

        void IInstrumentable.Write(IInstrumentationWriter writer)
        {
            writer.Write("TinyId", TinyId);
            writer.Write("WebSite", WebSite);
            writer.Write("UserId", UserId);
            writer.Write("Definition", Definition);
            writer.Write("MimeType", MimeType);
            writer.Write("VerticalId", VerticalId);
            writer.Write("LongUrl", LongUrl);
            writer.Write("ContextId", ContextId);
            writer.Write("Instance", Instance);
        }
    }

    public class TinyUrlMappings
        : IEnumerable<TinyUrlMapping>, IInstrumentable
    {
        private readonly Guid _contextId;
        private readonly string _definition;
        private readonly string _mimeType;
        private readonly IWebSiteQuery _webSiteQuery;
        private readonly WebSite _webSite;
        private readonly Guid? _userId;
        private readonly Guid? _verticalId;
        private readonly IDictionary<string, IList<TinyUrlMapping>> _mappings = new Dictionary<string, IList<TinyUrlMapping>>();

        public TinyUrlMappings(IWebSiteQuery webSiteQuery, Guid contextId, string definition, string mimeType, WebSite webSite, Guid? userId, Guid? verticalId)
        {
            _contextId = contextId;
            _definition = definition;
            _mimeType = mimeType;
            _webSiteQuery = webSiteQuery;
            _webSite = webSite;
            _userId = userId;
            _verticalId = verticalId;
        }

        public string Register(bool secure, string applicationPath, params string[] queryString)
        {
            return Register(secure, applicationPath, new ReadOnlyQueryString(queryString));
        }

        public string Register(bool secure, string applicationPath, NameValueCollection queryString)
        {
            return Register(secure, applicationPath, new ReadOnlyQueryString(queryString));
        }

        public string Register(bool secure, string applicationPath, ReadOnlyQueryString queryString)
        {
            var longUrl = _webSiteQuery.GetUrl(_webSite, _verticalId, secure, applicationPath, queryString);

            // Create a tiny id and add the mapping.

            var tinyId = Guid.NewGuid();
            Add(tinyId, longUrl);

            // The url contains the id.

            return _webSiteQuery.GetUrl(_webSite, _verticalId, false, "~/url/" + tinyId.ToString("n")).AbsoluteUri;
        }

        public string RegisterLogin(string loginApplicationPath, string applicationPath, params string[] queryString)
        {
            return RegisterLogin(loginApplicationPath, applicationPath, new ReadOnlyQueryString(queryString));
        }

        public string RegisterLogin(string loginApplicationPath, string applicationPath, ReadOnlyQueryString queryString)
        {
            var longUrl = _webSiteQuery.GetLoginUrl(_webSite, _verticalId, loginApplicationPath, applicationPath, queryString);

            // Create a tiny id and add the mapping.

            var tinyId = Guid.NewGuid();
            Add(tinyId, longUrl);

            // The url contains the id.

            return _webSiteQuery.GetUrl(_webSite, _verticalId, false, "~/url/" + tinyId.ToString("n")).AbsoluteUri;
        }

        private void Add(Guid tinyId, ReadOnlyUrl longUrl)
        {
            // There may be multiple instances of the long urls so keep track of them.

            IList<TinyUrlMapping> mappings;
            if (_mappings.TryGetValue(longUrl.AbsoluteUri, out mappings))
            {
                mappings.Add(new TinyUrlMapping
                {
                    TinyId = tinyId,
                    WebSite = _webSite,
                    UserId = _userId,
                    VerticalId = _verticalId,
                    LongUrl = longUrl,
                    ContextId = _contextId,
                    Definition = _definition,
                    MimeType = _mimeType,
                    Instance = mappings.Count
                });
            }
            else
            {
                mappings = new List<TinyUrlMapping>
                {
                    new TinyUrlMapping
                    {
                        TinyId = tinyId,
                        WebSite = _webSite,
                        UserId = _userId,
                        VerticalId = _verticalId,
                        LongUrl = longUrl,
                        ContextId = _contextId,
                        Definition = _definition,
                        MimeType = _mimeType,
                        Instance = 0
                    },
                };
                _mappings[longUrl.AbsoluteUri] = mappings;
            }
        }

        IEnumerator<TinyUrlMapping> IEnumerable<TinyUrlMapping>.GetEnumerator()
        {
            return _mappings.SelectMany(p => p.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _mappings.SelectMany(p => p.Value).GetEnumerator();
        }

        void IInstrumentable.Write(IInstrumentationWriter writer)
        {
            foreach (var mappings in _mappings.Values)
            {
                foreach (var mapping in mappings)
                    writer.Write(mapping);
            }
        }
    }
}