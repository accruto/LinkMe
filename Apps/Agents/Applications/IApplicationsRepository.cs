using System;
using System.Collections.Generic;

namespace LinkMe.Apps.Agents.Applications
{
    public interface IApplicationsRepository
    {
        void CreateMappings(IEnumerable<TinyUrlMapping> mappings);
        TinyUrlMapping GetMapping(Guid tinyId);
        IList<TinyUrlMapping> GetMappings(WebSite webSite, Guid? verticalId, bool secure, string longUrl, Guid contextId, string mimeType, int instance);
    }
}