namespace LinkMe.Framework.Tools.Mmc.Constants
{
	internal static class Icon
	{
		private const string Root = "LinkMe.Framework.Tools.Mmc.Icons.";
		internal const string DefaultClosed = Root + "DefaultClosed.ico";
		internal const string DefaultOpen = Root + "DefaultOpen.ico";
	}

    internal static class Win32
    {
        internal static class HResults
        {
            internal const int S_OK = LinkMe.Framework.Utility.Constants.Win32.HResults.S_OK;
            internal const int S_FALSE = LinkMe.Framework.Utility.Constants.Win32.HResults.S_FALSE;

            internal const int E_NOINTERFACE = LinkMe.Framework.Utility.Constants.Win32.HResults.E_NOINTERFACE;
            internal const int E_NOTIMPL = LinkMe.Framework.Utility.Constants.Win32.HResults.E_NOTIMPL;
        }
    }
}
