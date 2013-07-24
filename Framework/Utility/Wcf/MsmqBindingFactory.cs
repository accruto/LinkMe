using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace LinkMe.Framework.Utility.Wcf
{
    public class MsmqBindingFactory
        : BindingFactory
    {
        public override Binding CreateBinding(string configurationName)
        {
            return string.IsNullOrEmpty(configurationName)
                       ? new NetMsmqBinding()
                       : new NetMsmqBinding(configurationName);
        }
    }
}