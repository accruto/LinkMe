using System;
using System.Reflection;

namespace LinkMe.Framework.Utility.PublishedEvents
{
    internal class EventSubscriber
    {
        readonly Type _handlerEventArgsType;
        readonly MethodInfo _methodInfo;
        readonly WeakReference _subscriber;

        public EventSubscriber(object subscriber, MethodInfo methodInfo)
        {
            _subscriber = new WeakReference(subscriber);
            _methodInfo = methodInfo;

            var parameters = methodInfo.GetParameters();

            if (parameters.Length != 2 || !typeof(EventArgs).IsAssignableFrom(parameters[1].ParameterType))
                throw new ArgumentException("Method does not appear to be a valid event handler", "methodInfo");

            _handlerEventArgsType = typeof(EventHandler<>).MakeGenericType(parameters[1].ParameterType);
        }

        public object Target
        {
            get { return _subscriber.Target; }
        }

        public Exception Invoke(object sender, EventArgs e)
        {
            var subscriber = _subscriber.Target;

            try
            {
                if (subscriber != null)
                {
                    var method = Delegate.CreateDelegate(_handlerEventArgsType, subscriber, _methodInfo);
                    method.DynamicInvoke(sender, e);
                }

                return null;
            }
            catch (TargetInvocationException ex)
            {
                return ex.InnerException;
            }
        }
    }
}
