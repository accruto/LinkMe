using System.Runtime.Serialization;
using System.IO;
using System.Collections;
using System.Resources;
using System.Threading;
using System.Xml;
using System.Diagnostics;

using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Type.Exceptions
{
	[System.Serializable]
	public abstract class TypeException
		:	BaseException
	{
		#region Constructors

		protected TypeException()
			:	base()
		{
			Initialise();
		}

		protected TypeException(string source, string method, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Initialise();
		}

		protected TypeException(System.Type source, string method, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Initialise();
		}

		protected TypeException(string source, string method)
			:	base(source, method)
		{
			Initialise();
		}

		protected TypeException(System.Type source, string method)
			:	base(source, method)
		{
			Initialise();
		}

		protected TypeException(SerializationInfo info, StreamingContext context)
			:	base(info, context)
		{
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
				IDictionaryEnumerator enumerator = m_properties.GetEnumerator();
				while ( enumerator.MoveNext() )
					message = message.Replace("{" + enumerator.Key + "}", enumerator.Value == null ? string.Empty : enumerator.Value.ToString());
				return message;
			}
		}

		protected abstract PropertyInfo[] PropertyInfos { get; }

		#region Static methods

		protected static PropertyInfo[] AppendPropertyInfos(PropertyInfo[] one, params PropertyInfo[] two)
		{
			Debug.Assert(one != null && two != null, "one != null && two != null");

			PropertyInfo[] combined = new PropertyInfo[one.Length + two.Length];
			System.Array.Copy(one, 0, combined, 0, one.Length);
			System.Array.Copy(two, 0, combined, one.Length, two.Length);

			return combined;
		}

		#endregion

		public override bool Equals(object other)
		{
			if ( other == null || GetType() != other.GetType() || !base.Equals(other) )
				return false;

			// Work through the properties.

			TypeException otherException = (TypeException) other;
			IDictionaryEnumerator enumerator = m_properties.GetEnumerator();
			while ( enumerator.MoveNext() )
			{
				if ( !object.Equals(enumerator.Value, otherException.m_properties[enumerator.Key]) )
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
			m_properties[name] = TypeClone.Clone(value);
		}

		protected object GetPropertyValue(string name, object defaultValue)
		{
			Debug.Assert(name != null, "name != null");
			object value = TypeClone.Clone(m_properties[name]);
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

			foreach ( PropertyInfo propertyInfo in PropertyInfos )
			{
				// Write an element for each property, indicating whether it is null or not.

				adaptor.WriteStartElement(propertyInfo.Name);
				object value = m_properties[propertyInfo.Name];
				if ( value == null )
					adaptor.WriteAttribute(Constants.Xsi.Prefix, Constants.Xsi.NilAttribute, Constants.Xsi.Namespace, XmlConvert.ToString(true));
				else
					adaptor.WriteElementValue(TypeXmlConvert.ToString(m_properties[propertyInfo.Name], propertyInfo.Type));
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
						foreach ( PropertyInfo propertyInfo in PropertyInfos )
						{
							if ( propertyInfo.Name == name )
							{
								SetPropertyValue(name, TypeXmlConvert.ToType(value, propertyInfo.Type));
								break;
							}
						}
					}

					adaptor.ReadEndElement();
				}

				adaptor.ReadEndElement();
			}
		}

		protected override void WriteContents(BinaryWriter writer)
		{
			// Pass to base first.

			base.WriteContents(writer);

			// Write out all properties.

			BinaryWriteAdaptor adaptor = new BinaryWriteAdaptor(writer);
			foreach ( PropertyInfo propertyInfo in PropertyInfos )
			{
				object value = m_properties[propertyInfo.Name];
				if ( value == null )
				{
					adaptor.Write(true);
				}
				else
				{
					adaptor.Write(false);
					adaptor.Write(value, propertyInfo.Type);
				}
			}
		}

		protected override void ReadContents(BinaryReader reader)
		{
			// Pass to base first.

			base.ReadContents(reader);

			// Read all properties.

			BinaryReadAdaptor adaptor = new BinaryReadAdaptor(reader);
			foreach ( PropertyInfo propertyInfo in PropertyInfos )
			{
				bool isNull = adaptor.ReadBoolean();
				if ( isNull )
					m_properties[propertyInfo.Name] = null;
				else
					m_properties[propertyInfo.Name] = adaptor.ReadType(propertyInfo.Type);
			}
		}

		private void Initialise()
		{
			foreach ( PropertyInfo propertyInfo in PropertyInfos )
				m_properties[propertyInfo.Name] = null;
		}

		protected class PropertyInfo
		{
			public static readonly PropertyInfo[] EmptyInfos = new PropertyInfo[0];

			private string m_name;
			private PrimitiveType m_type;

			public PropertyInfo(string name, PrimitiveType type)
			{
				m_name = name;
				m_type = type;
			}

			public string Name
			{
				get { return m_name; }
			}

			public PrimitiveType Type
			{
				get { return m_type; }
			}
		}

		private Hashtable m_properties = new Hashtable();
	}
}