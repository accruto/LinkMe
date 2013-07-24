using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Domain.Industries.Data
{
    public class IndustriesRepository
        : Repository, IIndustriesRepository
    {
        private static readonly Func<IndustriesDataContext, IQueryable<Industry>> GetIndustriesQuery
            = CompiledQuery.Compile((IndustriesDataContext dc)
                => from i in dc.IndustryEntities
                   orderby i.displayName
                   select i.Map());

        private static readonly Func<IndustriesDataContext, IQueryable<IndustryAliasEntity>> GetIndustryAliasEntitiesQuery
            = CompiledQuery.Compile((IndustriesDataContext dc)
                => from i in dc.IndustryAliasEntities
                   select i);

        public IndustriesRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        IList<Industry> IIndustriesRepository.GetIndustries()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetIndustries(dc);
            }
        }

        private static IList<Industry> GetIndustries(IndustriesDataContext dc)
        {
            // Get all industries.

            var industries = GetIndustriesQuery(dc).ToList();
            var industryMap = industries.ToDictionary(i => i.Id, i => i);

            // Get all aliases.

            var aliases = GetIndustryAliasEntitiesQuery(dc).ToArray();
            foreach (var alias in aliases)
            {
                var industry = industryMap[alias.industryId];
                industry.Aliases.Add(alias.displayName);
                if (!string.IsNullOrEmpty(alias.urlName))
                    industry.UrlAliases.Add(alias.urlName);
            }

            return industries;
        }

        private IndustriesDataContext CreateContext()
        {
            return CreateContext(c => new IndustriesDataContext(c));
        }
    }
}
