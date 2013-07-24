using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Domain.Channels.Queries
{
    public class ChannelsQuery
        : IChannelsQuery
    {
        private readonly IDictionary<string, Channel> _channelsByName;
        private readonly IDictionary<Guid, IDictionary<string, ChannelApp>> _channelApps;

        public ChannelsQuery(IChannelsRepository repository)
        {
            var channels = repository.GetChannels();
            _channelsByName = channels.ToDictionary(c => c.Name, c => c);
            _channelApps = channels.ToDictionary(c => c.Id, c => (IDictionary<string, ChannelApp>)repository.GetChannelApps(c.Id).ToDictionary(a => a.Name, a => a));
        }

        Channel IChannelsQuery.GetChannel(string name)
        {
            Channel channel;
            _channelsByName.TryGetValue(name, out channel);
            return channel;
        }

        ChannelApp IChannelsQuery.GetChannelApp(Guid channelId, string name)
        {
            IDictionary<string, ChannelApp> channelApps;
            _channelApps.TryGetValue(channelId, out channelApps);
            if (channelApps == null)
                return null;
            ChannelApp channelApp;
            channelApps.TryGetValue(name, out channelApp);
            return channelApp;
        }
    }
}
