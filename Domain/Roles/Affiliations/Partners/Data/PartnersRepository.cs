using System;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Roles.Affiliations.Partners.Data
{
    public class PartnersRepository
        : Repository, IPartnersRepository
    {
        private static readonly Func<PartnersDataContext, Guid, EmployerEntity> GetEmployerEntity
            = CompiledQuery.Compile((PartnersDataContext dc, Guid userId)
                => (from e in dc.EmployerEntities
                    where e.id == userId
                    select e).SingleOrDefault());

        private static readonly Func<PartnersDataContext, Guid, Partner> GetPartner
            = CompiledQuery.Compile((PartnersDataContext dc, Guid userId)
                => (from p in dc.ServicePartnerEntities
                    join e in dc.EmployerEntities on p.id equals e.ownerPartnerId
                    where e.id == userId
                    select p.Map()).SingleOrDefault());

        public PartnersRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IPartnersRepository.CreatePartner(Partner partner)
        {
            using (var dc = CreateContext())
            {
                dc.ServicePartnerEntities.InsertOnSubmit(partner.Map());
                dc.SubmitChanges();
            }
        }

        void IPartnersRepository.SetPartner(Guid userId, Guid? partnerId)
        {
            using (var dc = CreateContext())
            {
                var entity = GetEmployerEntity(dc, userId);
                if (entity != null)
                {
                    entity.ownerPartnerId = partnerId;
                    dc.SubmitChanges();
                }
            }
        }

        Partner IPartnersRepository.GetPartner(Guid userId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetPartner(dc, userId);
            }
        }

        private PartnersDataContext CreateContext()
        {
            return CreateContext(c => new PartnersDataContext(c));
        }
    }
}
