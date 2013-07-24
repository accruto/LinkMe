using System.Xml;

namespace LinkMe.Framework.Tools.Settings
{
	/// <summary>
	/// An object that contains settings, which can be added to a SettingsBase object.
	/// </summary>
	public interface ISettingsObject
	{
		/// <summary>
		/// Checks whether the settings in this object are the same as in another instance of the same class.
		/// </summary>
		/// <param name="obj">The instance to compare with.</param>
		/// <returns>True if the settings are the same, otherwise false.</returns>
		bool SettingsEqual(ISettingsObject obj);

		/// <summary>
		/// Reads settings from the specified XML element. The XML prefix 
		/// </summary>
		/// <param name="xmlSetting">The XML element to read from.</param>
		/// <param name="xmlNsManager">The XML namespace manager.</param>
		/// <param name="xmlPrefix">The XML prefix to use when selecting child elements. The value must
		/// either be an empty string (no prefix) or must include the trailing ":".</param>
		/// <param name="readingFromPath">Absolute path of the XML file being read from.</param>
		/// <remarks>
		/// The <paramref name="readingFromPath"/> parameter enables the object to store file paths relative
		/// to the path of the XML file that contains these settings.
		/// </remarks>
		void ReadXmlSettings(XmlNode xmlSetting, XmlNamespaceManager xmlNsManager, string xmlPrefix,
			string readingFromPath);

		/// <summary>
		/// Write settings to XML, including a single top-level container element.
		/// </summary>
		/// <param name="writer">The XML writer to write to.</param>
		/// <param name="xmlns">The XML namespace to use for elements.</param>
		/// <param name="writingToPath">>Absolute path of the XML file being written to.</param>
		/// <remarks>
		/// The <paramref name="writingToPath"/> parameter enables the object to store file paths relative
		/// to the path of the XML file that contains these settings.
		/// </remarks>
		void WriteXmlSettings(XmlWriter writer, string xmlns, string writingToPath);

		/// <summary>
		/// Write settings to XML, but without a top-level container element. The caller must create a
		/// container element before calling this method, otherwise the resulting XML may have multiple
		/// top-level elements.
		/// </summary>
		/// <param name="writer">The XML writer to write to.</param>
		/// <param name="xmlns">The XML namespace to use for elements.</param>
		/// <param name="writingToPath">>Absolute path of the XML file being written to.</param>
		/// <remarks>
		/// The <paramref name="writingToPath"/> parameter enables the object to store file paths relative
		/// to the path of the XML file that contains these settings.
		/// </remarks>
		void WriteXmlSettingContents(XmlWriter writer, string xmlns, string writingToPath);
	}
}
