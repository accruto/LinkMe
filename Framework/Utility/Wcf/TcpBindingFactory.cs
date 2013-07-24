using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace LinkMe.Framework.Utility.Wcf
{
    public class TcpBindingFactory
        : BindingFactory
    {
        public override Binding CreateBinding(string configurationName)
        {
            return string.IsNullOrEmpty(configurationName)
                       ? new NetTcpBinding()
                       : new NetTcpBinding(configurationName);
        }
    }
}
