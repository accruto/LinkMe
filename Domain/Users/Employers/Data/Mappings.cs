using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Contacts.Data;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;

namespace LinkMe.Domain.Users.Employers.Data
{
    public interface IEmployerIndustryEntity
    {
        Guid industryId { get; }
    }

    public interface ICommunityAssociationEntity
    {
        Guid communityId { get; }
    }

    public interface IEmployerEntity
    {
        IRegisteredUserEntity RegisteredUserEntity { get; }
        string contactPhoneNumber { get; }
        string jobTitle { get; }
        Guid organisationId { get; }
        byte subRole { get; }
        IEnumerable<IEmployerIndustryEntity> EmployerIndustryEntities { get; }
    }

    internal partial class EmployerIndustryEntity
        : IEmployerIndustryEntity
    {
    }

    internal partial class EmployerEntity
        : IEmployerEntity, IHavePhoneNumberEntity
    {
        IRegisteredUserEntity IEmployerEntity.RegisteredUserEntity
        {
            get { return RegisteredUserEntity; }
        }

        IEnumerable<IEmployerIndustryEntity> IEmployerEntity.EmployerIndustryEntities
        {
            get { return EmployerIndustryEntities.Cast<IEmployerIndustryEntity>(); }
        }

        string IHavePhoneNumberEntity.phoneNumber
        {
            get { return contactPhoneNumber; }
            set { contactPhoneNumber = value; }
        }

        public byte? phoneNumberType
        {
            get { return (byte?) PhoneNumberType.Work; }
            set { }
        }
    }

    internal partial class RegisteredUserEntity
        : IRegisteredUserEntity, IHaveEmailAddressEntity
    {
    }

    public static class Mappings
    {
        public static Employer Map(IEmployerEntity employerEntity, IRegisteredUserEntity registeredUserEntity, IIndustriesQuery industriesQuery)
        {
            var employer = registeredUserEntity.MapTo<Employer>();
            ((IHaveEmailAddressEntity)registeredUserEntity).MapTo(employer);
            ((IHavePhoneNumberEntity)employerEntity).MapTo(employer);
            employer.JobTitle = employerEntity.jobTitle;
            employer.SubRole = (EmployerSubRole) employerEntity.subRole;
            employer.Industries = employerEntity.EmployerIndustryEntities == null
                ? null
                : employerEntity.EmployerIndustryEntities.Map(industriesQuery);
            return employer;
        }

        internal static EmployerEntity Map(this Employer employer)
        {
            var entity = new EmployerEntity
            {
                id = employer.Id,
                RegisteredUserEntity = new RegisteredUserEntity {id = employer.Id},
            };

            employer.MapTo(entity);
            return entity;
        }

        internal static void MapTo(this Employer employer, EmployerEntity entity)
        {
            employer.MapTo(entity.RegisteredUserEntity);
            employer.MapTo((IHaveEmailAddressEntity)entity.RegisteredUserEntity);
            employer.MapTo((IHavePhoneNumberEntity)entity);
            entity.jobTitle = employer.JobTitle;
            entity.organisationId = employer.Organisation.Id;
            entity.subRole = (byte) employer.SubRole;
            entity.EmployerIndustryEntities = employer.Industries == null ? null : employer.Industries.Map(employer.Id);
        }

        private static IList<Industry> Map(this IEnumerable<IEmployerIndustryEntity> entities, IIndustriesQuery industriesQuery)
        {
            return (from e in entities
                    select industriesQuery.GetIndustry(e.industryId)).ToList();
        }

        private static EntitySet<EmployerIndustryEntity> Map(this IEnumerable<Industry> industries, Guid employerId)
        {
            var set = new EntitySet<EmployerIndustryEntity>();
            set.AddRange(from i in industries select new EmployerIndustryEntity {employerId = employerId, industryId = i.Id});
            return set;
        }
    }
}