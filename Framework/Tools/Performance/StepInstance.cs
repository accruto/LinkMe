using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using LinkMe.Framework.Configuration.Exceptions;

namespace LinkMe.Framework.Tools.Performance
{
    internal abstract class StepInstance
    {
        private readonly ProfileInstance _profileInstance;
        private readonly StepInstance _parentStepInstance;
        private readonly bool _enabled;
        private readonly int _iterations;
        private int _currentIteration;

        protected StepInstance(ProfileInstance profileInstance, StepInstance parentStepInstance, Step step)
        {
            _profileInstance = profileInstance;
            _parentStepInstance = parentStepInstance;
            _enabled = step.Enabled;
            _iterations = step.Iterations.Next;
        }

        public ProfileInstance ProfileInstance
        {
            get { return _profileInstance; }
        }

        public StepInstance ParentStep
        {
            get { return _parentStepInstance; }
        }

        public bool Enabled
        {
            get { return _enabled; }
        }

        public Timer Timer { get; set; }

        public int Iterations
        {
            get { return _iterations; }
        }

        protected bool CheckCurrentIteration(out ComponentStepInstance nextStep)
        {
            // Check iterations.

            if (_currentIteration >= _iterations)
            {
                _currentIteration = 0;
                nextStep = ParentStep == null ? null : ParentStep.GetNextStep();
                return true;
            }
            
            nextStep = null;
            return false;
        }

        protected void StartNextIteration()
        {
            ++_currentIteration;
        }

        public abstract ComponentStepInstance GetNextStep();
    }

    internal abstract class StepInstanceList
        : StepInstance
    {
        protected StepInstanceList(ProfileInstance profileInstance, StepInstance parentStepInstance, Step step)
            : base(profileInstance, parentStepInstance, step)
        {
        }

        public void Add(StepInstance stepInstance)
        {
            _stepInstances.Add(stepInstance);
        }

        protected int Count
        {
            get { return _stepInstances.Count; }
        }

        protected StepInstance this[int index]
        {
            get { return _stepInstances[index]; }
        }

        private readonly StepInstances _stepInstances = new StepInstances();
    }

    internal class ComponentStepInstance
        : StepInstance
    {
        private readonly string _name;
        private readonly MethodInfo _beginMethodInfo;
        private readonly MethodInfo _endMethodInfo;
        private readonly Delay _delay;
        private readonly StepCounters _counters;

        public ComponentStepInstance(ProfileInstance profileInstance, StepInstance parentStepInstance, ComponentStep step, MethodInfo beginMethodInfo, MethodInfo endMethodInfo, StepCounters counters)
            : base(profileInstance, parentStepInstance, step)
        {
            _name = step.Name;
            _beginMethodInfo = beginMethodInfo;
            _endMethodInfo = endMethodInfo;
            _delay = step.Delay;
            _counters = counters;
        }

        public string Name
        {
            get { return _name; }
        }

        public MethodInfo BeginMethod
        {
            get { return _beginMethodInfo; }
        }

        public MethodInfo EndMethod
        {
            get { return _endMethodInfo; }
        }

        public int GetDelay()
        {
            return _delay.Next;
        }

        public void StartTracking()
        {
            _counters.StartTrackingStep();
        }

        public long StopTracking()
        {
            var elapsedTicks = ProfileInstance.StepTimer.ElapsedTicks;
            var elapsedMilliseconds = ProfileInstance.StepTimer.ElapsedMilliseconds;
            _counters.StopTrackingStep(elapsedTicks, elapsedMilliseconds);
            return elapsedMilliseconds;
        }

        public void TrackError()
        {
            _counters.TrackStepError();
        }

        public override ComponentStepInstance GetNextStep()
        {
            ComponentStepInstance nextStep;
            if (CheckCurrentIteration(out nextStep))
                return nextStep;

            StartNextIteration();

            // Check whether it is enabled.

            return Enabled ? this : GetNextStep();
        }
    }

    internal class OrStepInstance
        : StepInstanceList
    {
        private bool _alreadyRun;
        private readonly IList<int> _percentages = new List<int>();
        private readonly Random _random = new Random();

        public OrStepInstance(ProfileInstance profileInstance, StepInstance parentStepInstance, OrStep step)
            : base(profileInstance, parentStepInstance, step)
        {
            const string method = ".ctor";

            var currentPercentage = 0;
            var enabledSteps = 0;

            foreach (var childStep in step)
            {
                currentPercentage += childStep.Percentage * 100;
                if (childStep.Enabled)
                    ++enabledSteps;

                _percentages.Add(currentPercentage);
            }

            // Check the percentages.

            if (currentPercentage != 10000)
                throw new InvalidConfigurationValueException(GetType(), method, "percentage", currentPercentage / 100);
            if (enabledSteps == 0)
                throw new InvalidConfigurationValueException(GetType(), method, "enabled", false);
        }

        public override ComponentStepInstance GetNextStep()
        {
            ComponentStepInstance nextStep;
            if (CheckCurrentIteration(out nextStep))
                return nextStep;

            if (_alreadyRun)
            {
                // Go again.

                StartNextIteration();
                _alreadyRun = false;
                return GetNextStep();
            }

            // Need to choose a step to run for this instance.
            // Keep going until an enabled step is chosen, there should be at least one.

            for (; ; )
            {
                var percentage = _random.Next(0, 10000);
                var index = 0;
                for (; index < _percentages.Count; ++index)
                {
                    var data = _percentages[index];
                    if (percentage < data)
                    {
                        // Create an instance.

                        var childStep = this[index];
                        if (childStep.Enabled)
                        {
                            _alreadyRun = true;
                            return childStep.GetNextStep();
                        }

                        // Not enabled so try again.

                        break;
                    }
                }
            }
        }
    }

    internal class AndStepInstance
        : StepInstanceList
    {
        private int _currentStep;

        public AndStepInstance(ProfileInstance profileInstance, StepInstance parentStepInstance, AndStep step)
            : base(profileInstance, parentStepInstance, step)
        {
        }

        public override ComponentStepInstance GetNextStep()
        {
            ComponentStepInstance nextStep;
            if (CheckCurrentIteration(out nextStep))
                return nextStep;

            if (_currentStep >= Count)
            {
                // Go again.

                StartNextIteration();
                _currentStep = 0;
                return GetNextStep();
            }

            var childStep = this[_currentStep++];

            if (childStep.Enabled)
                return childStep.GetNextStep();

            // The next step is not enabled so go again.

            return GetNextStep();
        }
    }

    internal class StepInstances
        : IEnumerable<StepInstance>
    {
        private readonly IList<StepInstance> _stepInstances = new List<StepInstance>();

        public void Add(StepInstance stepInstance)
        {
            _stepInstances.Add(stepInstance);
        }

        public int Count
        {
            get { return _stepInstances.Count; }
        }

        public StepInstance this[int index]
        {
            get { return _stepInstances[index]; }
        }

        IEnumerator<StepInstance> IEnumerable<StepInstance>.GetEnumerator()
        {
            return _stepInstances.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _stepInstances.GetEnumerator();
        }
    }
}
