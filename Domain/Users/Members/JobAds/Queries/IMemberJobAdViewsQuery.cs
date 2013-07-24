using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Domain.Users.Members.JobAds.Queries
{
    public interface IMemberJobAdViewsQuery
    {
        JobAdView GetJobAdView(Guid jobAdId);
        JobAdView GetJobAdView(JobAd jobAd);
        IList<JobAdView> GetJobAdViews(IEnumerable<Guid> jobAdIds);

        MemberJobAdView GetMemberJobAdView(IMember member, Guid jobAdId);
        MemberJobAdView GetMemberJobAdView(IMember member, JobAd jobAd);
        IList<MemberJobAdView> GetMemberJobAdViews(IMember member, IEnumerable<Guid> jobAdIds);
    }
}
