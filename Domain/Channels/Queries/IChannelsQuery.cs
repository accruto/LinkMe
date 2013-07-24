using System;

namespace LinkMe.Domain.Channels.Queries
{
    public interface IChannelsQuery
    {
        Channel GetChannel(string name);
        ChannelApp GetChannelApp(Guid channelId, string name);
    }
}
