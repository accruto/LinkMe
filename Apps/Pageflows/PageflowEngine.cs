using System;
using System.Collections.Concurrent;
using System.Web;

namespace LinkMe.Apps.Pageflows
{
    public class PageflowEngine
        : IPageflowEngine
    {
        private const string PageflowsKey = "Pageflows";

        TPageflow IPageflowEngine.GetPageflow<TPageflow>(Guid instanceId)
        {
            var pageflows = GetPageflows();

            Pageflow pageflow;
            if (pageflows.TryGetValue(instanceId, out pageflow))
            {
                var tpageflow = pageflow as TPageflow;
                if (tpageflow != null)
                    return tpageflow;
            }

            return null;
        }

        void IPageflowEngine.StartPageflow<TPageflow>(TPageflow pageflow)
        {
            // Store the pageflow.

            var pageflows = GetPageflows();
            pageflows[pageflow.Id] = pageflow;

            // Move to the first step.

            ((IPageflow)pageflow).MoveFirst();
        }

        void IPageflowEngine.StopPageflow<TPageflow>(TPageflow pageflow)
        {
            var pageflows = GetPageflows();
            Pageflow tryPageflow;
            pageflows.TryRemove(pageflow.Id, out tryPageflow);
        }

        IPageflowStep IPageflowEngine.MoveToNextStep(Pageflow pageflow)
        {
            ((IPageflow) pageflow).MoveToNext();
            return pageflow.CurrentStep;
        }

        IPageflowStep IPageflowEngine.MoveToPreviousStep(Pageflow pageflow)
        {
            ((IPageflow)pageflow).MoveToPrevious();
            return pageflow.CurrentStep;
        }

        IPageflowStep IPageflowEngine.MoveToStep(Pageflow pageflow, string step)
        {
            ((IPageflow)pageflow).MoveTo(step);
            return pageflow.CurrentStep;
        }

        private static ConcurrentDictionary<Guid, Pageflow> GetPageflows()
        {
            var pageflows = HttpContext.Current.Session[PageflowsKey] as ConcurrentDictionary<Guid, Pageflow>;
            if (pageflows == null)
            {
                pageflows = new ConcurrentDictionary<Guid, Pageflow>();
                HttpContext.Current.Session[PageflowsKey] = pageflows;
            }

            return pageflows;
        }
    }
}
