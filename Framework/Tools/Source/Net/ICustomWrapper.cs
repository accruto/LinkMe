namespace LinkMe.Framework.Tools.Net
{
	/// <summary>
	/// Implemented by objects that can create custom GenericWrapper wrappers for themselves.
	/// </summary>
	public interface ICustomWrapper
	{
		/// <summary>
		/// True if the object may have members (children), which can be obtained by creating a GenericWrapper.
		/// If this property returns false the caller can avoid creating a GenericWrapper to save time.
		/// </summary>
		bool MayHaveChildren { get; }

		/// <summary>
		/// Creates a GenericWrapper for the object.
		/// </summary>
		/// <returns>The wrapper for the object or null if a wrapper cannot be created for this instance.</returns>
		GenericWrapper CreateWrapper();
	}
}
