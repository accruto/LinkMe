using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Domain.Channels.Data
{
    public class ChannelsRepository
        : Repository, IChannelsRepository
    {
        private static readonly Func<ChannelsDataContext, IQueryable<Channel>> GetChannels
            = CompiledQuery.Compile((ChannelsDataContext dc)
                => from c in dc.ChannelEntities
                   select c.Map());

        private static readonly Func<ChannelsDataContext, Guid, IQueryable<ChannelApp>> GetChannelApps
            = CompiledQuery.Compile((ChannelsDataContext dc, Guid channelId)
                => from a in dc.AppEntities
                   join ac in dc.ChannelAppEntities on a.id equals ac.appId
                   where ac.channelId == channelId
                   select a.Map(channelId));

        public ChannelsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        IList<Channel> IChannelsRepository.GetChannels()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetChannels(dc).ToList();
            }
        }

        IList<ChannelApp> IChannelsRepository.GetChannelApps(Guid channelId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetChannelApps(dc, channelId).ToList();
            }
        }

        private ChannelsDataContext CreateContext()
        {
            return CreateContext(c => new ChannelsDataContext(c));
        }
    }
}
