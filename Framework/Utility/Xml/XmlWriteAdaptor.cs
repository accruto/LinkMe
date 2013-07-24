using System.Text;
using System.Xml;
using LinkMe.Framework.Utility.Net;

namespace LinkMe.Framework.Utility.Xml
{
	public class XmlWriteAdaptor
		:	System.IDisposable
	{
		/// <summary>
		/// The encoding to use throughout. Use UTF-8 without emitting the byte order mark,
		/// because the BOM confuses some parsers.
		/// </summary>
		public static readonly Encoding XmlEncoding = new UTF8Encoding(false);

		public XmlWriteAdaptor(string fileName, string xmlNamespace)
		{
			XmlTextWriter writer = new XmlTextWriter(fileName, XmlEncoding);
			writer.Formatting = Formatting.Indented;
			writer.Indentation = 4;
			m_writer = writer;

			// Use the component catalogue namespace as the default.

			m_namespace = xmlNamespace;
		}

		public XmlWriteAdaptor(XmlWriter writer, string xmlNamespace)
		{
			m_writer = writer;
			m_namespace = xmlNamespace;
		}

		public XmlWriteAdaptor(XmlWriter writer)
		{
			m_writer = writer;
			m_namespace = null;
		}

		public XmlWriter XmlWriter
		{
			get { return m_writer; }
		}

		#region Static methods

		// XmlConvert..cctor creates some regexes, which is slow - don't use it for these simple cases.

		private static string ToXmlString(int value)
		{
			return value.ToString(null, System.Globalization.NumberFormatInfo.InvariantInfo);
		}

		private static string ToXmlString(bool value)
		{
			return (value ? "true" : "false");
		}

		#endregion

		public void Close()
		{
			m_writer.Close();
		}

		public string WriteNamespace(string prefix, string ns)
		{
			// Make sure it is not already there.

			string existingPrefix = m_writer.LookupPrefix(ns);
			if ( existingPrefix != null )
				return existingPrefix;

			m_writer.WriteAttributeString(Constants.Xmlns.Prefix, prefix, null, ns);
			return prefix;
		}

		public void WriteNamespace(string ns)
		{
			// Make sure it is not already there.

			if ( m_writer.LookupPrefix(ns) == null )
				m_writer.WriteAttributeString(Constants.Xmlns.Prefix, null, ns);
		}

		public void WriteStartElement(string name, string ns)
		{
			m_writer.WriteStartElement(name, ns);
		}

		public void WriteStartElement(string prefix, string name, string ns)
		{
            m_writer.WriteStartElement(prefix, XmlConvert.VerifyNCName(name), ns);
		}

		public void WriteStartElement(string name, bool useNamespace)
		{
			if ( useNamespace && m_namespace != null )
                m_writer.WriteStartElement(XmlConvert.VerifyNCName(name), m_namespace);
			else
                m_writer.WriteStartElement(XmlConvert.VerifyNCName(name));
		}

		public void WriteStartElement(string name)
		{
			WriteStartElement(name, true);
		}

		public void WriteEndElement()
		{
			m_writer.WriteEndElement();
		}

		public void WriteElement(string name, string value)
		{
			if ( m_namespace != null )
                m_writer.WriteElementString(XmlConvert.VerifyNCName(name), m_namespace, value);
			else
                m_writer.WriteElementString(XmlConvert.VerifyNCName(name), value);
		}

		public void WriteElementValue(string value)
		{
			m_writer.WriteString(value);
		}

		public void WriteAttribute(string name, string value)
		{
			m_writer.WriteAttributeString(name, value);
		}

		public void WriteAttribute(string name, string prefix, string ns, string value)
		{
			m_writer.WriteAttributeString(prefix, name, ns, value);
		}

		public void WriteAttribute(string name, int value)
		{
			m_writer.WriteAttributeString(name, ToXmlString(value));
		}

		public void WriteAttribute(string name, string prefix, string ns, int value)
		{
			m_writer.WriteAttributeString(prefix, name, ns, ToXmlString(value));
		}

		public void WriteAttribute(string name, bool value)
		{
			m_writer.WriteAttributeString(name, ToXmlString(value));
		}

		public void WriteAttribute(string name, string prefix, string ns, bool value)
		{
			m_writer.WriteAttributeString(prefix, name, ns, ToXmlString(value));
		}

		public void WriteAttribute(string name, System.Enum enumeration)
		{
			m_writer.WriteAttributeString(name, enumeration.ToString());
		}

		public void WriteAttribute(string name, string prefix, string ns, System.Enum enumeration)
		{
			m_writer.WriteAttributeString(prefix, name, ns, enumeration.ToString());
		}

		public void WriteAttribute(string name, string[] value)
		{
			WriteAttribute(name, string.Join(" ", value));
		}

		public void WriteXml(string xml)
		{
			m_writer.WriteRaw(xml);
		}

		public void WriteXsiNilAttribute(bool nil)
		{
			WriteAttribute(Constants.Xsi.NilAttribute, Constants.Xsi.Prefix,
				Constants.Xsi.Namespace, nil);
		}

		public void WriteName(string name)
		{
			WriteAttribute(Constants.Xml.NameAttribute, name);
		}

		public void WriteDescription(string description)
		{
			// Only write it out if there is something to write out.

			if ( description != null && description.Length > 0 )
				WriteElement(Constants.Xml.DescriptionElement, description);
		}

		public void WriteVersion(System.Version version)
		{
			if ( version != null )
				WriteAttribute(Constants.Xml.VersionAttribute, version.ToString());
		}

		public void WriteClass(ClassInfo classInfo)
		{
			if ( classInfo != null )
				WriteAttribute(Constants.Xml.ClassAttribute, classInfo.ToString());
		}

		public void WriteDefault(bool isDefaultSet, string defaultAsXml)
		{
			// Only write the element out if needed.

			if ( isDefaultSet )
			{
				if ( defaultAsXml == null )
				{
					WriteStartElement(Constants.Xml.DefaultElement);
					WriteAttribute(Constants.Xml.IsNullAttribute, true);
					WriteEndElement();
				}
				else
				{
					WriteElement(Constants.Xml.DefaultElement, defaultAsXml);
				}
			}
		}

		public string LookupPrefix(string ns)
		{
			return m_writer.LookupPrefix(ns);
		}

	    #region IDisposable Members

		void System.IDisposable.Dispose()
		{
			System.IDisposable disposable = m_writer as System.IDisposable;
			if ( disposable != null )
				disposable.Dispose();
		}

		#endregion

		private XmlWriter m_writer;
		private string m_namespace;
	}
}
