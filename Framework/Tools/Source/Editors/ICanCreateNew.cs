using LinkMe.Framework.Tools.Net;

namespace LinkMe.Framework.Tools.Editors
{
	/// <summary>
	/// An editor that can also create a new value, which may have a different type to the value originally
	/// passed to DisplayValue(), if any.
	/// </summary>
	public interface ICanCreateNew : IEditor
	{
		/// <summary>
		/// Creates and returns a new value of the specified type.
		/// </summary>
		object CreateNew(string typeName);
		/// <summary>
		/// Get the type of the value as a string (full name).
		/// </summary>
		string GetTypeName();
	}
}
