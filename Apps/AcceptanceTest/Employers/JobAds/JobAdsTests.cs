using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Employers.Models.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds
{
    [TestClass]
    public abstract class JobAdsTests
        : WebTestClass
    {
        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        protected readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        protected readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        protected readonly ICandidateFoldersCommand _candidateFoldersCommand = Resolve<ICandidateFoldersCommand>();
        protected readonly ICandidateListsCommand _candidateListsCommand = Resolve<ICandidateListsCommand>();
        protected readonly ICandidateFoldersQuery _candidateFoldersQuery = Resolve<ICandidateFoldersQuery>();

        private ReadOnlyUrl _baseManageCandidatesUrl;
        private ReadOnlyUrl _baseApiUrl;

        [TestInitialize]
        public void JobAdsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _baseManageCandidatesUrl = new ReadOnlyApplicationUrl("~/employers/candidates/manage/");
            _baseApiUrl = new ReadOnlyApplicationUrl("~/employers/candidates/jobads/api/");
        }

        protected Employer CreateEmployer()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Allocate credits.

            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id });

            return employer;
        }

        protected ReadOnlyUrl GetManageCandidatesUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl(_baseManageCandidatesUrl, jobAdId.ToString());
        }

        protected JsonJobAdModel ShortlistCandidates(Guid jobAdId, params Member[] members)
        {
            return ProcessCandidates(jobAdId, members, "shortlistcandidates");
        }

        protected JsonJobAdModel RejectCandidates(Guid jobAdId, params Member[] members)
        {
            return ProcessCandidates(jobAdId, members, "rejectcandidates");
        }

        protected JsonJobAdModel RemoveCandidates(Guid jobAdId, params Member[] members)
        {
            return ProcessCandidates(jobAdId, members, "removecandidates");
        }

        protected void AssertModel(JsonJobAdModel model, int shortlistedCandidates, int newCandidates, int rejectedCandidates)
        {
            AssertJsonSuccess(model);
            Assert.AreEqual(newCandidates, model.JobAd.ApplicantCounts.New);
            Assert.AreEqual(shortlistedCandidates, model.JobAd.ApplicantCounts.ShortListed);
            Assert.AreEqual(rejectedCandidates, model.JobAd.ApplicantCounts.Rejected);
        }

        private JsonJobAdModel ProcessCandidates(Guid jobAdId, IEnumerable<Member> members, string action)
        {
            var parameters = new string[0];
            if (members != null)
            {
                foreach (var member in members)
                    parameters = parameters.Concat(new[] { "candidateId", member.Id.ToString() }).ToArray();
            }

            var url = new ReadOnlyApplicationUrl(_baseApiUrl, jobAdId + "/" + action, new ReadOnlyQueryString(parameters));
            var response = Post(url);
            return new JavaScriptSerializer().Deserialize<JsonJobAdModel>(response);
        }
    }
}