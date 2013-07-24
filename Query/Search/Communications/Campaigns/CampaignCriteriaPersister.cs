using LinkMe.Domain.Criterias;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Query.Search.Employers;
using LinkMe.Query.Search.Members;

namespace LinkMe.Query.Search.Communications.Campaigns
{
    public class CampaignCriteriaPersister
        : ICriteriaPersister
    {
        private readonly ICriteriaPersister _memberPersister;
        private readonly OrganisationPersister _employerPersister;

        public CampaignCriteriaPersister(ILocationQuery locationQuery, IIndustriesQuery industriesQuery)
        {
            _memberPersister = new MemberSearchCriteriaPersister(locationQuery, industriesQuery);
            _employerPersister = new OrganisationPersister();
        }

        string ICriteriaPersister.GetCriteriaType(Criteria criteria)
        {
            if (criteria is EmployerSearchCriteria)
                return CampaignCategory.Employer.ToString();
            return CampaignCategory.Member.ToString();
        }

        Criteria ICriteriaPersister.CreateCriteria(string type)
        {
            return CreateCriteria(type);
        }

        TCriteria ICriteriaPersister.CreateCriteria<TCriteria>(string type)
        {
            return CreateCriteria(type) as TCriteria;
        }

        void ICriteriaPersister.OnSaving(Criteria criteria)
        {
            if (criteria is EmployerSearchCriteria)
                _employerPersister.OnSaving(criteria);
            else if (criteria is MemberSearchCriteria)
                _memberPersister.OnSaving(criteria);
        }

        void ICriteriaPersister.OnSaved(Criteria criteria)
        {
            if (criteria is EmployerSearchCriteria)
                _employerPersister.OnSaved(criteria);
            else if (criteria is MemberSearchCriteria)
                _memberPersister.OnSaved(criteria);
        }

        void ICriteriaPersister.OnLoading(Criteria criteria)
        {
            if (criteria is EmployerSearchCriteria)
                _employerPersister.OnLoading(criteria);
            else if (criteria is MemberSearchCriteria)
                _memberPersister.OnLoading(criteria);
        }

        void ICriteriaPersister.OnLoaded(Criteria criteria)
        {
            if (criteria is EmployerSearchCriteria)
                _employerPersister.OnLoaded(criteria);
            else if (criteria is MemberSearchCriteria)
                _memberPersister.OnLoaded(criteria);
        }

        private Criteria CreateCriteria(string type)
        {
            if (type == CampaignCategory.Employer.ToString())
                return _employerPersister.CreateCriteria();
            return _memberPersister.CreateCriteria(type);
        }
    }
}
