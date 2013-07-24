using LinkMe.Framework.Tools.Net;

namespace LinkMe.Framework.Tools.Editors
{
	/// <summary>
	/// An editor that can edit a value in a remote AppDomain without loading it into its own AppDomain using
	/// a GenericWrapper object as a proxy.
	/// </summary>
	public interface IRemoteEditor : IEditor
	{
		/// <summary>
		/// Returns one or more formats of the value that the editor can display. This value is expected to
		/// be constant.
		/// </summary>
		WrappedValueFormats UsedFormats { get; }

		/// <summary>
		/// Display the specified value in the editor.
		/// </summary>
		void DisplayRemoteValue(GenericWrapper wrapper);
		/// <summary>
		/// Returns the remote value as a GenericWrapper. The returned object may be the same object that was
		/// passed to DisplayRemoteValue().
		/// </summary>
		GenericWrapper GetRemoteValue();
	}
}
