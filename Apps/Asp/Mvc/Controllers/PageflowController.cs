using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Apps.Pageflows;
using LinkMe.Framework.Utility.Linq;

namespace LinkMe.Apps.Asp.Mvc.Controllers
{
    public class PageflowRoute
    {
        private readonly string _step;
        private readonly RouteReference _routeReference;

        public PageflowRoute(string step, RouteReference routeReference)
        {
            _step = step;
            _routeReference = routeReference;
        }

        public string Step
        {
            get { return _step; }
        }

        public RouteReference RouteReference
        {
            get { return _routeReference; }
        }
    }

    public class PageflowRoutes
        : IEnumerable<PageflowRoute>
    {
        private readonly IDictionary<string, PageflowRoute> _routeMap = new Dictionary<string, PageflowRoute>();

        public void Add(PageflowRoute route)
        {
            _routeMap.Add(route.Step, route);
        }

        public PageflowRoute this[string step]
        {
            get
            {
                PageflowRoute route;
                _routeMap.TryGetValue(step, out route);
                return route;
            }
        }

        IEnumerator<PageflowRoute> IEnumerable<PageflowRoute>.GetEnumerator()
        {
            return _routeMap.Values.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return _routeMap.GetEnumerator();
        }
    }

    public abstract class PageflowController<TPageflow>
        : ViewController
        where TPageflow : Pageflow
    {
        private readonly PageflowRoutes _routes;
        private readonly IPageflowEngine _pageflowEngine;

        protected PageflowController(PageflowRoutes routes, IPageflowEngine pageflowEngine)
        {
            _routes = routes;
            _pageflowEngine = pageflowEngine;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            // Get the instanceId to identify the current page flow.

            var instanceId = GetInstanceIdValue();

            if (instanceId != null)
            {
                // Get the current state for the identified page flow.

                Pageflow = _pageflowEngine.GetPageflow<TPageflow>(instanceId.Value);
            }

            // If no state then start a page flow.

            if (Pageflow == null)
            {
                Pageflow = CreatePageflow();
                _pageflowEngine.StartPageflow(Pageflow);
            }

            // If the user is not at the right point in the flow then reset them.

            var currentStep = CurrentStep;
            if (filterContext.ActionDescriptor.ActionName != currentStep.Name)
            {
                var step = Pageflow.GetStep(filterContext.ActionDescriptor.ActionName);
                if (step != null && step.CanBeMovedTo)
                {
                    _pageflowEngine.MoveToStep(Pageflow, filterContext.ActionDescriptor.ActionName);
                }
                else
                {
                    // Propagate any query string.

                    var routeUrl = GetRoute(currentStep).RouteReference.GenerateUrl(new { instanceId = Pageflow.Id }).AsNonReadOnly();
                    var clientUrl = HttpContext.GetClientUrl().AsNonReadOnly();
                    if (clientUrl.QueryString != null)
                    {
                        clientUrl.QueryString.Remove("instanceId");
                        if (clientUrl.QueryString.Count > 0)
                            routeUrl.QueryString.Add(clientUrl.QueryString);
                    }
                    filterContext.Result = RedirectToUrl(routeUrl);
                }
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            // Set up the model.

            var viewResult = filterContext.Result as ViewResult;
            if (viewResult != null)
            {
                var model = viewResult.ViewData.Model as PageflowModel;
                if (model != null)
                {
                    model.InstanceId = Pageflow.Id;
                    model.Steps = GetSteps(filterContext.ActionDescriptor.ActionName);
                }
            }
        }

        protected ActionResult Next()
        {
            var step = _pageflowEngine.MoveToNextStep(Pageflow);
            return Step(step);
        }

        protected virtual ActionResult Step(IPageflowStep step)
        {
            return RedirectToRoute(GetRoute(step).RouteReference, new { instanceId = Pageflow.Id });
        }

        protected virtual ActionResult Step(string name)
        {
            _pageflowEngine.MoveToStep(Pageflow, name);
            return RedirectToRoute(GetRoute(Pageflow.GetStep(name)).RouteReference, new { instanceId = Pageflow.Id });
        }

        protected ActionResult Previous()
        {
            var step = _pageflowEngine.MoveToPreviousStep(Pageflow);
            return RedirectToRoute(GetRoute(step).RouteReference, new { instanceId = Pageflow.Id });
        }

        protected void CancelPageflow()
        {
            _pageflowEngine.StopPageflow(Pageflow);
        }

        protected IList<IPageflowStep> ActiveSteps
        {
            get { return Pageflow.ActiveSteps; }
        }

        protected IPageflowStep CurrentStep
        {
            get { return Pageflow.CurrentStep; }
        }

        protected TPageflow Pageflow { get; private set; }

        protected abstract TPageflow CreatePageflow();

        private Guid? GetInstanceIdValue()
        {
            var value = ValueProvider.GetValue("instanceId");
            if (value != null)
            {
                var rawValue = value.RawValue;
                var avalue = rawValue as string[];
                if (avalue != null)
                {
                    if (avalue.Length > 0)
                    {
                        Guid instanceId;
                        if (Guid.TryParse(avalue[0], out instanceId))
                            return instanceId;
                    }
                }
            }

            return null;
        }

        private PageflowRoute GetRoute(IPageflowStep step)
        {
            return _routes[step.Name];
        }

        private StepsModel GetSteps(string currentStep)
        {
            var steps = ActiveSteps;
            return new StepsModel
            {
                Steps = steps,
                CurrentStepIndex = steps.IndexOf(m => m.Name, currentStep)
            };
        }
    }
}
