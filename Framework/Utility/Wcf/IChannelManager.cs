using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinkMe.Framework.Utility.Wcf
{
    public interface IChannelManager<TChannel>
    {
        TChannel Create();
        void Close(TChannel channel);
        void Abort(TChannel channel);
    }
}
