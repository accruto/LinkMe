using System.Collections;
using System.Diagnostics;
using System.Xml;
using System.Xml.Schema;

using LinkMe.Framework.Type.Exceptions;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Type
{
	/// <summary>
	/// Provides information about LinkMe primitive types.
	/// </summary>
	public class PrimitiveTypeInfo
	{
		public static readonly PrimitiveTypeInfo String = new PrimitiveTypeInfo(
			"String", PrimitiveType.String, typeof(System.String), System.String.Empty, XmlSchema.Namespace, "string");
		public static readonly PrimitiveTypeInfo Byte = new PrimitiveTypeInfo(
			"Byte", PrimitiveType.Byte, typeof(System.Byte), new System.Byte(), XmlSchema.Namespace, "unsignedByte");
		public static readonly PrimitiveTypeInfo Int16 = new PrimitiveTypeInfo(
			"Int16", PrimitiveType.Int16, typeof(System.Int16), new System.Int16(), XmlSchema.Namespace, "short");
		public static readonly PrimitiveTypeInfo Int32 = new PrimitiveTypeInfo(
			"Int32", PrimitiveType.Int32, typeof(System.Int32), new System.Int32(), XmlSchema.Namespace, "int");
		public static readonly PrimitiveTypeInfo Int64 = new PrimitiveTypeInfo(
			"Int64", PrimitiveType.Int64, typeof(System.Int64), new System.Int64(), XmlSchema.Namespace, "long");
		public static readonly PrimitiveTypeInfo Single = new PrimitiveTypeInfo(
			"Single", PrimitiveType.Single, typeof(System.Single), new System.Single(), XmlSchema.Namespace, "float");
		public static readonly PrimitiveTypeInfo Double = new PrimitiveTypeInfo(
			"Double", PrimitiveType.Double, typeof(System.Double), new System.Double(), XmlSchema.Namespace, "double");
		public static readonly PrimitiveTypeInfo Decimal = new PrimitiveTypeInfo(
			"Decimal", PrimitiveType.Decimal, typeof(System.Decimal), new System.Decimal(), XmlSchema.Namespace, "decimal");
		public static readonly PrimitiveTypeInfo Boolean = new PrimitiveTypeInfo(
			"Boolean", PrimitiveType.Boolean, typeof(System.Boolean), new System.Boolean(), XmlSchema.Namespace, "boolean");
		public static readonly PrimitiveTypeInfo Guid = new PrimitiveTypeInfo(
			"Guid", PrimitiveType.Guid, typeof(System.Guid), System.Guid.Empty, XmlSchema.Namespace, "string",
			Constants.Xml.Namespace, "guid", @"[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}");
		public static readonly PrimitiveTypeInfo Date = new PrimitiveTypeInfo(
			"Date", PrimitiveType.Date, typeof(Date), new Date(), XmlSchema.Namespace, "date", Constants.Xml.Namespace,
			"date", @"\d{4}-[01]\d-[0-3]\d");
		public static readonly PrimitiveTypeInfo DateTime = new PrimitiveTypeInfo(
			"DateTime", PrimitiveType.DateTime, typeof(DateTime), new DateTime(), XmlSchema.Namespace, "dateTime",
			Constants.Xml.Namespace, "dateTime", @"\d{4}-[01]\d-[0-3]\dT[012]\d:[0-5]\d:[0-5]\d(\.\d{1,9})?([+-][01]\d:[0-5]\d|Z( \S.{0,39})?)");
		public static readonly PrimitiveTypeInfo TimeOfDay = new PrimitiveTypeInfo(
			"TimeOfDay", PrimitiveType.TimeOfDay, typeof(TimeOfDay), new TimeOfDay(), XmlSchema.Namespace, "time",
			Constants.Xml.Namespace, "timeOfDay", @"[012]\d:[0-5]\d:[0-5]\d(\.\d{1,9})?");
		public static readonly PrimitiveTypeInfo TimeSpan = new PrimitiveTypeInfo(
			"TimeSpan", PrimitiveType.TimeSpan, typeof(TimeSpan), new TimeSpan(), XmlSchema.Namespace, "duration",
			Constants.Xml.Namespace, "timeSpan", @"-?P(\d{1,8}D)?(T((\d{1,9}H)?(\d{1,9}M)?\d{1,9}(\.\d{1,9})?S|(\d{1,9}H)?\d{1,9}M|\d{1,9}H))?");

		/// <summary>
		/// The collection of all primitive types.
		/// </summary>
		public static readonly PrimitiveTypeInfos PrimitiveTypeInfos = new PrimitiveTypeInfos(
			Boolean, Byte, Date, DateTime, Decimal, Double, Guid, Int16, Int32, Int64, Single, String, TimeOfDay, TimeSpan);

		private static readonly string m_typeNamespace = typeof(PrimitiveTypeInfo).Namespace;
		private static readonly string m_typeNamespacePrefix = m_typeNamespace + ".";

		private readonly string m_name;
		private readonly PrimitiveType m_primitiveType;
		private readonly System.Type m_netType;
		private readonly bool m_isXmlPrimitiveType;
		private readonly XmlQualifiedName m_xmlPrimitiveTypeQualifiedName;
		private readonly string m_xmlPatternFacetValue;
		private readonly XmlQualifiedName m_xmlTypeQualifiedName;
		private readonly object m_default;

		#region Constructors

		/// <summary>
		/// Private constructor for preventing the creation of PrimitiveTypeInfo
		/// </summary>
		/// <param name="name">name of the primitive type</param>
		/// <param name="primitiveType">Type of the primitive type</param>
		/// <param name="netType">.Net Type of the primitive type</param>
		/// <param name="defaultValue">Default value of the primitive type</param>
		/// <param name="xmlPrimitiveTypeNamespace">XmlNamespace of the primitive type</param>
		/// <param name="xmlPrimitiveTypeName">XmlTypeName of the primitive type</param>
		private PrimitiveTypeInfo(
			string name, PrimitiveType primitiveType, System.Type netType, object defaultValue,
			string xmlPrimitiveTypeNamespace, string xmlPrimitiveTypeName
			) :
			this(name, primitiveType, netType, defaultValue,
			xmlPrimitiveTypeNamespace, xmlPrimitiveTypeName,
			null, null, null)
		{
		}

		/// <summary>
		/// Private constructor for preventing the creation of PrimitiveTypeInfo
		/// </summary>
		/// <param name="name">name of the primitive type</param>
		/// <param name="primitiveType">Type of the primitive type</param>
		/// <param name="netType">.Net Type of the primitive type</param>
		/// <param name="defaultValue">Default value of the primitive type</param>
		/// <param name="xmlSimpleTypeNamespace">Xml simple type namesspace</param>
		/// <param name="xmlPatternFacet">Xml pattern facet</param>
		private PrimitiveTypeInfo(
			string name, PrimitiveType primitiveType, System.Type netType, object defaultValue,
			string xmlPrimitiveTypeNamespace, string xmlPrimitiveTypeName,
			string xmlSimpleTypeNamespace, string xmlSimpleTypeName, string xmlPatternFacet)
		{
			m_name = name;
			m_primitiveType = primitiveType;
			m_netType = netType;
			m_default = defaultValue;

			// Xml information
			
			m_isXmlPrimitiveType = (xmlSimpleTypeNamespace == null);
			m_xmlPrimitiveTypeQualifiedName = new XmlQualifiedName(xmlPrimitiveTypeName, xmlPrimitiveTypeNamespace);

			if ( m_isXmlPrimitiveType )
			{
				// The Qualified Name is just that of the Primitive Type

				m_xmlTypeQualifiedName = m_xmlPrimitiveTypeQualifiedName;
			}
			else
			{
				// The Qualified Name is that of the Simple Type

				m_xmlTypeQualifiedName = new XmlQualifiedName(xmlSimpleTypeName, xmlSimpleTypeNamespace);

				// Store the information with which to create the Xml Schema Simple Type

				m_xmlPatternFacetValue = xmlPatternFacet;
			}
		}

		#endregion

		#region Static methods

		/// <summary>
		/// Gets a PrimitiveTypeInfo object for the specified primitive type.
		/// </summary>
		public static PrimitiveTypeInfo GetPrimitiveTypeInfo(PrimitiveType type)
		{
			const string method = "GetPrimitiveTypeInfo";

			switch ( type )
			{
				case PrimitiveType.None:
					return null;

				case PrimitiveType.String:
					return String;

				case PrimitiveType.Byte:
					return Byte;

				case PrimitiveType.Int16:
					return Int16;

				case PrimitiveType.Int32:
					return Int32;

				case PrimitiveType.Int64:
					return Int64;

				case PrimitiveType.Single:
					return Single;

				case PrimitiveType.Double:
					return Double;

				case PrimitiveType.Decimal:
					return Decimal;

				case PrimitiveType.Boolean:
					return Boolean;

				case PrimitiveType.Guid:
					return Guid;

				case PrimitiveType.Date:
					return Date;

				case PrimitiveType.DateTime:
					return DateTime;

				case PrimitiveType.TimeOfDay:
					return TimeOfDay;

				case PrimitiveType.TimeSpan:
					return TimeSpan;

				default:
					throw new InvalidParameterValueException(typeof(PrimitiveTypeInfo), method, "type", typeof(PrimitiveType), type);
			}
		}

		/// <summary>
		/// Gets a PrimitiveTypeInfo object for the specified .NET type. If there is no
		/// corresponding the method returns null.
		/// </summary>
		public static PrimitiveTypeInfo GetPrimitiveTypeInfo(System.Type type)
		{
			return GetPrimitiveTypeInfo(type, false);
		}

		/// <summary>
		/// Gets a PrimitiveTypeInfo object for the specified .NET type, specifying whether
		/// to throw an exception if there is no corresponding LinkMe primitive type.
		/// </summary>
		/// <param name="throwOnError"> true to throw an exception if there is no
		///  corresponding .NET type  -or-  false to return null in that case.</param>
		public static PrimitiveTypeInfo GetPrimitiveTypeInfo(System.Type type, bool throwOnError)
		{
			const string method = "GetPrimitiveTypeInfo";

			if ( type == null )
			{
				if ( throwOnError )
					throw new NullParameterException(typeof(PrimitiveTypeInfo), method, "type");
				else 
					return null;
			}

			if ( type == typeof(System.String) )
				return String;
			if ( type == typeof(System.Byte) )
				return Byte;
			if ( type == typeof(System.Int16) )
				return Int16;
			if ( type == typeof(System.Int32) )
				return Int32;
			if ( type == typeof(System.Int64) )
				return Int64;
			if ( type == typeof(System.Single) )
				return Single;
			if ( type == typeof(System.Double) )
				return Double;
			if ( type == typeof(System.Decimal) )
				return Decimal;
			if ( type == typeof(System.Boolean) )
				return Boolean;
			if ( type == typeof(System.Guid) )
				return Guid;
			if ( type == typeof(Type.Date) )
				return Date;
			if ( type == typeof(Type.DateTime) )
				return DateTime;
			if ( type == typeof(Type.TimeOfDay) )
				return TimeOfDay;
			if ( type == typeof(Type.TimeSpan) )
				return TimeSpan;

			if ( throwOnError )
				throw new InvalidParameterValueException(typeof(PrimitiveTypeInfo), method, "type", typeof(PrimitiveType), type.AssemblyQualifiedName);
			else
				return null;
		}

		/// <summary>
		/// Gets a PrimitiveTypeInfo object for the specified type name. If there is no
		/// type with this name the method returns null.
		/// </summary>
		public static PrimitiveTypeInfo GetPrimitiveTypeInfo(string type)
		{
			return GetPrimitiveTypeInfo(type, false);
		}

		/// <summary>
		/// Gets a PrimitiveTypeInfo object for the specified type name, specifying whether
		/// to throw an exception if there is no type with this name.
		/// </summary>
		/// <param name="throwOnError"> true to throw an exception if there is no
		/// type with the specified name  -or-  false to return null in that case.</param>
		public static PrimitiveTypeInfo GetPrimitiveTypeInfo(string type, bool throwOnError)
		{
			const string method = "GetPrimitiveTypeInfo";

			if ( type == null || type.Length == 0 )
			{
				if ( throwOnError )
					throw new NullParameterException(typeof(PrimitiveTypeInfo), method, "type");
				else 
					return null;
			}

			// Perform a case-insensitive comparison. string.Compare() is faster for a single comparison, but
			// converting to lowercase and using == is faster for multiple comparisons.

			string typeName;
			if ( string.Compare(type, 0, m_typeNamespacePrefix, 0, m_typeNamespacePrefix.Length, true ) == 0 )
				typeName = type.Substring(m_typeNamespacePrefix.Length).ToLower();
			else
				typeName = type.ToLower();

			switch ( typeName )
			{
				case "string":
					return String;
				case "byte":
					return Byte;
				case "int16":
					return Int16;
				case "int32":
					return Int32;
				case "int64":
					return Int64;
				case "single":
					return Single;
				case "double":
					return Double;
				case "decimal":
					return Decimal;
				case "boolean":
					return Boolean;
				case "guid":
					return Guid;
				case "date":
					return Date;
				case "datetime":
					return DateTime;
				case "timeofday":
					return TimeOfDay;
				case "timespan":
					return TimeSpan;
				default:
					if ( throwOnError )
						throw new PrimitiveTypeNotFoundException(typeof(PrimitiveTypeInfo), method, type);
					else
						return null;
			}
		}

		/// <summary>
		/// Gets a PrimitiveTypeInfo object for the specified type name. If there is no
		/// type with this name the method returns null.
		/// </summary>
		public static PrimitiveTypeInfo GetPrimitiveTypeInfoByXmlType(string type, string ns)
		{
			return GetPrimitiveTypeInfoByXmlType(type, ns, false);
		}

		public static PrimitiveTypeInfo GetPrimitiveTypeInfoByXmlType(string type, string ns, bool throwOnError)
		{
			const string method = "GetPrimitiveTypeInfoByXmlType";

			if ( type == null || type.Length == 0 )
			{
				if ( throwOnError )
				{
					if ( type == null )
						throw new NullParameterException(typeof(PrimitiveTypeInfo), method, "type");
					else
						throw new ParameterStringTooShortException(typeof(PrimitiveTypeInfo), method, "type", type, 1);
				}
				else 
					return null;
			}

			if ( ns == null || ns.Length == 0 )
			{
				switch ( type )
				{
					case "string":
						return String;
					case "unsignedByte":
						return Byte;
					case "short":
						return Int16;
					case "int":
						return Int32;
					case "long":
						return Int64;
					case "float":
						return Single;
					case "double":
						return Double;
					case "decimal":
						return Decimal;
					case "boolean":
						return Boolean;
				}
			}
			else if ( ns == Constants.Xml.Namespace )
			{
				switch ( type )
				{
					case "guid":
						return Guid;
					case "date":
						return Date;
					case "dateTime":
						return DateTime;
					case "timeOfDay":
						return TimeOfDay;
					case "timeSpan":
						return TimeSpan;
				}
			}

			if ( throwOnError )
				throw new PrimitiveTypeXsiTypeNotFoundException(typeof(PrimitiveTypeInfo), method, type, ns);
			else
				return null;
		}

		public static PrimitiveTypeInfo ReadXsiTypeAttribute(XmlReadAdaptor adaptor)
		{
			return ReadXsiTypeAttribute(adaptor, false);
		}

		public static PrimitiveTypeInfo ReadXsiTypeAttribute(XmlReadAdaptor adaptor, bool throwOnError)
		{
			const string method = "ReadXsiTypeAttribute";

			if (adaptor == null)
				throw new NullParameterException(typeof(PrimitiveTypeInfo), method, "adaptor");

			string xsiType = adaptor.ReadAttributeString(Constants.Xsi.TypeAttribute,
				Constants.Xsi.Namespace, throwOnError);
			if (xsiType == null)
			{
				Debug.Assert(!throwOnError, "!throwOnError");
				return null;
			}

			if (xsiType.Length == 0)
				throw new PrimitiveTypeXsiTypeNotFoundException(typeof(PrimitiveTypeInfo), method, xsiType, null);

			int index = xsiType.IndexOf(':');
			if (index == -1)
				return GetPrimitiveTypeInfoByXmlType(xsiType, null, throwOnError);

			string prefix = xsiType.Substring(0, index);
			string ns = adaptor.LookupNamespace(prefix);

			// If a prefix was specified it should resolve to a valid namespace - throw.

			if (ns == null)
				throw new XmlPrefixNotFoundException(typeof(PrimitiveTypeInfo), method, prefix);

			return GetPrimitiveTypeInfoByXmlType(xsiType.Substring(index + 1), ns, throwOnError);
		}

		public static PrimitiveTypeInfo GetPrimitiveTypeInfoByXmlType(XmlQualifiedName qualifiedName)
		{
			return GetPrimitiveTypeInfoByXmlType(qualifiedName, false);
		}

		public static PrimitiveTypeInfo GetPrimitiveTypeInfoByXmlType(XmlQualifiedName qualifiedName, bool throwOnError)
		{
			const string method = "GetPrimitiveTypeInfoByXmlType";

			if ( qualifiedName == null )
			{
				if ( throwOnError )
					throw new NullParameterException(typeof(PrimitiveTypeInfo), method, "qualifiedName");
				else 
					return null;
			}

			return GetPrimitiveTypeInfoByXmlType(qualifiedName.Name, qualifiedName.Namespace, throwOnError);
		}

		public static string TrimTypeNamespace(string typeName)
		{
			if (typeName == null)
				return null;

			// Remove the LinkMe.Framework.Type namespace from the beginning.

			if (typeName.StartsWith(m_typeNamespace))
				return typeName.Substring(m_typeNamespace.Length + 1);
			else
				return typeName;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Name of the primitive type.
		/// </summary>
		public string Name
		{
			get { return m_name; }
		}

		/// <summary>
		/// Full name of the primitive type.
		/// </summary>
		public string FullName
		{
			get { return m_typeNamespace + "." + m_name; }
		}

		/// <summary>
		/// PrimitiveType enum value of the primitive type.
		/// </summary>
		public PrimitiveType PrimitiveType
		{
			get { return m_primitiveType; }
		}

		/// <summary>
		/// .NET type of the primitive type.
		/// </summary>
		public System.Type NetType
		{
			get { return m_netType; }
		}

		/// <summary>
		/// Gets the IsXmlSchemaPrimitiveType property.
		/// </summary>
		public bool IsXmlSchemaPrimitiveType
		{
			get { return m_isXmlPrimitiveType; }
		}

		/// <summary>
		/// Gets the XmlQualifiedName of the primitive type
		/// </summary>
		public XmlQualifiedName XmlSchemaTypeQualifiedName
		{
			get { return m_xmlTypeQualifiedName; }
		}

		/// <summary>
		/// Gets the XmlSchemaType of the primitive type
		/// </summary>
		public XmlSchemaType XmlSchemaType
		{
			get
			{
				XmlSchemaSimpleType xmlSimpleType = null;
				if ( !m_isXmlPrimitiveType )
				{
					XmlSchemaPatternFacet pattern = new XmlSchemaPatternFacet();
					pattern.Value = m_xmlPatternFacetValue;

					XmlSchemaSimpleTypeRestriction content = new XmlSchemaSimpleTypeRestriction();
					content.BaseTypeName = m_xmlPrimitiveTypeQualifiedName;
					content.Facets.Add(pattern);

					xmlSimpleType = new XmlSchemaSimpleType();
					xmlSimpleType.Name = m_name;
					xmlSimpleType.Content = content;
				}
				return xmlSimpleType;
			}
		}

		/// <summary>
		/// Gets the default value for the primitive type
		/// </summary>
		public object Default
		{
			get { return TypeClone.Clone(m_default); }
		}

		#endregion

		public void WriteXsiTypeAttribute(XmlWriteAdaptor adaptor)
		{
			const string method = "WriteXsiTypeAttribute";

			if (adaptor == null)
				throw new NullParameterException(GetType(), method, "adaptor");

			if (m_xmlTypeQualifiedName.Namespace == Constants.Xml.Namespace)
			{
				// Get the LinkMe Type prefix or write it if not found.

				string prefix = adaptor.WriteNamespace(Constants.Xml.Prefix, Constants.Xml.Namespace);

				adaptor.WriteAttribute(Constants.Xsi.TypeAttribute, Constants.Xsi.Prefix,
					Constants.Xsi.Namespace, prefix + ":" + m_xmlTypeQualifiedName.Name);
			}
			else
			{
				// XSD type - no prefix.

				adaptor.WriteAttribute(Constants.Xsi.TypeAttribute, Constants.Xsi.Prefix,
					Constants.Xsi.Namespace, m_xmlTypeQualifiedName.Name);
			}
		}
	}

	/// <summary>
	/// A read-only collection of PrimitiveTypeInfo objects.
	/// </summary>
	public sealed class PrimitiveTypeInfos : ICollection
	{
		private readonly PrimitiveTypeInfo[] m_values;

		internal PrimitiveTypeInfos(params PrimitiveTypeInfo[] values)
		{
			m_values = values;
		}

		#region ICollection Members

		public int Count
		{
			get { return m_values.Length; }
		}

		public bool IsSynchronized
		{
			get { return m_values.IsSynchronized; }
		}

		public object SyncRoot
		{
			get { return m_values.Length; }
		}

		void ICollection.CopyTo(System.Array array, int index)
		{
			m_values.CopyTo(array, index);
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return m_values.GetEnumerator();
		}

		#endregion

		public PrimitiveTypeInfo this[int index]
		{
			get { return m_values[index]; }
		}

		public void CopyTo(PrimitiveTypeInfo[] array, int index)
		{
			m_values.CopyTo(array, index);
		}
	}
}