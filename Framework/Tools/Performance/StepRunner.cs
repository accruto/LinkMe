using System;
using LinkMe.Framework.Host;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Tools.Performance.Exceptions;
using Exception=System.Exception;

namespace LinkMe.Framework.Tools.Performance
{
    public class StepRunner
        : IChannelSink
    {
        private static readonly EventSource EventSource = new EventSource<StepRunner>();

        private class StepAsyncResult
            : AsyncResult, IAsyncTestResult
        {
            private readonly ComponentStepInstance _step;

            public StepAsyncResult(AsyncCallback callback, object asyncState, ComponentStepInstance step)
                : base(callback, asyncState)
            {
                _step = step;
            }

            public void SetComplete(bool completedSynchronously)
            {
                SetComplete(_step, null, completedSynchronously);
            }

            public void SetComplete(Exception ex, bool completedSynchronously)
            {
                SetComplete(_step, ex, completedSynchronously);
            }

            public ComponentStepInstance Step
            {
                get { return _step; }
            }
        }

        #region IChannelSink Members

        public IAsyncResult BeginProcessRequest(object message, AsyncCallback callback, object asyncState)
        {
            const string method = "BeginProcessRequest";

            var result = new StepAsyncResult(callback, asyncState, message as ComponentStepInstance);

            try
            {
                if (message is ComponentStepInstance)
                {
                    var step = (ComponentStepInstance)message;
                    if (EventSource.IsEnabled(Event.Flow))
                        EventSource.Raise(Event.Flow, method, "Running step '" + step.Name + "' in profile '" + step.ProfileInstance.Profile + "' for user " + step.ProfileInstance.User + ".", Event.Arg("Profile", step.ProfileInstance.Profile), Event.Arg("User", step.ProfileInstance.User), Event.Arg("Step", step.Name));

                    if (step.BeginMethod != null && step.EndMethod != null && step.ProfileInstance.TestFixture is IAsyncProfileTestFixture)
                    {
                        // Run it asynchronously.

                        RunAsync(result, step);
                    }
                    else
                    {
                        // Run it synchonrously.

                        Run(result, step);
                    }
                }
                else
                {
                    result.SetComplete(message, null, true);
                }
            }
            catch (Exception e)
            {
                result.SetComplete(message, e, true);
            }

            return result;
        }

        private void Run(AsyncResult result, ComponentStepInstance step)
        {
            const string method = "Run";

            try
            {
                // Run the step.

                step.StartTracking();
                step.BeginMethod.Invoke(step.ProfileInstance.TestFixture, null);
                var elapsed = step.StopTracking();

                if (EventSource.IsEnabled(Event.Flow))
                    EventSource.Raise(Event.Flow, method, "Step '" + step.Name + "' in profile '" + step.ProfileInstance.Profile + "' has been run for user " + step.ProfileInstance.User + ".", Event.Arg("Profile", step.ProfileInstance.Profile), Event.Arg("User", step.ProfileInstance.User), Event.Arg("Step", step.Name), Event.Arg("elapsed", elapsed));

                // Mark it as done.

                result.SetComplete(step, null, true);
            }
            catch (Exception ex)
            {
                step.TrackError();
                result.SetComplete(step, new StepFailedException(GetType(), method, step.ProfileInstance.Profile.Name, step.Name, step.ProfileInstance.User, ex), true);
            }
        }

        private void RunAsync(StepAsyncResult result, ComponentStepInstance step)
        {
            const string method = "RunAsync";

            try
            {
                // Run the step.

                step.StartTracking();
                ((IAsyncProfileTestFixture)step.ProfileInstance.TestFixture).Begin(result, step.BeginMethod, step.EndMethod);
            }
            catch (Exception ex)
            {
                step.TrackError();
                result.SetComplete(step, new StepFailedException(GetType(), method, step.ProfileInstance.Profile.Name, step.Name, step.ProfileInstance.User, ex), true);
            }
        }

        public object EndProcessRequest(IAsyncResult asyncResult)
        {
            const string method = "EndProcessRequest";

            var result = (StepAsyncResult)asyncResult;
            var step = result.Step;

            try
            {
                var message = result.End();

                // Mark it as complete.

                var elapsed = step.StopTracking();
                if (EventSource.IsEnabled(Event.Flow))
                    EventSource.Raise(Event.Flow, method, "Step '" + step.Name + "' in profile '" + step.ProfileInstance.Profile + "' has been run for user " + step.ProfileInstance.User + ".", Event.Arg("Profile", step.ProfileInstance.Profile), Event.Arg("User", step.ProfileInstance.User), Event.Arg("Step", step.Name), Event.Arg("elapsed", elapsed));

                return message;
            }
            catch (Exception ex)
            {
                step.TrackError();
                throw new StepFailedException(GetType(), method, step.ProfileInstance.Profile.Name, step.Name, step.ProfileInstance.User, ex);
            }
        }

        public void Open(object sinkInfo, IChannelSink nextSink)
        {
        }

        public void Start()
        {
        }

        public void Pause()
        {
        }

        public void Stop()
        {
        }

        public void Close()
        {
        }

        public void Continue()
        {
        }

        #endregion
    }
}
