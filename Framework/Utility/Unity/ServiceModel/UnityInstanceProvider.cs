using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Microsoft.Practices.Unity;

namespace LinkMe.Framework.Utility.Unity.ServiceModel
{
    public class UnityInstanceProvider : IInstanceProvider
    {
        private readonly IUnityContainer _container;
        private readonly Type _serviceType;
 
        #region Constructors
 
        public UnityInstanceProvider(IUnityContainer container, Type serviceType)
        {
            _container = container;
            _serviceType = serviceType;
        } 
 
        #endregion
 
        #region IInstanceProvider Members
 
        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }
 
        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return _container.Resolve(_serviceType);
        }
 
        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            _container.Teardown(instance);
        }
 
        #endregion
    }
}
