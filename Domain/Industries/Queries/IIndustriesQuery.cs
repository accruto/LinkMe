using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Industries.Queries
{
    public interface IIndustriesQuery
    {
        IList<Industry> GetIndustries();
        IList<Industry> GetIndustries(IEnumerable<Guid> ids);

        Industry GetIndustry(Guid id);
        Industry GetIndustry(string name);
        Industry GetIndustryByUrlName(string urlName);
        Industry GetIndustryByAnyName(string name);
    }
}