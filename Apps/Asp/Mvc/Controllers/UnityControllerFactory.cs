using System;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace LinkMe.Apps.Asp.Mvc.Controllers
{
    public class UnityControllerFactory
        : DefaultControllerFactory
    {
        private readonly IUnityContainer _container;
        private readonly UnityActionInvoker _actionInvoker;

        public UnityControllerFactory(IUnityContainer container)
        {
            _container = container;
            _actionInvoker = new UnityActionInvoker(container);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            IController controller;
            if (controllerType == null)
                controller = base.GetControllerInstance(requestContext, controllerType);
            else
                controller = (IController)_container.Resolve(controllerType);

            // Install an action invoker that is also aware of unity.

            var controllerBase = controller as ViewController;
            if (controllerBase != null)
                controllerBase.ActionInvoker = _actionInvoker;

            return controller;
        }
    }
}
