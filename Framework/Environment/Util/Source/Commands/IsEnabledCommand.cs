using System;
using System.Linq;
using LinkMe.Framework.Instrumentation.Management;

namespace LinkMe.Framework.Environment.Util.Commands
{
    public class IsEnabledCommand
        : EventCommand
    {
        protected override void Execute(Catalogue catalogue, string[] eventNames)
        {
            foreach (var eventName in eventNames)
            {
                var eventType = catalogue.EventTypes[eventName];
                if (eventType == null)
                    Console.WriteLine(eventName + ":" + new string(' ', 30 - eventName.Length) + false);
                else
                    Console.WriteLine(eventName + ":" + new string(' ', 30 - eventName.Length) + eventType.IsEnabled);
            }
        }

        protected override void Execute(Catalogue catalogue, string sourceName, string[] allEventNames, string[] eventNames)
        {
            var sourceType = catalogue.GetSearcher().GetSource(sourceName);
            if (sourceType == null)
            {
                Console.WriteLine(sourceName + ":");
                Console.WriteLine("  <not set>");
            }
            else
            {
                foreach (var eventName in eventNames)
                    Console.WriteLine("  " + eventName + ":" + new string(' ', 30 - eventName.Length) + sourceType.GetEffectiveEnabledEvents().Contains(eventName));
            }
        }
    }
}
