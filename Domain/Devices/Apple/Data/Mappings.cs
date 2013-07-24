using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Domain.Devices.Apple.Data
{
    public interface IUserAppleDeviceEntity
    {
        Guid id { get; }
        Guid ownerId { get; }
        string deviceToken { get; }
        bool active { get; }
    }

    internal partial class UserAppleDeviceEntity
        : IUserAppleDeviceEntity
    {
    }

    public static class Mappings
    {

        internal static IList<AppleDevice> Map(this IEnumerable<IUserAppleDeviceEntity> entities)
        {
            return (from e in entities
                    select e.Map()).ToList();
        }

        internal static AppleDevice Map(this IUserAppleDeviceEntity entity)
        {
            return new AppleDevice
            {
                Id = entity.id,
                OwnerId = entity.ownerId,
                DeviceToken = entity.deviceToken,
                Active = entity.active,
            };
        }

        internal static UserAppleDeviceEntity Map(this AppleDevice device)
        {
            var entity = new UserAppleDeviceEntity();
            device.MapTo(entity);

            return entity;
        }

        internal static void MapTo(this AppleDevice device, UserAppleDeviceEntity entity)
        {
            entity.id = device.Id;
            entity.ownerId = device.OwnerId;
            entity.deviceToken = device.DeviceToken;
            entity.active = device.Active;
        }
    }
}