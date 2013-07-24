using System;
using System.Threading;
using System.Diagnostics;
using LinkMe.Framework.Host.Exceptions;
using Exception=System.Exception;

namespace LinkMe.Framework.Host
{
    public class AsyncResult
        : MarshalByRefObject,
            IAsyncResult
    {
        #region Private Fields

        private readonly AsyncCallback _asyncCallback;
        private readonly object _asyncState;
        private bool _completed;
        private bool _completedSynchronously;
        private object _completeEvent;
        private object _message;
        private Exception _exception;
        private int _setCompleteCount;

        #endregion

        public AsyncResult(AsyncCallback asyncCallback, object asyncState)
        {
            _asyncCallback = asyncCallback;
            _asyncState = asyncState;
        }

        public void SetComplete(object message, Exception exception, bool completedSynchronously)
        {
            if (Interlocked.Increment(ref _setCompleteCount) == 1) // prevent possible race conditions in the caller's code
            {
                _completed = true;
                _completedSynchronously = completedSynchronously;
                _message = message;
                if (exception != null)
                {
                    if (exception is AsyncCallException)
                        _exception = exception.InnerException;
                    else
                        _exception = exception;
                }
                else
                {
                    _exception = null;
                }

                if (_completeEvent != null)
                    ((ManualResetEvent)_completeEvent).Set();

                if (_asyncCallback != null)
                    _asyncCallback(this);
            }
        }

        public object End()
        {
            const string method = "End";

            if (!_completed)
                GetWaitHandle().WaitOne();

            if (_exception != null)
                throw new AsyncCallException(GetType(), method, _exception);

            return _message;
        }

        public object End(out Exception exception)
        {
            if (!_completed)
                GetWaitHandle().WaitOne();

            exception = _exception;
            return _message;
        }

        #region IAsyncResult Members

        object IAsyncResult.AsyncState
        {
            get { return _asyncState; }
        }

        bool IAsyncResult.CompletedSynchronously
        {
            get { return _completedSynchronously; }
        }

        WaitHandle IAsyncResult.AsyncWaitHandle
        {
            get { return GetWaitHandle(); }
        }

        bool IAsyncResult.IsCompleted
        {
            get { return _completed; }
        }

        #endregion

        #region Private Methods

        private WaitHandle GetWaitHandle()
        {
            bool wasCompleted = _completed;

            // Create the event if it's not already created.

            if (_completeEvent == null)
                Interlocked.CompareExchange(ref _completeEvent, new ManualResetEvent(wasCompleted), null);

            Debug.Assert(_completeEvent != null, "_completeEvent != null");
            var completeEvent = (ManualResetEvent)_completeEvent;

            // Set the event if the operation was completed while we were here.

            if (!wasCompleted && _completed)
                completeEvent.Set();

            return completeEvent;
        }

        #endregion
    }

}
