using System;
using System.Text;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Views
{
    [TestClass]
    public abstract class ViewsTests
        : WebTestClass
    {
        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        protected ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        protected ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        protected IResumesCommand _resumesCommand = Resolve<IResumesCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();

        private ReadOnlyUrl _baseCandidateUrl;
        private ReadOnlyUrl _candidatesUrl;
        private ReadOnlyUrl _candidatePartialUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _baseCandidateUrl = new ReadOnlyApplicationUrl(true, "~/candidates/");
            _candidatesUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates");
            _candidatePartialUrl = new ReadOnlyApplicationUrl("~/employers/candidates/partial");
        }

        protected void TestCandidateUrls(IMember member, Action assert)
        {
            var candidateUrl = GetCandidateUrl(member, _candidatesCommand.GetCandidate(member.Id));

            Get(candidateUrl);
            AssertUrl(candidateUrl);
            assert();

            Get(GetCandidatesUrl(member.Id));
            AssertUrl(candidateUrl);
            assert();
        }

        protected ReadOnlyUrl GetCandidatesUrl(Guid memberId)
        {
            var candidateUrl = _candidatesUrl.AsNonReadOnly();
            candidateUrl.QueryString["candidateId"] = memberId.ToString();
            return candidateUrl;
        }

        protected ReadOnlyUrl GetCandidateUrl(IMember member, ICandidate candidate)
        {
            return GetCandidateUrl(member.Id, member.VisibilitySettings.Professional.EmploymentVisibility, member.Address.Location, candidate.DesiredSalary, candidate.DesiredJobTitle);
        }

        protected ReadOnlyUrl GetCandidateUrl(Guid candidateId, ProfessionalVisibility visibility, LocationReference location, Salary desiredSalary, string desiredJobTitle)
        {
            var sb = new StringBuilder();

            // Location.

            if (!string.IsNullOrEmpty(location.ToString()))
            {
                sb.Append(TextUtil.StripExtraWhiteSpace(TextUtil.StripToAlphaNumericAndWhiteSpace(location.ToString())).ToLower().Replace(' ', '-'));
            }
            else
            {
                if (location.IsCountry)
                    sb.Append(location.Country.ToString().ToLower());
                else
                    sb.Append("-");
            }

            // Salary.

            sb.Append("/");
            if (desiredSalary == null || !visibility.IsFlagSet(ProfessionalVisibility.Salary))
            {
                sb.Append("-");
            }
            else
            {
                desiredSalary = desiredSalary.ToRate(SalaryRate.Year);
                if (desiredSalary.LowerBound != null)
                {
                    if (desiredSalary.UpperBound != null)
                        sb.Append((int) (desiredSalary.LowerBound)/1000).Append("k-").Append((int) (desiredSalary.UpperBound)/1000).Append("k");
                    else
                        sb.Append((int)(desiredSalary.LowerBound) / 1000).Append("k-and-above");
                }
                else
                {
                    sb.Append("up-to-").Append((int)(desiredSalary.UpperBound) / 1000).Append("k");
                }
            }

            // Job title.

            sb.Append("/");
            if (string.IsNullOrEmpty(desiredJobTitle))
                sb.Append("-");
            else
                sb.Append(TextUtil.StripExtraWhiteSpace(TextUtil.StripToAlphaNumericAndSpace(desiredJobTitle)).ToLower().Replace(' ', '-'));

            // Id

            sb.Append("/");
            sb.Append(candidateId.ToString());

            return new ReadOnlyApplicationUrl(_baseCandidateUrl, sb.ToString());
        }

        protected ReadOnlyUrl GetCandidatePartialUrl(Guid memberId)
        {
            var candidatePartialUrl = _candidatePartialUrl.AsNonReadOnly();
            candidatePartialUrl.QueryString["candidateId"] = memberId.ToString();
            return candidatePartialUrl;
        }

        protected Member CreateMember(int index)
        {
            var member = _memberAccountsCommand.CreateTestMember(index);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            return member;
        }

        protected Employer CreateEmployer(int index)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id });
            return employer;
        }

        protected void AssertCandidate(Guid memberId)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@data-memberid='" + memberId + "']");
            Assert.IsNotNull(node);
        }
    }
}
