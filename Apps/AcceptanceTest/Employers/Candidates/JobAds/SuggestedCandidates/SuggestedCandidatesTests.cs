using System;
using LinkMe.AcceptanceTest.Employers.Search;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.Members.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.JobAds.SuggestedCandidates
{
    [TestClass]
    public abstract class SuggestedCandidatesTests
        : SearchTests
    {
        protected readonly ISuggestedMembersQuery _suggestedMembersQuery = Resolve<ISuggestedMembersQuery>();
        protected readonly IEmployerJobAdsCommand _employerJobAdsCommand = Resolve<IEmployerJobAdsCommand>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();

        private ReadOnlyUrl _baseSuggestedCandidatesUrl;
        private ReadOnlyUrl _externalSuggestedCandidatesUrl;

        private const string TitleFormat = "The is the {0} title.";
        private const string ContentFormat = "The is the {0} title.";
        private const string ExternalReferenceIdFormat = "ABC00{0}";

        [TestInitialize]
        public void TestInitialize()
        {
            _baseSuggestedCandidatesUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/suggested/");
            _externalSuggestedCandidatesUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/suggested");
        }

        protected ReadOnlyUrl GetSuggestedCandidatesUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl(_baseSuggestedCandidatesUrl, jobAdId.ToString());
        }

        protected ReadOnlyUrl GetSuggestedCandidatesUrl(string externalReferenceId)
        {
            var url = _externalSuggestedCandidatesUrl.AsNonReadOnly();
            url.QueryString["externalReferenceId"] = externalReferenceId;
            return url;
        }

        protected JobAd CreateJobAd(IEmployer employer, int index)
        {
            var jobAd = new JobAd
            {
                Title = string.Format(TitleFormat, index),
                Description =
                {
                    Content = string.Format(ContentFormat, index),
                },
                Integration =
                {
                    ExternalReferenceId = string.Format(ExternalReferenceIdFormat, index),
                },
            };

            _employerJobAdsCommand.CreateJobAd(employer, jobAd);
            return jobAd;
        }

        protected void OpenJobAd(IEmployer employer, JobAd jobAd)
        {
            _employerJobAdsCommand.OpenJobAd(employer, jobAd, false);
        }

        protected void AssertJobAdTitle(string title)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//span[@id='results-header-text']/span[@class='suggested-candidate_search-criterion search-criterion']/span[@class='search-criterion-data']");
            Assert.IsNotNull(node);
            Assert.AreEqual(title, node.InnerText);
        }

        protected void AssertNoCandidates()
        {
            AssertPageContains("No candidates match your criteria");
        }

        protected Employer CreateEmployer(int index, int? jobAdCredits)
        {
            var employer = CreateEmployer(index);
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = jobAdCredits, OwnerId = employer.Id });
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });
            return employer;
        }
    }
}
