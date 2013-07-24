using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.Practices.Unity;

namespace LinkMe.Framework.Utility.Unity.ServiceModel
{
    public class UnityServiceBehavior : IServiceBehavior
    {
        private readonly IUnityContainer _container;

        public UnityServiceBehavior(IUnityContainer container)
        {
            _container = container;
        }

        #region IServiceBehavior Members

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcherBase cdb in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher cd = cdb as ChannelDispatcher;

                if (cd != null)
                {
                    foreach (EndpointDispatcher ed in cd.Endpoints)
                    {
                        ed.DispatchRuntime.InstanceProvider =
                            new UnityInstanceProvider(_container, serviceDescription.ServiceType);
                    }
                }
            }
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
                Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        { }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) 
        { }

        #endregion
    }
}
