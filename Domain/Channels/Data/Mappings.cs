using System;

namespace LinkMe.Domain.Channels.Data
{
    internal static class Mappings
    {
        public static Channel Map(this ChannelEntity entity)
        {
            return new Channel
            {
                Id = entity.id,
                Name = entity.name,
            };
        }

        public static ChannelApp Map(this AppEntity entity, Guid channelId)
        {
            return new ChannelApp
            {
                Id = entity.id,
                Name = entity.name,
                ChannelId = channelId,
            };
        }
    }
}
