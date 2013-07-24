using System;
using System.Collections.Generic;
using System.Threading;
using System.Workflow.ComponentModel;
using System.Workflow.Runtime;
using System.Workflow.Runtime.Hosting;

namespace LinkMe.Apps.Workflow.Test
{
    /// <summary>
    /// Provides in-memory workflow persistence. 
    /// Based on CustomPersistenceService sample from SDK.
    /// NOTE. Not suitable for production: leaks memory in _instanceStore.
    /// </summary>
    public class MemoryPersistenceService : WorkflowPersistenceService
    {
        public static readonly TimeSpan MaxInterval = new TimeSpan(30, 0, 0, 0);

        private readonly bool _unloadOnIdle;
        private readonly Dictionary<Guid, Timer> _instanceTimers = new Dictionary<Guid, Timer>();
        private readonly Dictionary<Guid, byte[]> _instanceStore = new Dictionary<Guid, byte[]>();

        public MemoryPersistenceService(bool unloadOnIdle)
        {
            _unloadOnIdle = unloadOnIdle;
        }

        // Save the workflow instance state at the point of persistence with option of locking the instance state if it is shared
        // across multiple runtimes or multiple phase instance updates
        protected override void SaveWorkflowInstanceState(Activity rootActivity, bool unlock)
        {
            // Save the workflow
            var contextId = (Guid)rootActivity.GetValue(Activity.ActivityContextGuidProperty);
            _instanceStore[contextId] = GetDefaultSerializedForm(rootActivity);

            // See when the next timer (Delay activity) for this workflow will expire
            var timers = (TimerEventSubscriptionCollection)rootActivity.GetValue(TimerEventSubscriptionCollection.TimerCollectionProperty);
            TimerEventSubscription subscription = timers.Peek();
            if (subscription != null)
            {
                // Set a system timer to automatically reload this workflow when its next timer expires
                TimeSpan timeDifference = subscription.ExpiresAt - DateTime.UtcNow;

                // check to make sure timeDifference is in legal range
                if (timeDifference > MaxInterval)
                    timeDifference = MaxInterval;
                else if (timeDifference < TimeSpan.Zero)
                    timeDifference = TimeSpan.Zero;

                _instanceTimers.Add(contextId, new Timer(
                    ReloadWorkflow,
                    subscription.WorkflowInstanceId,
                    timeDifference,
                    new TimeSpan(-1)));
            }
        }

        // Load workflow instance state.
        protected override Activity LoadWorkflowInstanceState(Guid instanceId)
        {
            byte[] workflowBytes = _instanceStore[instanceId];
            var activity = RestoreFromDefaultSerializedForm(workflowBytes, null);
            return activity;
        }

        // Unlock the workflow instance state.  
        // Instance state locking is necessary when multiple runtimes share instance persistence store
        protected override void UnlockWorkflowInstanceState(Activity state)
        {
            //File locking is not supported in this sample
        }

        // Save the completed activity state.
        protected override void SaveCompletedContextActivity(Activity activity)
        {
            var contextId = (Guid)activity.GetValue(Activity.ActivityContextGuidProperty);
            _instanceStore[contextId] = GetDefaultSerializedForm(activity);
        }

        // Load the completed activity state.
        protected override Activity LoadCompletedContextActivity(Guid activityId, Activity outerActivity)
        {
            byte[] workflowBytes = _instanceStore[activityId];
            Activity deserializedActivities = RestoreFromDefaultSerializedForm(workflowBytes, outerActivity);
            return deserializedActivities;

        }

        protected override bool UnloadOnIdle(Activity activity)
        {
            return _unloadOnIdle;
        }

        private void ReloadWorkflow(object timerData)
        {
            var instanceId = (Guid)timerData;

            Timer timer;
            if (_instanceTimers.TryGetValue(instanceId, out timer))
            {
                _instanceTimers.Remove(instanceId);
                timer.Dispose();
            }

            // Reload the workflow so that it will continue processing
            Runtime.GetWorkflow(instanceId);
        }
    }
}