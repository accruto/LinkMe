using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Framework.Instrumentation
{
    public class ContainerEventSource
        : IContainerEventSource
    {
        private readonly EventSource _eventSource = new EventSource<ContainerEventSource>();

        public void RaiseError(string method, string message)
        {
            _eventSource.Raise(Event.Error, method, message);
        }
    }
}
