using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Html;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using Microsoft.Practices.Unity;

namespace LinkMe.Web.Views
{
    public class VerticalViewEngine
        : CacheViewEngine
    {
        private readonly IVerticalsQuery _verticalsQuery;

        public VerticalViewEngine(IUnityContainer container, IVerticalsQuery verticalsQuery)
            : base(container)
        {
            _verticalsQuery = verticalsQuery;
        }

        protected override IList<FindViewName> GetFindViewNames(ControllerContext controllerContext, string viewName)
        {
            var vertical = GetVertical(controllerContext);
            if (vertical == null)
                return null;

            // The Not- prefix indicates that for this view no vertical-specific content should be used, including
            // anything found under the Shared folder.

            return new[]
            {
                new FindViewName { Name = "Verticals/" + vertical.Url + "/Not-" + viewName, Use = false },
                new FindViewName { Name = "Verticals/" + vertical.Url + "/" + viewName, Use = true },
                new FindViewName { Name = "Verticals/Shared/" + viewName, Use = true },
            };
        }

        private Vertical GetVertical(ControllerContext controllerContext)
        {
            // Two ways that the vertical can be specified, either in the view data.

            if (controllerContext is ViewContext)
            {
                var vertical = GetVertical(((ViewContext)controllerContext).ViewData);
                if (vertical != null)
                    return vertical;
            }

            // Or from the context.

            return GetVertical(controllerContext.Controller.ViewData.GetActivityContext());
        }

        private Vertical GetVertical(ActivityContext activityContext)
        {
            if (activityContext == null)
                return null;

            var verticalId = activityContext.Vertical.Id;
            return verticalId == null ? null : GetVertical(verticalId.Value);
        }

        private Vertical GetVertical(IDictionary<string, object> viewData)
        {
            if (!viewData.ContainsKey(PartialViews.VerticalIdKey))
                return null;

            var data = viewData[PartialViews.VerticalIdKey];
            if (!(data is Guid))
                return null;

            return GetVertical((Guid) data);
        }

        private Vertical GetVertical(Guid verticalId)
        {
            // The vertical must have a url.

            var vertical = _verticalsQuery.GetVertical(verticalId);
            return vertical == null || string.IsNullOrEmpty(vertical.Url)
                ? null
                : vertical;
        }
    }
}