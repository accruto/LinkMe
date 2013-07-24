using System.Collections.Generic;
using Ionic.Zip;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Apps.Presentation.Domain.Roles.JobAds
{
    public interface IMemberJobAdFilesQuery
    {
        DocFile GetJobAdFile(JobAd jobAd);
        ZipFile GetJobAdFile(IEnumerable<JobAd> jobAds);
    }
}
