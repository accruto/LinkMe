using System;
using System.ServiceModel.Channels;

namespace LinkMe.Framework.Utility.Wcf
{
    public abstract class BindingFactory
    {
        public abstract Binding CreateBinding(string configurationName);
    }
}