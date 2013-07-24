using System.Windows.Forms;

namespace LinkMe.Framework.Configuration.Connection
{
    public abstract class ContainerInitialise
        : IContainerInitialise
	{
        private string _initialisationString;

        string IContainerInitialise.InitialisationString
        {
            get { return InitialisationString; }
            set { InitialisationString = value; }
        }

        bool IContainerInitialise.Prompt(IWin32Window parent)
        {
            return Prompt(parent);
        }

        protected string InitialisationString
        {
            get { return _initialisationString; }
            set { _initialisationString = value; }
        }

        protected abstract bool Prompt(IWin32Window parent);
    }
}
