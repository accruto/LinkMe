using System;
using LinkMe.Domain.Criterias;

namespace LinkMe.Query.Search.Employers
{
    public abstract class Persister
    {
        public abstract Criteria CreateCriteria();

        public virtual void OnSaving(Criteria criteria)
        {
        }

        public virtual void OnSaved(Criteria criteria)
        {
        }

        public virtual void OnLoading(Criteria criteria)
        {
        }

        public virtual void OnLoaded(Criteria criteria)
        {
        }
    }

    public class OrganisationPersister
        : Persister
    {
        public override Criteria CreateCriteria()
        {
            return new OrganisationEmployerSearchCriteria();
        }
    }
}
