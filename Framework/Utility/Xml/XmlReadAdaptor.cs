using System.Collections;
using System.IO;
using System.Xml;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Net;

namespace LinkMe.Framework.Utility.Xml
{
	/// <summary>
	/// An adaptor that wraps an XmlReader to simplify reading XML.
	/// </summary>
	/// <remarks>
	/// <para>
	/// <c>XmlReadAdaptor</c> simplifies reading XML by automatically keeping track of the current namespace
	/// and passing it to the underlying <c>XmlReader</c>. It also provides some helper methods for reading
	/// mandatory elements and strongly-typed values of certain common types. Whitespace nodes are automatically
	/// skipped.
	/// </para>
	/// <para>
	/// <c>XmlReadAdaptor</c> is ideal for two scenarios:
	/// <list type="bullet">
	/// <item>All child elements of a particular element are mandatory and must appear in a predefined order.</item>
	/// <item>All child elements of a particular element are optional and may appear in any order.</item>
	/// </list>
	/// See below for examples of both of these scenarios. If most elements are optional, but a few are
	/// mandatory it may be easiest to read all elements as optional and manually check for the few mandatory
	/// ones at the end. For other, more complex, scenarios it may be appropriate to use <c>XmlReader</c> directly.
	/// </para>
	/// <seealso cref="System.Xml.XmlReader"/>
	/// <seealso cref="System.Xml.XmlTextReader"/>
	/// </remarks>
	/// <example>
	/// The following example reads mandatory, ordered elements, one of which may contain a variable number of
	/// child elements with the same name (a collection).
	/// <code>
	/// XmlReadAdaptor adaptor = new XmlReadAdaptor(reader, "http://xmlns.linkme.com.au/Framework/Configuration");
	/// 
	/// string domainName = adaptor.ReadElementString("DomainName", null); // Read an element with a text value.
	/// if (domainName == null)
	///		throw new CannotReadXmlElementException(GetType(), method, "DomainName");
	///	
	///	if (!adaptor.ReadElement("DomainVariables")) // Read the start of an element with child elements.
	///		throw new CannotReadXmlElementException(GetType(), method, "DomainVariables");
	///	
	///	while (adaptor.ReadElement("Variable")) // Iterate over a variable number of child elements.
	///	{
	///		string typeName = adaptor.ReadAttributeString("type", true); // Read a mandatory attribute.
	///		string valueXml = adaptor.ReadString(); // Read the element text value.
	///		adaptor.ReadEndElement(); // End of the &lt;Variable&gt; element - advance the reader to the next element.
	///	}
	///	
	///	adaptor.ReadEndElement(); // End of the &lt;DomainVariables&gt; element.
	/// </code>
	/// The above code could be used to read the following XML fragment:
	/// <code>
	/// &lt;DomainName&gt;MyDomain&lt;/DomainName&gt;
	/// &lt;DomainVariables&gt;
	///		&lt;Variable type="int"&gt;1&lt;/Variable&gt;
	///		&lt;Variable type="string"&gt;Two&lt;/Variable&gt;
	/// &lt;/DomainVariables&gt;
	/// </code>
	/// </example>
	/// <example>
	/// The following example reads the same XML content as the previous example, but all elements are treated
	/// as optional.
	/// <code>
	/// XmlReadAdaptor adaptor = new XmlReadAdaptor(reader, "http://xmlns.linkme.com.au/Framework/Configuration");
	/// 
	///	while (adaptor.ReadElement()) // Advance to the next element.
	///	{
	///		// Check the name of the element at which the reader is positioned. Note that the XML namespace
	///		// specified in the constructor is used by default.
	///
	///		if (adaptor.IsReadingElement("DomainName"))
	///		{
	///			m_domainName = adaptor.ReadString();
	///		}
	///		else if (adaptor.IsReadingElement("DomainVariables"))
	///		{
	///			while (adaptor.ReadElement("Variable")) // Iterate over a variable number of child elements.
	///			{
	///				string typeName = adaptor.ReadAttributeString("type", true); // Read a mandatory attribute.
	///				string valueXml = adaptor.ReadString(); // Read the element text value.
	///				adaptor.ReadEndElement(); // End of the &lt;Variable&gt; element - advance the reader to the next element.
	///			}
	///		}
	///
	///		// No need to call ReadEndElement() in this case, as ReadElement() automatically reads past the
	///		// end of the current element to the start of the next one.
	///	}
	/// </code>
	/// Note that the ReadElement() overloads that take the name (and optionally, the namespace) of the element
	/// would not be appropriate in this case, because they skip over any non-matching elements. For example, if
	/// the &lt;DomainVariables&gt; element occurred before the &lt;DomainName&gt; element it would not be read at
	/// all. The call to <c>ReadElementString("DomainName")</c> would skip over the &lt;DomainVariables&gt; element
	/// to find the &lt;DomainName&gt; element and the subsequent call to <c>ReadElement("DomainVariables")</c>
	/// would advance to the end of the enclosing parent element and return false.
	/// </example>
	public class XmlReadAdaptor
	{
		#region Constructors

