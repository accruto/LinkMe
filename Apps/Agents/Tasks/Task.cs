using System;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Apps.Agents.Tasks
{
    public abstract class Task
        : ITask
    {
        protected readonly EventSource _eventSource;

        protected Task(EventSource eventSource)
        {
            _eventSource = eventSource;
        }

        public virtual void ExecuteTask()
        {
            throw new ArgumentException("Task '" + GetType().Name + "' does not expect to be called"
                + " without arguments.");
        }

        public virtual void ExecuteTask(string[] args)
        {
            throw new ArgumentException("Task '" + GetType().Name + "' does not expect to be called"
                + " with arguments.");
        }

        void ITask.Execute(string[] args)
        {
            const string method = "Execute";

            try
            {
                _eventSource.Raise(Event.Information, method, string.Format("Task {0} started.", GetType().Name));

                if (args == null || args.Length == 0)
                    ExecuteTask();
                else
                    ExecuteTask(args);

                _eventSource.Raise(Event.Information, method, string.Format("Task {0} finished.", GetType().Name));
            }
            catch (Exception ex)
            {
                _eventSource.Raise(Event.Error, method, "Unhandled exception thrown by task " + GetType().Name, ex);
                throw;
            }
        }
    }
}
