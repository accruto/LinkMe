using System;
using System.ServiceModel;

namespace LinkMe.Framework.Utility.Wcf
{
    public class WcfMsmqChannelManager<TChannel>
        : IChannelManager<TChannel>
    {
        private readonly ChannelFactory<TChannel> _channelFactory;

        public WcfMsmqChannelManager(string address, string bindingConfiguration)
        {
            var binding = new NetMsmqBinding(bindingConfiguration);
            _channelFactory = new ChannelFactory<TChannel>(binding, new EndpointAddress(address));
        }

        #region Implementation of IChannelManager<TChannel>

        public TChannel Create()
        {
            return _channelFactory.CreateChannel();
        }

        public void Close(TChannel channel)
        {
            var clientChannel = channel as IClientChannel;
            if (clientChannel != null)
                clientChannel.Close();
        }

        public void Abort(TChannel channel)
        {
            var clientChannel = channel as IClientChannel;
            if (clientChannel != null)
                clientChannel.Abort();
        }

        #endregion
    }
}
