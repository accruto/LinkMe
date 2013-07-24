using LinkMe.Framework.Configuration;
using LinkMe.Framework.Configuration.Tools;
using LinkMe.Framework.Configuration.Tools.Forms;
using LinkMe.Framework.Configuration.Tools.Mmc.Forms;
using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Management.Connection;

namespace LinkMe.Framework.Instrumentation.Tools.Mmc
{
	internal class ImportRepositoryResultsRunner
		:	ResultsRunner
	{
		public ImportRepositoryResultsRunner(IRepositoryReader reader, IRepositoryWriter writer)
			:	base(new ReaderWriter(reader, writer))
		{
		}

		protected override void Run(object data, IConfigurationEventSource eventSource)
		{
			ReaderWriter rw = data as ReaderWriter;
			if ( rw != null )
			{
				// Read the catalogue.

                eventSource.Raise(ConfigurationEvent.Information, "Reading from the '" + rw.Reader.RepositoryType + "' repository with initialisation string '" + rw.Reader.InitialisationString + "'.");

				Catalogue catalogue;
                using (ConnectionState state = new ConnectionState(eventSource))
				{
					catalogue = rw.Reader.Read(state);
				}

				// Write the catalogue.

                eventSource.Raise(ConfigurationEvent.Information, "Writing to the '" + rw.Writer.RepositoryType + "' repository with initialisation string '" + rw.Writer.InitialisationString + "'.");

				rw.Writer.Write(catalogue);
				rw.Writer.Close();
			}
		}

		protected override ResultsForm CreateForm(object data)
		{
			ReaderWriter rw = data as ReaderWriter;
			IRepositoryLink link = rw == null ? null : rw.Writer as IRepositoryLink;
			return new ImportRepositoryResultsForm(link == null ? string.Empty : link.Name);
		}

		private class ReaderWriter
		{
			public ReaderWriter(IRepositoryReader reader, IRepositoryWriter writer)
			{
				Reader = reader;
				Writer = writer;
			}

			public IRepositoryReader Reader;
			public IRepositoryWriter Writer;
		}
	}
}
