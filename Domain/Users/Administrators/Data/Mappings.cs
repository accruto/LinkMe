using LinkMe.Domain.Contacts;
using LinkMe.Domain.Contacts.Data;

namespace LinkMe.Domain.Users.Administrators.Data
{
    internal enum UserFlags
    {
        Disabled = 0x04,
    }

    internal partial class RegisteredUserEntity
        : IRegisteredUserEntity, IHaveEmailAddressEntity
    {
    }

    internal static class Mappings
    {
        public static Administrator Map(AdministratorEntity administratorEntity, RegisteredUserEntity registeredUserEntity)
        {
            var administrator = registeredUserEntity.MapTo<Administrator>();
            registeredUserEntity.MapTo(administrator);
            return administrator;
        }

        public static AdministratorEntity Map(this Administrator administrator)
        {
            var entity = new AdministratorEntity
            {
                id = administrator.Id,
                RegisteredUserEntity = new RegisteredUserEntity {id = administrator.Id},
            };
            administrator.MapTo(entity);
            return entity;
        }

        public static void MapTo(this Administrator administrator, AdministratorEntity entity)
        {
            administrator.MapTo(entity.RegisteredUserEntity);
            administrator.MapTo((IHaveEmailAddressEntity)entity.RegisteredUserEntity);
        }
    }
}