		public XmlReadAdaptor(TextReader textReader, string ns)
		{
			// Construct an XML reader.

			m_reader = new XmlTextReader(textReader);
			m_namespace = ns;

			// Position it at the root element.

			m_reader.MoveToContent();
			m_isReadingElement = false;
			m_isEmptyElement = false;
			m_isReadXmlElement = false;
			m_emptyElements = new Stack();
		}

		public XmlReadAdaptor(XmlReader reader, string ns)
			: this(reader, ns, true)
		{
		}
		
		public XmlReadAdaptor(XmlReader reader, string ns, bool readChildren)
		{
			// Keep a reference to the reader and namespace.

			m_reader = reader;
			m_namespace = ns;

			if (readChildren)
			{
				// Indicate that the current element is being read,
				// ie this is assuming that the reader is currently positioned at the element
				// below which it is intended that nodes will be read.

				m_isReadingElement = true;
			}
			else
			{
				// Indicate that the current element is not being read,
				// ie this is assuming that the reader is currently positioned at the element
				// which is intended to be read.

				m_isReadingElement = false;
			}

			m_isEmptyElement = m_reader.IsEmptyElement;
			m_isReadXmlElement = false;
			m_emptyElements = new Stack();
		}

		public XmlReadAdaptor(TextReader textReader)
			:	this(textReader, null)
		{
		}

		public XmlReadAdaptor(XmlReader reader)
			:	this(reader, null)
		{
		}

		#endregion

		#region Properties

		public XmlReader XmlReader
		{
			get 
			{
				return m_reader; 
			}
		}

		public string Name
		{
			get { return m_reader.LocalName; }
		}

		public string NamespaceURI
		{
			get { return m_reader.NamespaceURI; }
		}

        public bool HasValue
        {
            get { return m_reader.HasValue; }
        }

	    public bool IsEmptyElement
	    {
            get { return m_reader.IsEmptyElement; }
	    }

	    #endregion

		#region Elements

		public bool IsReadingElement(string name, bool useNamespace)
		{
			// Check that in fact there is an element to read.

			if ( !m_isReadingElement )
				return false;

			// Now check that it matches the criteria.

			if ( useNamespace )
				return m_reader.IsStartElement(name, m_namespace);
			else
				return m_reader.IsStartElement(name);
		}

		public bool IsReadingElement(string name, string ns)
		{
			// Check that in fact there is an element to read.

			if ( !m_isReadingElement )
				return false;

			// Determine whether the reader is at the element.

			return m_reader.IsStartElement(name, ns);
		}

		public bool IsReadingElement(string name)
		{
			return IsReadingElement(name, !(m_namespace == null || m_namespace.Length == 0));
		}

