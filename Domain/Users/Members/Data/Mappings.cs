using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Contacts.Data;
using LinkMe.Domain.Data;
using LinkMe.Domain.Location.Data;
using LinkMe.Domain.Location.Queries;

namespace LinkMe.Domain.Users.Members.Data
{
    internal enum UserFlags
    {
        Disabled = 0x04,
        Activated = 0x20,
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

    public interface IMemberEntity<TAddressEntity, TLocationReferenceEntity>
        : IHaveAddressEntity<TAddressEntity, TLocationReferenceEntity>,
        IHavePartialDateEntity,
        IHavePhoneNumbersEntity
        where TAddressEntity : class, IAddressEntity<TLocationReferenceEntity>
        where TLocationReferenceEntity : class, ILocationReferenceEntity
    {
        Guid id { get; set; }
        DateTime lastEditedTime { get; set; }
        byte gender { get; set; }
        int? ethnicFlags { get; set; }
        int? disabilityFlags { get; set; }
        Guid? profilePhotoId { get; set; }
        int firstDegreeAccess { get; set; }
        int secondDegreeAccess { get; set; }
        int publicAccess { get; set; }
        byte employerAccess { get; set; }
    }

    public interface ICommunityMemberEntity
    {
        Guid primaryCommunityId { get; set; }
    }

    internal partial class MemberEntity
        : IMemberEntity<AddressEntity, LocationReferenceEntity>
    {
        DateTime? IHavePartialDateEntity.date
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        byte? IHavePartialDateEntity.dateParts
        {
            get { return dateOfBirthParts; }
            set { dateOfBirthParts = value; }
        }
    }

    internal partial class RegisteredUserEntity
        : IRegisteredUserEntity, IHaveEmailAddressesEntity
    {
        string IHaveEmailAddressesEntity.primaryEmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        bool IHaveEmailAddressesEntity.primaryEmailAddressVerified
        {
            get { return emailAddressVerified; }
            set { emailAddressVerified = value; }
        }

        string IHaveEmailAddressesEntity.secondaryEmailAddress
        {
            get { return secondaryEmailAddress; }
            set { secondaryEmailAddress = value; }
        }

        bool? IHaveEmailAddressesEntity.secondaryEmailAddressVerified
        {
            get { return secondaryEmailAddressVerified; }
            set { secondaryEmailAddressVerified = value; }
        }
    }

    internal partial class CommunityMemberEntity
        : ICommunityMemberEntity
    {
    }

    public static class Mappings
    {
        public static Member Map<TAddressEntity, TLocationReferenceEntity>(this IMemberEntity<TAddressEntity, TLocationReferenceEntity> memberEntity, IRegisteredUserEntity registeredUserEntity, ICommunityMemberEntity communityMemberEntity, ILocationQuery locationQuery)
            where TAddressEntity : class, IAddressEntity<TLocationReferenceEntity>
            where TLocationReferenceEntity : class, ILocationReferenceEntity
        {
            var member = registeredUserEntity.MapTo<Member>();
            ((IHaveEmailAddressesEntity) registeredUserEntity).MapTo(member);
            memberEntity.MapTo(member);
            member.LastUpdatedTime = memberEntity.lastEditedTime;
            member.Gender = (Gender) memberEntity.gender;
            member.DateOfBirth = memberEntity.Map();
            member.EthnicStatus = (EthnicStatus) (memberEntity.ethnicFlags == null ? 0 : memberEntity.ethnicFlags.Value);
            member.Address = memberEntity.AddressEntity == null ? null : memberEntity.AddressEntity.Map(locationQuery);
            member.PhotoId = memberEntity.profilePhotoId;
            member.AffiliateId = communityMemberEntity == null ? (Guid?) null : communityMemberEntity.primaryCommunityId;
            member.VisibilitySettings = new VisibilitySettings
            {
                Personal =
                {
                    FirstDegreeVisibility = (PersonalVisibility) memberEntity.firstDegreeAccess,
                    SecondDegreeVisibility = (PersonalVisibility) memberEntity.secondDegreeAccess,
                    PublicVisibility = (PersonalVisibility) memberEntity.publicAccess
                },
                Professional =
                {
                    EmploymentVisibility = (ProfessionalVisibility) memberEntity.employerAccess,
                }
            };
            return member;
        }

        internal static MemberEntity Map(this Member member)
        {
            var entity = new MemberEntity
            {
                id = member.Id,
                RegisteredUserEntity = new RegisteredUserEntity {id = member.Id},
            };
            member.MapTo(entity);
            return entity;
        }

        internal static void MapTo(this Member member, MemberEntity entity)
        {
            member.MapTo(entity.RegisteredUserEntity);
            member.MapTo((IHaveEmailAddressesEntity)entity.RegisteredUserEntity);
            member.MapTo((IHavePhoneNumbersEntity)entity);
            entity.lastEditedTime = member.LastUpdatedTime;
            entity.gender = (byte) member.Gender;
            member.DateOfBirth.MapTo(entity);
            entity.ethnicFlags = member.EthnicStatus == 0 ? null : (int?) member.EthnicStatus;
            entity.profilePhotoId = member.PhotoId;

            entity.firstDegreeAccess = (int) member.VisibilitySettings.Personal.FirstDegreeVisibility;
            entity.secondDegreeAccess = (int) member.VisibilitySettings.Personal.SecondDegreeVisibility;
            entity.publicAccess = (int) member.VisibilitySettings.Personal.PublicVisibility;
            entity.employerAccess = (byte) member.VisibilitySettings.Professional.EmploymentVisibility;

            if (entity.AddressEntity == null)
                entity.AddressEntity = member.Address.MapTo<AddressEntity, LocationReferenceEntity>();
            else
                member.Address.MapTo(entity.AddressEntity);
        }

        internal static CommunityMemberEntity MapMember(Guid memberId, Guid communityId)
        {
            return new CommunityMemberEntity { id = memberId, primaryCommunityId = communityId };
        }

        internal static CommunityMemberDataEntity[] Map(this IDictionary<string, string> items, Guid memberId, Guid affiliateId)
        {
            return (from d in items
                    select new CommunityMemberDataEntity {memberId = memberId, id = affiliateId, name = d.Key, value = d.Value}).ToArray();
        }
    }
}
