using System;

namespace LinkMe.Apps.Pageflows
{
    public interface IPageflowEngine
    {
        void StartPageflow<TPageflow>(TPageflow pageflow) where TPageflow : Pageflow;
        void StopPageflow<TPageflow>(TPageflow pageflow) where TPageflow : Pageflow;

        TPageflow GetPageflow<TPageflow>(Guid instanceId) where TPageflow : Pageflow;

        IPageflowStep MoveToNextStep(Pageflow pageflow);
        IPageflowStep MoveToPreviousStep(Pageflow pageflow);
        IPageflowStep MoveToStep(Pageflow pageflow, string step);

        //IList<PageflowStep> GetSteps(Pageflow pageflow);
        //PageflowStep GetStep(Pageflow pageflow, string name);
        //PageflowStep GetCurrentStep(Pageflow pageflow);
    }
}
