using System.ServiceModel;
using System.ServiceModel.Description;

namespace LinkMe.Framework.Utility.Wcf
{
    public class WcfTcpChannelManager<TChannel>
        : IChannelManager<TChannel>
    {
        private readonly ChannelFactory<TChannel> _channelFactory;

        public WcfTcpChannelManager(string address, string bindingConfiguration, int maxItemsInObjectGraph)
        {
            var binding = new NetTcpBinding(bindingConfiguration);
            _channelFactory = new ChannelFactory<TChannel>(binding, new EndpointAddress(address));

            // Adjust MaxItemsInObjectGraph as queries could return more then 64K records.

            foreach (OperationDescription operation in _channelFactory.Endpoint.Contract.Operations)
            {
                var behavior = operation.Behaviors.Find<DataContractSerializerOperationBehavior>();
                if (behavior != null)
                    behavior.MaxItemsInObjectGraph = maxItemsInObjectGraph;
            }
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
