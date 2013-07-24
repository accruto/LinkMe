using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using LinkMe.Framework.Configuration.Exceptions;
using LinkMe.Framework.Host;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Tools.Performance.Exceptions;
using LinkMe.Framework.Utility.Unity;
using Exception=System.Exception;

namespace LinkMe.Framework.Tools.Performance
{
    public class LoadRunner
        : IChannelSource
    {
        private class ProfileData
        {
            private readonly Profile _profile;
            private readonly ProfileCounters _counters;
            private readonly int _percentage;
            private int _iterations;

            public ProfileData(Profile profile, ProfileCounters counters, int percentage)
            {
                _profile = profile;
                _counters = counters;
                _percentage = percentage;
            }

            public Profile Profile
            {
                get { return _profile; }
            }

            public ProfileCounters Counters
            {
                get { return _counters; }
            }

            public int Percentage
            {
                get { return _percentage; }
            }

            public int IncrementIteration()
            {
                return Interlocked.Increment(ref _iterations);
            }
        }

        #region Private Fields

        private const int DefaultConnectionLimit = 100;
        private static readonly EventSource EventSource = new EventSource<LoadRunner>();
        private readonly int _users = 1;
        private readonly int _rampUp;
        private readonly int _connectionLimit;
        private readonly Profile[] _profiles;

        private ScenarioCounters _counters;
        private IList<ProfileData> _profileData;
        private IChannelSink _stepRunner;
        private Thread _thread;
        private ManualResetEvent _stopSignal;
        private ManualResetEvent _pauseSignal;
        private ManualResetEvent _continueSignal;
        private ManualResetEvent[] _pauseSignals;
        private ManualResetEvent[] _continueSignals;

        private int _outstandingRequests;
        private readonly Random _random = new Random((int) DateTime.Now.Ticks);

        public LoadRunner(int users, int rampUp, int connectionLimit, Profile[] profiles)
        {
            _connectionLimit = connectionLimit;
            _rampUp = rampUp;
            _users = users;
            _profiles = profiles;
        }

        #endregion

        #region IChannelSource Members

        void IChannelSource.Open()
        {
            const string method = "Open";

            _stopSignal = new ManualResetEvent(false);
            _pauseSignal = new ManualResetEvent(false);
            _continueSignal = new ManualResetEvent(true);
            _pauseSignals = new[] { _stopSignal, _pauseSignal };
            _continueSignals = new[] { _stopSignal, _continueSignal };

            _stepRunner = new StepRunner();

            _counters = new ScenarioCounters("test", _users);

            // Setup the details for each profile.

            _profileData = new List<ProfileData>();
            var currentPercentage = 0;
            var runnableProfiles = 0;

            foreach (var profile in _profiles)
            {
                var profileCounters = CreateCounters(_counters, profile);

                // The percentage can be specified down to 0.01% so multiply by 100.

                currentPercentage += profile.Percentage * 100;
                if (profile.Runnable)
                    ++runnableProfiles;

                _profileData.Add(new ProfileData(profile, profileCounters, currentPercentage));
            }

            // Check the percentages.

            if (currentPercentage != 10000)
                throw new InvalidConfigurationValueException(GetType(), method, "percentage", currentPercentage / 100);
            if (runnableProfiles == 0)
                throw new InvalidConfigurationValueException(GetType(), method, "enabled", false);

            _thread = new Thread(Run);
        }

        void IChannelSource.Start()
        {
            _stopSignal.Reset();
            _thread.Start();
        }

        void IChannelSource.Stop()
        {
            // Set the signal to give everything notice.

            _stopSignal.Set();

            // Make sure that the starting thread has finished.

            if (_thread.ThreadState != ThreadState.Unstarted && _thread.ThreadState != ThreadState.Stopped)
                _thread.Join(5000);

            // Make sure there are no outstanding requests, giving them 5 seconds to stop.
            // Because the requests may be out on the thread pool threads started by timers
            // need to do this here.

            for (var iterations = 0; iterations < 50; ++iterations)
            {
                if (_outstandingRequests == 0)
                    break;
                Thread.Sleep(100);
            }
        }

        void IChannelSource.Pause()
        {
            _continueSignal.Reset();
            _pauseSignal.Set();
        }

        void IChannelSource.Continue()
        {
            _pauseSignal.Reset();
            _continueSignal.Set();
        }

        void IChannelSource.Shutdown()
        {
        }

        void IChannelSource.Close()
        {
            _stopSignal.Close();
            _pauseSignal.Close();
            _continueSignal.Close();
        }

        #endregion

        private static ProfileCounters CreateCounters(ScenarioCounters scenarioCounters, Profile profile)
        {
            var counters = new ProfileCounters(scenarioCounters, profile.Name, profile.Enabled);
            foreach (var step in profile.Steps)
                AddCounters(counters, profile, step);
            return counters;
        }

        private static void AddCounters(ProfileCounters profileCounters, Profile profile, Step step)
        {
            var componentStep = step as ComponentStep;
            if (componentStep != null)
            {
                profileCounters.Add(componentStep.Name, new StepCounters(profileCounters, profile.Name, componentStep.Name, step.Enabled));
                return;
            }

            var stepList = step as StepList;
            if (stepList != null)
            {
                foreach (var childStep in stepList)
                    AddCounters(profileCounters, profile, childStep);
            }
        }

        private void Run()
        {
            const string method = "Run";

            // All set up is done on this thread so as not to block the control functions.

            try
            {
                SetUp();
                RampUp();
            }
            catch (Exception ex)
            {
                EventSource.Raise(Event.Error, method, "An exception occurred on the LoadRunner runner thread.", ex);
            }
        }

        private void SetUp()
        {
            // Accept all SSL certificates.

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            // Set the connection limit if set.

            ServicePointManager.DefaultConnectionLimit = _connectionLimit != 0 ? _connectionLimit : DefaultConnectionLimit;

            // Set up all profile runners before starting them. Do this
            // sequentially so that they can do their stuff without having to
            // worry too much about other profiles.

            foreach (var data in _profileData)
            {
                // Create a component and ask it to set up.

                var profile = data.Profile;
                if (profile.Runnable)
                    Container.Current.Resolve<IProfileTestFixture>(profile.TestFixture).SetUp(_users, profile);
            }
        }

        private void RampUp()
        {
            const string method = "RampUp";
            if (EventSource.IsEnabled(Event.MethodEnter))
                EventSource.Raise(Event.MethodEnter, method);

            // Start off all the users.

            var rampUpInterval = _rampUp == 0 ? 1000 : _rampUp * 1000 / _users;

            var stopping = false;
            for (var user = 0; user < _users && !stopping; ++user)
            {
                switch (WaitHandle.WaitAny(_pauseSignals, rampUpInterval))
                {
                    case 0:

                        // Stopping.

                        stopping = true;
                        break;

                    case 1:

                        // Paused, wait for it to start again.

                        switch (WaitHandle.WaitAny(_continueSignals))
                        {
                            case 0:

                                // Stopping.

                                stopping = true;
                                break;

                            case 1:

                                // Continuing.

                                break;
                        }

                        break;

                    case WaitHandle.WaitTimeout:

                        // Interval over, run another user.

                        _counters.StartTrackingUser();
                        StartIteration(user);
                        break;
                }
            }

            if (EventSource.IsEnabled(Event.MethodExit))
                EventSource.Raise(Event.MethodExit, method);
        }

        public void StartIteration(int user)
        {
            // Need to choose a profile to run for this instance.
            // Keep going until an enabled profile is chosen, there should be at least one.

            for (; ; )
            {
                var percentage = _random.Next(0, 10000);
                var index = 0;
                for (; index < _profileData.Count; ++index)
                {
                    var data = _profileData[index];
                    if (percentage < data.Percentage)
                    {
                        // Create an instance.

                        var profile = data.Profile;
                        if (profile.Runnable)
                        {
                            var instance = new ProfileInstance(profile, data.Counters, user, _users);

                            // Only start another iteration if no maximum has been specified or if that maximum has not yet been reached.

                            if (profile.Iterations == 0 || data.IncrementIteration() <= profile.Iterations)
                                StartIteration(instance);
                            return;
                        }

                        // Not enabled so try again.

                        break;
                    }
                }
            }
        }

        private void StartIteration(ProfileInstance instance)
        {
            const string method = "StartIteration";
            if (EventSource.IsEnabled(Event.FlowEnter))
                EventSource.Raise(Event.FlowEnter, method, "Starting profile '" + instance.Profile + "' for user " + instance.User + ".", Event.Arg("Profile", instance.Profile.Name), Event.Arg("User", instance.User));

            var stopping = false;
            switch (WaitHandle.WaitAny(_pauseSignals, 0))
            {
                case 0:

                    // Stopping.

                    stopping = true;
                    break;

                case 1:

                    // Paused, wait for it to start again.

                    switch (WaitHandle.WaitAny(_continueSignals))
                    {
                        case 0:

                            // Stopping.

                            stopping = true;
                            break;

                        case 1:

                            // Continuing.

                            break;
                    }

                    break;

                case WaitHandle.WaitTimeout:

                    break;
            }

            if (!stopping)
            {
                instance.SetUp();
                instance.StartTracking();
                Run(instance);
            }
        }

        private static void StopIteration(ProfileInstance instance)
        {
            const string method = "StopIteration";

            instance.StopTracking();
            instance.TearDown();

            if (EventSource.IsEnabled(Event.FlowExit))
                EventSource.Raise(Event.FlowExit, method, "Profile '" + instance.Profile + "' has ended for user " + instance.User + ".", Event.Arg("Profile", instance.Profile.Name), Event.Arg("User", instance.User));
        }

        private static void StopIteration(ProfileInstance instance, Exception ex)
        {
            const string method = "StopIteration";
            if (EventSource.IsEnabled(Event.Error))
                EventSource.Raise(Event.Error, method, "An error has occurred trying to run a step.", ex);

            instance.TrackError();
            instance.TearDown();
        }

        private void Run(ProfileInstance instance)
        {
            const string method = "Run";
            if (EventSource.IsEnabled(Event.MethodEnter))
                EventSource.Raise(Event.MethodEnter, method);

            for (; ; )
            {
                // Ask the current step for the next step.

                var nextStep = instance.CurrentStep.GetNextStep();
                if (nextStep == null)
                {
                    StopIteration(instance);

                    // All steps have been run, so start another.
                    
                    StartIteration(instance.User);
                    break;
                }

                if (!_stopSignal.WaitOne(0))
                {
                    if (EventSource.IsEnabled(Event.Trace))
                        EventSource.Raise(Event.Trace, method, "Next step for user.", Event.Arg("Profile", instance.Profile), Event.Arg("User", instance.User), Event.Arg("CurrentStep", instance.CurrentStep), Event.Arg("Step", nextStep.Name));

                    instance.CurrentStep = nextStep;
                    
                    // Always delay, even if it doesn't need to simply to make sure that
                    // another thread is used and stack overflow does not happen.
                    
                    Delay(nextStep);
                    break;
                }
            }

            if (EventSource.IsEnabled(Event.MethodExit))
                EventSource.Raise(Event.MethodExit, method);
        }

        private void Delay(ComponentStepInstance stepInstance)
        {
            const string method = "Delay";
            if (EventSource.IsEnabled(Event.MethodEnter))
                EventSource.Raise(Event.MethodEnter, method);

            // Create the timer first so it can be attached to the instance.

            var timer = new Timer(DelayCallback, stepInstance, Timeout.Infinite, Timeout.Infinite);
            stepInstance.Timer = timer;

            var delay = stepInstance.GetDelay();
            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, method, "Delaying before running the next step.", Event.Arg("Profile", stepInstance.ProfileInstance.Profile), Event.Arg("User", stepInstance.ProfileInstance.User), Event.Arg("Step", stepInstance.Name), Event.Arg("Delay", delay));

            // Now set it.

            Interlocked.Increment(ref _outstandingRequests);
            timer.Change(delay, Timeout.Infinite);

            if (EventSource.IsEnabled(Event.MethodExit))
                EventSource.Raise(Event.MethodExit, method);
        }

