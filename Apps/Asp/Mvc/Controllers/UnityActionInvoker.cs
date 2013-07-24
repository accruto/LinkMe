using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace LinkMe.Apps.Asp.Mvc.Controllers
{
    public class UnityActionInvoker
        : ControllerActionInvoker
    {
        private readonly IUnityContainer _container;
        
        public UnityActionInvoker(IUnityContainer container)
        {
            _container = container;
        }

        protected override AuthorizationContext InvokeAuthorizationFilters(ControllerContext controllerContext, IList<IAuthorizationFilter> filters, ActionDescriptor actionDescriptor)
        {
            // The filters are created by the framework so build up their dependencies before invoking them and the action.

            var injectedFilters = new List<IAuthorizationFilter>();
            foreach (var filter in filters)
            {
                var injectedFilter = _container.BuildUp(filter.GetType(), filter) as IAuthorizationFilter;
                injectedFilters.Add(injectedFilter ?? filter);
            }

            return base.InvokeAuthorizationFilters(controllerContext, injectedFilters, actionDescriptor);
        }
        
        protected override ActionExecutedContext InvokeActionMethodWithFilters(ControllerContext controllerContext, IList<IActionFilter> filters, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
        {
            // The filters are created by the framework so build up their dependencies before invoking them and the action.
            
            var injectedFilters = new List<IActionFilter>();
            foreach (var filter in filters)
            {
                var injectedFilter = _container.BuildUp(filter.GetType(), filter) as IActionFilter;
                injectedFilters.Add(injectedFilter ?? filter);
            }

            return base.InvokeActionMethodWithFilters(controllerContext, injectedFilters, actionDescriptor, parameters);
        }
    }
}