		public bool IsReadingElement()
		{
			// Check that in fact there is an element to read.

			if ( !m_isReadingElement )
				return false;

			// Now check that it matches the criteria.

			return m_reader.IsStartElement();
		}

		public bool ReadElement(string name, bool useNamespace)
		{
			// Check that in fact there is an element to read.

			if ( CannotReadElement() )
				return false;

			// Now check that it matches the criteria.

			int initialDepth = m_reader.Depth;
			while ( m_reader.NodeType == XmlNodeType.Element && m_reader.Depth == initialDepth )
			{
				// Determine whether the reader is at the element.

				if ( useNamespace )
				{
					if ( m_reader.IsStartElement(name, m_namespace) )
						return ReadingElement();
				}
				else
				{
					if ( m_reader.IsStartElement(name) )
						return ReadingElement();
				}

				// Not intersted in this element so skip it.

				Skip();
			}

			return false;
		}

		public bool ReadElement(string name, string ns)
		{
			// Check that in fact there is an element to read.

			if ( CannotReadElement() )
				return false;

			// Now check that it matches the criteria.

			int initialDepth = m_reader.Depth;
			while ( m_reader.NodeType == XmlNodeType.Element && m_reader.Depth == initialDepth )
			{
				// Determine whether the reader is at the element.

				if ( m_reader.IsStartElement(name, ns) )
					return ReadingElement();

				// Not intersted in this element so skip it.

				Skip();
			}

			return false;
		}

		public bool ReadElement(string name)
		{
			return ReadElement(name, !(m_namespace == null || m_namespace.Length == 0));
		}

		public bool ReadElement()
		{
			// Check that in fact there is an element to read.

			if ( CannotReadElement() )
				return false;

			// Now check that it matches the criteria.

			if ( m_reader.IsStartElement() )
				return ReadingElement();
			else
				return false;
		}

		public void ReadEndElement()
		{
			if ( m_isReadXmlElement )
			{
				// Simply pop the value because the Inner* call has moved past all approrpiate nodes.

				m_emptyElements.Pop();
			}
			else
			{
				if ( !m_isReadingElement )
				{
					// If the reader is at an element node when this method is called
					// these are superflous child elements, so skip past them.

					while ( m_reader.NodeType == XmlNodeType.Element )
						Skip();
				}

				// Retrieve whether or not the element being ended is empty.

				if ( m_emptyElements.Count > 0 && (bool) m_emptyElements.Pop() )
				{
					// Only need to skip if the reader is still reading the element itself.

					if ( m_isReadingElement )
						Skip();
				}
				else
				{
					// Move up a level.

					Skip();
				}
			}

			m_isReadingElement = false;
			m_isEmptyElement = false;
			m_isReadXmlElement = false;
		}

		#endregion

		#region Element Values

		public string ReadElementString(string name)
		{
			return ReadElementString(name, !(m_namespace == null || m_namespace.Length == 0));
		}

		public string ReadElementString(string name, bool useNamespace)
		{
			const string method = "ReadElementString";

			// Check that in fact there is an element to read.

			if ( CannotReadElement() )
				throw new CannotReadXmlElementException(typeof(XmlReadAdaptor), method, name);

			// Now check that it matches the criteria.

			bool isStartElement;
			if ( useNamespace )
				isStartElement = m_reader.IsStartElement(name, m_namespace);
			else
				isStartElement = m_reader.IsStartElement(name);
			
			if ( isStartElement )
			{
				ReadingElement();
				string value = ReadString();
				ReadEndElement();
				return value;
			}
			else
			{
				throw new CannotReadXmlElementException(typeof(XmlReadAdaptor), method, name);
			}
		}

		public string ReadElementString(string name, string defaultValue)
		{
			// Check that in fact there is an element to read.

			if ( CannotReadElement() )
				return defaultValue;

			// Now check that it matches the criteria.

			if ( m_reader.IsStartElement(name, m_namespace) )
			{
				ReadingElement();
				string value = ReadString();
				ReadEndElement();
				return value;
			}
			else
			{
				return defaultValue;
			}
		}