        private void DelayCallback(object state)
        {
            const string method = "DelayCallback";
            if (EventSource.IsEnabled(Event.MethodEnter))
                EventSource.Raise(Event.MethodEnter, method);

            var stepInstance = (StepInstance)state;

            // Clean up the timer.

            var timer = stepInstance.Timer;
            stepInstance.Timer = null;
            timer.Dispose();

            // Process the step now.

            if (!_stopSignal.WaitOne(0))
                Process(stepInstance);

            Interlocked.Decrement(ref _outstandingRequests);

            if (EventSource.IsEnabled(Event.MethodExit))
                EventSource.Raise(Event.MethodExit, method);
        }

        private void Process(StepInstance stepInstance)
        {
            const string method = "Process";
            if (EventSource.IsEnabled(Event.MethodEnter))
                EventSource.Raise(Event.MethodEnter, method);

            // Process the next step.

            Interlocked.Increment(ref _outstandingRequests);
            _stepRunner.BeginProcessRequest(stepInstance, ProcessCallback, stepInstance.ProfileInstance);

            if (EventSource.IsEnabled(Event.MethodExit))
                EventSource.Raise(Event.MethodExit, method);
        }

        private void ProcessCallback(IAsyncResult result)
        {
            const string method = "ProcessCallback";
            if (EventSource.IsEnabled(Event.MethodEnter))
                EventSource.Raise(Event.MethodEnter, method);

            // Let the channel clean up anything it needs to.

            var profileInstance = (ProfileInstance)result.AsyncState;

            try
            {
                _stepRunner.EndProcessRequest(result);

                // Run the next step.

                if (!_stopSignal.WaitOne(0))
                    Run(profileInstance);
            }
            catch (Exception ex)
            {
                StopIteration(profileInstance, ex);

                // Try to start off another instance for this user.

                if (!_stopSignal.WaitOne(0))
                {
                    if (ex.InnerException is StepFailedException)
                        StartIteration(((StepFailedException)ex.InnerException).User);
                }
            }

            Interlocked.Decrement(ref _outstandingRequests);

            if (EventSource.IsEnabled(Event.MethodExit))
                EventSource.Raise(Event.MethodExit, method);
        }
    }
}
