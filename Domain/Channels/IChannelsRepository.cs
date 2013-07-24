using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Channels
{
    public interface IChannelsRepository
    {
        IList<Channel> GetChannels();
        IList<ChannelApp> GetChannelApps(Guid channelId);
    }
}
