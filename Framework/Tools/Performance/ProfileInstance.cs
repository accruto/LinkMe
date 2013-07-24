using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Framework.Tools.Performance
{
    internal class ProfileInstance
    {
        private readonly Profile _profile;
        private readonly int _user;
        private readonly int _totalUsers;
        private readonly ProfileCounters _counters;
        private readonly int _iteration;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly StepTimer _stepTimer = new StepTimer();
        private readonly IProfileTestFixture _testFixture;

        public ProfileInstance(Profile profile, ProfileCounters counters, int user, int totalUsers)
        {
            _profile = profile;
            _user = user;
            _totalUsers = totalUsers;
            _counters = counters;
            _iteration = counters.NextIteration;

            // Create the test fixture to run the profile.

            _testFixture = Container.Current.Resolve<IProfileTestFixture>(_profile.TestFixture);

            // Create the step hierarchy.

            var rootStep = new AndStepInstance(this, null, new AndStep());
            CreateChildStepInstances(rootStep, profile.Steps, profile, counters);
            CurrentStep = rootStep;
        }

        public Profile Profile
        {
            get { return _profile; }
        }

        public ProfileCounters Counters
        {
            get { return _counters; } 
        }

        public StepTimer StepTimer
        {
            get { return _stepTimer; }
        }

        public int User
        {
            get { return _user; }
        }

        public int TotalUsers
        {
            get { return _totalUsers; }
        }

        public object TestFixture
        {
            get { return _testFixture; }
        }

        public int Iteration
        {
            get { return _iteration; }
        }

        public StepInstance CurrentStep { get; set; }

        public void StartTracking()
        {
            _counters.StartTrackingProfile();
            _stopwatch.Start();
        }

        public void StopTracking()
        {
            _stopwatch.Stop();
            _counters.StopTrackingProfile(_stopwatch.ElapsedTicks, _stopwatch.ElapsedMilliseconds);
        }

        public void TrackError()
        {
            _counters.TrackProfileError();
        }

        public void SetUp()
        {
            // Create and set up the component for this iteration.

            _testFixture.SetUpIteration(_user, _iteration, _stepTimer);
        }

        public void TearDown()
        {
            _testFixture.TearDownIteration();
        }

        private void CreateChildStepInstances(StepInstanceList stepInstance, IEnumerable<Step> steps, Profile profile, ProfileCounters counters)
        {
            foreach (var step in steps)
            {
                var childStepInstance = CreateStepInstance(profile, counters, stepInstance, step);
                if (stepInstance != null)
                    stepInstance.Add(childStepInstance);
            }
        }

        private StepInstance CreateStepInstance(Profile profile, ProfileCounters counters, StepInstance parentStepInstance, Step step)
        {
            if (step is ComponentStep)
                return CreateStepInstance(counters, parentStepInstance, (ComponentStep)step);
            if (step is OrStep)
                return CreateStepInstance(profile, counters, parentStepInstance, (OrStep)step);
            if (step is AndStep)
                return CreateStepInstance(profile, counters, parentStepInstance, (AndStep)step);
            return null;
        }

        private StepInstance CreateStepInstance(ProfileCounters counters, StepInstance parentStepInstance, ComponentStep step)
        {
            // Look for the method.

            MethodInfo endMethod = null;
            var beginMethod = _testFixture.GetType().GetMethod(step.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.ExactBinding);
            if (beginMethod == null)
            {
                // Look for a Begin-End pair.

                beginMethod = _testFixture.GetType().GetMethod("Begin" + step.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.ExactBinding);
                if (beginMethod == null)
                    throw new ApplicationException("Method '" + step.Name + "' not found on profile '" + _testFixture.GetType().FullName + ".");

                endMethod = _testFixture.GetType().GetMethod("End" + step.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.ExactBinding);
            }

            return new ComponentStepInstance(this, parentStepInstance, step, beginMethod, endMethod, counters.StepCounters[step.Name]);
        }

        private StepInstance CreateStepInstance(Profile profile, ProfileCounters counters, StepInstance parentStepInstance, OrStep step)
        {
            var stepInstance = new OrStepInstance(this, parentStepInstance, step);
            CreateChildStepInstances(stepInstance, step, profile, counters);
            return stepInstance;
        }

        private StepInstance CreateStepInstance(Profile profile, ProfileCounters counters, StepInstance parentStepInstance, AndStep step)
        {
            var stepInstance = new AndStepInstance(this, parentStepInstance, step);
            CreateChildStepInstances(stepInstance, step, profile, counters);
            return stepInstance;
        }
    }
}
