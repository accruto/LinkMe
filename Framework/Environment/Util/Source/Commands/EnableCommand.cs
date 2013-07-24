using LinkMe.Framework.Configuration;
using LinkMe.Framework.Instrumentation.Management;

namespace LinkMe.Framework.Environment.Util.Commands
{
    public class EnableCommand
        : EventCommand
    {
        protected override void Execute(Catalogue catalogue, string[] eventNames)
        {
            foreach (var eventName in eventNames)
            {
                var eventType = catalogue.EventTypes[eventName];
                if (eventType == null)
                    catalogue.Add(catalogue.CreateEventType(catalogue, eventName, true));
                else
                    eventType.IsEnabled = true;
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
                parent.Add(catalogue.CreateSource(parent, catalogueName.Name, eventNames), true);
            }
            else
            {
                sourceType.EnableEvents(eventNames);
            }

            catalogue.Commit();
        }
    }
}