		public string ReadElementString(string name, bool useNamespace, string defaultValue)
		{
			// Check that in fact there is an element to read.

			if ( CannotReadElement() )
				return defaultValue;

			// Now check that it matches the criteria.

			bool isStartElement;
			if ( useNamespace )
				isStartElement = m_reader.IsStartElement(name, m_namespace);
			else
				isStartElement = m_reader.IsStartElement(name);
			
			if ( isStartElement )
			{
				ReadingElement();
				string value = ReadString();
				ReadEndElement();
				return value;
			}
			else
			{
				return defaultValue;
			}
		}

		public string ReadElementString(string name, string ns, string defaultValue)
		{
			// Check that in fact there is an element to read.

			if ( CannotReadElement() )
				return defaultValue;

			// Now check that it matches the criteria.

			if ( m_reader.IsStartElement(name, ns) )
			{
				ReadingElement();
				string value = ReadString();
				ReadEndElement();
				return value;
			}
			else
			{
				return defaultValue;
			}
		}

		public System.Enum ReadElementEnum(string name, System.Type enumType, System.Enum defaultValue)
		{
			// Check that in fact there is an element to read.

			if ( CannotReadElement() )
				return defaultValue;

			// Now check that it matches the criteria.

			if ( m_reader.IsStartElement(name, m_namespace) )
			{
				ReadingElement();
				string value = ReadString();
				ReadEndElement();
				return (System.Enum) System.Enum.Parse(enumType, value);
			}
			else
			{
				return defaultValue;
			}
		}

		public string ReadElementInnerXml(string name)
		{
			return ReadElementInnerXml(name, !(m_namespace == null || m_namespace.Length == 0));
		}

		public string ReadElementInnerXml(string name, bool useNamespace)
		{
			const string method = "ReadElementInnerXml";

			// Check that in fact there is an element to read.

			if ( CannotReadElement() )
				throw new CannotReadXmlElementException(typeof(XmlReadAdaptor), method, name);

			// Now check that it matches the criteria.

			bool isStartElement;
			if ( useNamespace )
				isStartElement = m_reader.IsStartElement(name, m_namespace);
			else
				isStartElement = m_reader.IsStartElement(name);
			
			if ( isStartElement )
			{
				ReadingElement();
				string value = ReadInnerXml();
				ReadEndElement();
				return value;
			}
			else
			{
				throw new CannotReadXmlElementException(typeof(XmlReadAdaptor), method, name);
			}
		}

		public string ReadElementInnerXml(string name, string defaultValue)
		{
			// Check that in fact there is an element to read.

			if ( CannotReadElement() )
				return defaultValue;

			// Now check that it matches the criteria.

			if ( m_reader.IsStartElement(name, m_namespace) )
			{
				ReadingElement();
				string value = ReadInnerXml();
				ReadEndElement();
				return value;
			}
			else
			{
				return defaultValue;
			}
		}

		#endregion

		#region Attribute Values

		public string ReadAttributeString(string name)
		{
			return ReadAttributeString(name, true);
		}

		public string ReadAttributeString(string name, string defaultValue)
		{
			string value = m_reader.GetAttribute(name);
			return value != null ? value : defaultValue;
		}

		public string ReadAttributeString(string name, bool mandatory)
		{
			const string method = "ReadAttributeString";

			string value = m_reader.GetAttribute(name);

			if ( value != null )
				return value;
			else if (mandatory)
				throw new CannotReadXmlAttributeException(typeof(XmlReadAdaptor), method, m_reader.Name, name);
			else
				return null;
		}

		public string ReadAttributeString(string name, string ns, bool mandatory)
		{
			const string method = "ReadAttributeString";

			string value = m_reader.GetAttribute(name, ns);

			if ( value != null )
				return value;
			else if (mandatory)
				throw new CannotReadXmlAttributeException(typeof(XmlReadAdaptor), method, m_reader.Name, name);
			else
				return null;
		}

