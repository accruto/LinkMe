using LinkMe.Framework.Tools.Controls;

namespace LinkMe.Framework.Tools.Editors
{
	/// <summary>
	/// A control that is designed to display and edit some type of value. The implementing class must be
	/// derived from System.Windows.Forms.Control.
	/// </summary>
	public interface IEditor : IReadOnlySettable
	{
		/// <summary>
		/// True if the value has been modified in the editor since it was loaded by calling DisplayValue().
		/// </summary>
		bool Modified { get; }
		/// <summary>
		/// True if the editor can change the value, false if it can only display the value
		/// (ie. it is always read-only).
		/// </summary>
		bool SupportsEditing { get; }

		/// <summary>
		/// Begin editing the value immediately (eg. set focus to the first editor control).
		/// </summary>
		void BeginEditNew();
		/// <summary>
		/// Returns true if the editor can display a value of the specified type.
		/// </summary>
		bool CanDisplay(System.Type type);
		/// <summary>
		/// Clear the current value. The control may or may not represent a valid value after this method is called.
		/// </summary>
		void Clear();
		/// <summary>
		/// Display the specified value in the editor.
		/// </summary>
		void DisplayValue(object value);
		/// <summary>
		/// Returns a value of the specified type represented by the current state of the editor. The value
		/// returned should be a new object if possible (not the object passed to DisplayValue). If the value
		/// cannot be converted to the specified type an exception should be thrown.
		/// </summary>
		object GetValue(System.Type type);
		/// <summary>
		/// Returns the value represented by the current state of the editor. Some editors may not support
		/// this, ie. they may require a type to be passed in.
		/// </summary>
		object GetValue();
	}
}
