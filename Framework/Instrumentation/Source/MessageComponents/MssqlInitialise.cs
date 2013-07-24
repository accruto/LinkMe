using System.Windows.Forms;
using LinkMe.Framework.Configuration.Connection;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Framework.Instrumentation.MessageComponents
{
    public class MssqlInitialise
        : ContainerInitialise
	{
		protected override bool Prompt(IWin32Window parent)
		{
            var dialog = new MssqlConnectionPrompt { ConnectionString = string.Empty };

            if (dialog.ShowDialog(parent) != DialogResult.OK)
                return false;

            InitialisationString = SqlUtil.OleDbToSqlConnectionString(dialog.ConnectionString);
            return true;
		}
	}
}
