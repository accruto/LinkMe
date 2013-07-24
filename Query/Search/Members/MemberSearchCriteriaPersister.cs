using LinkMe.Domain.Criterias;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;

namespace LinkMe.Query.Search.Members
{
    public class MemberSearchCriteriaPersister
        : SearchPersister, ICriteriaPersister
    {
        public MemberSearchCriteriaPersister(ILocationQuery locationQuery, IIndustriesQuery industriesQuery)
            : base(locationQuery, industriesQuery)
        {
        }

        string ICriteriaPersister.GetCriteriaType(Criteria criteria)
        {
            return criteria.GetType().Name;
        }

        Criteria ICriteriaPersister.CreateCriteria(string type)
        {
            return CreateCriteria();
        }

        TCriteria ICriteriaPersister.CreateCriteria<TCriteria>(string type)
        {
            return CreateCriteria() as TCriteria;
        }

        void ICriteriaPersister.OnSaving(Criteria criteria)
        {
            var memberSearchCriteria = criteria as MemberSearchCriteria;
            if (memberSearchCriteria != null)
            {
                var expression = SearchCriteria.CombineKeywords(false, memberSearchCriteria.AllKeywords, memberSearchCriteria.ExactPhrase, memberSearchCriteria.AnyKeywords, memberSearchCriteria.WithoutKeywords);
                var keywords = expression == null ? null : expression.GetUserExpression();
                memberSearchCriteria.Keywords = keywords;
            }
        }

        void ICriteriaPersister.OnSaved(Criteria criteria)
        {
        }

        void ICriteriaPersister.OnLoading(Criteria criteria)
        {
        }

        void ICriteriaPersister.OnLoaded(Criteria criteria)
        {
            var memberSearchCriteria = criteria as MemberSearchCriteria;
            if (memberSearchCriteria != null)
            {
                memberSearchCriteria.JobTitle = memberSearchCriteria.JobTitle;
                OnLocationLoaded(memberSearchCriteria);
                OnIndustriesLoaded(memberSearchCriteria);

                var keywords = memberSearchCriteria.Keywords;
                memberSearchCriteria.SetKeywords(keywords);

                memberSearchCriteria.CompanyKeywords = memberSearchCriteria.CompanyKeywords;
                memberSearchCriteria.DesiredJobTitle = memberSearchCriteria.DesiredJobTitle;
            }
        }

        private static Criteria CreateCriteria()
        {
            return new MemberSearchCriteria();
        }
    }
}
