using System.Collections;
using System.Xml;

namespace LinkMe.Framework.Utility.Xml
{
	/// <summary>
	/// Xml serialization interfaces
	/// </summary>
	public interface IXmlSerializable
		:	System.Xml.Serialization.IXmlSerializable
	{
		void ReadOuterXml(XmlReader xmlReader);
		void WriteOuterXml(XmlWriter xmlWriter);
	}

	/// <summary>
	/// Xml merging interfaces
	/// </summary>
	public interface IXmlMerge
	{
		void ReadXml(XmlReader xmlReader);
		void WriteXml(XmlWriter xmlWriter);
		void ReadXmlContents(XmlReader xmlReader);
		void WriteXmlContents(XmlWriter xmlWriter);
	}

	/// <summary>
	/// XPath navigation interfaces
	/// </summary>
	public interface IXPathSelectable
	{
		IEnumerable Select(string query);
	}
}
