using System;
using System.Linq;
using System.ServiceModel;
using LinkMe.Framework.Utility.Wcf;

namespace LinkMe.Framework.Host.Wcf
{
    public abstract class WcfSource<TBindingFactory>
        : IChannelSource
        where TBindingFactory : BindingFactory, new()
    {
        private ServiceHost[] _serviceHosts;
        private readonly ServiceDefinition[] _serviceDefinitions;
        private readonly IChannelAware _application;

        protected WcfSource(ServiceDefinition[] serviceDefinitions, IChannelAware application)
        {
            _serviceDefinitions = serviceDefinitions;
            _application = application;
        }

        protected WcfSource(ServiceDefinition[] serviceDefinitions)
        {
            _serviceDefinitions = serviceDefinitions;
        }

        #region Implementation of IChannelSource

        void IChannelSource.Open()
        {
            if (_application != null)
                _application.OnOpen();

            BuildServiceHosts();

            foreach (var service in _serviceHosts.Select(host => host.SingletonInstance).OfType<IChannelAware>())
                service.OnOpen();
        }

        void IChannelSource.Close()
        {
            if (_application != null)
                _application.OnClose();

            foreach (var service in _serviceHosts.Select(host => host.SingletonInstance).OfType<IChannelAware>())
                service.OnClose();
        }

        void IChannelSource.Start()
        {
            if (_application != null)
                _application.OnStart();

            foreach (var service in _serviceHosts.Select(host => host.SingletonInstance).OfType<IChannelAware>())
                service.OnStart();

            foreach (var host in _serviceHosts)
                host.Open();
        }

        void IChannelSource.Stop()
        {
            if (_application != null)
                _application.OnStop();

            foreach (var host in _serviceHosts)
                host.Close();

            foreach (var service in _serviceHosts.Select(host => host.SingletonInstance).OfType<IChannelAware>())
                service.OnStop();
        }

        void IChannelSource.Pause()
        {
            if (_application != null)
                _application.OnPause();

            foreach (var service in _serviceHosts.Select(host => host.SingletonInstance).OfType<IChannelAware>())
                service.OnPause();
        }

        void IChannelSource.Continue()
        {
            if (_application != null)
                _application.OnContinue();

            foreach (var service in _serviceHosts.Select(host => host.SingletonInstance).OfType<IChannelAware>())
                service.OnContinue();
        }

        void IChannelSource.Shutdown()
        {
            if (_application != null)
                _application.OnShutdown();

            foreach (var service in _serviceHosts.Select(host => host.SingletonInstance).OfType<IChannelAware>())
                service.OnShutdown();
        }

        #endregion

        private void BuildServiceHosts()
        {
            if (_serviceDefinitions == null || _serviceDefinitions.Length == 0)
                throw new InvalidOperationException("At least one servcie must be specified using the 'ServiceDefinitions' property.");

            _serviceHosts = new ServiceHost[_serviceDefinitions.Length];

            for (int i = 0; i < _serviceDefinitions.Length; i++)
            {
                var definition = _serviceDefinitions[i];
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
