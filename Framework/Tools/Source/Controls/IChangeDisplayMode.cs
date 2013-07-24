namespace LinkMe.Framework.Tools.Controls
{
	public interface IChangeDisplayMode
	{
		event DisplayModeChangedEventHandler DisplayModeChanged;

		void OnForcedRestore();
	}
}
