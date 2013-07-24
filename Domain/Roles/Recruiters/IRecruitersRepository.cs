using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Roles.Recruiters
{
    public interface IRecruitersRepository
    {
        void CreateOrganisation(Organisation organisation);
        void UpdateOrganisation(Organisation organisation);
        Organisation GetOrganisation(Guid id);
        Organisation GetOrganisation(Guid? parentId, string name);
        VerifiedOrganisation GetVerifiedOrganisation(string fullName);
        Organisation GetRootOrganisation(Guid id);
        IList<Organisation> GetOrganisations(IEnumerable<Guid> ids);
        bool IsDescendent(Guid parentId, Guid childId);
        OrganisationHierarchy GetOrganisationHierarchy(Guid organisationId);
        OrganisationHierarchy GetSubOrganisationHierarchy(Guid organisationId);
        ContactDetails GetEffectiveContactDetails(Guid organisationId);

        Organisation GetRecruiterOrganisation(Guid recruiterId);
        IDictionary<Guid, Organisation> GetRecruiterOrganisations(IEnumerable<Guid> recruiterIds);
        IDictionary<Guid, Organisation> GetRecruiterOrganisations(IEnumerable<Guid> recruiterIds, IEnumerable<Guid> organisationIds);
        OrganisationHierarchyPath GetRecruiterOrganisationHierarchyPath(Guid recruiterId);
        IList<Guid> GetRecruiters(Guid organisationId);
        IList<Guid> GetRecruiters(IEnumerable<Guid> organisationIds);

        void CreateAffiliationEnquiry(Guid affiliateId, AffiliationEnquiry enquiry);
    }
}
