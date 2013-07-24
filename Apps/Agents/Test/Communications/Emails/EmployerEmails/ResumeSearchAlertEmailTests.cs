using System;
using System.Linq;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Presentation;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.EmployerEmails
{
    [TestClass]
    public class ResumeSearchAlertEmailTests
        : EmployerMemberViewEmailTests
    {
        private static readonly TimeSpan SearchResultFreshDays = new TimeSpan(28, 0, 0, 0);
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();

        private const string EmailBase = "linkme@test.linkme.net.au";

        private const string JobTitle = "Business Analyst";
        private const string LongJobTitle = "Temporary part-time libraries North-West inter-library loan business unit administration assistant";
        private const string LongEmployer = "Ziffren, Brittenham, Branca, Fischer, Gilbert-Lurie, Stiffelman, Cook, Johnson, Lande & Wolf";
        private LocationReference _location;
        private const string LongLocation = "Llanfairpwllgwyngyllgogerychwyrndrobwllllantysiliogogogoch";
        private const string SingleKeyword = "Developer";
        private const string OrKeywords = "Developer or Architect";
        private const string AndKeywords = "Developer Architect";
        private const string ComplexKeywords = "Developer or (Technical Architect)";

        private const int MaximumResults = 20;
        private const int MaximumSubjectLength = 300;

        [TestInitialize]
        public void TestInitialize()
        {
            _location = new LocationReference(_locationQuery.GetPostalSuburb(_locationQuery.GetPostalCode(_australia, "3000"), "Melbourne"));
        }

        public override TemplateEmail GeneratePreview(Community community)
        {
            if (community != null)
                return null;

            // Create an employer and search.

            var employer = CreateEmployer(0, true);
            var criteria = new MemberSearchCriteria
            {
                JobTitle = JobTitle,
                Location = _location,
                Salary = new Salary { LowerBound = 100000, UpperBound = 200000, Rate = SalaryRate.Year, Currency = Currency.AUD },
                JobTypes = JobTypes.Contract,
            };
            criteria.SetKeywords(SingleKeyword);
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // Create some results.

            var results = CreateResults(1, false, false, false);

            // Send.

            return new ResumeSearchAlertEmail(employer, criteria, null, results, _employerMemberViewsQuery.GetEmployerMemberViews(employer, results.MemberIds), search.Id);
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Create an employer and search.

            var employer = CreateEmployer(0, true);
            var criteria = new MemberSearchCriteria
            {
                JobTitle = JobTitle,
                Location = _location,
                Salary = new Salary { LowerBound = 100000, UpperBound = 200000, Rate = SalaryRate.Year, Currency = Currency.AUD },
                JobTypes = JobTypes.Contract,
            };
            criteria.SetKeywords(SingleKeyword);
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // Create some results.

            var results = CreateResults(1, false, false, false);

            // Send.

            var email = SendAlertEmail(employer, search, results);

            // Check.

            AssertMail(email, employer, search, results, 0, 0, false);
        }

        [TestMethod]
        public void TestDisabled()
        {
            // Create an employer and search.

            var employer = CreateEmployer(0, true);
            var criteria = new MemberSearchCriteria
            {
                JobTitle = JobTitle,
                Location = _location,
                Salary = new Salary { LowerBound = 100000, UpperBound = 200000, Rate = SalaryRate.Year, Currency = Currency.AUD },
                JobTypes = JobTypes.Contract,
            };
            criteria.SetKeywords(SingleKeyword);
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // Create some results.

            var results = CreateResults(1, false, false, false);

            // Disable.

            employer.IsEnabled = false;

            // Send.

            SendAlertEmail(employer, search, results);

            // Check.

            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestDeactivated()
        {
            // Create an employer and search.

            var employer = CreateEmployer(0, true);
            var criteria = new MemberSearchCriteria
            {
                JobTitle = JobTitle,
                Location = _location,
                Salary = new Salary { LowerBound = 100000, UpperBound = 200000, Rate = SalaryRate.Year, Currency = Currency.AUD },
                JobTypes = JobTypes.Contract,
            };
            criteria.SetKeywords(SingleKeyword);
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // Create some results.

            var results = CreateResults(1, false, false, false);

            // Deactivate.

            employer.IsActivated = false;

            // Send.

            var email = SendAlertEmail(employer, search, results);

            // Check - no difference.

            AssertMail(email, employer, search, results, 0, 0, false);
        }

        [TestMethod]
        public void TestTruncation()
        {
            // Create an employer and search.

            var employer = CreateEmployer(0, true);
            var criteria = new MemberSearchCriteria
            {
                JobTitle = JobTitle,
                Location = _location,
                Salary = new Salary { LowerBound = 100000, UpperBound = 200000, Rate = SalaryRate.Year, Currency = Currency.AUD },
                JobTypes = JobTypes.Contract,
            };
            criteria.SetKeywords(SingleKeyword);
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // Create some results.

            var results = CreateResults(1, true, false, false);

            // Send.

            var email = SendAlertEmail(employer, search, results);

            // Check.

            AssertMail(email, employer, search, results, 0, 0, false);
        }

        [TestMethod]
        public void TestEmployerAccessResume()
        {
            // Create an employer and search.

            var employer = CreateEmployer(0, false);
            var criteria = new MemberSearchCriteria
            {
                JobTitle = JobTitle,
                Location = _location,
                Salary = new Salary { LowerBound = 100000, UpperBound = 200000, Rate = SalaryRate.Year, Currency = Currency.AUD },
                JobTypes = JobTypes.Contract,
            };
            criteria.SetKeywords(SingleKeyword);
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // Create some results with resume hidden.

            var results = CreateResults(1, false, true, false);
            var email = SendAlertEmail(employer, search, results);
            AssertMail(email, employer, search, results, 0, 0, false);
        }

        [TestMethod]
        public void TestEmployerAccessRecentEmployers()
        {
            // Create an employer and search.

            var employer = CreateEmployer(0, false);
            var criteria = new MemberSearchCriteria
            {
                JobTitle = JobTitle,
                Location = _location,
                Salary = new Salary { LowerBound = 100000, UpperBound = 200000, Rate = SalaryRate.Year, Currency = Currency.AUD },
                JobTypes = JobTypes.Contract,
            };
            criteria.SetKeywords(SingleKeyword);
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // Create some results with employers hidden.

            var results = CreateResults(1, false, false, true);
            var email = SendAlertEmail(employer, search, results);
            AssertMail(email, employer, search, results, 0, 0, false);
        }

        [TestMethod]
        public void TestSubject()
        {
            // Create some results.

            var results = CreateResults(1, false, false, false);

            // Nothing.

            var index = 0;
            AssertSubject(++index, results, null, null, null, null, JobTypes.None, MaximumSubjectLength);

            // Job title.

            AssertSubject(++index, results, JobTitle, null, null, null, JobTypes.None, MaximumSubjectLength);

            // Keywords

            AssertSubject(++index, results, null, SingleKeyword, null, null, JobTypes.None, MaximumSubjectLength);
            AssertSubject(++index, results, null, OrKeywords, null, null, JobTypes.None, MaximumSubjectLength);
            AssertSubject(++index, results, null, AndKeywords, null, null, JobTypes.None, MaximumSubjectLength);
            AssertSubject(++index, results, null, ComplexKeywords, null, null, JobTypes.None, MaximumSubjectLength);

            // Location

            AssertSubject(++index, results, null, null, _location, null, JobTypes.None, MaximumSubjectLength);

            // Salary

            AssertSubject(++index, results, null, null, null, new Salary(), JobTypes.None, MaximumSubjectLength);
            AssertSubject(++index, results, null, null, null, new Salary { LowerBound = 50000, UpperBound = null, Rate = SalaryRate.Year, Currency = Currency.AUD }, JobTypes.None, MaximumSubjectLength);
            AssertSubject(++index, results, null, null, null, new Salary { LowerBound = null, UpperBound = 50000, Rate = SalaryRate.Year, Currency = Currency.AUD }, JobTypes.None, MaximumSubjectLength);
            AssertSubject(++index, results, null, null, null, new Salary { LowerBound = 50000, UpperBound = 100000, Rate = SalaryRate.Year, Currency = Currency.AUD }, JobTypes.None, MaximumSubjectLength);
            AssertSubject(++index, results, null, null, null, new Salary { LowerBound = 50000, UpperBound = 100000, Rate = SalaryRate.Month, Currency = Currency.AUD }, JobTypes.None, MaximumSubjectLength);

            // Job type.

            AssertSubject(++index, results, null, null, null, null, JobTypes.Contract, MaximumSubjectLength);
            AssertSubject(++index, results, null, null, null, null, JobTypes.FullTime, MaximumSubjectLength);
            AssertSubject(++index, results, null, null, null, null, JobTypes.Contract | JobTypes.FullTime, MaximumSubjectLength);
            AssertSubject(++index, results, null, null, null, null, JobTypes.All, MaximumSubjectLength);

            // Combinations.

            AssertSubject(++index, results, JobTitle, null, null, null, JobTypes.Contract, MaximumSubjectLength);
            AssertSubject(++index, results, null, SingleKeyword, null, null, JobTypes.Contract, MaximumSubjectLength);
            AssertSubject(++index, results, null, SingleKeyword, _location, null, JobTypes.Contract, MaximumSubjectLength);
            AssertSubject(++index, results, JobTitle, SingleKeyword, _location, null, JobTypes.Contract, MaximumSubjectLength);
            AssertSubject(++index, results, JobTitle, SingleKeyword, _location, new Salary { LowerBound = 50000, UpperBound = 100000, Rate = SalaryRate.Year, Currency = Currency.AUD }, JobTypes.Contract, MaximumSubjectLength);
        }

        [TestMethod]
        public void TestNoCriteria()
        {
            // Create an employer and search.

            var employer = CreateEmployer(0, false);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id});

            var criteria = new MemberSearchCriteria();
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // Some results.

            var results = CreateResults(1, false, false, false);
            var email = SendAlertEmail(employer, search, results);
            AssertMail(email, employer, search, results, 0, 0, false);

            // No results.

            results = CreateResults(0, false, false, false);
            email = SendAlertEmail(employer, search, results);
            AssertMail(email, employer, search, results, 0, 0, false);
        }

        [TestMethod]
        public void TestNoResults()
        {
            // Create an employer and search.

            var employer = CreateEmployer(0, false);
            var criteria = new MemberSearchCriteria { JobTitle = JobTitle };
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // No results.

            var results = CreateResults(0, false, false, false);

            // Send.

            var email = SendAlertEmail(employer, search, results);

            // Check.

            AssertMail(email, employer, search, results, 0, 0, false);
        }

        [TestMethod]
        public void TestResultsNoCurrentJob()
        {
            // Create an employer and search.

            var employer = CreateEmployer(0, false);
            var criteria = new MemberSearchCriteria { JobTitle = JobTitle };
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // No current job.

            var member = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            var resume = _candidateResumesCommand.AddTestResume(candidate);
            resume.Jobs = null;
            _candidateResumesCommand.UpdateResume(candidate, resume);
            var results = new MemberSearchResults { MemberIds = new [] { member.Id }, TotalMatches = 1 };

            // Send.

            var email = SendAlertEmail(employer, search, results);

            // Check.

            AssertMail(email, employer, search, results, 0, 0, false);
        }

        [TestMethod]
        public void TestResultsNoLocation()
        {
            // Create an employer and search.

            var employer = CreateEmployer(0, false);
            var criteria = new MemberSearchCriteria { JobTitle = JobTitle };
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // No location.

            var member = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate); 
            _locationQuery.ResolvePostalSuburb(member.Address.Location, _australia, "");
            var results = new MemberSearchResults { MemberIds = new[] { member.Id }, TotalMatches = 1 };

            // Send.

            SendAlertEmail(employer, search, results);
        }

        [TestMethod]
        public void TestResultsNotNew()
        {
            // Create an employer and search.

            var employer = CreateEmployer(0, true);
            var criteria = new MemberSearchCriteria { JobTitle = JobTitle };
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // Set created time back beyond the fresh days interval.

            var member = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            member.CreatedTime -= SearchResultFreshDays.Add(SearchResultFreshDays);
            var results = new MemberSearchResults { MemberIds = new[] { member.Id }, TotalMatches = 1 };

            // Send.

            var email = SendAlertEmail(employer, search, results);

            // Check.

            AssertMail(email, employer, search, results, 0, 0, false);
        }

        [TestMethod]
        public void TestResultsNoPreviousJob()
        {
            // Create an employer and search.

            var employer = CreateEmployer(0, true);
            var criteria = new MemberSearchCriteria { JobTitle = JobTitle };
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // Remove all jobs except the first, which is the current job.

            var member = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            var resume = _candidateResumesCommand.AddTestResume(candidate);
            while (resume.Jobs.Count > 1)
                resume.Jobs.RemoveAt(1);
            var results = new MemberSearchResults { MemberIds = new[] { member.Id }, TotalMatches = 1 };

            // Send.

            var email = SendAlertEmail(employer, search, results);

            // Check.

            AssertMail(email, employer, search, results, 0, 0, false);
        }

        [TestMethod]
        public void TestResultsNoCurrentEmployer()
        {
            // Create an employer and search.

            var employer = CreateEmployer(0, true);
            var criteria = new MemberSearchCriteria { JobTitle = JobTitle };
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // Remove the employer.

            var member = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            var resume = _candidateResumesCommand.AddTestResume(candidate);
            resume.Jobs[0].Company = "";
            var results = new MemberSearchResults { MemberIds = new[] { member.Id }, TotalMatches = 1 };

            // Send.

            var email = SendAlertEmail(employer, search, results);

            // Check.

            AssertMail(email, employer, search, results, 0, 0, false);
        }

        [TestMethod]
        public void TestResultsNoPreviousEmployer()
        {
            // Create an employer and search.

            var employer = CreateEmployer(0, true);
            var criteria = new MemberSearchCriteria { JobTitle = JobTitle };
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // Remove the employer.

            var member = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            var resume = _candidateResumesCommand.AddTestResume(candidate);
            resume.Jobs[1].Company = "";
            var results = new MemberSearchResults { MemberIds = new[] { member.Id }, TotalMatches = 1 };

            // Send.

            var email = SendAlertEmail(employer, search, results);

            // Check.

            AssertMail(email, employer, search, results, 0, 0, false);
        }

        [TestMethod]
        public void TestResultsSalary()
        {
            // Create an employer and search.

            var employer = CreateEmployer(0, true);
            var criteria = new MemberSearchCriteria { JobTitle = JobTitle };
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // Lower and upper bound.

            var member = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            candidate.DesiredSalary = new Salary {LowerBound = 100000, UpperBound = 200000, Rate = SalaryRate.Year, Currency = Currency.AUD};
            var results = new MemberSearchResults { MemberIds = new[] { member.Id }, TotalMatches = 1 };

            // Send.

            var email = SendAlertEmail(employer, search, results);

            // Check.

            AssertMail(email, employer, search, results, 0, 0, false);

            // Lower bound.

            candidate.DesiredSalary = new Salary { LowerBound = 100000, Rate = SalaryRate.Year, Currency = Currency.AUD };
            _candidatesCommand.UpdateCandidate(candidate);
            email = SendAlertEmail(employer, search, results);
            AssertMail(email, employer, search, results, 0, 0, false);

            // Upper bound.

            candidate.DesiredSalary = new Salary { UpperBound = 200000, Rate = SalaryRate.Year, Currency = Currency.AUD };
            _candidatesCommand.UpdateCandidate(candidate);
            email = SendAlertEmail(employer, search, results);
            AssertMail(email, employer, search, results, 0, 0, false);

            // Both.

            candidate.DesiredSalary = new Salary { LowerBound = 100000, UpperBound = 200000, Rate = SalaryRate.Year, Currency = Currency.AUD };
            _candidatesCommand.UpdateCandidate(candidate);
            email = SendAlertEmail(employer, search, results);
            AssertMail(email, employer, search, results, 0, 0, false);
        }

        [TestMethod]
        public void TestResults()
        {
            TestResults(2, 2, 0);
        }

        [TestMethod]
        public void TestResultsLessMax()
        {
            TestResults(MaximumResults - 10, 10, 0);
        }

        [TestMethod]
        public void TestResultsMax()
        {
            TestResults(MaximumResults, 20, 0);
        }

        [TestMethod]
        public void TestResultsMoreMax()
        {
            TestResults(MaximumResults + 10, 20, 10);
        }

        [TestMethod]
        public void TestResultsZero()
        {
            TestResults(2, 2, 0);
        }

        [TestMethod]
        public void TestDesiredSalary()
        {
            var employer = CreateEmployer(0, true);
            var criteria = new MemberSearchCriteria { JobTitle = JobTitle };
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // No location.

            var member = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            candidate.DesiredSalary = new Salary {LowerBound = 20000, UpperBound = 40000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            _candidateResumesCommand.AddTestResume(candidate);
            var results = new MemberSearchResults { MemberIds = new[] { member.Id }, TotalMatches = 1 };

            // Send.

            var email = SendAlertEmail(employer, search, results);

            // Check.

            AssertMail(email, employer, search, results, 0, 0, false);
        }

        [TestMethod]
        public void TestCriteriaJobTypes()
        {
            var employer = CreateEmployer(0, true);
            var criteria = new MemberSearchCriteria { JobTitle = JobTitle, JobTypes = JobTypes.FullTime | JobTypes.Contract };
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // No location.

            var member = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            var results = new MemberSearchResults { MemberIds = new[] { member.Id }, TotalMatches = 1 };

            // Send.

            var email = SendAlertEmail(employer, search, results);

            // Check.

            AssertMail(email, employer, search, results, 0, 0, false);
        }

        [TestMethod]
        public void TestCriteriaEthnicStatus()
        {
            var employer = CreateEmployer(0, true);
            var criteria = new MemberSearchCriteria { JobTitle = JobTitle, EthnicStatus = EthnicStatus.Aboriginal | EthnicStatus.TorresIslander };
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // No location.

            var member = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            var results = new MemberSearchResults { MemberIds = new[] { member.Id }, TotalMatches = 1 };

            // Send.

            var email = SendAlertEmail(employer, search, results);

            // Check.

            AssertMail(email, employer, search, results, 0, 0, false);
        }

        [TestMethod]
        public void TestCriteriaCandidateStatus()
        {
            var employer = CreateEmployer(0, true);
            var criteria = new MemberSearchCriteria { JobTitle = JobTitle, CandidateStatusFlags = CandidateStatusFlags.ActivelyLooking | CandidateStatusFlags.AvailableNow };
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // No location.

            var member = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            var results = new MemberSearchResults { MemberIds = new[] { member.Id }, TotalMatches = 1 };

            // Send.

            var email = SendAlertEmail(employer, search, results);

            // Check.

            AssertMail(email, employer, search, results, 0, 0, false);
        }

        [TestMethod]
        public void TestUpdatedNewGrouping()
        {
            var employer = CreateEmployer(0, true);
            var criteria = new MemberSearchCriteria { JobTitle = JobTitle, CandidateStatusFlags = CandidateStatusFlags.ActivelyLooking | CandidateStatusFlags.AvailableNow };
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // No location.

            var newMember1 = CreateMember(0);
            var candidate = _candidatesCommand.GetCandidate(newMember1.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            var updatedMember1 = CreateMember(1, DateTime.Now.AddDays(-100));
            
            var newMember2 = CreateMember(2);

            var updatedMember2 = CreateMember(3, DateTime.Now.AddDays(-100));
            candidate = _candidatesCommand.GetCandidate(updatedMember2.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            //ordering is important - should be new, new, updated, updated
            var results = new MemberSearchResults { MemberIds = new[] { newMember1.Id, newMember2.Id, updatedMember1.Id, updatedMember2.Id }, TotalMatches = 4 };

            // Send.

            var email = SendAlertEmail(employer, search, results);

            // Check.

            AssertMail(email, employer, search, results, 2, 2, true);
        }

        private void TestResults(int strong, int newCount, int updatedCount)
        {
            // Create an employer and search.

            var employer = CreateEmployer(0, true);
            var criteria = new MemberSearchCriteria { JobTitle = JobTitle };
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // Create results.

            var results = CreateResults(strong, false, false, false);

            // Send.

            var email = SendAlertEmail(employer, search, results);

            // Check.

            AssertMail(email, employer, search, results, newCount, updatedCount, false);
        }

        private Employer CreateEmployer(int index, bool grantUnlimitedCredits)
        {
            var employer = CreateEmployer(index);
            if (grantUnlimitedCredits)
                _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            return employer;
        }

        private void AssertMail(TemplateEmail templateEmail, Employer employer, MemberSearch search, MemberSearchResults results, int newCount, int updatedCount, bool useExtraReturn)
        {
            var email = _emailServer.AssertEmailSent();

            // Asert addresses.

            email.AssertAddresses(Return, Return, employer);
            AssertCompatibleAddresses(email);

            // Assert subject.

            email.AssertSubject(GetSubject(search.Criteria, MaximumSubjectLength));

            // Assert body.

            var longLines = new[]
                                {
                                    "<strong>1 candidate</strong> was found for: Business Analyst, Developer, Melbourne VIC 3000, $100,000 to $200,000, Contract",
                                    "<strong>0 candidates (0 new and 0 updated)</strong> were found for: (no criteria)",
                                    "<strong>0 candidates (0 new and 0 updated)</strong> were found for: Business Analyst",
                                    "<strong>2 candidates (2 new and 0 updated)</strong> were found for: Business Analyst",
                                    "<strong>10 candidates (10 new and 0 updated)</strong> were found for: Business Analyst",
                                    "<strong>20 candidates (20 new and 0 updated)</strong> were found for: Business Analyst",
                                    "<strong>20 candidates (20 new and 10 updated)</strong> were found for: Business Analyst",
                                    "<strong>30 candidates (20 new and 10 updated)</strong> were found for: Business Analyst",
                                    "Temporary part-time libraries North-W..., Llanfairpwllgwyngyllgogerychwyrndrobw...",
                                    "Temporary part-time libraries North-W..., Ziffren, Brittenham, Branca, Fischer,...",
                                };

            email.AssertHtmlViewChecks(null, true, longLines);
            var expected = Math.Min(results.TotalMatches, MaximumResults);
            int written;

            email.AssertHtmlView(GetBody(templateEmail, employer, GetContent(templateEmail, employer, search, results, newCount, updatedCount, useExtraReturn, out written)));

            Assert.AreEqual(expected, written);

            // Assert attachments.

            email.AssertNoAttachments();
        }

        private static string GetSubject(MemberSearchCriteria criteria, int maximumLength)
        {
            var criteriaText = GetCriteriaText(criteria);
            var subject = HtmlUtil.StripHtmlTags("Candidate alert" + (criteriaText.Length == 0 ? string.Empty : ": " + criteriaText));

            // Truncate if exceeds the limit.

            return TextUtil.TruncateForDisplay(subject, maximumLength);
        }

        private string GetContent(TemplateEmail templateEmail, IEmployer employer, MemberSearch search, MemberSearchResults results, int newCount, int updatedCount, bool useExtraReturn, out int written)
        {
            written = 0;

            var sb = new StringBuilder();
            sb.AppendLine("<p>");
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("  <strong>" + results.TotalMatches + " candidate");

            if (results.TotalMatches != 1)
            {
                sb.Append("s");
                sb.AppendFormat(" ({0} new and {1} updated)", newCount, updatedCount);
            }

            sb.Append("</strong>");
            sb.Append(results.TotalMatches != 1 ? " were" : " was");
            sb.Append(" found");

            var criteriaText = GetCriteriaText(search.Criteria);
            if (string.IsNullOrEmpty(criteriaText))
                sb.AppendLine();
            else
                sb.AppendLine(" for: " + criteriaText);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("</p>");
            sb.AppendLine();

            // Results.

            var header = GetHeader(results);
            if (!string.IsNullOrEmpty(header))
            {
                sb.AppendLine("<p>");
                sb.AppendLine("  " + header);
                sb.AppendLine("</p>");
                sb.AppendLine("<p>");
                sb.Append(GetResults(templateEmail, employer, results, useExtraReturn, ref written));
                sb.AppendLine("</p>");
                sb.AppendLine();
            }

            var tinyUrl = GetTinyUrl(templateEmail, true, "~/employers/login", "returnUrl", new ReadOnlyApplicationUrl("~/ui/registered/employers/SavedResumeSearchAlerts.aspx").PathAndQuery);

            sb.AppendLine("<p>");
            sb.AppendLine("  Edit your daily candidate alerts");
            sb.AppendLine("  <a href=\"" + tinyUrl + "\">here.</a>");
            sb.AppendLine("</p>");
            sb.AppendLine("<!-- Saved resume search " + search.Id.ToString("B") + " -->");
            return sb.ToString();
        }

        private static string GetHeader(SearchResults results)
        {
            var sb = new StringBuilder();

            // Matches.

            var matches = results.TotalMatches;
            if (matches > 0)
            {
                if (matches <= MaximumResults)
                    sb.AppendFormat("{0} result{1}", matches, (matches == 1 ? "" : "s"));
                else
                    sb.AppendFormat("{0} results (first {1} shown)", matches, MaximumResults);
            }

            return sb.ToString();
        }

        private string GetResults(TemplateEmail templateEmail, IEmployer employer, MemberSearchResults results, bool useExtraReturn, ref int resultsWritten)
        {
            var sb = new StringBuilder();
            var matches = results.TotalMatches;
            if (matches > 0)
                AppendResults(sb, templateEmail, employer, results, 0, Math.Min(matches, MaximumResults), useExtraReturn, ref resultsWritten);
            return sb.ToString();
        }

        private void AssertSubject(int index, MemberSearchResults results, string jobTitle, string keywords, LocationReference location, Salary salary, JobTypes jobTypes, int maximumLength)
        {
            // Create an employer and search.

            var employer = CreateEmployer(index, false);
            var criteria = new MemberSearchCriteria
            {
                JobTitle = jobTitle,
                Location = location,
                Salary = salary,
                JobTypes = jobTypes
            };
            criteria.SetKeywords(keywords);
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            // Send an email.

            SendAlertEmail(employer, search, results);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertSubject(GetSubject(search.Criteria, maximumLength));
        }

        private TemplateEmail SendAlertEmail(Employer employer, MemberSearch search, MemberSearchResults results)
        {
            var email = new ResumeSearchAlertEmail(employer, search.Criteria, null, results, _employerMemberViewsQuery.GetEmployerMemberViews(employer, results.MemberIds), search.Id);
            _emailsCommand.TrySend(email);
            return email;
        }

        private MemberSearchResults CreateResults(int matches, bool useLongForms, bool hideResume, bool hideRecentEmployers)
        {
            // Create members.

            var memberIds = new Guid[matches];
            for (var index = 0; index < matches; ++index)
            {
                var member = _memberAccountsCommand.CreateTestMember(index + EmailBase);
                var candidate = _candidatesCommand.GetCandidate(member.Id);
                var resume = _candidateResumesCommand.AddTestResume(candidate);

                if (useLongForms)
                {
                    _locationQuery.ResolvePostalSuburb(member.Address.Location, _notAustralia, LongLocation);
                    _memberAccountsCommand.UpdateMember(member);

                    resume.Jobs[0].Title = LongJobTitle;
                    resume.Jobs[0].Company = LongEmployer;
                    resume.Jobs[1].Title = LongJobTitle;
                    resume.Jobs[1].Company = LongEmployer;
                    _candidatesCommand.UpdateCandidate(candidate);
                }

                if (hideResume)
                {
                    member.VisibilitySettings.Professional.EmploymentVisibility &= ~ProfessionalVisibility.Resume;
                    _memberAccountsCommand.UpdateMember(member);
                }
                if (hideRecentEmployers)
                {
                    member.VisibilitySettings.Professional.EmploymentVisibility &= ~ProfessionalVisibility.RecentEmployers;
                    _memberAccountsCommand.UpdateMember(member);
                }

                memberIds[index] = member.Id;
            }

            // Create the results.

            return new MemberSearchResults { MemberIds = memberIds, TotalMatches = memberIds.Length };
        }

        private static string GetCriteriaText(MemberSearchCriteria criteria)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(criteria.JobTitle))
                sb.Append(JobTitle);

            if (!string.IsNullOrEmpty(criteria.GetKeywords()))
            {
                if (sb.Length != 0)
                    sb.Append(", ");
                sb.Append(criteria.GetKeywords());
            }

            if (criteria.Location != null && !criteria.Location.IsCountry)
            {
                if (sb.Length != 0)
                    sb.Append(", ");
                sb.Append(criteria.Location);
            }

            if (criteria.Salary != null)
            {
                if (sb.Length != 0)
                    sb.Append(", ");
                sb.Append(criteria.Salary.GetDisplayText());
            }

            if (criteria.JobTypes != JobTypes.All)
            {
                if (sb.Length != 0)
                    sb.Append(", ");
                sb.Append(criteria.JobTypes.GetDisplayText(" OR ", false, false));
            }

            if (criteria.CandidateStatusFlags != null)
            {
                if (sb.Length != 0)
                    sb.Append(", ");
                sb.Append(string.Join(" OR ", criteria.CandidateStatusFlags.Value.GetDisplayTexts(CandidateStatusDisplay.Values, CandidateStatusDisplay.GetDisplayText).ToArray()));
            }

            if (criteria.EthnicStatus != null)
            {
                if (sb.Length != 0)
                    sb.Append(", ");
                sb.Append(string.Join(" OR ", criteria.EthnicStatus.Value.GetDisplayTexts(EthnicStatusDisplay.Values, EthnicStatusDisplay.GetDisplayText).ToArray()));
            }

            return sb.Length == 0 ? "(no criteria)" : sb.ToString();
        }
    }
}