		public string ReadAttributeString(string name, string ns, string defaultValue)
		{
			string value = m_reader.GetAttribute(name, ns);
			return value != null ? value : defaultValue;
		}

		public bool ReadAttributeBoolean(string name, bool defaultValue)
		{
			string value = m_reader.GetAttribute(name);
			return value != null ? XmlConvert.ToBoolean(value) : defaultValue;
		}

		public bool ReadAttributeBoolean(string name, string ns, bool defaultValue)
		{
			string value = m_reader.GetAttribute(name, ns);
			return value != null ? XmlConvert.ToBoolean(value) : defaultValue;
		}

		public int ReadAttributeInt32(string name)
		{
			const string method = "ReadAttributeInt32";
			
			string value = m_reader.GetAttribute(name);
			if ( value != null )
				return XmlConvert.ToInt32(value);
			else
				throw new CannotReadXmlAttributeException(typeof(XmlReadAdaptor), method, m_reader.Name, name);
		}

		public int ReadAttributeInt32(string name, int defaultValue)
		{
			string value = m_reader.GetAttribute(name);
			return value != null ? XmlConvert.ToInt32(value) : defaultValue;
		}

		public int ReadAttributeInt32(string name, string ns, int defaultValue)
		{
			string value = m_reader.GetAttribute(name, ns);
			return value != null ? XmlConvert.ToInt32(value) : defaultValue;
		}

		public System.Enum ReadAttributeEnum(string name, System.Type enumType)
		{
			const string method = "ReadAttributeEnum";

			string value = m_reader.GetAttribute(name);
			if ( value != null )
				return (System.Enum) System.Enum.Parse(enumType, value);
			else
				throw new CannotReadXmlAttributeException(typeof(XmlReadAdaptor), method, m_reader.Name, name);
		}

		public System.Enum ReadAttributeEnum(string name, string ns, System.Type enumType)
		{
			const string method = "ReadAttributeEnum";

			string value = m_reader.GetAttribute(name, ns);
			if ( value != null )
				return (System.Enum) System.Enum.Parse(enumType, value);
			else
				throw new CannotReadXmlAttributeException(typeof(XmlReadAdaptor), method, m_reader.Name, name);
		}

		public System.Enum ReadAttributeEnum(string name, System.Type enumType, System.Enum defaultValue)
		{
			string value = m_reader.GetAttribute(name);
			return value != null ? (System.Enum) System.Enum.Parse(enumType, value) : defaultValue;
		}

		public System.Enum ReadAttributeEnum(string name, string ns, System.Type enumType, System.Enum defaultValue)
		{
			string value = m_reader.GetAttribute(name, ns);
			return value != null ? (System.Enum) System.Enum.Parse(enumType, value) : defaultValue;
		}

		public string[] ReadAttributeStringArray(string name, string[] defaultValue)
		{
			string value = m_reader.GetAttribute(name);
			return value != null && value.Length > 0 ? value.Split(' ') : defaultValue;
		}

		public string[] ReadAttributeStringArray(string name, string ns, string[] defaultValue)
		{
			string value = m_reader.GetAttribute(name, ns);
			return value != null && value.Length > 0 ? value.Split(' ') : defaultValue;
		}

		#endregion

		#region Read

		public string GetValue()
		{
			return m_reader.NodeType == XmlNodeType.Element ? ReadString() : m_reader.Value;
		}

		public string ReadOuterXml()
		{
			// Get the value.

			string xml = m_reader.ReadOuterXml();

			// The previous call has consumed all the appropriate nodes so we need to track that.

			m_isReadXmlElement = true;

			// Move past any whitespace.

			if ( m_reader.NodeType == XmlNodeType.Whitespace )
			{
				m_reader.Read();
				m_isReadingElement = false;
				m_isEmptyElement = false;
			}

			return xml;
		}

