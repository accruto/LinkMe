using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Query.Search.Members;
using LinkMe.Web.Areas.Employers.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Results
{
    [TestClass]
    public class TitleTests
        : SearchTests
    {
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand = Resolve<IEmployerMemberViewsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();

        private readonly ChannelApp _app = new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() };

        private const string DesiredJobTitle = "Archeologist";
        private const string Affiliations = "Dinosaurs";
        private const string CurrentJob = "Accountant";
        private const string PreviousJob = "Lion Tamer";

        [TestMethod]
        public void TestFullName()
        {
            var member = CreateMember(false, true, true);
            var employer = CreateEmployer();
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);
            LogIn(employer);

            TestTitle(member.FullName);
        }

        [TestMethod]
        public void TestHiddenName()
        {
            var member = CreateMember(true, true, true);
            var employer = CreateEmployer();
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);
            LogIn(employer);

            TestTitle(CurrentJob);
        }

        [TestMethod]
        public void TestNoCurrentJob()
        {
            var member = CreateMember(true, false, true);
            var employer = CreateEmployer();
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);
            LogIn(employer);

            TestTitle(DesiredJobTitle);
        }

        [TestMethod]
        public void TestNoDesiredJobTitle()
        {
            var member = CreateMember(true, false, false);
            var employer = CreateEmployer();
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);
            LogIn(employer);

            TestTitle("&lt;Name hidden&gt;");
        }

        private void TestTitle(string expectedTitle)
        {
            // Search.

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(Affiliations);

            // Expanded.

            Get(GetPartialSearchUrl(criteria, DetailLevel.Expanded));
            AssertTitle(expectedTitle);

            // Compact.

            Get(GetPartialSearchUrl(criteria, DetailLevel.Compact));
            AssertTitle(expectedTitle);
        }

        private void AssertTitle(string expectedTitle)
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='candidate-name']/a");
            Assert.IsNotNull(nodes);
            Assert.AreEqual(1, nodes.Count);
            Assert.AreEqual(expectedTitle, nodes[0].InnerText);
        }

        private Member CreateMember(bool hidden, bool currentJob, bool desiredJobTitle)
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            if (hidden)
            {
                member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Name);
                _memberAccountsCommand.UpdateMember(member);
            }

            var candidate = _candidatesCommand.GetCandidate(member.Id);

            if (desiredJobTitle)
            {
                candidate.DesiredJobTitle = DesiredJobTitle;
                _candidatesCommand.UpdateCandidate(candidate);
            }

            var resume = new Resume
            {
                Affiliations = Affiliations,
            };

            var now = DateTime.Now;
            if (currentJob)
            {
                resume.Jobs = new List<Job>
                {
                    new Job { Title = CurrentJob, Dates = new PartialDateRange(new PartialDate(now.AddMonths(-2))) },
                    new Job { Title = PreviousJob, Dates = new PartialDateRange(new PartialDate(now.AddYears(-2)), new PartialDate(now.AddYears(-1))) },
                };
            }
            else
            {
                resume.Jobs = new List<Job>
                {
                    new Job { Title = PreviousJob, Dates = new PartialDateRange(new PartialDate(now.AddYears(-2)), new PartialDate(now.AddYears(-1))) },
                };
            }

            _candidateResumesCommand.CreateResume(candidate, resume);
            return member;
        }

        protected override Employer CreateEmployer()
        {
            var employer = base.CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });
            return employer;
        }
    }
}
