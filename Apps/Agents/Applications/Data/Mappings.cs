using System;
using LinkMe.Apps.Agents.Applications.Queries;

namespace LinkMe.Apps.Agents.Applications.Data
{
    internal static class Mappings
    {
        public static TinyUrlMappingEntity Map(this TinyUrlMapping mapping, string longUrl, bool secure)
        {
            return new TinyUrlMappingEntity
            {
                createdTime = mapping.CreatedTime,
                tinyId = mapping.TinyId,
                webSite = (byte)mapping.WebSite,
                verticalId = mapping.VerticalId ?? Guid.Empty,
                secure = secure,
                longUrl = longUrl,
                contextId = mapping.ContextId,
                definition = mapping.Definition,
                mimeType = mapping.MimeType,
                instance = mapping.Instance,
                userId = mapping.UserId,
            };
        }

        public static TinyUrlMapping Map(this TinyUrlMappingEntity entity, IWebSiteQuery webSiteQuery)
        {
            var webSite = (WebSite)entity.webSite;
            var verticalId = entity.verticalId == Guid.Empty ? (Guid?)null : entity.verticalId;

            return new TinyUrlMapping
            {
                CreatedTime = entity.createdTime,
                TinyId = entity.tinyId,
                WebSite = webSite,
                UserId = entity.userId,
                VerticalId = verticalId,
                LongUrl = webSiteQuery.GetUrl(webSite, verticalId, entity.secure, entity.longUrl),
                ContextId = entity.contextId,
                Definition = entity.definition,
                MimeType = entity.mimeType,
                Instance = entity.instance
            };
        }
    }
}