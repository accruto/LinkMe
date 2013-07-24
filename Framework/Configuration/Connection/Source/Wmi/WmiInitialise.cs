using System.Windows.Forms;

namespace LinkMe.Framework.Configuration.Connection.Wmi
{
	public class WmiInitialise
        : ContainerInitialise
	{
        protected override bool Prompt(IWin32Window parent)
		{
			WmiInitialiseForm form = new WmiInitialiseForm(InitialisationString);
			if ( form.ShowDialog() == DialogResult.OK )
			{
				InitialisationString = form.Namespace;
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
