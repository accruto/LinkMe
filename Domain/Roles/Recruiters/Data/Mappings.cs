using System;
using System.Data.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Contacts.Data;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Data;
using LinkMe.Domain.Location.Queries;

namespace LinkMe.Domain.Roles.Recruiters.Data
{
    internal partial class ContactDetailsEntity
        : IContactDetailsEntity
    {
    }

    internal partial class LocationReferenceEntity
        : ILocationReferenceEntity
    {
    }

    internal partial class AddressEntity
        : IAddressEntity<LocationReferenceEntity>
    {
        LocationReferenceEntity IAddressEntity<LocationReferenceEntity>.LocationReferenceEntity
        {
            get { return LocationReferenceEntity; }
            set { LocationReferenceEntity = value; }
        }
    }

    internal partial class OrganisationEntity
        : IHaveAddressEntity<AddressEntity, LocationReferenceEntity>
    {
    }

    internal partial class OrganisationalUnitEntity
        : IHaveContactDetailsEntity<ContactDetailsEntity>
    {
        string IHaveContactDetailsEntity<ContactDetailsEntity>.companyName
        {
            get { return null; }
            set { }
        }
    }

    internal partial class RecruitersDataContext
        : IHaveAddressEntities<AddressEntity, LocationReferenceEntity>,
        IHaveContactDetailsEntities<ContactDetailsEntity>
    {
        Table<AddressEntity> IHaveAddressEntities<AddressEntity, LocationReferenceEntity>.AddressEntities
        {
            get { return AddressEntities; }
        }

        Table<LocationReferenceEntity> IHaveLocationReferenceEntities<LocationReferenceEntity>.LocationReferenceEntities
        {
            get { return LocationReferenceEntities; }
        }

        Table<ContactDetailsEntity> IHaveContactDetailsEntities<ContactDetailsEntity>.ContactDetailsEntities
        {
            get { return ContactDetailsEntities; }
        }
    }

    internal static class Mappings
    {
        public static Organisation Map(this OrganisationEntity entity, string parentFullName, Guid? affiliateId, ILocationQuery locationQuery)
        {
            if (entity.OrganisationalUnitEntity == null)
            {
                var organisation = new Organisation { Id = entity.id, AffiliateId = affiliateId };
                entity.MapTo(organisation, locationQuery);
                return organisation;
            }

            var verifiedOrganisation = new VerifiedOrganisation { Id = entity.id, AffiliateId = affiliateId };
            entity.MapTo(verifiedOrganisation, locationQuery);
            entity.MapTo(verifiedOrganisation, parentFullName);
            return verifiedOrganisation;
        }

        public static OrganisationEntity Map(this Organisation organisation)
        {
            var entity = new OrganisationEntity
            {
                id = organisation.Id,
            };
            organisation.MapTo(entity);
            entity.OrganisationalUnitEntity = organisation is VerifiedOrganisation ? ((VerifiedOrganisation)organisation).Map() : null;
            return entity;
        }

        public static void MapTo(this Organisation organisation, OrganisationEntity entity)
        {
            entity.displayName = organisation.Name;
            ((IHaveAddress)organisation).MapTo(entity);
        }

        public static OrganisationalUnitEntity Map(this VerifiedOrganisation organisation)
        {
            var entity = new OrganisationalUnitEntity();
            organisation.MapTo(entity);
            return entity;
        }

        public static void MapTo(this VerifiedOrganisation organisation, OrganisationalUnitEntity entity)
        {
            entity.parentId = organisation.ParentId;
            entity.accountManagerId = organisation.AccountManagerId;
            entity.verifiedById = organisation.VerifiedById;
            ((IHaveContactDetails)organisation).MapTo(entity);
        }

        public static CommunityEmployerEnquiryEntity Map(this AffiliationEnquiry enquiry, Guid communityId)
        {
            return new CommunityEmployerEnquiryEntity
            {
                id = enquiry.Id,
                communityId = communityId,
                createdTime = enquiry.CreatedTime,
                companyName = enquiry.CompanyName,
                emailAddress = enquiry.EmailAddress,
                firstName = enquiry.FirstName,
                lastName = enquiry.LastName,
                jobTitle = enquiry.JobTitle,
                phoneNumber = enquiry.PhoneNumber,
            };
        }

        public static CommunityOrganisationalUnitEntity MapCommunityOrganisation(Guid organisationId, Guid communityId)
        {
            return new CommunityOrganisationalUnitEntity
            {
                id = organisationId,
                CommunityAssociationEntities = new EntitySet<CommunityAssociationEntity>
                {
                    new CommunityAssociationEntity { communityId = communityId, organisationalUnitId = organisationId }
                }
            };
        }

        public static void MapTo(this Organisation organisation, CommunityOrganisationalUnitEntity entity)
        {
            entity.CommunityAssociationEntities = new EntitySet<CommunityAssociationEntity>
            {
                new CommunityAssociationEntity { communityId = organisation.AffiliateId.Value, organisationalUnitId = entity.id }
            };
        }

        private static void MapTo(this OrganisationEntity entity, Organisation organisation, ILocationQuery locationQuery)
        {
            organisation.Name = entity.displayName;
            organisation.Address = entity.AddressEntity == null ? null : entity.AddressEntity.Map(locationQuery);
        }

        private static void MapTo(this OrganisationEntity entity, VerifiedOrganisation organisation, string parentFullName)
        {
            organisation.AccountManagerId = entity.OrganisationalUnitEntity.accountManagerId;
            organisation.VerifiedById = entity.OrganisationalUnitEntity.verifiedById;
            organisation.ContactDetails = entity.OrganisationalUnitEntity.Map();
            organisation.SetParent(entity.OrganisationalUnitEntity.parentId, parentFullName);
        }
    }
}
