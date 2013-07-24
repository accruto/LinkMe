using LinkMe.Apps.Presentation.Query.Search;
using LinkMe.Apps.Presentation.Query.Search.JobAds;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Presentation.Test.Query.Search.JobAds
{
    [TestClass]
    public class JobAdHighlighterTests
        : TestClass
    {
        private readonly IJobAdHighlighterFactory _highlighterFactory = Resolve<IJobAdHighlighterFactory>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();

        [TestInitialize]
        public void JobAdHighlighterTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestHighlightJobTitleDirectly()
        {
            var criteria = new JobAdSearchCriteria { AdTitle = "best" };
            var highlighter = _highlighterFactory.Create(JobAdHighlighterKind.Snippet, criteria, new HighlighterConfiguration { FragmentSize = 40 });

            var highlightedText = highlighter.HighlightTitle("The Best Job in the World");

            Assert.AreEqual("The <span class=\"highlighted-word\">Best</span> Job in the World", highlightedText);
        }

        [TestMethod]
        public void TestHighlightAdvertiserDirectly()
        {
            var criteria = new JobAdSearchCriteria { AdvertiserName = "world" };
            var highlighter = _highlighterFactory.Create(JobAdHighlighterKind.Snippet, criteria, new HighlighterConfiguration { FragmentSize = 40 });

            var highlightedText = highlighter.HighlightAdvertiser("The Best Company in the World");

            Assert.AreEqual("The Best Company in the <span class=\"highlighted-word\">World</span>", highlightedText);
        }

        [TestMethod]
        public void TestHighlightJobTitleIndirectly()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords("best");
            var highlighter = _highlighterFactory.Create(JobAdHighlighterKind.Snippet, criteria, new HighlighterConfiguration { FragmentSize = 40 });

            var highlightedText = highlighter.HighlightTitle("The Best Job in the World");

            Assert.AreEqual("The <span class=\"highlighted-word\">Best</span> Job in the World", highlightedText);
        }

        [TestMethod]
        public void TestHighlightAdvertiserIndirectly()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords("world");
            var highlighter = _highlighterFactory.Create(JobAdHighlighterKind.Snippet, criteria, new HighlighterConfiguration { FragmentSize = 40 });

            var highlightedText = highlighter.HighlightAdvertiser("The Best Company in the World");

            Assert.AreEqual("The Best Company in the <span class=\"highlighted-word\">World</span>", highlightedText);
        }

        [TestMethod]
        public void TestHighlightContent()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords("develop");
            var highlighter = _highlighterFactory.Create(JobAdHighlighterKind.Snippet, criteria, new HighlighterConfiguration { FragmentSize = 40 });

            var highlightedText = highlighter.HighlightContent("A developer wanted to develop in our development department");

            Assert.AreEqual("A developer wanted to <span class=\"highlighted-word\">develop</span> in our development department", highlightedText);
        }

        [TestMethod]
        public void TestSnippetContent()
        {
            var employer = _employersCommand.CreateTestEmployer(_organisationsCommand.CreateTestOrganisation(0));

            var poster = new JobPoster { Id = employer.Id, SendSuggestedCandidates = false, ShowSuggestedCandidates = true };
            _jobPostersCommand.CreateJobPoster(poster);

            var jobAd = _jobAdsCommand.PostTestJobAd(employer, JobAdStatus.Open);
            jobAd.Description.Content += jobAd.Description.Content;
            jobAd.Description.Content += jobAd.Description.Content;

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords("mutley");
            var highlighter = _highlighterFactory.Create(JobAdHighlighterKind.Full, criteria, new HighlighterConfiguration { FragmentSize = 40 });

            var highlightedText = highlighter.SummarizeContent(jobAd);

            Assert.AreEqual("<span class=\"highlighted-word\">Mutley</span>, you snickering, floppy eared ...  never around.<span class=\"highlighted-word\">Mutley</span>, you snickering, floppy ...  is needed, you're never around.<span class=\"highlighted-word\">Mutley</span>, you snickering", highlightedText);
        }
    }
}