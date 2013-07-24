using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Instrumentation.Message;

namespace LinkMe.Apps.Tracking
{
    internal static class EventMessageExtensions
    {
        public static object GetParameterValue(this EventMessage message, string name)
        {
            foreach (EventParameter parameter in message.Parameters)
            {
                if (parameter.Name == name)
                    return parameter.Value;
            }

            return null;
        }

        public static object GetValue(this InstrumentationDetails details, string name)
        {
            foreach (var entry in details)
            {
                if (entry.Key == name)
                    return entry.Value;
            }

            return null;
        }
    }
}