using System.Collections.Generic;

namespace LinkMe.Domain.Industries
{
    public interface IIndustriesRepository
    {
        IList<Industry> GetIndustries();
    }
}
