using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Query.Search.Members.Queries
{
    public interface ISuggestedMembersQuery
    {
        MemberSearchCriteria GetCriteria(JobAd jobAd);
    }
}
