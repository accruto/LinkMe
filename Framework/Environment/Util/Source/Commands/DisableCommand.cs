using System.Linq;
using LinkMe.Framework.Configuration;
using LinkMe.Framework.Instrumentation.Management;

namespace LinkMe.Framework.Environment.Util.Commands
{
    public class DisableCommand
        : EventCommand
    {
        protected override void Execute(Catalogue catalogue, string[] eventNames)
        {
            foreach (var eventName in eventNames)
            {
                var eventType = catalogue.EventTypes[eventName];
                if (eventType == null)
                    catalogue.Add(catalogue.CreateEventType(catalogue, eventName, false));
                else
                    eventType.IsEnabled = false;
            }

            catalogue.Commit();
        }

        protected override void Execute(Catalogue catalogue, string sourceName, string[] allEventNames, string[] eventNames)
        {
            var sourceType = catalogue.GetSearcher().GetSource(sourceName);
            if (sourceType == null)
            {
                var catalogueName = new CatalogueName(sourceName);
                var parent = catalogue.EnsureSourceParent(catalogueName.Namespace);
                parent.Add(catalogue.CreateSource(parent, catalogueName.Name, allEventNames.Except(eventNames).ToArray()), true);
            }
            else
            {
                sourceType.DisableEvents(eventNames);
            }

            catalogue.Commit();
        }
    }
}
