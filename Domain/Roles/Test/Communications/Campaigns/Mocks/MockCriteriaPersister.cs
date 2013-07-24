using LinkMe.Domain.Criterias;
using LinkMe.Domain.Roles.Communications.Campaigns;

namespace LinkMe.Domain.Roles.Test.Communications.Campaigns.Mocks
{
    public class MockCriteriaPersister
        : ICriteriaPersister
    {
        string ICriteriaPersister.GetCriteriaType(Criteria criteria)
        {
            return criteria is MockMemberCriteria
                ? CampaignCategory.Member.ToString()
                : CampaignCategory.Employer.ToString();
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
        }

        void ICriteriaPersister.OnSaved(Criteria criteria)
        {
        }

        void ICriteriaPersister.OnLoading(Criteria criteria)
        {
        }

        void ICriteriaPersister.OnLoaded(Criteria criteria)
        {
        }

        private static Criteria CreateCriteria(string type)
        {
            return type == CampaignCategory.Member.ToString()
                ? (Criteria) new MockMemberCriteria()
                : new MockEmployerCriteria();
        }
    }
}