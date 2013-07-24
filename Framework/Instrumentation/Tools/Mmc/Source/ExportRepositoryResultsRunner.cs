using System.Collections;

using LinkMe.Framework.Configuration;
using LinkMe.Framework.Configuration.Tools;
using LinkMe.Framework.Configuration.Tools.Forms;
using LinkMe.Framework.Configuration.Tools.Mmc.Forms;
using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Management.Connection;

namespace LinkMe.Framework.Instrumentation.Tools.Mmc
{
	internal class ExportRepositoryResultsRunner
		:	ResultsRunner
	{
		public ExportRepositoryResultsRunner(IRepositoryWriter writer, ArrayList elements)
			:	base(new WriterElements(writer, elements))
		{
		}

        protected override void Run(object data, IConfigurationEventSource eventSource)
		{
			var we = data as WriterElements;
			if ( we != null )
			{
				// Write the catalogue.

                eventSource.Raise(ConfigurationEvent.Information, "Writing to the '" + we.Writer.RepositoryType + "' repository with initialisation string '" + we.Writer.InitialisationString + "'.");
		
				// Get the selected elements.

				foreach ( object element in we.Elements )
				{
					// Write out the element.

					if ( element is Namespace )
					{
						var ns = (Namespace) element;
						// configurationSource.Raise(ConfigurationEvent.Information, "Writing namespace '" + ns.FullName + "'.");
						we.Writer.Write(ns, false);
					}
					else if ( element is Source )
					{
						var source = (Source) element;
						// configurationSource.Raise(ConfigurationEvent.Information, "Writing source '" + source.FullyQualifiedReference + "'.");
						we.Writer.Write(source);
					}
					else if ( element is EventType )
					{
						var eventType = (EventType) element;
						// configurationSource.Raise(ConfigurationEvent.Information, "Writing event '" + instrumentationEvent.FullName + "'.");
						we.Writer.Write(eventType);
					}
				}

				we.Writer.Close();
			}
		}

		protected override ResultsForm CreateForm(object data)
		{
			var we = data as WriterElements;
            return new ExportRepositoryResultsForm(we.Writer.RepositoryType);
		}

		private class WriterElements
		{
			public WriterElements(IRepositoryWriter writer, ArrayList elements)
			{
				Writer = writer;
				Elements = elements;
			}

			public readonly IRepositoryWriter Writer;
			public readonly ArrayList Elements;
		}
	}
}
