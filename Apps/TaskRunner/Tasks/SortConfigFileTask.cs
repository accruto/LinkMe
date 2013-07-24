using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Framework.Instrumentation;
using Property=LinkMe.Utility.Configuration.Property;

namespace LinkMe.TaskRunner.Tasks
{
	/// <summary>
	/// Sorts the properties in a configuration file alphabetically.
	/// </summary>
	public class SortConfigFileTask : Task
	{
        public SortConfigFileTask()
            : base(new EventSource<SortConfigFileTask>())
        {
        }

		public override void ExecuteTask(string[] args)
		{
			if (args.Length != 1)
				throw new ArgumentException("One argument is expected: the config file path.");

			string filePath = args[0];
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException("The specified configuration file, '" + filePath
					+ "', could not be found.");
			}

			using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
			{
				// Read and sort.

				var serializer = new XmlSerializer(typeof(Property[]));
				var array = (Property[])serializer.Deserialize(new StreamReader(stream));

				IDictionary dictionary = new SortedList(array.Length);
				foreach (Property property in array)
				{
					try
					{
						dictionary.Add(property.Name, property);
					}
					catch (ArgumentException ex)
					{
						throw new ApplicationException("Property '" + property.Name
							+ "' was defined more than once in the same file.", ex);
					}
				}
				Debug.Assert(dictionary.Count == array.Length, "dictionary.Count == array.Length");

				// Write back.

				dictionary.Values.CopyTo(array, 0);
				stream.Position = 0;
				serializer.Serialize(stream, array);
				stream.SetLength(stream.Position);
			}
		}
	}
}
