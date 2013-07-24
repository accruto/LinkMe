using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Users.Custodians.Data
{
    public class CustodiansRepository
        : Repository, ICustodiansRepository
    {
        private static readonly Func<CustodiansDataContext, Guid, CommunityAdministratorEntity> GetCommunityAdministratorEntity
            = CompiledQuery.Compile((CustodiansDataContext dc, Guid id)
                => (from e in dc.CommunityAdministratorEntities
                    where e.id == id
                    select e).SingleOrDefault());

        private static readonly Func<CustodiansDataContext, Guid, Custodian> GetCustodian
            = CompiledQuery.Compile((CustodiansDataContext dc, Guid id)
                => (from a in dc.CommunityAdministratorEntities
                    join u in dc.RegisteredUserEntities on a.id equals u.id
                    join o in dc.CommunityOwnerEntities on a.id equals o.id
                    where a.id == id
                    select Mappings.Map(u, o)).SingleOrDefault());

        private static readonly Func<CustodiansDataContext, string, IQueryable<Custodian>> GetCustodians
            = CompiledQuery.Compile((CustodiansDataContext dc, string ids)
                => from a in dc.CommunityAdministratorEntities
                   join u in dc.RegisteredUserEntities on a.id equals u.id
                   join o in dc.CommunityOwnerEntities on a.id equals o.id
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on a.id equals i.value
                   select Mappings.Map(u, o));

        private static readonly Func<CustodiansDataContext, Guid, IQueryable<Custodian>> GetCustodiansByAffiliation
            = CompiledQuery.Compile((CustodiansDataContext dc, Guid affiliateId)
                => from a in dc.CommunityAdministratorEntities
                   join u in dc.RegisteredUserEntities on a.id equals u.id
                   join o in dc.CommunityOwnerEntities on a.id equals o.id
                   where o.communityId == affiliateId
                   select Mappings.Map(u, o));

        private static readonly Func<CustodiansDataContext, Guid, CommunityOwnerEntity> GetCommunityOwnerEntity
            = CompiledQuery.Compile((CustodiansDataContext dc, Guid id)
                => (from o in dc.CommunityOwnerEntities
                    where o.id == id
                    select o).SingleOrDefault());

        public CustodiansRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void ICustodiansRepository.CreateCustodian(Custodian custodian)
        {
            using (var dc = CreateContext())
            {
                dc.CommunityAdministratorEntities.InsertOnSubmit(custodian.Map());
                dc.SubmitChanges();
            }
        }

        void ICustodiansRepository.UpdateCustodian(Custodian custodian)
        {
            using (var dc = CreateContext())
            {
                var entity = GetCommunityAdministratorEntity(dc, custodian.Id);
                if (entity != null)
                {
                    custodian.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        Custodian ICustodiansRepository.GetCustodian(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCustodian(dc, id);
            }
        }

        IList<Custodian> ICustodiansRepository.GetCustodians(IEnumerable<Guid> ids)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCustodians(dc, new SplitList<Guid>(ids.Distinct()).ToString()).ToList();
            }
        }

        IList<Custodian> ICustodiansRepository.GetAffiliationCustodians(Guid affiliateId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCustodiansByAffiliation(dc, affiliateId).ToList();
            }
        }

        Guid? ICustodiansRepository.GetAffiliationId(Guid custodianId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var entity = GetCommunityOwnerEntity(dc, custodianId);
                if (entity == null)
                    return null;
                return entity.communityId;
            }
        }

        void ICustodiansRepository.SetAffiliation(Guid custodianId, Guid? affiliateId)
        {
            using (var dc = CreateContext())
            {
                var entity = GetCommunityOwnerEntity(dc, custodianId);

                // If there is no entity then add it if needed.

                if (entity == null)
                {
                    if (affiliateId != null)
                        dc.CommunityOwnerEntities.InsertOnSubmit(Mappings.Map(custodianId, affiliateId.Value));
                }
                else
                {
                    // If the custodian does not have an affiliation then delete the entity.

                    if (affiliateId == null)
                        dc.CommunityOwnerEntities.DeleteOnSubmit(entity);
                    else
                        entity.communityId = affiliateId.Value;
                }

                dc.SubmitChanges();
            }
        }

        private CustodiansDataContext CreateContext()
        {
            return CreateContext(c => new CustodiansDataContext(c));
        }
    }
}