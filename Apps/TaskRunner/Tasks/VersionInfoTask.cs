using System;
using System.Collections.Generic;
using System.IO;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Utility.Configuration;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.TaskRunner.Tasks
{
	public class VersionInfoTask : Task
	{
		public VersionInfoTask()
            : base(new EventSource<VersionInfoTask>())
		{
		}

        public override void ExecuteTask()
		{
            IDictionary<string, string> values = ApplicationContext.Instance.GetPropertyValues();
            IDictionary<string, string> sources = ApplicationContext.Instance.GetPropertySources();

            TextWriter writer = Console.Out;
            writer.WriteLine("Printing all Properties.");
			writer.WriteLine("------------------------------------------------");

            foreach (KeyValuePair<string, string> kvp in sources)
			{
                writer.WriteLine("{0} = \"{1}\" [{2}]", kvp.Key, values[kvp.Key], kvp.Value);
			}

            writer.WriteLine("------------------------------------------------");
		}
	}
}
