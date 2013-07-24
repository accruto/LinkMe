using System;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Apps.Agents.Context
{
    public interface ISubActivityContext
    {
    }

    public sealed class ActivityContext
    {
        private static readonly object CurrentLock = new object();
        private static ActivityContext _current;

        private IVerticalContext _verticalContext = new DefaultVerticalContext();
        private ICommunityContext _communityContext = new DefaultCommunityContext();
        private ILocationContext _locationContext = new DefaultLocationContext();
        private IChannelContext _channelContext = new ChannelContext(new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() });

        private ActivityContext()
        {
        }

        public static ActivityContext Current
        {
            get
            {
                if (_current == null)
                {
                    lock (CurrentLock)
                    {
                        if (_current == null)
                        {
                            _current = new ActivityContext();
                        }
                    }
                }

                return _current;
            }
        }

        public void Register(ISubActivityContext context)
        {
            if (context is IVerticalContext)
                _verticalContext = (IVerticalContext)context;
            else if (context is ICommunityContext)
                _communityContext = (ICommunityContext)context;
            else if (context is ILocationContext)
                _locationContext = (ILocationContext) context;
            else if (context is IChannelContext)
                _channelContext = (IChannelContext) context;
        }

        public IVerticalContext Vertical
        {
            get { return _verticalContext; }
        }

        public ICommunityContext Community
        {
            get { return _communityContext; }
        }

        public ILocationContext Location
        {
            get { return _locationContext; }
        }

        public IChannelContext Channel
        {
            get { return _channelContext; }
        }

        public void Set(Vertical vertical)
        {
            _verticalContext.Set(vertical);

            // Set the community context.

            var community = Container.Current.Resolve<ICommunitiesQuery>().GetCommunity(vertical.Id);
            if (community != null)
                _communityContext.Set(community);

            // Set the location context.

            if (vertical.CountryId != null)
            {
                var country = Container.Current.Resolve<ILocationQuery>().GetCountry(vertical.CountryId.Value);
                if (country != null)
                    _locationContext.Country = country;
            }
        }

        public void Reset()
        {
            // Reset the contexts.

            _verticalContext.Reset();
            _communityContext.Reset();
            _locationContext.Reset();
        }
    }
}