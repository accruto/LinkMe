using System;

namespace LinkMe.Framework.Utility.Wcf
{
    public class LocalChannelManager<TChannel>
        : IChannelManager<TChannel>
    {
        private readonly TChannel _channel;

        public LocalChannelManager(TChannel channel)
        {
            _channel = channel;
        }

        #region Implementation of IChannelManager<TChannel>

        TChannel IChannelManager<TChannel>.Create()
        {
            return _channel;
        }

        void IChannelManager<TChannel>.Close(TChannel channel)
        { }

        void IChannelManager<TChannel>.Abort(TChannel channel)
        { }

        #endregion
    }
}
