using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Apps.Pageflows
{
    public abstract class Pageflow
        : IPageflow
    {
        private readonly Guid _id;
        private PageflowStep _currentStep;
        private IList<PageflowStep> _steps;
        private bool _isComplete;

        protected Pageflow()
        {
            _id = Guid.NewGuid();
        }

        public Guid Id
        {
            get { return _id; }
        }

        void IPageflow.MoveFirst()
        {
            _steps = GetSteps();
            _currentStep = _steps[0];
        }

        void IPageflow.MoveToNext()
        {
            _currentStep.HasExecuted = true;
            _currentStep = MoveToNext(_currentStep);
        }

        void IPageflow.MoveToPrevious()
        {
            _currentStep.HasExecuted = true;
            _currentStep = MoveToPrevious(_currentStep);
        }

        void IPageflow.MoveTo(string step)
        {
            _currentStep.HasExecuted = true;
            _currentStep = (from s in _steps where s.Name == step select s).Single();
        }

        public IPageflowStep CurrentStep
        {
            get
            {
                return new ReadOnlyPageflowStep
                {
                    Name = _currentStep.Name,
                    HasExecuted = _currentStep.HasExecuted,
                    CanBeMovedTo = true,
                };
            }
        }

        public IPageflowStep GetStep(string name)
        {
            return (from s in _steps
                    where s.Name == name
                    && s.IsEnabled
                    select new ReadOnlyPageflowStep
                    {
                        Name = s.Name,
                        CanBeMovedTo = CanBeMovedTo(s),
                        HasExecuted = s.HasExecuted,
                    }).SingleOrDefault();
        }

        public IList<IPageflowStep> ActiveSteps
        {
            get
            {
                return (from s in _steps
                        where s.IsEnabled
                        select (IPageflowStep)new ReadOnlyPageflowStep
                        {
                            Name = s.Name,
                            CanBeMovedTo = CanBeMovedTo(s),
                            HasExecuted = s.HasExecuted,
                        }).ToList();
            }
        }

        protected abstract IList<PageflowStep> GetSteps();
        protected abstract PageflowStep MoveToNext(PageflowStep currentStep);
        protected abstract PageflowStep MoveToPrevious(PageflowStep currentStep);

        protected void Complete()
        {
            _isComplete = true;
        }

        private bool CanBeMovedTo(PageflowStep step)
        {
            return !_isComplete
                && (step.Name == _currentStep.Name
                    || step.HasExecuted
                    || step.IsAvailable);
        }
    }
}
