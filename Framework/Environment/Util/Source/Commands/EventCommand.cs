using System.Linq;
using LinkMe.Environment.CommandLines;
using LinkMe.Framework.Instrumentation.Connection.Wmi;
using LinkMe.Framework.Instrumentation.Management;

namespace LinkMe.Framework.Environment.Util.Commands
{
    public abstract class EventCommand
        : Command
    {
        private static readonly string[] EventNames = new[]
        {
            "CriticalError",
            "Error",
            "Warning",
            "NonCriticalError",
            "Information",
            "Trace",
            "MethodEnter",
            "MethodExit",
            "RequestTracking",
            "CommunicationTracking",
            "Flow",
            "FlowEnter",
            "FlowExit",
        };

        public override void Execute()
        {
            var eventOptions = Options["event"] == null
                ? new string[0]
                : (from i in Enumerable.Range(0, Options["event"].Values.Count) select Options["event"].Values[i]).ToArray();
            var sourceOption = Options["source"] == null ? null : Options["source"].Values[0];

            var catalogue = GetCatalogue();
            if (sourceOption == null)
            {
                if (eventOptions.Length == 0)
                    Execute(catalogue, EventNames);
                else
                    Execute(catalogue, EventNames.Intersect(eventOptions).ToArray());
            }
            else
            {
                if (eventOptions.Length == 0)
                    Execute(catalogue, sourceOption, EventNames, EventNames);
                else
                    Execute(catalogue, sourceOption, EventNames, EventNames.Intersect(eventOptions).ToArray());
            }
        }

        protected abstract void Execute(Catalogue catalogue, string[] eventNames);
        protected abstract void Execute(Catalogue catalogue, string sourceName, string[] allEventNames, string[] eventNames);

        private static Catalogue GetCatalogue()
        {
            var repositoryReader = new WmiConnection(@"\\.\root\LinkMe");
            return repositoryReader.Read();
        }
    }
}
