using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Api.Areas.Employers.Models.Candidates;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers
{
    public abstract class CandidateListsTests
        : WebTestClass
    {
        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        protected readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        protected readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        protected readonly IResumesQuery _resumesQuery = Resolve<IResumesQuery>();
        protected readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        protected readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly ICandidateListsCommand _candidateListsCommand = Resolve<ICandidateListsCommand>();
        protected readonly IMemberSearchService _memberSearchService = Resolve<IMemberSearchService>();

        protected static ReadOnlyUrl GetUrl(ReadOnlyUrl baseUrl, MemberSortOrder sortOrder)
        {
            var url = baseUrl.AsNonReadOnly();
            url.QueryString.Add("SortOrder", sortOrder.ToString());
            return url;
        }

        protected static ReadOnlyUrl GetUrl(ReadOnlyUrl baseUrl, int? page, int? items)
        {
            var queryString = new QueryString();
            if (page != null)
                queryString.Add("page", page.Value.ToString());
            if (items != null)
                queryString.Add("items", items.Value.ToString());

            var url = baseUrl.AsNonReadOnly();
            url.QueryString.Add(queryString);
            return url;
        }

        protected void AssertMembers(CandidatesResponseModel model, params Member[] members)
        {
            AssertMembers(model, false, members);
        }

        protected void AssertMembers(CandidatesResponseModel model, bool assertOrder, params Member[] members)
        {
            Assert.AreEqual(members.Length, model.Candidates.Count);

            if (assertOrder)
            {
                for (var index = 0; index < members.Length; ++index)
                    Assert.AreEqual(members[index].Id, model.Candidates[index].Id);
            }
            else
            {
                foreach (var candidateModel in model.Candidates)
                {
                    var candidateId = candidateModel.Id;
                    Assert.IsTrue((from m in members where m.Id == candidateId select m).Any());
                }
            }
        }

        protected Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        protected Member CreateMember(int index)
        {
            var member = _memberAccountsCommand.CreateTestMember(index);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _memberSearchService.UpdateMember(member.Id);
            return member;
        }
    }
}