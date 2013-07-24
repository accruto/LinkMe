using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.JobAds;

namespace LinkMe.Apps.Presentation.Query.Search.JobAds
{
    public static class JobAdSearchCriteriaExtensions
    {
        private static readonly ILocationQuery LocationQuery = Container.Current.Resolve<ILocationQuery>();
        private static readonly IIndustriesQuery IndustriesQuery = Container.Current.Resolve<IIndustriesQuery>();

        public static ReadOnlyQueryString GetQueryString(this JobAdSearchCriteria criteria)
        {
            return new QueryStringGenerator(new JobAdSearchCriteriaConverter(LocationQuery, IndustriesQuery)).GenerateQueryString(criteria);
        }
    }
}
