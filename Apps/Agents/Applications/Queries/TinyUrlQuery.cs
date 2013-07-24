using System;
using System.Collections.Generic;

namespace LinkMe.Apps.Agents.Applications.Queries
{
    public class TinyUrlQuery
        : ITinyUrlQuery
    {
        private readonly IApplicationsRepository _repository;

        public TinyUrlQuery(IApplicationsRepository repository)
        {
            _repository = repository;
        }

        TinyUrlMapping ITinyUrlQuery.GetMapping(Guid tinyId)
        {
            return _repository.GetMapping(tinyId);
        }

        IList<TinyUrlMapping> ITinyUrlQuery.GetMappings(WebSite webSite, Guid? verticalId, bool secure, string longUrl, Guid contextId, string mimeType, int instance)
        {
            return _repository.GetMappings(webSite, verticalId, secure, longUrl, contextId, mimeType, instance);
        }
    }
}