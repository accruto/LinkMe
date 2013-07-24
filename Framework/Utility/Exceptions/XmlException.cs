using System.Runtime.Serialization;

namespace LinkMe.Framework.Utility.Exceptions
{
	[System.Serializable]
	public sealed class CannotReadXmlElementException
		:	UtilityException
	{
		public CannotReadXmlElementException()
			:	base(m_propertyInfos)
		{
		}

		public CannotReadXmlElementException(System.Type source, string method, string element, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(element);
		}

		public CannotReadXmlElementException(string source, string method, string element, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(element);
		}

		public CannotReadXmlElementException(System.Type source, string method, string element)
			:	base(m_propertyInfos, source, method)
		{
			Set(element);
		}

		public CannotReadXmlElementException(string source, string method, string element)
			:	base(m_propertyInfos, source, method)
		{
			Set(element);
		}

		private CannotReadXmlElementException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string Element
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.Element, string.Empty); }
		}

		private void Set(string element)
		{
			SetPropertyValue(Constants.Exceptions.Element, element == null ? string.Empty : element);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.Element, System.TypeCode.String),
			};
	}

	[System.Serializable]
	public sealed class CannotReadXmlAttributeException
		:	UtilityException
	{
		public CannotReadXmlAttributeException()
			:	base(m_propertyInfos)
		{
		}

		public CannotReadXmlAttributeException(System.Type source, string method, string element, string attribute, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(element, attribute);
		}

		public CannotReadXmlAttributeException(string source, string method, string element, string attribute, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(element, attribute);
		}

		public CannotReadXmlAttributeException(System.Type source, string method, string element, string attribute)
			:	base(m_propertyInfos, source, method)
		{
			Set(element, attribute);
		}

		public CannotReadXmlAttributeException(string source, string method, string element, string attribute)
			:	base(m_propertyInfos, source, method)
		{
			Set(element, attribute);
		}

		private CannotReadXmlAttributeException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string Element
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.Element, string.Empty); }
		}

		public string Attribute
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.Attribute, string.Empty); }
		}

		private void Set(string element, string attribute)
		{
			SetPropertyValue(Constants.Exceptions.Element, element == null ? string.Empty : element);
			SetPropertyValue(Constants.Exceptions.Attribute, attribute == null ? string.Empty : attribute);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.Element, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.Attribute, System.TypeCode.String),
			};
	}

	[System.Serializable]
	public sealed class CannotSaveFormattedXmlException
		:	UtilityException
	{
		public CannotSaveFormattedXmlException()
			:	base(m_propertyInfos)
		{
		}

		public CannotSaveFormattedXmlException(System.Type source, string method, string xml, string fileName, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(xml, fileName);
		}

		public CannotSaveFormattedXmlException(string source, string method, string xml, string fileName, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(xml, fileName);
		}

		private CannotSaveFormattedXmlException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string XmlString
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.XmlString, string.Empty); }
		}

		public string FileName
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.FileName, string.Empty); }
		}

		private void Set(string xml, string fileName)
		{
			SetPropertyValue(Constants.Exceptions.XmlString, xml == null ? "<null>" : xml);
			SetPropertyValue(Constants.Exceptions.FileName, fileName == null ? "<null>" : fileName);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.XmlString, System.TypeCode.String),
			new PropertyInfo(Constants.Exceptions.FileName, System.TypeCode.String)
		};
	}

	[System.Serializable]
	public sealed class XmlPrefixNotFoundException
		:	UtilityException
	{
		public XmlPrefixNotFoundException()
			:	base(m_propertyInfos)
		{
		}

		public XmlPrefixNotFoundException(System.Type source, string method, string prefix)
			:	base(m_propertyInfos, source, method)
		{
			Set(prefix);
		}

		public XmlPrefixNotFoundException(string source, string method, string prefix)
			:	base(m_propertyInfos, source, method)
		{
			Set(prefix);
		}

		private XmlPrefixNotFoundException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string Prefix
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.Prefix, string.Empty); }
		}

		private void Set(string prefix)
		{
			SetPropertyValue(Constants.Exceptions.Prefix, prefix == null ? "<null>" : prefix);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.Prefix, System.TypeCode.String)
		};
	}
}
