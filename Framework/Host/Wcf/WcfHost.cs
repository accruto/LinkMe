using System;
using System.Linq;
using System.ServiceModel;
using LinkMe.Framework.Utility.Wcf;

namespace LinkMe.Framework.Host.Wcf
{
    public class WcfHost<TBindingFactory>
        where TBindingFactory : BindingFactory, new()
    {
        private ServiceHost[] _serviceHosts;

        public IChannelAware Application { get; set; }
        public ServiceDefinition[] ServiceDefinitions { get; set; }

        public void Open()
        {
            if (Application != null)
                Application.OnOpen();

            BuildServiceHosts();

            foreach (var service in _serviceHosts.Select(host => host.SingletonInstance).OfType<IChannelAware>())
                service.OnOpen();
        }

        public void Close()
        {
            if (Application != null)
                Application.OnClose();

            foreach (var service in _serviceHosts.Select(host => host.SingletonInstance).OfType<IChannelAware>())
                service.OnClose();
        }

        public  void Start()
        {
            if (Application != null)
                Application.OnStart();

            foreach (var service in _serviceHosts.Select(host => host.SingletonInstance).OfType<IChannelAware>())
                service.OnStart();

            foreach (var host in _serviceHosts)
                host.Open();
        }

        public void Stop()
        {
            if (Application != null)
                Application.OnStop();

            foreach (var host in _serviceHosts)
                host.Close();

            foreach (var service in _serviceHosts.Select(host => host.SingletonInstance).OfType<IChannelAware>())
                service.OnStop();
        }

        private void BuildServiceHosts()
        {
            if (ServiceDefinitions == null || ServiceDefinitions.Length == 0)
                throw new InvalidOperationException("At least one servcie must be specified using the 'ServiceDefinitions' property.");

            _serviceHosts = new ServiceHost[ServiceDefinitions.Length];

            for (int i = 0; i < ServiceDefinitions.Length; i++)
            {
                var definition = ServiceDefinitions[i];
                object service = definition.Service;

                Type contract = FindContract(service);
                if (contract == null)
                    throw new ApplicationException(string.Format("The '{0}' service is not derived from the ServiceContract interface", service.GetType()));

                var host = _serviceHosts[i] = new ServiceHost(service);

                var binding = new TBindingFactory().CreateBinding(definition.BindingName);

                host.AddServiceEndpoint(contract, binding, definition.Address);
            }
        }

        private static Type FindContract(object service)
        {
            foreach (var iface in service.GetType().GetInterfaces())
            {
                object[] attributes = iface.GetCustomAttributes(typeof(ServiceContractAttribute), true);
                if (attributes.Length != 0)
                    return iface;
            }

            return null;
        }
    }
}
