using System;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Services.External.CareerOne.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Contenders.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application = LinkMe.Domain.Roles.Contenders.Application;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.Previous
{
    [TestClass]
    public abstract class PreviousApplicationsTests
        : JobsTests
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IJobAdApplicationSubmissionsCommand _jobAdsApplicationSubmissionsCommand = Resolve<IJobAdApplicationSubmissionsCommand>();
        private readonly IApplicationsCommand _applicationsCommand = Resolve<IApplicationsCommand>();
        private readonly ICareerOneQuery _careerOneQuery = Resolve<ICareerOneQuery>();

        protected Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(0);
        }

        protected JobAd CreateInternalJobAd(IEmployer employer)
        {
            var jobAd = employer.CreateTestJobAd();
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }

        protected JobAd CreateExternalJobAd(IEmployer employer)
        {
            var jobAd = employer.CreateTestJobAd();
            jobAd.Integration.IntegratorUserId = _careerOneQuery.GetIntegratorUser().Id;
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }

        protected Application ApplyForInternalJob(JobAdEntry jobAd, Guid applicantId)
        {
            var application = new InternalApplication { ApplicantId = applicantId };
            _jobAdsApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdsApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
            return application;
        }

        protected Application ApplyForExternalJob(IJobAd jobAd, Guid applicantId)
        {
            var application = new ExternalApplication { ApplicantId = applicantId, PositionId = jobAd.Id };
            _applicationsCommand.CreateApplication(application);
            return application;
        }
    }
}