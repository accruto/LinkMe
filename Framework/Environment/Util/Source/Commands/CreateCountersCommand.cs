using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using LinkMe.Environment.CommandLines;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Environment.Util.Commands
{
    public class CreateCountersCommand
        : Command
    {
        private abstract class Definition
        {
            private readonly string _name;
            private readonly string _help;

            protected Definition(string name, string help)
            {
                _name = name;
                _help = help;
            }

            public string Name
            {
                get { return _name; }
            }

            public string Help
            {
                get { return _help; }
            }
        }

        private class Category
            : Definition
        {
            private readonly PerformanceCounterCategoryType _type;
            private readonly IList<Counter> _counters = new List<Counter>();

            public Category(string name, string help, PerformanceCounterCategoryType type)
                : base(name, help)
            {
                _type = type;
            }

            public PerformanceCounterCategoryType Type
            {
                get { return _type; }
            }

            public IList<Counter> Counters
            {
                get { return _counters; }
            }
        }

        private class Counter
            : Definition
        {
            private readonly PerformanceCounterType _type;

            public Counter(string name, string help, PerformanceCounterType type)
                : base(name, help)
            {
                _type = type;
            }

            public PerformanceCounterType Type
            {
                get { return _type; }
            }
        }

        public override void Execute()
        {
            // Make sure the file path is absolute.

            var filePath = Options["counterFile"].Values[0];
            filePath = FileSystem.GetAbsolutePath(filePath);
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Performance counter file does not exist.", filePath);

            // Load all definitions.

            var categories = Read(filePath);

            // Create.

            Create(categories);
        }

        private static void Create(IEnumerable<Category> categories)
        {
            foreach (var category in categories)
            {
                // Delete what might already be there.

                Delete(category);

                var data = new CounterCreationDataCollection();
                foreach (var counter in category.Counters)
                    data.Add(new CounterCreationData(counter.Name, counter.Help, counter.Type));
                PerformanceCounterCategory.Create(category.Name, category.Help, category.Type, data);
            }
        }

        private static void Delete(Definition category)
        {
            if (PerformanceCounterCategory.Exists(category.Name))
                PerformanceCounterCategory.Delete(category.Name);
        }

        private static IList<Category> Read(string filePath)
		{
            var categories = new List<Category>();

            using (var fileReader = new StreamReader(filePath))
            {
                var adaptor = new XmlReadAdaptor(fileReader, Constants.Xml.Namespace);
                Read(adaptor, categories);
            }

            return categories;
        }

        private static void Read(XmlReadAdaptor adaptor, ICollection<Category> categories)
        {
            if ( adaptor.ReadElement(Constants.Config.RootElement, false) )
			{
				if ( adaptor.ReadElement(Constants.Config.SectionElement, false) )
				{
                    if (adaptor.ReadElement(Constants.Xml.PerformanceCountersElement))
                    {
                        if (adaptor.ReadElement(Constants.Xml.CategoriesElement))
                        {
                            while (adaptor.ReadElement(Constants.Xml.CategoryElement))
                            {
                                ReadCategory(adaptor, categories);
                                adaptor.ReadEndElement();
                            }

                            adaptor.ReadEndElement();
                        }

                        adaptor.ReadEndElement();
                    }

                    adaptor.ReadEndElement();
                }

                adaptor.ReadEndElement();
            }
        }

        private static void ReadCategory(XmlReadAdaptor adaptor, ICollection<Category> categories)
        {
            // Create the category itself.

            var name = adaptor.ReadName();
            var help = adaptor.ReadAttributeString(Constants.Xml.HelpAttribute, string.Empty);
            var type = (PerformanceCounterCategoryType) adaptor.ReadAttributeEnum(Constants.Xml.TypeAttribute, typeof(PerformanceCounterCategoryType), PerformanceCounterCategoryType.SingleInstance);
            var category = new Category(name, help, type);
            categories.Add(category);

            // Look for counters.

            if (adaptor.ReadElement(Constants.Xml.CountersElement))
            {
                while (adaptor.ReadElement(Constants.Xml.CounterElement))
                {
                    ReadCounter(adaptor, category);
                    adaptor.ReadEndElement();
                }

                adaptor.ReadEndElement();
            }
        }

        private static void ReadCounter(XmlReadAdaptor adaptor, Category category)
        {
            var name = adaptor.ReadName();
            var help = adaptor.ReadAttributeString(Constants.Xml.HelpAttribute, string.Empty);
            var type = (PerformanceCounterType)adaptor.ReadAttributeEnum(Constants.Xml.TypeAttribute, typeof(PerformanceCounterType), PerformanceCounterType.NumberOfItems32);
            var counter = new Counter(name, help, type);
            category.Counters.Add(counter);
        }
    }
}
