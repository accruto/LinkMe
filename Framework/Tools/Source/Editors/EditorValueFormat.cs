namespace LinkMe.Framework.Tools.Editors
{
	/// <summary>
	/// Specifies how values are displayed as strings in the GenericEditorGrid control.
	/// </summary>
	public enum EditorValueFormat
	{
		/// <summary>
		/// The output of ToString() for the value is displayed unchanged.
		/// </summary>
		PlainString,
		/// <summary>
		/// The values are displayed using the same encoding as C# - strings are enclosed in quotes and
		/// escaped as needed, numeric values may have suffixes to denote the type.
		/// </summary>
		CSharp
	}
}