		public string ReadInnerXml()
		{
			// Get the value.

			string xml = m_reader.ReadInnerXml();

			// The previous call has consumed all the appropriate nodes so we need to track that.

			m_isReadXmlElement = true;

			// Move past any whitespace.

			if ( m_reader.NodeType == XmlNodeType.Whitespace )
			{
				m_reader.Read();
				m_isReadingElement = false;
				m_isEmptyElement = false;
			}

			return xml;
		}

		public bool ReadXsiNilAttribute()
		{
			return ReadAttributeBoolean(Constants.Xsi.NilAttribute, Constants.Xsi.Namespace, false);
		}

		public string ReadName()
		{
			return ReadAttributeString(Constants.Xml.NameAttribute);
		}

		public string ReadDescription()
		{
			return ReadElementString(Constants.Xml.DescriptionElement, string.Empty);
		}

		public System.Version ReadVersion()
		{
			string value = m_reader.GetAttribute(Constants.Xml.VersionAttribute);
            return (value == null || value.Length == 0 ? null : new System.Version(value));
		}

		public ClassInfo ReadClass()
		{
			string value = m_reader.GetAttribute(Constants.Xml.ClassAttribute);
			return value != null ? new ClassInfo(value) : null;
		}

		public bool ReadDefault(out string defaultValueAsXml)
		{
			defaultValueAsXml = null;

			// Check that in fact there is an element to read.

			if ( CannotReadElement() )
				return false;

			// Now check that it matches the criteria.

			if ( m_reader.IsStartElement(Constants.Xml.DefaultElement) )
			{
				ReadingElement();

				// Read the "isNull" attribute, and, if not set to true, the string value.

				if ( !ReadAttributeBoolean(Constants.Xml.IsNullAttribute, false) )
					defaultValueAsXml = ReadString();
				ReadEndElement();
				return true;
			}
			else
				return false;
		}

		#endregion

		public string LookupNamespace(string prefix)
		{
			return m_reader.LookupNamespace(prefix);
		}

		public void MoveToElement()
		{
			if (CannotReadElement())
				throw new CannotReadXmlElementException(this.GetType(), "MoveToElement", m_reader.LocalName);
		}

		private bool CannotReadElement()
		{
			if ( m_isReadingElement || m_reader.NodeType == XmlNodeType.None )
			{
				// Currently on an element, requesting to move to a child element,
				// so check if there are any elements to read.

				if ( m_isEmptyElement )
					return true;

				// Try to move to the next element.

				Read();
				m_isReadingElement = false;
				m_isEmptyElement = false;
				m_isReadXmlElement = false;
			}

			// Can now check that the element is in fact the one requested.

			return false;
		}

		private bool ReadingElement()
		{
			// Update the state.

			m_isReadingElement = true;
			m_isEmptyElement = m_reader.IsEmptyElement;
			m_isReadXmlElement = false;
			m_emptyElements.Push(m_reader.IsEmptyElement);
			return true;
		}

		private void Skip()
		{
			m_reader.Skip();

			// Move past anything not of interest.

			while ( m_reader.NodeType == XmlNodeType.Whitespace || m_reader.NodeType == XmlNodeType.Comment || m_reader.NodeType == XmlNodeType.XmlDeclaration )
				m_reader.Read();
		}

		private void Read()
		{
			m_reader.Read();

			// Move past anything not of interest.

			while ( m_reader.NodeType == XmlNodeType.Whitespace || m_reader.NodeType == XmlNodeType.Comment || m_reader.NodeType == XmlNodeType.XmlDeclaration )
				m_reader.Read();
		}

		public string ReadString()
		{
			string value = m_reader.ReadString();

			// Move past any whitespace.

			if ( m_reader.NodeType == XmlNodeType.Whitespace )
				m_reader.Read();
			return value;
		}

		private string m_namespace;
		private XmlReader m_reader;
		private bool m_isReadingElement;
		private bool m_isEmptyElement;
		private bool m_isReadXmlElement;
		private Stack m_emptyElements;
	}
}
