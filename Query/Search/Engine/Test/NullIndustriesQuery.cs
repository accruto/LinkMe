using System;
using System.Collections.Generic;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;

namespace LinkMe.Query.Search.Engine.Test
{
    class NullIndustriesQuery
        : IIndustriesQuery
    {
        #region Implementation of IIndustriesQuery

        public IList<Industry> GetIndustries()
        {
            return new Industry[0];
        }

        public IList<Industry> GetIndustries(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Industry GetIndustry(Guid id)
        {
            throw new NotImplementedException();
        }

        public Industry GetIndustry(string name)
        {
            return null;
        }

        public Industry GetIndustryByUrlName(string urlName)
        {
            throw new NotImplementedException();
        }

        public Industry GetIndustryByAnyName(string name)
        {
            throw new NotImplementedException();
        }

        public Industry GetDefaultIndustry()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
