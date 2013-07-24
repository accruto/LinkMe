using System;
using System.Collections.Generic;
using System.Workflow.Activities;

namespace LinkMe.Apps.Pageflows
{
    /*
    public class PageflowState
    {
        private readonly IDictionary<string, bool> _isEverAvailability = new Dictionary<string, bool>();
        private readonly IDictionary<string, bool> _isCurrentlyAvailability = new Dictionary<string, bool>();
        private readonly IDictionary<string, bool> _hasExecuted = new Dictionary<string, bool>();

        public PageflowState()
        {
            IsStopped = false;
        }

        public Guid InstanceId { get; set; }
        public bool IsStopped { get; private set; }

        public void Stop()
        {
            IsStopped = true;
        }

        public bool IsEverAvailable(string stepName)
        {
            return Get(_isEverAvailability, stepName, true);
        }

        public void IsEverAvailable(string stepName, bool value)
        {
            Set(_isEverAvailability, stepName, value);
        }

        public bool IsCurrentlyAvailable(string stepName)
        {
            return Get(_isCurrentlyAvailability, stepName, false);
        }

        public void IsCurrentlyAvailable(string stepName, bool value)
        {
            Set(_isCurrentlyAvailability, stepName, value);
        }

        public bool HasExecuted(string stepName)
        {
            return Get(_hasExecuted, stepName, false);
        }

        public void HasExecuted(string stepName, bool value)
        {
            Set(_hasExecuted, stepName, value);
        }

        public virtual void Prepare(StateMachineWorkflowActivity workflowInstance)
        {
        }

        private static bool Get(IDictionary<string, bool> availability, string stepName, bool defaultValue)
        {
            bool isAvailable;
            return availability.TryGetValue(stepName, out isAvailable)
                ? isAvailable
                : defaultValue;
        }

        private static void Set(IDictionary<string, bool> availability, string stepName, bool value)
        {
            availability[stepName] = value;
        }
    }
     */
}
