using System;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Api.Areas.Employers.Models.Candidates;
using LinkMe.Apps.Presentation.Domain.Users.Members;
using LinkMe.Domain;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Candidates
{
    [TestClass]
    public abstract class CandidateTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly IMemberSearchService _memberSearchService = Resolve<IMemberSearchService>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();
        private readonly IMemberStatusQuery _memberStatusQuery = Resolve<IMemberStatusQuery>();

        protected readonly ChannelApp _app = new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() };
        private ReadOnlyUrl _baseCandidateUrl;

        [TestInitialize]
        public void CandidateTestsInitialize()
        {
            _baseCandidateUrl = new ReadOnlyApplicationUrl(true, "~/v1/employers/candidates/");
        }

        protected ReadOnlyUrl GetCandidateUrl(Guid candidateId)
        {
            return new ReadOnlyApplicationUrl(_baseCandidateUrl, candidateId.ToString());
        }

        protected CandidateResponseModel Candidate(Guid candidateId)
        {
            return Deserialize<CandidateResponseModel>(Get(GetCandidateUrl(candidateId)), new CandidateModelJavaScriptConverter());
        }

        protected Member CreateMember(int index)
        {
            var member = _memberAccountsCommand.CreateTestMember(index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _memberSearchService.UpdateMember(member.Id);
            return member;
        }

        protected EmployerMemberView GetView(Employer employer, Member member)
        {
            return _employerMemberViewsQuery.GetEmployerMemberView(employer, member.Id);
        }

        protected Employer CreateEmployer(int index)
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        protected void AssertCandidate(CandidateResponseModel model, EmployerMemberView member)
        {
            AssertJsonSuccess(model);
            AssertCandidate(model.Candidate, member);
        }

        private void AssertCandidate(CandidateModel model, EmployerMemberView view)
        {
            Assert.AreEqual(view.Id, model.Id);
            Assert.AreEqual(view.GetCandidateTitle(), model.FullName);
            Assert.AreEqual(view.Status, model.Status);
            Assert.AreEqual(_memberStatusQuery.GetLastUpdatedTime(view, view, view.Resume).ToUniversalTime(), model.LastUpdatedTime);
            Assert.AreEqual(view.Address.Location.ToString(), model.Location);

            var salary = view.DesiredSalary == null ? null : view.DesiredSalary.ToRate(SalaryRate.Year);
            Assert.AreEqual(salary == null ? null : salary.LowerBound, model.DesiredSalary == null ? null : model.DesiredSalary.LowerBound);
            Assert.AreEqual(salary == null ? null : salary.UpperBound, model.DesiredSalary == null ? null : model.DesiredSalary.UpperBound);

            Assert.AreEqual(view.DesiredJobTitle, model.DesiredJobTitle);
            Assert.AreEqual(view.DesiredJobTypes, model.DesiredJobTypes);

            Assert.AreEqual(view.Resume == null ? null : view.Resume.Summary, model.Summary);

            if (view.Resume == null || view.Resume.Jobs == null || view.Resume.Jobs.Count == 0)
            {
                Assert.IsNull(model.Jobs);
            }
            else
            {
                for (var index = 0; index < view.Resume.Jobs.Count; ++index)
                    AssertJob(view.Resume.Jobs[index], model.Jobs[index]);
            }
        }

        private static void AssertJob(IJob job, JobModel model)
        {
            Assert.AreEqual(job.Title, model.Title);
            Assert.AreEqual(job.IsCurrent, model.IsCurrent);
            Assert.AreEqual(job.Dates == null ? null : job.Dates.Start, model.StartDate);
            Assert.AreEqual(job.Dates == null ? null : job.Dates.End, model.EndDate);
            Assert.AreEqual(job.Company, model.Company);
        }

        protected void AssertCandidates(CandidatesResponseModel model, params EmployerMemberView[] members)
        {
            AssertJsonSuccess(model);
            Assert.AreEqual(members.Length, model.TotalCandidates);
            Assert.AreEqual(members.Length, model.Candidates.Count);
            foreach (var member in members)
            {
                var memberId = member.Id;
                var candidateModel = (from c in model.Candidates where c.Id == memberId select c).SingleOrDefault();
                Assert.IsNotNull(candidateModel);
                AssertCandidate(candidateModel, member);
            }
        }
    }
}
