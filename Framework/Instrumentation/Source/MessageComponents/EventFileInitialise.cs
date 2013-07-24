using System.Windows.Forms;
using LinkMe.Framework.Configuration.Connection;

namespace LinkMe.Framework.Instrumentation.MessageComponents
{
    public class EventFileInitialise
        : ContainerInitialise
	{
		protected override bool Prompt(IWin32Window parent)
		{
			// Prompt for the file.

			var openFileDialog = new OpenFileDialog
            {
                Filter = Constants.MessageComponents.EventFile.FileFilter,
                DefaultExt = Constants.MessageComponents.EventFile.FileExtension,
                CheckFileExists = true,
                Title = "Open Event File"
            };

		    if ( openFileDialog.ShowDialog(parent) == DialogResult.OK )
			{
                InitialisationString = openFileDialog.FileName;
				return true;
			}
		    
            return false;
		}
	}
}
