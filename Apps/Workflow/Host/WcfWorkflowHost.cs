using System;
using System.Linq;
using System.ServiceModel;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Utility.Configuration;
using IChannelAware = LinkMe.Framework.Host.IChannelAware;

namespace LinkMe.Workflow.Host
{
    public class WcfWorkflowHost
        : Framework.Host.IChannelSource
    {
        private ServiceHost[] _serviceHosts;

        public ServiceDefinition[] ServiceDefinitions { private get; set; }

        #region Implementation of IChannelSource

        public void Open()
        {
            ApplicationContext.SetupApplications(WebSite.LinkMe); // set up the application to point to the web site if needed

            Container.Current.BuildUp(this);
            BuildServiceHosts();

            foreach (var service in _serviceHosts.Select(host => host.SingletonInstance).OfType<IChannelAware>())
                service.OnOpen();
        }

        public void Close()
        {
            foreach (var service in _serviceHosts.Select(host => host.SingletonInstance).OfType<IChannelAware>())
                service.OnClose();
        }

        public void Start()
        {
            foreach (var service in _serviceHosts.Select(host => host.SingletonInstance).OfType<IChannelAware>())
                service.OnStart();

            foreach (var host in _serviceHosts)
                host.Open();
        }

        public void Stop()
        {
            foreach (var host in _serviceHosts)
                host.Close();

            foreach (var service in _serviceHosts.Select(host => host.SingletonInstance).OfType<IChannelAware>())
                service.OnStop();
        }

        public void Pause()
        {
            foreach (var service in _serviceHosts.Select(host => host.SingletonInstance).OfType<IChannelAware>())
                service.OnPause();
        }

        public void Continue()
        {
            foreach (var service in _serviceHosts.Select(host => host.SingletonInstance).OfType<IChannelAware>())
                service.OnContinue();
        }

        public void Shutdown()
        {
            foreach (var service in _serviceHosts.Select(host => host.SingletonInstance).OfType<IChannelAware>())
                service.OnShutdown();
        }

        #endregion

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

                var binding = string.IsNullOrEmpty(definition.BindingConfiguration)
                    ? new NetMsmqBinding()
                    : new NetMsmqBinding(definition.BindingConfiguration);

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
