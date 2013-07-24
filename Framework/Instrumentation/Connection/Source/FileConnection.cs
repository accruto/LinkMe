using System.IO;
using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Management.Connection;

namespace LinkMe.Framework.Instrumentation.Connection
{
    public abstract class FileConnection
        : RepositoryConnection
    {
        protected FileConnection(string initialisationString)
            : base(initialisationString)
        {
        }

        protected abstract IRepositoryReader CreateReader();

        protected override void PrepareCatalogue(Catalogue catalogue)
        {
            // Check that the file exists.

            if (File.Exists(InitialisationString))
            {
                // Create a catalogue reader for the file.

                IRepositoryReader reader = CreateReader();
                CatalogueCopier.Copy(catalogue, reader.Read(), ReadOnlyOption.Clear);
            }
        }
    }
}
