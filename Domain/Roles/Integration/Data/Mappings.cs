namespace LinkMe.Domain.Roles.Integration.Data
{
    internal static class Mappings
    {
        public static T MapTo<T>(this AtsIntegratorEntity entity)
            where T : IntegrationSystem, new()
        {
            if (entity == null)
                return null;

            return new T
            {
                Id = entity.id,
                Name = entity.name,
            };
        }

        public static AtsIntegratorEntity Map(this IntegrationSystem system)
        {
            return new AtsIntegratorEntity
            {
                id = system.Id,
                name = system.Name,
            };
        }

        public static IntegratorUser Map(this IntegratorUserEntity entity)
        {
            return new IntegratorUser
            {
                Id = entity.id,
                IntegrationSystemId = entity.integratorId,
                LoginId = entity.username,
                PasswordHash = entity.password,
                Permissions = (IntegratorPermissions) entity.permissions
            };
        }

        public static IntegratorUserEntity Map(this IntegratorUser user)
        {
            return new IntegratorUserEntity
            {
                id = user.Id,
                username = user.LoginId,
                password = user.PasswordHash,
                integratorId = user.IntegrationSystemId,
                permissions = (short) user.Permissions,
            };
        }
    }
}
