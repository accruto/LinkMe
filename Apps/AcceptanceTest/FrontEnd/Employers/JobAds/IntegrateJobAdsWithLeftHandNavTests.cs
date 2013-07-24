using System;
using System.Collections.Generic;
using LinkMe.AcceptanceTest.Employers.Search;
using LinkMe.AcceptanceTest.FrontEnd.Extension;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;

namespace LinkMe.AcceptanceTest.FrontEnd.Employers.JobAds
{
    [TestClass]
    [Ignore]
    public class IntegrateJobAdsWithLeftHandNavTests : SearchTests
    {
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        [STAThread]
        public void TestNoJobAds()
        {
            var emp = CreateUnlimitEmployer();
            var members = CreateMembers(10);
            using(var browser = new IE())
            {
                browser.Setup();
                browser.GoToEmployerHomePage();
                browser.LogIn();
                browser.PerformSearch("linkme");
                Assert.AreEqual(browser.Div(Find.ByClass("jobads_ascx", false)).Style.Display, "none");
            }
        }

        [TestMethod]
        public void TestOnlyOneOpenJobAds()
        {
        }

        [TestMethod]
        public void TestOnlyOneClosedJobAds()
        {
            
        }

        [TestMethod]
        public void TestMoreThanFiveJobAds()
        {
        }

        [TestMethod]
        public void TestDropCandidateToShortlist()
        {
            
        }

        private IEmployer CreateUnlimitEmployer()
        {
            var employer = CreateEmployer();
            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, InitialQuantity = null, OwnerId = employer.Id });
            var jobAdCredit = _creditsQuery.GetCredit<JobAdCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = jobAdCredit.Id, InitialQuantity = null, OwnerId = employer.Id });
            var applicantCredit = _creditsQuery.GetCredit<ApplicantCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = applicantCredit.Id, InitialQuantity = null, OwnerId = employer.Id });

            return employer;
        }

        private IList<IMember> CreateMembers(int count)
        {
            var al = new List<IMember>();
            for (var index = 0; index < count; ++index)
            {
                var member = _memberAccountsCommand.CreateTestMember(index);
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                _candidateResumesCommand.AddTestResume(candidate);
                al.Add(member);
            }
            return al;
        }
    }
}