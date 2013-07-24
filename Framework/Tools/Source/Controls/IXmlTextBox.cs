using System.Drawing;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// The implementing class is a control that contains an XmlTextBox.
	/// </summary>
	public interface IXmlTextBox
	{
		/// <summary>
		/// True to automatically format the text in the contained. XmlTextBox when it changes. The default should
		/// be false.
		/// </summary>
		bool AutoFormatXml { get; set; }
		/// <summary>
		/// Gets or sets the current text in the XmlTextBox.
		/// </summary>
		string XmlText { get; set; }

		/// <summary>
		/// Selects <paramref name="length"/> characters starting from <paramref name="startIndex"/> and sets
		/// their colour in the contained XmlTextBox.
		/// </summary>
		/// <param name="color">The colour to apply.</param>
		/// <param name="startIndex">The index of the first character to select.</param>
		/// <param name="length">The number of characters following <paramref name="startIndex"/> to select.</param>
		void SelectAndSetTextColor(Color color, int startIndex, int length);
	}
}
