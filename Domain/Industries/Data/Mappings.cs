using System.Collections.Generic;

namespace LinkMe.Domain.Industries.Data
{
    internal static class Mappings
    {
        public static Industry Map(this IndustryEntity entity)
        {
            return new Industry
            {
                Id = entity.id,
                Name = entity.displayName,
                ShortName = entity.shortDisplayName,
                UrlName = entity.urlName,
                KeywordExpression = entity.keywordExpression,
                Aliases = new List<string>(),
                UrlAliases = new List<string>(),
            };
        }
    }
}
