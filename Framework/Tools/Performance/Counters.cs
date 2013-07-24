using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace LinkMe.Framework.Tools.Performance
{
    internal abstract class Counters
    {
        private int _iteration;

        public int NextIteration
        {
            get { return Interlocked.Increment(ref _iteration); }
        }

        protected static PerformanceCounter GetPerformanceCounter(string categoryName, string counterName, string instanceName)
        {
            const string runCommandMessage = "Run the following command to create it:"
                + " LinkMe.Framework.Environment.Util.exe /createPerf /i"
                + @" Tools\Performance\Config\LinkMe.Framework.Tools.Performance.Counters.config";

            try
            {
                return new PerformanceCounter(categoryName, counterName, instanceName, false);
            }
            catch (Exception ex)
            {
                if (!PerformanceCounterCategory.Exists(categoryName))
                {
                    throw new ApplicationException("Performance counter category '" + categoryName
                        + "' does not exist. " + runCommandMessage, ex);
                }
                if (!PerformanceCounterCategory.CounterExists(counterName, categoryName))
                {
                    throw new ApplicationException("Performance counter '" + counterName + "' in category '"
                        + categoryName + "' does not exist ." + runCommandMessage, ex);
                }

                throw new ApplicationException("Failed to get performance counter '" + counterName + "' in category '"
                    + categoryName + "'.", ex);
            }
        }
    }

    internal class ScenarioCounters
        : Counters
    {
        private readonly PerformanceCounter _totalUsers;
        private readonly PerformanceCounter _currentUsers;
        private readonly PerformanceCounter _totalCalls;
        private readonly PerformanceCounter _totalErrors;
        private readonly PerformanceCounter _callsPerSecond;

        public ScenarioCounters(string scenario, int totalUsers)
        {
            var instance = scenario;
            _totalUsers = GetPerformanceCounter(Constants.Counters.Scenario.Name, Constants.Counters.Scenario.TotalUsers, instance);
            _currentUsers = GetPerformanceCounter(Constants.Counters.Scenario.Name, Constants.Counters.Scenario.CurrentUsers, instance);
            _totalCalls = GetPerformanceCounter(Constants.Counters.Scenario.Name, Constants.Counters.Scenario.TotalCalls, instance);
            _totalErrors = GetPerformanceCounter(Constants.Counters.Scenario.Name, Constants.Counters.Scenario.TotalErrors, instance);
            _callsPerSecond = GetPerformanceCounter(Constants.Counters.Scenario.Name, Constants.Counters.Scenario.CallsPerSecond, instance);

            _totalUsers.RawValue = totalUsers;
            _currentUsers.RawValue = 0;
            _totalCalls.RawValue = 0;
            _totalErrors.RawValue = 0;
            _callsPerSecond.RawValue = 0;
        }

        public void StartTrackingUser()
        {
            _currentUsers.Increment();
        }

        public void StartTrackingProfile()
        {
        }

        public void StopTrackingProfile()
        {
        }

        public void TrackProfileError()
        {
        }

        public void StartTrackingStep()
        {
        }

        public void StopTrackingStep()
        {
            _totalCalls.Increment();
            _callsPerSecond.Increment();
        }

        public void TrackStepError()
        {
            _totalErrors.Increment();
        }
    }

    internal class ProfileStepCounters
    {
        private readonly IDictionary<string, StepCounters> _stepCounters = new Dictionary<string, StepCounters>();

        public StepCounters this[string name]
        {
            get { return _stepCounters[name]; }
            set { _stepCounters[name] = value; }
        }
    }

    internal class ProfileCounters
        : Counters
    {
        private readonly ScenarioCounters _scenarioCounters;
        private readonly PerformanceCounter _enabled;
        private readonly PerformanceCounter _currentlyRunning;
        private readonly PerformanceCounter _totalRuns;
        private readonly PerformanceCounter _totalErrors;
        private readonly PerformanceCounter _averageTime;
        private readonly PerformanceCounter _averageTimeBase;
        private readonly PerformanceCounter _lastTime;
        private readonly ProfileStepCounters _stepCounters = new ProfileStepCounters();

        public ProfileCounters(ScenarioCounters scenarioCounters, string name, bool enabled)
        {
            _scenarioCounters = scenarioCounters;

            _enabled = GetPerformanceCounter(Constants.Counters.Profile.Name, Constants.Counters.Profile.Enabled, name);
            _currentlyRunning = GetPerformanceCounter(Constants.Counters.Profile.Name, Constants.Counters.Profile.CurrentlyRunning, name);
            _totalRuns = GetPerformanceCounter(Constants.Counters.Profile.Name, Constants.Counters.Profile.TotalRuns, name);
            _totalErrors = GetPerformanceCounter(Constants.Counters.Profile.Name, Constants.Counters.Profile.TotalErrors, name);
            _averageTime = GetPerformanceCounter(Constants.Counters.Profile.Name, Constants.Counters.Profile.AverageExecutionTime, name);
            _averageTimeBase = GetPerformanceCounter(Constants.Counters.Profile.Name, Constants.Counters.Profile.AverageExecutionTimeBase, name);
            _lastTime = GetPerformanceCounter(Constants.Counters.Profile.Name, Constants.Counters.Profile.LastExecutionTime, name);

            _enabled.RawValue = enabled ? 1 : 0;
            _currentlyRunning.RawValue = 0;
            _totalRuns.RawValue = 0;
            _totalErrors.RawValue = 0;
            _averageTime.RawValue = 0;
            _averageTimeBase.RawValue = 0;
            _lastTime.RawValue = 0;
        }

        public void Add(string stepName, StepCounters stepCounters)
        {
            _stepCounters[stepName] = stepCounters;
        }

        public ProfileStepCounters StepCounters
        {
            get { return _stepCounters; }
        }

        public void StartTrackingProfile()
        {
            _currentlyRunning.Increment();

            _scenarioCounters.StartTrackingProfile();
        }

        public void StopTrackingProfile(long elapsedTicks, long elapsedMilliseconds)
        {
            _currentlyRunning.Decrement();
            _totalRuns.Increment();

            // Need to include the factor of 1000 to show milliseconds.

            _averageTime.IncrementBy(elapsedTicks * 1000);
            _averageTimeBase.Increment();

            _lastTime.RawValue = elapsedMilliseconds;

            _scenarioCounters.StopTrackingProfile();
        }

        public void TrackProfileError()
        {
            _currentlyRunning.Decrement();
            _totalErrors.Increment();

            _scenarioCounters.TrackProfileError();
        }

        public void StartTrackingStep()
        {
            _scenarioCounters.StartTrackingStep();
        }

        public void StopTrackingStep()
        {
            _scenarioCounters.StopTrackingStep();
        }

        public void TrackStepError()
        {
            _scenarioCounters.TrackStepError();
        }
    }

    internal class StepCounters
    : Counters
    {
        private readonly ProfileCounters _profileCounters;
        private readonly PerformanceCounter _enabled;
        private readonly PerformanceCounter _currentlyExecuting;
        private readonly PerformanceCounter _totalCalls;
        private readonly PerformanceCounter _totalErrors;
        private readonly PerformanceCounter _callsPerSecond;
        private readonly PerformanceCounter _averageTime;
        private readonly PerformanceCounter _averageTimeBase;
        private readonly PerformanceCounter _lastTime;

        public StepCounters(ProfileCounters profileCounters, string profile, string name, bool enabled)
        {
            _profileCounters = profileCounters;

            var instance = profile + ":" + name;
            _enabled = GetPerformanceCounter(Constants.Counters.Step.Name, Constants.Counters.Step.Enabled, instance);
            _currentlyExecuting = GetPerformanceCounter(Constants.Counters.Step.Name, Constants.Counters.Step.CurrentlyExecuting, instance);
            _totalCalls = GetPerformanceCounter(Constants.Counters.Step.Name, Constants.Counters.Step.TotalCalls, instance);
            _totalErrors = GetPerformanceCounter(Constants.Counters.Step.Name, Constants.Counters.Step.TotalErrors, instance);
            _callsPerSecond = GetPerformanceCounter(Constants.Counters.Step.Name, Constants.Counters.Step.CallsPerSecond, instance);
            _averageTime = GetPerformanceCounter(Constants.Counters.Step.Name, Constants.Counters.Step.AverageExecutionTime, instance);
            _averageTimeBase = GetPerformanceCounter(Constants.Counters.Step.Name, Constants.Counters.Step.AverageExecutionTimeBase, instance);
            _lastTime = GetPerformanceCounter(Constants.Counters.Step.Name, Constants.Counters.Step.LastExecutionTime, instance);

            _enabled.RawValue = enabled ? 1 : 0;
            _currentlyExecuting.RawValue = 0;
            _totalCalls.RawValue = 0;
            _totalErrors.RawValue = 0;
            _callsPerSecond.RawValue = 0;
            _averageTime.RawValue = 0;
            _averageTimeBase.RawValue = 0;
            _lastTime.RawValue = 0;
        }

        public void StartTrackingStep()
        {
            _currentlyExecuting.Increment();

            _profileCounters.StartTrackingStep();
        }

        public void StopTrackingStep(long elapsedTicks, long elapsedMilliseconds)
        {
            _currentlyExecuting.Decrement();
            _totalCalls.Increment();
            _callsPerSecond.Increment();
            _averageTime.IncrementBy(elapsedTicks * 1000);
            _averageTimeBase.Increment();
            _lastTime.RawValue = elapsedMilliseconds;

            _profileCounters.StopTrackingStep();
        }

        public void TrackStepError()
        {
            _currentlyExecuting.Decrement();
            _totalErrors.Increment();

            _profileCounters.TrackStepError();
        }
    }
}
