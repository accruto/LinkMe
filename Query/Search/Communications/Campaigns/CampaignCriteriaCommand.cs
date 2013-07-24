using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Criterias;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Campaigns.Commands;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Query.Search.Employers;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;

namespace LinkMe.Query.Search.Communications.Campaigns
{
    public class CampaignCriteriaCommand
        : ICampaignCriteriaCommand
    {
        private readonly ICampaignQueriesRepository _repository;
        private readonly IExecuteMemberSearchCommand _executeMemberSearchCommand;
        private readonly IMembersQuery _membersQuery;
        private readonly IEmployersQuery _employersQuery;
        private readonly IExecuteEmployerSearchCommand _executeEmployerSearchCommand;

        public CampaignCriteriaCommand(ICampaignQueriesRepository repository, IExecuteMemberSearchCommand executeMemberSearchCommand, IMembersQuery membersQuery, IEmployersQuery employersQuery, IExecuteEmployerSearchCommand executeEmployerSearchCommand)
        {
            _repository = repository;
            _executeMemberSearchCommand = executeMemberSearchCommand;
            _repository = repository;
            _membersQuery = membersQuery;
            _employersQuery = employersQuery;
            _executeEmployerSearchCommand = executeEmployerSearchCommand;
        }

        IList<RegisteredUser> ICampaignCriteriaCommand.Match(CampaignCategory category, Criteria criteria)
        {
            return category == CampaignCategory.Employer ? MatchEmployers(criteria) : MatchMembers(criteria);
        }

        IList<RegisteredUser> ICampaignCriteriaCommand.Match(CampaignCategory category, string query)
        {
            return category == CampaignCategory.Employer ? MatchEmployers(query) : MatchMembers(query);
        }

        private IList<RegisteredUser> MatchMembers(Criteria criteria)
        {
            var memberCriteria = criteria as MemberSearchCriteria;
            if (memberCriteria == null)
                return new List<RegisteredUser>();

            var execution = _executeMemberSearchCommand.Search(null, memberCriteria, null);
            return _membersQuery.GetMembers(from r in execution.Results.MemberIds select r).Cast<RegisteredUser>().ToList();
        }

        private IList<RegisteredUser> MatchMembers(string query)
        {
            if (string.IsNullOrEmpty(query == null ? null : query.Trim()))
                return new List<RegisteredUser>();

            var memberIds = _repository.GetUserIds(query);
            return _membersQuery.GetMembers(from r in memberIds select r).Cast<RegisteredUser>().ToList();
        }

        private IList<RegisteredUser> MatchEmployers(Criteria criteria)
        {
            var employerCriteria = criteria as OrganisationEmployerSearchCriteria;
            return employerCriteria == null
                ? new List<RegisteredUser>()
                : _executeEmployerSearchCommand.Search(employerCriteria).Cast<RegisteredUser>().ToList();
        }

        private IList<RegisteredUser> MatchEmployers(string query)
        {
            if (string.IsNullOrEmpty(query == null ? null : query.Trim()))
                return new List<RegisteredUser>();

            var employerIds = _repository.GetUserIds(query);
            return _employersQuery.GetEmployers(from r in employerIds select r).Cast<RegisteredUser>().ToList();
        }
    }
}
