using System.Windows.Forms;

namespace LinkMe.Framework.Configuration.Connection.Msmq
{
	public class MsmqInitialise
        : ContainerInitialise
	{
		protected override bool Prompt(IWin32Window parent)
		{
			var form = new MsmqInitialiseForm();
			form.RefreshQueueList();
			if ( form.ShowDialog() == DialogResult.OK )
			{
				InitialisationString = "DIRECT=OS:" + form.GetMachineName() + "\\" + form.GetSelectedQueueName();
				return true;
			}

		    return false;
		}
	}
}
