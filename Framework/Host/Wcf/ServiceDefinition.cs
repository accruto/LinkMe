using System;

namespace LinkMe.Framework.Host.Wcf
{
    public class ServiceDefinition
    {
        public object Service { get; set; }
        public string Address { get; set; }
        public string BindingName { get; set; }
    }
}