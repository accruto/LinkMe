using System;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace LinkMe.InstallUtil
{
	internal static class PerfCounterUtil
	{
		private struct PerfCategoryInfo
		{
			public readonly CounterCreationDataCollection CounterDataCollection;
			public readonly string CategoryName;
			public readonly string CategoryHelp;
		    public readonly PerformanceCounterCategoryType CategoryType;

			public PerfCategoryInfo(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, CounterCreationDataCollection counterDataCollection)
			{
				CounterDataCollection = counterDataCollection;
				CategoryName = categoryName;
				CategoryHelp = categoryHelp;
			    CategoryType = categoryType;
			}
		}

	    internal static void CreatePerfCounters(string[] args)
		{
			if (args.Length != 2)
			{
				Program.Usage();
				return;
			}

			PerfCategoryInfo[] infos = GetPerfCounterCategories(args);

			foreach (PerfCategoryInfo info in infos)
			{
				if (PerformanceCounterCategory.Exists(info.CategoryName))
				{
					Console.WriteLine("Category '" + info.CategoryName + "' already exists.");
				}
				else
				{
					try
					{
						PerformanceCounterCategory.Create(info.CategoryName, info.CategoryHelp, info.CategoryType, info.CounterDataCollection);
					}
					catch (Exception ex)
					{
						throw new ApplicationException("Failed to create performance counter category '"
							+ info.CategoryName + "'.", ex);
					}
					Console.WriteLine("Category '" + info.CategoryName + "' has been created.");
				}
			}
		}

		internal static void DeletePerfCounters(string[] args)
		{
			if (args.Length != 2)
			{
				Program.Usage();
				return;
			}

			PerfCategoryInfo[] infos = GetPerfCounterCategories(args);

			foreach (PerfCategoryInfo info in infos)
			{
				if (PerformanceCounterCategory.Exists(info.CategoryName))
				{
					PerformanceCounterCategory.Delete(info.CategoryName);
					Console.WriteLine("Category '" + info.CategoryName + "' has been deleted.");
				}
				else
				{
					Console.WriteLine("Category '" + info.CategoryName + "' does not exist.");
				}
			}
		}

		private static PerfCategoryInfo[] GetPerfCounterCategories(string[] args)
		{
			string filePath = args[1];
			if (!File.Exists(filePath))
			{
				throw new ArgumentException("The specified monitors file, '" + filePath
					+ "', does not exist.", "filePath");
			}

			var monitorDoc = new XmlDocument();
			try
			{
				monitorDoc.Load(filePath);

				XmlNodeList categoryNodes = monitorDoc.SelectNodes("linkMe/performanceMonitors/monitorCategory");
				if (categoryNodes.Count == 0)
				{
					throw new ArgumentException("The specified monitors file, '" + filePath
						+ "', does not contain any performance counter names.", "filePath");
				}

				var categories = new PerfCategoryInfo[categoryNodes.Count];
				for (int index = 0; index < categoryNodes.Count; index++)
				{
					XmlNode categoryNode = categoryNodes[index];

					string categoryName = categoryNode.Attributes["name"].Value;
					string categoryHelp = categoryNode.Attributes["help"].Value;

				    PerformanceCounterCategoryType categoryType = PerformanceCounterCategoryType.Unknown;
				    XmlAttribute categoryTypeAttribute = categoryNode.Attributes["type"];
                    if (categoryTypeAttribute != null)
                        categoryType = (PerformanceCounterCategoryType) Enum.Parse(typeof(PerformanceCounterCategoryType), categoryTypeAttribute.Value);

					var categoryCreationDataCollection  = new CounterCreationDataCollection();
					XmlNodeList counterNodeList = categoryNode.SelectNodes("./monitor");
					foreach (XmlNode counterNode in counterNodeList)
					{
						string name = counterNode.Attributes["name"].Value;
						string help = counterNode.Attributes["help"].Value;
						string type = counterNode.Attributes["type"].Value;

						var creationData = new CounterCreationData(name, help,
							(PerformanceCounterType) Enum.Parse(typeof(PerformanceCounterType), type));
						categoryCreationDataCollection.Add(creationData);
					}

					categories[index] = new PerfCategoryInfo(categoryName, categoryHelp, categoryType, categoryCreationDataCollection);
				}

				return categories;
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Failed to process monitors file '" + filePath + "'.", ex);
			}
		}
	}
}
