using LinkMe.Domain.Criterias;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;

namespace LinkMe.Query.Search.JobAds
{
    public class JobAdSearchCriteriaPersister
        : SearchPersister, ICriteriaPersister
    {
        public JobAdSearchCriteriaPersister(ILocationQuery locationQuery, IIndustriesQuery industriesQuery)
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
            var jobAdSearchCriteria = criteria as JobAdSearchCriteria;
            if (jobAdSearchCriteria != null)
            {
                var expression = SearchCriteria.CombineKeywords(true, jobAdSearchCriteria.AllKeywords, jobAdSearchCriteria.ExactPhrase, jobAdSearchCriteria.AnyKeywords, jobAdSearchCriteria.WithoutKeywords);
                var keywords = expression == null ? null : expression.GetUserExpression();
                jobAdSearchCriteria.Keywords = keywords;
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
            var jobAdSearchCriteria = criteria as JobAdSearchCriteria;
            if (jobAdSearchCriteria != null)
            {
                // This causes a new parsing of the expression.

                jobAdSearchCriteria.AdTitle = jobAdSearchCriteria.AdTitle;

                OnLocationLoaded(jobAdSearchCriteria);
                OnIndustriesLoaded(jobAdSearchCriteria);

                var keywords = jobAdSearchCriteria.Keywords;
                jobAdSearchCriteria.SetKeywords(keywords);

                // This causes a new parsing of the expression.

                jobAdSearchCriteria.AdvertiserName = jobAdSearchCriteria.AdvertiserName;
            }
        }

        private static Criteria CreateCriteria()
        {
            return new JobAdSearchCriteria();
        }
    }
}
