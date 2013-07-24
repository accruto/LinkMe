using LinkMe.Domain.Channels;

namespace LinkMe.Apps.Agents.Context
{
    public interface IChannelContext
        : ISubActivityContext
    {
        ChannelApp App { get; }
    }

    public class ChannelContext
        : IChannelContext
    {
        private readonly ChannelApp _app;

        public ChannelContext(ChannelApp app)
        {
            _app = app;
        }

        ChannelApp IChannelContext.App
        {
            get { return _app; }
        }
    }
}