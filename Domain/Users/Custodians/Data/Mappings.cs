using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Contacts.Data;

namespace LinkMe.Domain.Users.Custodians.Data
{
    internal partial class RegisteredUserEntity
        : IRegisteredUserEntity, IHaveEmailAddressEntity
    {
    }

    internal static class Mappings
    {
        public static Custodian Map(RegisteredUserEntity registeredUserEntity, CommunityOwnerEntity communityOwnerEntity)
        {
            var custodian = registeredUserEntity.MapTo<Custodian>();
            registeredUserEntity.MapTo(custodian);
            custodian.AffiliateId = communityOwnerEntity.communityId;
            return custodian;
        }

        public static CommunityOwnerEntity Map(Guid custodianId, Guid affiliateId)
        {
            return new CommunityOwnerEntity
            {
                id = custodianId,
                communityId = affiliateId
            };
        }

        public static CommunityAdministratorEntity Map(this Custodian custodian)
        {
            var entity = new CommunityAdministratorEntity
            {
                id = custodian.Id,
                RegisteredUserEntity = new RegisteredUserEntity { id = custodian.Id },
            };
            custodian.MapTo(entity);
            return entity;
        }

        public static void MapTo(this Custodian custodian, CommunityAdministratorEntity entity)
        {
            custodian.MapTo(entity.RegisteredUserEntity);
            custodian.MapTo((IHaveEmailAddressEntity)entity.RegisteredUserEntity);
        }
    }
}