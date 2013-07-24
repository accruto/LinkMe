using System;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Agents.Security.Data
{
    internal enum UserFlags
    {
        None = 0x00,
        MustChangePassword = 0x02,
        Disabled = 0x04,
        Activated = 0x20,
    }

    internal static class Mappings
    {
        public static LoginCredentials Map(this RegisteredUserEntity entity)
        {
            return new LoginCredentials
                       {
                           LoginId = entity.loginId,
                           MustChangePassword = ((UserFlags) entity.flags).IsFlagSet(UserFlags.MustChangePassword),
                           PasswordHash = entity.passwordHash
                       };
        }

        public static void MapTo(this LoginCredentials credentials, RegisteredUserEntity entity)
        {
            entity.loginId = credentials.LoginId;
            entity.passwordHash = credentials.PasswordHash;
            entity.flags = (short)((UserFlags)entity.flags).SetFlag(UserFlags.MustChangePassword, credentials.MustChangePassword);
        }

        public static ExternalUserEntity Map(this ExternalCredentials credentials, Guid userId)
        {
            var entity = new ExternalUserEntity {id = userId};
            credentials.MapTo(entity);
            return entity;
        }

        public static void MapTo(this ExternalCredentials credentials, ExternalUserEntity entity)
        {
            entity.externalProviderId = credentials.ProviderId;
            entity.externalId = credentials.ExternalId;
        }

        public static ExternalCredentials Map(this ExternalUserEntity entity)
        {
            return new ExternalCredentials
                       {
                           ProviderId = entity.externalProviderId,
                           ExternalId = entity.externalId
                       };
        }
    }
}