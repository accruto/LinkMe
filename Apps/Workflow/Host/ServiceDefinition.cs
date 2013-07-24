using System;

namespace LinkMe.Workflow.Host
{
    public class ServiceDefinition
    {
        public object Service { get; set; }
        public string Address { get; set; }
        public string BindingConfiguration { get; set; }
    }
}
