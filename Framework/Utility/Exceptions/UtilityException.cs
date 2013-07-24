using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Resources;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Xml;

using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Utility.Exceptions
{
	[System.Serializable]
	public abstract class UtilityException
		:	BaseException,
			ISerializable
	{
		#region Constructors

		protected UtilityException(PropertyInfo[] propertyInfos)
			:	base()
		{
			Initialise(propertyInfos);
		}

		protected UtilityException(PropertyInfo[] propertyInfos, string source, string method, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Initialise(propertyInfos);
		}

		protected UtilityException(PropertyInfo[] propertyInfos, System.Type source, string method, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Initialise(propertyInfos);
		}

		protected UtilityException(PropertyInfo[] propertyInfos, string source, string method)
			:	base(source, method)
		{
			Initialise(propertyInfos);
		}

		protected UtilityException(PropertyInfo[] propertyInfos, System.Type source, string method)
			:	base(source, method)
		{
			Initialise(propertyInfos);
		}

		protected UtilityException(PropertyInfo[] propertyInfos, SerializationInfo info, StreamingContext context)
			:	base(info, context)
		{
			m_propertyInfos = propertyInfos;

			// Read the properties.

			m_properties = (Hashtable) info.GetValue(Constants.Serialization.Exception.Properties, typeof(Hashtable));
		}

		#endregion

		#region ISerializable Members

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);

			// Add the properties.

			info.AddValue(Constants.Serialization.Exception.Properties, m_properties);
		}

		#endregion

		public override string Message
		{
			get
			{
				// Get the message format and then replace the properties.

				string message = GetMessageFormat();

				// Need to substitute the property names with their corresponding index.
				// Find tags that look like '{PropertyName[,M][:FormatString]}'.

				StringBuilder newMessageText = new StringBuilder();
				object[] values = new object[m_properties.Count];

				int pos = message.IndexOf('{');
				while ( pos >= 0 )
				{
					newMessageText.Append(message, 0, pos + 1);
					message = message.Substring(pos + 1);

					// Look for the end of the property name.

					pos = message.IndexOfAny(new char[] { '}', ',', ':' });
					string propertyName = message.Substring(0, pos);
					int index = IndexOfPropertyInfo(propertyName);
					if ( index != -1 )
					{
						newMessageText.Append(index);
						values[index] = m_properties[propertyName];
						message = message.Substring(pos);
					}

					pos = message.IndexOf('{');
				}

				if ( message.Length != 0 )
					newMessageText.Append(message);

				message = newMessageText.ToString();

				// Replace the indexes with the values.

				return string.Format(message, values);
			}
		}

		public override bool Equals(object other)
		{
			if ( other == null || GetType() != other.GetType() || !base.Equals(other) )
				return false;

			// Work through the properties.

			UtilityException otherException = (UtilityException) other;
			IDictionaryEnumerator enumerator = m_properties.GetEnumerator();
			while ( enumerator.MoveNext() )
			{
				object otherValue = otherException.m_properties[enumerator.Key];
				if ( !object.Equals(enumerator.Value, otherValue) )
					return false;
			}

			return true;
		}

		public override int GetHashCode()
		{
			int hashcode = base.GetHashCode();
			foreach (string key in m_properties.Keys)
				hashcode ^= key.GetHashCode();
			return hashcode;
		}

		protected virtual string GetMessageFormat()
		{
			ResourceManager resourceManager = new ResourceManager(GetResourceBaseName(), GetType().Assembly);
			string format = resourceManager.GetString(GetType().Name, Thread.CurrentThread.CurrentCulture);
			return format == null ? string.Empty : format;
		}

		protected virtual string GetResourceBaseName()
		{
			return Constants.Exceptions.ResourceBaseName;
		}

		protected override string GetXmlNamespace()
		{
			return Constants.Xml.Namespace;
		}

		protected void SetPropertyValue(string name, object value)
		{
			Debug.Assert(name != null, "name != null");
			m_properties[name] = value;
		}

		protected object GetPropertyValue(string name, object defaultValue)
		{
			Debug.Assert(name != null, "name != null");
			object value = m_properties[name];
			return value == null ? defaultValue : value;
		}

		protected IDictionaryEnumerator GetEnumerator()
		{
			return m_properties.GetEnumerator();
		}

		protected override void WriteContents(XmlWriteAdaptor adaptor)
		{
			adaptor.WriteStartElement(Constants.Xml.Exception.PropertiesElement);

			// Write out all properties.

			foreach ( PropertyInfo propertyInfo in m_propertyInfos )
			{
				// Write an element for each property, indicating whether it is null or not.

				adaptor.WriteStartElement(propertyInfo.Name);
				object value = m_properties[propertyInfo.Name];
				if ( value == null )
					adaptor.WriteAttribute(Constants.Xsi.Prefix, Constants.Xsi.NilAttribute, Constants.Xsi.Namespace, XmlConvert.ToString(true));
				else
					adaptor.WriteElementValue(XmlToString(m_properties[propertyInfo.Name], propertyInfo.TypeCode));
				adaptor.WriteEndElement();
			}

			adaptor.WriteEndElement();
		}

		protected override void ReadContents(XmlReadAdaptor adaptor)
		{
			// Read all properties.

			m_properties.Clear();
			if ( adaptor.ReadElement(Constants.Xml.Exception.PropertiesElement) )
			{
				while ( adaptor.ReadElement() )
				{
					string name = adaptor.Name;

					// Determine whether the value is null or not.

					bool isNil = adaptor.ReadAttributeBoolean(Constants.Xsi.NilAttribute, Constants.Xsi.Namespace, false);
					if ( isNil )
					{
						m_properties[name] = null;
					}
					else
					{
						string value = adaptor.GetValue();
						foreach ( PropertyInfo propertyInfo in m_propertyInfos )
						{
							if ( propertyInfo.Name == name )
								SetPropertyValue(name, XmlFromString(value, propertyInfo.TypeCode));
						}
					}

					adaptor.ReadEndElement();
				}

				adaptor.ReadEndElement();
			}
		}

		private static string XmlToString(object value, System.TypeCode typeCode)
		{
			switch ( typeCode )
			{
				case System.TypeCode.String:
					return (string) value;

				case System.TypeCode.Boolean:
					return XmlConvert.ToString((bool) value);

				case System.TypeCode.Byte:
					return XmlConvert.ToString((byte) value);

				case System.TypeCode.Decimal:
					return XmlConvert.ToString((decimal) value);

				case System.TypeCode.Double:
					return XmlConvert.ToString((double) value);

				case System.TypeCode.Int16:
					return XmlConvert.ToString((short) value);

				case System.TypeCode.Int32:
					return XmlConvert.ToString((int) value);

				case System.TypeCode.Int64:
					return XmlConvert.ToString((long) value);

				case System.TypeCode.Single:
					return XmlConvert.ToString((float) value);

				default:
					Debug.Fail("Invalid type code");
					return string.Empty;
			}
		}

		private static object XmlFromString(string value, System.TypeCode typeCode)
		{
			switch ( typeCode )
			{
				case System.TypeCode.String:
					return value;

				case System.TypeCode.Boolean:
					return XmlConvert.ToBoolean(value);

				case System.TypeCode.Byte:
					return XmlConvert.ToByte(value);

				case System.TypeCode.Decimal:
					return XmlConvert.ToDecimal(value);

				case System.TypeCode.Double:
					return XmlConvert.ToDouble(value);

				case System.TypeCode.Int16:
					return XmlConvert.ToInt16(value);

				case System.TypeCode.Int32:
					return XmlConvert.ToInt32(value);

				case System.TypeCode.Int64:
					return XmlConvert.ToInt64(value);

				case System.TypeCode.Single:
					return XmlConvert.ToSingle(value);

				default:
					Debug.Fail("Invalid type code");
					return string.Empty;
			}
		}

		protected override void WriteContents(BinaryWriter writer)
		{
			// Pass to base first.

			base.WriteContents(writer);

			// Write out all properties.

			foreach ( PropertyInfo propertyInfo in m_propertyInfos )
			{
				object value = m_properties[propertyInfo.Name];
				if ( value == null )
				{
					writer.Write(true);
				}
				else
				{
					writer.Write(false);
					Write(writer, value, propertyInfo.TypeCode);
				}
			}
		}

		protected override void ReadContents(BinaryReader reader)
		{
			// Pass to base first.

			base.ReadContents(reader);

			// Read all properties.

			foreach ( PropertyInfo propertyInfo in m_propertyInfos )
			{
				bool isNull = reader.ReadBoolean();
				if ( isNull )
					m_properties[propertyInfo.Name] = null;
				else
					m_properties[propertyInfo.Name] = Read(reader, propertyInfo.TypeCode);
			}
		}

		private static void Write(BinaryWriter writer, object value, System.TypeCode typeCode)
		{
			switch ( typeCode )
			{
				case System.TypeCode.String:
					writer.Write((string) value);
					break;

				case System.TypeCode.Boolean:
					writer.Write((bool) value);
					break;

				case System.TypeCode.Byte:
					writer.Write((byte) value);
					break;

				case System.TypeCode.Decimal:
					writer.Write((decimal) value);
					break;

				case System.TypeCode.Double:
					writer.Write((double) value);
					break;

				case System.TypeCode.Int16:
					writer.Write((short) value);
					break;

				case System.TypeCode.Int32:
					writer.Write((int) value);
					break;

				case System.TypeCode.Int64:
					writer.Write((long) value);
					break;

				case System.TypeCode.Single:
					writer.Write((float) value);
					break;

				default:
					Debug.Fail("Unexpected type of value supplied for writing to binary: " + value.GetType().ToString());
					break;
			}
		}

		private static object Read(BinaryReader reader, System.TypeCode typeCode)
		{
			switch ( typeCode )
			{
				case System.TypeCode.Empty:
					return null;

				case System.TypeCode.String:
					return reader.ReadString();

				case System.TypeCode.Boolean:
					return reader.ReadBoolean();

				case System.TypeCode.Byte:
					return reader.ReadByte();

				case System.TypeCode.Decimal:
					return reader.ReadDecimal();

				case System.TypeCode.Double:
					return reader.ReadDouble();

				case System.TypeCode.Int16:
					return reader.ReadInt16();

				case System.TypeCode.Int32:
					return reader.ReadInt32();

				case System.TypeCode.Int64:
					return reader.ReadInt64();

				case System.TypeCode.Single:
					return reader.ReadSingle();

				default:
					Debug.Fail("Unexpected type code read from binary: " + typeCode.ToString());
					return null;
			}
		}

		private void Initialise(PropertyInfo[] propertyInfos)
		{
			m_propertyInfos = propertyInfos;
			foreach ( PropertyInfo propertyInfo in m_propertyInfos )
				m_properties[propertyInfo.Name] = null;
		}

		private int IndexOfPropertyInfo(string propertyName)
		{
			for (int index = 0; index < m_propertyInfos.Length; index++)
			{
				if (m_propertyInfos[index].Name == propertyName)
					return index;
			}

			return -1;
		}

		protected class PropertyInfo
		{
			public PropertyInfo(string name, System.TypeCode typeCode)
			{
				Name = name;
				TypeCode = typeCode;
			}

			public string Name;
			public System.TypeCode TypeCode;
		}

		private Hashtable m_properties = new Hashtable();
		private PropertyInfo[] m_propertyInfos;
	}
}