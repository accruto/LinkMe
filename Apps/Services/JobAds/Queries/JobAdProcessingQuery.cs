using LinkMe.Apps.Services.External.CareerOne.Queries;
using LinkMe.Apps.Services.External.JXT.Queries;
using LinkMe.Apps.Services.External.MyCareer.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Services.JobAds.Queries
{
    public class JobAdProcessingQuery
        : IJobAdProcessingQuery
    {
        private readonly ICareerOneQuery _careerOneQuery;
        private readonly IMyCareerQuery _myCareerQuery;
        private readonly IJxtQuery _jxtQuery;

        public JobAdProcessingQuery(ICareerOneQuery careerOneQuery, IMyCareerQuery myCareerQuery, IJxtQuery jxtQuery)
        {
            _careerOneQuery = careerOneQuery;
            _myCareerQuery = myCareerQuery;
            _jxtQuery = jxtQuery;
        }

        JobAdProcessing IJobAdProcessingQuery.GetJobAdProcessing(IJobAd jobAd)
        {
            // If it does not have a url or application questions then it can only be managed internally.

            if (string.IsNullOrEmpty(jobAd.Integration.ExternalApplyUrl) && string.IsNullOrEmpty(jobAd.Integration.ExternalApplyApiUrl) && jobAd.Application.Questions.IsNullOrEmpty())
                return JobAdProcessing.ManagedInternally;

            // If it is a CareerOne or MyCareer job ad then everything is done externally.

            return jobAd.Integration.IntegratorUserId != null
                &&
                (
                    jobAd.Integration.IntegratorUserId.Value == _careerOneQuery.GetIntegratorUser().Id
                    || jobAd.Integration.IntegratorUserId.Value == _myCareerQuery.GetIntegratorUser().Id
                    || jobAd.Integration.IntegratorUserId.Value == _jxtQuery.GetIntegratorUser().Id
                )
                ? JobAdProcessing.AppliedForExternally
                : JobAdProcessing.ManagedExternally;
        }
    }
}
