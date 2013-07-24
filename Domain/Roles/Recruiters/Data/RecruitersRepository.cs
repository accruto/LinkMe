using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Contacts.Data;
using LinkMe.Domain.Data;
using LinkMe.Domain.Location.Data;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Roles.Recruiters.Data
{
    public class RecruitersRepository
        : Repository, IRecruitersRepository
    {
        private readonly ILocationQuery _locationQuery;

        private static readonly DataLoadOptions OrganisationLoadOptions;
        private static readonly DataLoadOptions OrganisationalUnitLoadOptions = DataOptions.CreateLoadOptions<CommunityOrganisationalUnitEntity>(o => o.CommunityAssociationEntities);

        private static readonly Func<RecruitersDataContext, Guid, ILocationQuery, Organisation> GetOrganisationQuery
            = CompiledQuery.Compile((RecruitersDataContext dc, Guid id, ILocationQuery locationQuery)
                => (from o in dc.OrganisationEntities
                    where o.id == id
                    select o.Map(dc.GetOrganisationFullName(o.OrganisationalUnitEntity.parentId, null), dc.GetOrganisationAffiliateId(o.id), locationQuery)).SingleOrDefault());

        private static readonly Func<RecruitersDataContext, string, ILocationQuery, IQueryable<Organisation>> GetOrganisationsQuery
            = CompiledQuery.Compile((RecruitersDataContext dc, string ids, ILocationQuery locationQuery)
                => from o in dc.OrganisationEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on o.id equals i.value
                   select o.Map(dc.GetOrganisationFullName(o.OrganisationalUnitEntity.parentId, null), dc.GetOrganisationAffiliateId(o.id), locationQuery));

        private static readonly Func<RecruitersDataContext, Guid, ILocationQuery, Organisation> GetRootOrganisationQuery
            = CompiledQuery.Compile((RecruitersDataContext dc, Guid id, ILocationQuery locationQuery)
                => (from o in dc.OrganisationEntities
                    where o.id == dc.GetOrganisationRootId(id)
                    select o.Map(dc.GetOrganisationFullName(o.OrganisationalUnitEntity.parentId, null), dc.GetOrganisationAffiliateId(o.id), locationQuery)).SingleOrDefault());

        private static readonly Func<RecruitersDataContext, Guid, OrganisationEntity> GetOrganisationEntityQuery
            = CompiledQuery.Compile((RecruitersDataContext dc, Guid id)
                => (from o in dc.OrganisationEntities
                    where o.id == id
                    select o).SingleOrDefault());

        private static readonly Func<RecruitersDataContext, Guid?, string, ILocationQuery, Organisation> GetOrganisationByParentQuery
            = CompiledQuery.Compile((RecruitersDataContext dc, Guid? parentId, string name, ILocationQuery locationQuery)
                => (from o in dc.OrganisationEntities
                    where o.displayName == name
                    && o.OrganisationalUnitEntity != null
                    && Equals(o.OrganisationalUnitEntity.parentId, parentId)
                    select o.Map(dc.GetOrganisationFullName(o.OrganisationalUnitEntity.parentId, null), dc.GetOrganisationAffiliateId(o.id), locationQuery)).SingleOrDefault());

        private static readonly Func<RecruitersDataContext, string, ILocationQuery, VerifiedOrganisation> GetOrganisationByFullNameQuery
            = CompiledQuery.Compile((RecruitersDataContext dc, string fullName, ILocationQuery locationQuery)
                => (from o in dc.OrganisationEntities
                    where dc.GetOrganisationFullName(o.id, null) == fullName
                    && o.OrganisationalUnitEntity != null
                    select o.Map(dc.GetOrganisationFullName(o.OrganisationalUnitEntity.parentId, null), dc.GetOrganisationAffiliateId(o.id), locationQuery) as VerifiedOrganisation).SingleOrDefault());

        private static readonly Func<RecruitersDataContext, Guid, Guid, bool?> IsDescendent
            = CompiledQuery.Compile((RecruitersDataContext dc, Guid parentId, Guid childId)
                => dc.IsOrganisationDescendent(parentId, childId));

        private static readonly Func<RecruitersDataContext, Guid, ILocationQuery, IQueryable<Tuple<Organisation, Guid?>>> GetOrganisationsInHierarchyQuery
            = CompiledQuery.Compile((RecruitersDataContext dc, Guid organisationId, ILocationQuery locationQuery)
                => from o in dc.OrganisationEntities
                   where dc.GetOrganisationRootId(o.id) == dc.GetOrganisationRootId(organisationId)
                   && o.OrganisationalUnitEntity != null
                   select new Tuple<Organisation, Guid?>(o.Map(dc.GetOrganisationFullName(o.OrganisationalUnitEntity.parentId, null), dc.GetOrganisationAffiliateId(o.id), locationQuery), o.OrganisationalUnitEntity.parentId));

        private static readonly Func<RecruitersDataContext, Guid, ContactDetails> GetEffectiveContactDetailsQuery
            = CompiledQuery.Compile((RecruitersDataContext dc, Guid organisationId)
                => (from o in dc.OrganisationalUnitEntities
                    where o.ContactDetailsEntity.id == dc.GetEffectiveOrgUnitContactDetailsId(organisationId)
                    select o.Map()).SingleOrDefault());

        private static readonly Func<RecruitersDataContext, Guid, ILocationQuery, Organisation> GetRecruiterOrganisationQuery
            = CompiledQuery.Compile((RecruitersDataContext dc, Guid recruiterId, ILocationQuery locationQuery)
                => (from o in dc.OrganisationEntities
                    join e in dc.EmployerEntities on o.id equals e.organisationId
                    where e.id == recruiterId
                    select o.Map(dc.GetOrganisationFullName(o.OrganisationalUnitEntity.parentId, null), dc.GetOrganisationAffiliateId(o.id), locationQuery)).SingleOrDefault());

        private static readonly Func<RecruitersDataContext, string, ILocationQuery, IQueryable<Tuple<Guid, Organisation>>> GetRecruiterOrganisationsQuery
            = CompiledQuery.Compile((RecruitersDataContext dc, string recruiterIds, ILocationQuery locationQuery)
                => (from i in dc.SplitGuids(SplitList<Guid>.Delimiter, recruiterIds)
                    join e in dc.EmployerEntities on i.value equals e.id
                    join o in dc.OrganisationEntities on e.organisationId equals o.id
                    select new Tuple<Guid, Organisation>(e.id, o.Map(dc.GetOrganisationFullName(o.OrganisationalUnitEntity.parentId, null), dc.GetOrganisationAffiliateId(o.id), locationQuery))));

        private static readonly Func<RecruitersDataContext, string, string, ILocationQuery, IQueryable<Tuple<Guid, Organisation>>> GetFilteredRecruiterOrganisationsQuery
            = CompiledQuery.Compile((RecruitersDataContext dc, string recruiterIds, string organisationIds, ILocationQuery locationQuery)
                => (from ri in dc.SplitGuids(SplitList<Guid>.Delimiter, recruiterIds)
                    join e in dc.EmployerEntities on ri.value equals e.id
                    join o in dc.OrganisationEntities on e.organisationId equals o.id
                    join oi in dc.SplitGuids(SplitList<Guid>.Delimiter, organisationIds) on o.id equals oi.value
                    select new Tuple<Guid, Organisation>(e.id, o.Map(dc.GetOrganisationFullName(o.OrganisationalUnitEntity.parentId, null), dc.GetOrganisationAffiliateId(o.id), locationQuery))));

        private static readonly Func<RecruitersDataContext, Guid, IQueryable<Guid>> GetRecruiters
            = CompiledQuery.Compile((RecruitersDataContext dc, Guid organisationId)
                => from e in dc.EmployerEntities
                   where e.organisationId == organisationId
                   select e.id);

        private static readonly Func<RecruitersDataContext, string, IQueryable<Guid>> GetRecruitersForOrganisations
            = CompiledQuery.Compile((RecruitersDataContext dc, string organisationIds)
                => from e in dc.EmployerEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, organisationIds) on e.organisationId equals i.value
                   select e.id);

        private static readonly Func<RecruitersDataContext, Guid, IQueryable<Guid>> GetRecruiterOrganisationHierarchyPath
            = CompiledQuery.Compile((RecruitersDataContext dc, Guid recruiterId)
                => from e in dc.GetRecruiterOrganisationalHierarchy(recruiterId)
                   select e.id.Value);

        private static readonly Func<RecruitersDataContext, Guid, CommunityOrganisationalUnitEntity> GetCommunityOrganisationalUnitEntity
            = CompiledQuery.Compile((RecruitersDataContext dc, Guid id)
                => (from o in dc.CommunityOrganisationalUnitEntities
                    where o.id == id
                    select o).SingleOrDefault());

        static RecruitersRepository()
        {
            OrganisationLoadOptions = new DataLoadOptions();
            OrganisationLoadOptions.LoadWith<OrganisationEntity>(o => o.OrganisationalUnitEntity);
            OrganisationLoadOptions.LoadWith<OrganisationEntity>(o => o.AddressEntity);
            OrganisationLoadOptions.LoadWith<AddressEntity>(a => a.LocationReferenceEntity);
            OrganisationLoadOptions.LoadWith<OrganisationalUnitEntity>(o => o.ContactDetailsEntity);
        }

        public RecruitersRepository(IDataContextFactory dataContextFactory, ILocationQuery locationQuery)
            : base(dataContextFactory)
        {
            _locationQuery = locationQuery;
        }

        void IRecruitersRepository.CreateOrganisation(Organisation organisation)
        {
            using (var dc = CreateContext())
            {
                // Create the organisation entity.

                dc.OrganisationEntities.InsertOnSubmit(organisation.Map());

                // Create the affiliation entity.

                if (organisation.AffiliateId != null)
                    dc.CommunityOrganisationalUnitEntities.InsertOnSubmit(Mappings.MapCommunityOrganisation(organisation.Id, organisation.AffiliateId.Value));

                dc.SubmitChanges();
            }
        }

        void IRecruitersRepository.UpdateOrganisation(Organisation organisation)
        {
            using (var dc = CreateContext())
            {
                dc.LoadOptions = OrganisationalUnitLoadOptions;

                // Update what is already there taking into account that a verified may be turning into an unverified etc.

                var entity = GetOrganisationEntity(dc, organisation.Id);

                // Check whether anything needs deleting first.

                dc.CheckDeleteAddress(organisation, entity);

                // Update all entities.

                organisation.MapTo(entity);
                UpdateOrganisationalEntity(dc, organisation, entity);
                UpdateCommunityOrganisationalEntity(dc, organisation);

                dc.SubmitChanges();
            }
        }

        Organisation IRecruitersRepository.GetOrganisation(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetOrganisation(dc, id);
            }
        }

        Organisation IRecruitersRepository.GetOrganisation(Guid? parentId, string name)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetOrganisation(dc, parentId, name);
            }
        }

        Organisation IRecruitersRepository.GetRootOrganisation(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRootOrganisation(dc, id);
            }
        }

        IList<Organisation> IRecruitersRepository.GetOrganisations(IEnumerable<Guid> ids)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetOrganisations(dc, ids).ToList();
            }
        }

        bool IRecruitersRepository.IsDescendent(Guid parentId, Guid childId)
        {
            if (parentId == childId)
                return false;
            using (var dc = CreateContext().AsReadOnly())
            {
                var result = IsDescendent(dc, parentId, childId);
                return result != null && result.Value;
            }
        }

        OrganisationHierarchy IRecruitersRepository.GetOrganisationHierarchy(Guid organisationId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var organisations = GetOrganisationsInHierarchy(dc, organisationId).ToArray();

                // Build up hierarchy.

                var parent = (from o in organisations
                              where o.Item2 == null
                              select o).SingleOrDefault();

                if (parent == null)
                    return CreateOrganisationHierarchy(dc, organisationId);

                return GetOrganisationHierarchy(parent.Item1, organisations);
            }
        }

        OrganisationHierarchy IRecruitersRepository.GetSubOrganisationHierarchy(Guid organisationId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var organisations = GetOrganisationsInHierarchy(dc, organisationId).ToArray();

                // Build up hierarchy.

                var parent = (from o in organisations
                              where o.Item1.Id == organisationId
                              select o).SingleOrDefault();

                if (parent == null)
                    return CreateOrganisationHierarchy(dc, organisationId);

                return GetOrganisationHierarchy(parent.Item1, organisations);
            }
        }

        VerifiedOrganisation IRecruitersRepository.GetVerifiedOrganisation(string fullName)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetOrganisation(dc, fullName);
            }
        }

        Organisation IRecruitersRepository.GetRecruiterOrganisation(Guid recruiterId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRecruiterOrganisation(dc, recruiterId);
            }
        }

        OrganisationHierarchyPath IRecruitersRepository.GetRecruiterOrganisationHierarchyPath(Guid recruiterId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return new OrganisationHierarchyPath(GetRecruiterOrganisationHierarchyPath(dc, recruiterId));
            }
        }

        IDictionary<Guid, Organisation> IRecruitersRepository.GetRecruiterOrganisations(IEnumerable<Guid> recruiterIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRecruiterOrganisations(dc, recruiterIds).ToDictionary(t => t.Item1, t => t.Item2);
            }
        }

        IDictionary<Guid, Organisation> IRecruitersRepository.GetRecruiterOrganisations(IEnumerable<Guid> recruiterIds, IEnumerable<Guid> organisationIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRecruiterOrganisations(dc, recruiterIds, organisationIds).ToDictionary(t => t.Item1, t => t.Item2);
            }
        }

        IList<Guid> IRecruitersRepository.GetRecruiters(Guid organisationId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRecruiters(dc, organisationId).ToList();
            }
        }

        IList<Guid> IRecruitersRepository.GetRecruiters(IEnumerable<Guid> organisationIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRecruitersForOrganisations(dc, new SplitList<Guid>(organisationIds).ToString()).ToList();
            }
        }

        ContactDetails IRecruitersRepository.GetEffectiveContactDetails(Guid organisationId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetEffectiveContactDetails(dc, organisationId);
            }
        }

        void IRecruitersRepository.CreateAffiliationEnquiry(Guid affiliateId, AffiliationEnquiry enquiry)
        {
            using (var dc = CreateContext())
            {
                dc.CommunityEmployerEnquiryEntities.InsertOnSubmit(enquiry.Map(affiliateId));
                dc.SubmitChanges();
            }
        }

        private OrganisationHierarchy CreateOrganisationHierarchy(RecruitersDataContext dc, Guid organisationId)
        {
            return new OrganisationHierarchy
            {
                Organisation = GetOrganisation(dc, organisationId),
                ChildOrganisationHierarchies = new List<OrganisationHierarchy>(),
            };
        }

        private static void UpdateOrganisationalEntity(RecruitersDataContext dc, Organisation organisation, OrganisationEntity entity)
        {
            if (organisation.IsVerified)
            {
                // Make sure there is an organisational unit.

                var verifiedOrganisation = (VerifiedOrganisation)organisation;
                if (entity.OrganisationalUnitEntity == null)
                {
                    entity.OrganisationalUnitEntity = verifiedOrganisation.Map();
                }
                else
                {
                    dc.CheckDeleteContactDetails(verifiedOrganisation, entity.OrganisationalUnitEntity);
                    verifiedOrganisation.MapTo(entity.OrganisationalUnitEntity);
                }
            }
            else
            {
                // Not verified, make sure there is no organisational unit.

                if (entity.OrganisationalUnitEntity != null)
                {
                    if (entity.OrganisationalUnitEntity.ContactDetailsEntity != null)
                    {
                        dc.ContactDetailsEntities.DeleteOnSubmit(entity.OrganisationalUnitEntity.ContactDetailsEntity);
                        entity.OrganisationalUnitEntity.ContactDetailsEntity = null;
                    }

                    dc.OrganisationalUnitEntities.DeleteOnSubmit(entity.OrganisationalUnitEntity);
                    entity.OrganisationalUnitEntity = null;
                }
            }
        }

        private static void UpdateCommunityOrganisationalEntity(RecruitersDataContext dc, Organisation organisation)
        {
            var entity = GetCommunityOrganisationalUnitEntity(dc, organisation.Id);

            // If there is no entity then just add it.

            if (entity == null)
            {
                if (organisation.AffiliateId != null)
                    dc.CommunityOrganisationalUnitEntities.InsertOnSubmit(Mappings.MapCommunityOrganisation(organisation.Id, organisation.AffiliateId.Value));
            }
            else
            {
                DeleteAssociations(dc, entity);
                if (organisation.AffiliateId == null)
                    dc.CommunityOrganisationalUnitEntities.DeleteOnSubmit(entity);
                else
                    organisation.MapTo(entity);
            }
        }

        private Organisation GetOrganisation(RecruitersDataContext dc, Guid id)
        {
            dc.LoadOptions = OrganisationLoadOptions;
            return GetOrganisationQuery(dc, id, _locationQuery);
        }

        private IEnumerable<Organisation> GetOrganisations(RecruitersDataContext dc, IEnumerable<Guid> ids)
        {
            dc.LoadOptions = OrganisationLoadOptions;
            return GetOrganisationsQuery(dc, new SplitList<Guid>(ids).ToString(), _locationQuery);
        }

        private Organisation GetRootOrganisation(RecruitersDataContext dc, Guid id)
        {
            dc.LoadOptions = OrganisationLoadOptions;
            return GetRootOrganisationQuery(dc, id, _locationQuery);
        }

        private static OrganisationEntity GetOrganisationEntity(RecruitersDataContext dc, Guid id)
        {
            dc.LoadOptions = OrganisationLoadOptions;
            return GetOrganisationEntityQuery(dc, id);
        }

        private Organisation GetOrganisation(RecruitersDataContext dc, Guid? parentId, string name)
        {
            dc.LoadOptions = OrganisationLoadOptions;
            return GetOrganisationByParentQuery(dc, parentId, name, _locationQuery);
        }

        private VerifiedOrganisation GetOrganisation(RecruitersDataContext dc, string fullName)
        {
            dc.LoadOptions = OrganisationLoadOptions;
            return GetOrganisationByFullNameQuery(dc, fullName, _locationQuery);
        }

        private IEnumerable<Tuple<Organisation, Guid?>> GetOrganisationsInHierarchy(RecruitersDataContext dc, Guid organisationId)
        {
            dc.LoadOptions = OrganisationLoadOptions;
            return GetOrganisationsInHierarchyQuery(dc, organisationId, _locationQuery);
        }

        private Organisation GetRecruiterOrganisation(RecruitersDataContext dc, Guid recruiterId)
        {
            dc.LoadOptions = OrganisationLoadOptions;
            return GetRecruiterOrganisationQuery(dc, recruiterId, _locationQuery);
        }

        private IEnumerable<Tuple<Guid, Organisation>> GetRecruiterOrganisations(RecruitersDataContext dc, IEnumerable<Guid> recruiterIds)
        {
            dc.LoadOptions = OrganisationLoadOptions;
            return GetRecruiterOrganisationsQuery(dc, new SplitList<Guid>(recruiterIds).ToString(), _locationQuery);
        }

        private IEnumerable<Tuple<Guid, Organisation>> GetRecruiterOrganisations(RecruitersDataContext dc, IEnumerable<Guid> recruiterIds, IEnumerable<Guid> organisationIds)
        {
            dc.LoadOptions = OrganisationLoadOptions;
            return GetFilteredRecruiterOrganisationsQuery(dc, new SplitList<Guid>(recruiterIds).ToString(), new SplitList<Guid>(organisationIds).ToString(), _locationQuery);
        }

        private static ContactDetails GetEffectiveContactDetails(RecruitersDataContext dc, Guid organisationId)
        {
            dc.LoadOptions = OrganisationLoadOptions;
            return GetEffectiveContactDetailsQuery(dc, organisationId);
        }

        private static OrganisationHierarchy GetOrganisationHierarchy(Organisation parent, IEnumerable<Tuple<Organisation, Guid?>> organisations)
        {
            return new OrganisationHierarchy
            {
                Organisation = parent,
                ChildOrganisationHierarchies = (from o in organisations
                                                where o.Item2 == parent.Id
                                                orderby o.Item1.Name
                                                select GetOrganisationHierarchy(o.Item1, organisations)).ToList()
            };
        }

        private static void DeleteAssociations(RecruitersDataContext dc, CommunityOrganisationalUnitEntity entity)
        {
            if (entity.CommunityAssociationEntities != null && entity.CommunityAssociationEntities.Count > 0)
            {
                dc.CommunityAssociationEntities.DeleteAllOnSubmit(entity.CommunityAssociationEntities);
                entity.CommunityAssociationEntities.Clear();
            }
        }

        private RecruitersDataContext CreateContext()
        {
            return CreateContext(c => new RecruitersDataContext(c));
        }
    }
}