using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using LinkMe.Framework.Type;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Net;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Instrumentation
{
	/// <summary>
	/// The Event Paramter class.
	/// </summary>
	public class EventParameter
        : IInternable
	{
		#region Nested types

		// The integer values of these enum values are written to the database by MssqlMessageHandler - do not
		// change them.
		internal enum ValueFormat
		{
			Raw = 0,
			SystemBinary = 1,
			LinkMeBinary = 2,
			SystemXml = 3,
			LinkMeXml = 4,
			String = 5
		}

		#endregion

		private const string m_systemNamespace = "System";

		private static readonly string m_typeAssembly = typeof(PrimitiveType).Assembly.FullName;
		private static readonly string m_systemAssembly = typeof(object).Assembly.FullName;

		/// <summary>
		/// Initilizes an Event parameters with the passed parameter.
		/// </summary>
		/// <param name="name">The Name of the parameter.</param>
		/// <param name="value">The parameter value.</param>
		internal EventParameter(string name, object value)
		{
			m_name = name == null ? string.Empty : name;
			m_class = null;
			m_value = value;
			m_valueFormat = ValueFormat.Raw;
		}

		private EventParameter(string name, string className, object value, ValueFormat valueFormat)
		{
			m_name = name;
			m_class = className;
			m_value = value;
			m_valueFormat = valueFormat;
		}

		private EventParameter(string name, ClassInfo info, object value, ValueFormat valueFormat)
		{
			m_name = name;
			m_class = info.ToString();
			m_value = value;
			m_valueFormat = valueFormat;
		}

		/// <summary>
		/// Returns the Name of the parameter.
		/// </summary>
		public string Name
		{
			get { return m_name; }
		}

        public string Class
        {
            get { return m_class; }
        }

		/// <summary>
		/// Returns the value of the parameter.
		/// </summary>
		public object Value
		{
			get
			{
				Unpack();
				return m_value;
			}
		}

		internal string Type
		{
			get
			{
				if (m_valueFormat == ValueFormat.Raw)
					return (m_value == null ? null : m_value.GetType().AssemblyQualifiedName);

				Debug.Assert(m_class != null, "m_class != null");
				return m_class; 
			}
		}

		public override bool Equals(object other)
		{
			if ( !(other is EventParameter) )
				return false;
			return Equals((EventParameter) other);
		}

		public override string ToString()
		{
			if (Value == null)
				return string.Empty;

			return Value.ToString ();
		}

		public override int GetHashCode()
		{
			return m_name.GetHashCode();
		}

        /// <summary>
        /// Provides a fast and "safe" way to view the value - does not try to deserialize anything
        /// and does not let exceptions escape.
        /// </summary>
        public string GetSafeDisplayString()
        {
            try
            {
                if (m_value == null)
                    return "<null>";

                switch (m_valueFormat)
                {
                    case ValueFormat.String:
                        return (string)m_value;

                    case ValueFormat.Raw:
                        return m_value.ToString();

                    default:
                        return "<" + m_class + ">";
                }
            }
            catch (System.Exception ex)
            {
                return "<error: " + ex.GetType().FullName + ">";
            }
        }

	    public bool Equals(EventParameter other)
		{
            // Some shortcuts first.

            if (ReferenceEquals(this, other))
                return true;
			if ( m_name != other.m_name )
				return false;
			if ( m_value == null )
				return (other.m_value == null);
			if ( other.m_value == null )
				return false;
            if (m_valueFormat == other.m_valueFormat && ReferenceEquals(m_value, other.m_value))
                return true;

			// The only format that cannot be unpacked is string so explicitly check for it.

			if ( m_valueFormat == ValueFormat.String )
			{
				if ( other.m_valueFormat == ValueFormat.String )
				{
					return m_class == other.m_class && string.Equals((string)m_value, (string)other.m_value);
				}
				else
				{
					ClassInfo info = new ClassInfo(m_class);
					return info.AssemblyName == other.m_value.GetType().Assembly.FullName && info.FullName == other.m_value.GetType().FullName && ((string) m_value).Equals(other.m_value.ToString());
				}
			}
			else if ( other.m_valueFormat == ValueFormat.String )
			{
				ClassInfo info = new ClassInfo(other.m_class);
				return m_value.GetType().Assembly.FullName == info.AssemblyName && m_value.GetType().FullName == info.FullName && m_value.ToString().Equals(other.m_value);
			}

			// To go further unpack.

            try
            {
                Unpack();
                other.Unpack();
            }
            catch (System.Exception)
            {
                // Failed to unpack. If the format is the same try to compare them packed,
                // otherwise assume not equal.

                if (m_valueFormat != other.m_valueFormat)
                    return false;

                if (m_value is string && other.m_value is string)
                    return string.Equals((string)m_value, (string)other.m_value);

                if (m_value is byte[] && other.m_value is byte[])
                    return ByteArraysEqual((byte[])m_value, (byte[])other.m_value);

                return false;
            }

	        return m_value.Equals(other.m_value);
		}

		#region Clone

		internal EventParameter Clone()
		{
			EventParameter newParameter = null;

			switch ( m_valueFormat )
			{
				case ValueFormat.SystemBinary:
				case ValueFormat.LinkMeBinary:

					// Clone the byte array.

					newParameter = new EventParameter(m_name, m_class, ((byte[]) m_value).Clone(), m_valueFormat);
					break;

				case ValueFormat.SystemXml:
				case ValueFormat.LinkMeXml:
				case ValueFormat.String:

					// Clone the string.

					newParameter = new EventParameter(m_name, m_class, m_value, m_valueFormat);
					break;

				default:
					// Check for null.

					if ( m_value == null )
					{
						newParameter = new EventParameter(m_name, null);
					}
					else
					{
						// Check for a PrimitiveType value.
						
						newParameter = ClonePrimitiveTypeParameter();
						if ( newParameter == null )
							newParameter = CloneParameter();
					}

					break;
			}

			return newParameter;
		}

		private EventParameter ClonePrimitiveTypeParameter()
		{
			// Look for the primitive type corresponding to the value's .NET type.

			PrimitiveTypeInfo primitiveTypeInfo = PrimitiveTypeInfo.GetPrimitiveTypeInfo(m_value.GetType());
			if ( primitiveTypeInfo != null )
				return new EventParameter(m_name, TypeClone.Clone(m_value));
			else
				return null;
		}

		private EventParameter CloneParameter()
		{
			// Determine whether the value supports ICloneable.

			System.ICloneable cloneable = m_value as System.ICloneable;
			if ( cloneable != null )
				return new EventParameter(m_name, cloneable.Clone());

			// Determine whether the value supports IBinarySerializable.

			IBinarySerializable serializable = m_value as IBinarySerializable;
			if ( serializable != null )
			{
				MemoryStream stream = new MemoryStream();
				using ( BinaryWriter writer = new BinaryWriter(stream) )
				{
					serializable.Write(writer);
					return new EventParameter(m_name, new ClassInfo(m_value.GetType()), stream.ToArray(), ValueFormat.LinkMeBinary);
				}
			}

			// Determine whether the value supports IXmlSerializable.

			IXmlSerializable xmlSerializable = m_value as IXmlSerializable;
			if ( xmlSerializable != null )
				return new EventParameter(m_name, new ClassInfo(m_value.GetType()), XmlSerializer.Serialize(xmlSerializable), ValueFormat.LinkMeXml);

			// Determine whether the value can be XmlSerialized.

			try
			{
				return new EventParameter(m_name, new ClassInfo(m_value.GetType()), XmlSerializer.Serialize(m_value), ValueFormat.SystemXml);
			}
			catch ( System.Exception )
			{
			}

			// Just return the string representation.

			return new EventParameter(m_name, new ClassInfo(m_value.GetType()), m_value.ToString(), ValueFormat.String);
		}

		#endregion

		#region IXmlSerializable

		internal void WriteXml(XmlWriteAdaptor adaptor)
		{
			// The name becomes the name of the element.

			adaptor.WriteStartElement(Constants.Xml.ParameterElement);
			adaptor.WriteName(m_name);

			switch ( m_valueFormat )
			{
				case ValueFormat.SystemXml:
				case ValueFormat.LinkMeXml:

					WriteXmlFormat(adaptor);
					break;

				case ValueFormat.String:

					WriteStringFormat(adaptor);
					break;

				case ValueFormat.SystemBinary:
				case ValueFormat.LinkMeBinary:

					// Unpack before writing.

					Unpack();
					WriteRawFormat(adaptor);
					break;

				case ValueFormat.Raw:

					WriteRawFormat(adaptor);
					break;
			}

			adaptor.WriteEndElement();
		}

		private void WriteXmlFormat(XmlWriteAdaptor adaptor)
		{
			WriteValueInfo(adaptor, m_class, m_valueFormat);

			// Need to read in the value in the string.

			XmlTextReader reader = new XmlTextReader(new StringReader((string) m_value));
			reader.MoveToContent();
			adaptor.XmlWriter.WriteNode(reader, true);
		}

		private void WriteStringFormat(XmlWriteAdaptor adaptor)
		{
			WriteValueInfo(adaptor, m_class, m_valueFormat);
			adaptor.WriteXml((string) m_value);
		}

		private void WriteRawFormat(XmlWriteAdaptor adaptor)
		{
			// Determine whether the value is null.

			if ( WriteNullParameter(adaptor) )
				return;

			// Determine whether this is a primitive type.

			if ( WritePrimitiveTypeParameter(adaptor) )
				return;

			WriteParameter(adaptor);
		}

		private void WriteValueInfo(XmlWriteAdaptor adaptor, ClassInfo info, ValueFormat valueFormat)
		{
			WriteValueInfo(adaptor, info.ToString(), valueFormat);
		}

		private void WriteValueInfo(XmlWriteAdaptor adaptor, string className, ValueFormat valueFormat)
		{
			adaptor.WriteAttribute(Constants.Xml.ClassAttribute, className);

			string format;
			switch ( valueFormat )
			{
				case ValueFormat.LinkMeXml:
					format = Constants.Xml.LinkMeFormat;
					break;

				case ValueFormat.SystemXml:
					format = Constants.Xml.SystemFormat;
					break;

				case ValueFormat.String:
					format = Constants.Xml.StringFormat;
					break;

				default:
					format = string.Empty;
					break;
			}

			adaptor.WriteAttribute(Constants.Xml.FormatAttribute, format);
		}

		private bool WriteNullParameter(XmlWriteAdaptor adaptor)
		{
			if ( m_value != null )
				return false;

			// Apply the xsi:nil attribute.

			adaptor.WriteXsiNilAttribute(true);
			return true;
		}

		private bool WritePrimitiveTypeParameter(XmlWriteAdaptor adaptor)
		{
			// Get the info.

			PrimitiveTypeInfo primitiveTypeInfo = PrimitiveTypeInfo.GetPrimitiveTypeInfo(m_value.GetType());
			if ( primitiveTypeInfo == null )
				return false;

			// Apply the appropriate xsi:type attribute.

			primitiveTypeInfo.WriteXsiTypeAttribute(adaptor);

			// Write the value itself.

			adaptor.WriteElementValue(TypeXmlConvert.ToString(m_value));
			return true;
		}

		private void WriteParameter(XmlWriteAdaptor adaptor)
		{
			if ( WriteLinkMeXmlParameter(adaptor) )
				return;

			if ( WriteSystemXmlParameter(adaptor) )
				return;

			WriteStringParameter(adaptor);
		}

		private bool WriteLinkMeXmlParameter(XmlWriteAdaptor adaptor)
		{
			// Ask the object to serialize itself using the IXmlSerializable interface.
			
			IXmlSerializable serializable = m_value as IXmlSerializable;
			if ( serializable == null )
				return false;

            WriteValueInfo(adaptor, new ClassInfo(m_value.GetType()), ValueFormat.LinkMeXml);
			serializable.WriteOuterXml(adaptor.XmlWriter);
			return true;
		}

		private bool WriteSystemXmlParameter(XmlWriteAdaptor adaptor)
		{
			// Try to serialize it using the serializer.

			try
			{
				// Try it.

				StringBuilder builder = new StringBuilder();
				XmlWriter writer = new XmlTextWriter(new StringWriter(builder));
				System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(m_value.GetType());
				xmlSerializer.Serialize(writer, m_value);

				// If it gets to here can assume it is OK.

				WriteValueInfo(adaptor, new ClassInfo(m_value.GetType()), ValueFormat.SystemXml);
				xmlSerializer.Serialize(adaptor.XmlWriter, m_value);
				return true;
			}
			catch ( System.Exception )
			{
				return false;
			}
		}

		private void WriteStringParameter(XmlWriteAdaptor adaptor)
		{
			// Write out a default ToString element.

			WriteValueInfo(adaptor, new ClassInfo(m_value.GetType()), ValueFormat.String);
			adaptor.WriteElementValue(m_value.ToString());
		}

		internal static EventParameter ReadXml(XmlReadAdaptor adaptor)
		{
			string name = adaptor.ReadName();

			EventParameter parameter = ReadNullParameter(adaptor, name);
			if ( parameter != null )
				return parameter;

			// Look for the type.

			parameter = ReadPrimitiveTypeParameter(adaptor, name);
			if ( parameter != null )
				return parameter;

			return ReadParameter(adaptor, name);
		}

		private static EventParameter ReadNullParameter(XmlReadAdaptor adaptor, string name)
		{
			// Look for xsi:nil="true".

			bool nil = adaptor.ReadXsiNilAttribute();
			if ( nil )
				return new EventParameter(name, null);
			else
				return null;
		}

		private static EventParameter ReadPrimitiveTypeParameter(XmlReadAdaptor adaptor, string name)
		{
			// Look for a primitive type.

			PrimitiveTypeInfo primitiveTypeInfo = PrimitiveTypeInfo.ReadXsiTypeAttribute(adaptor);
			if ( primitiveTypeInfo == null )
				return null;

			return new EventParameter(name, TypeXmlConvert.ToType(adaptor.ReadInnerXml(), primitiveTypeInfo.PrimitiveType));
		}

		private static EventParameter ReadParameter(XmlReadAdaptor adaptor, string name)
		{
			// Look for class and format attributes.

			string className = adaptor.ReadAttributeString(Constants.Xml.ClassAttribute, string.Empty);
			string format = adaptor.ReadAttributeString(Constants.Xml.FormatAttribute, string.Empty);

			switch ( format )
			{
				case Constants.Xml.LinkMeFormat:
					return ReadLinkMeXmlParameter(adaptor, name, className);

				case Constants.Xml.SystemFormat:
					return ReadSystemXmlParameter(adaptor, name, className);

				default:
					return ReadStringParameter(adaptor, name, className);
			}
		}

		private static EventParameter ReadLinkMeXmlParameter(XmlReadAdaptor adaptor, string name, string className)
		{
			return new EventParameter(name, className, adaptor.ReadInnerXml(), ValueFormat.LinkMeXml);
		}

		private static EventParameter ReadSystemXmlParameter(XmlReadAdaptor adaptor, string name, string className)
		{
			return new EventParameter(name, className, adaptor.ReadInnerXml(), ValueFormat.SystemXml);
		}

		private static EventParameter ReadStringParameter(XmlReadAdaptor adaptor, string name, string className)
		{
			if ( className.Length == 0 )
				return new EventParameter(name, adaptor.ReadInnerXml());
			else
				return new EventParameter(name, className, adaptor.ReadInnerXml(), ValueFormat.String);
		}

		#endregion

		#region IBinarySerializable

		internal void Write(BinaryWriter writer)
		{
			writer.Write(m_name);

			switch ( m_valueFormat )
			{
				case ValueFormat.SystemXml:
				case ValueFormat.LinkMeXml:
					WriteXmlFormat(writer);
					return;

				case ValueFormat.String:
					WriteStringFormat(writer);
					return;

				case ValueFormat.SystemBinary:
				case ValueFormat.LinkMeBinary:
					WriteBinaryFormat(writer);
					break;

				case ValueFormat.Raw:
					WriteRawFormat(writer);
					break;
			}
		}

		private void WriteXmlFormat(BinaryWriter writer)
		{
			WriteValueInfo(writer, m_class, m_valueFormat);
			writer.Write((string) m_value);
		}

		private void WriteStringFormat(BinaryWriter writer)
		{
			WriteValueInfo(writer, m_class, m_valueFormat);
			writer.Write((string) m_value);
		}

		private void WriteBinaryFormat(BinaryWriter writer)
		{
			WriteValueInfo(writer, m_class, m_valueFormat);
			new BinaryWriteAdaptor(writer).Write((byte[]) m_value);
		}

		private void WriteRawFormat(BinaryWriter writer)
		{
			if ( WriteNullParameter(writer) )
				return;

			if ( WritePrimitiveTypeParameter(writer) )
				return;

            if ( WriteEnumParameter(writer) )
                return;

			WriteParameter(writer);
		}

		private void WriteValueInfo(BinaryWriter writer, ClassInfo info, ValueFormat valueFormat)
		{
			WriteValueInfo(writer, info.ToString(), valueFormat);
		}

		private void WriteValueInfo(BinaryWriter writer, string className, ValueFormat valueFormat)
		{
			writer.Write((int) valueFormat);
			writer.Write(className);
		}

		private void WriteRawValueInfo(BinaryWriter writer, ValueFormat valueFormat, bool isNull)
		{
			writer.Write((int) valueFormat);
			writer.Write(isNull);
		}

		private bool WriteNullParameter(BinaryWriter writer)
		{
			if ( m_value != null )
				return false;

			// Apply the xsi:nil attribute.

			WriteRawValueInfo(writer, m_valueFormat, true);
			return true;
		}

		private bool WritePrimitiveTypeParameter(BinaryWriter writer)
		{
			// Get the info.

			PrimitiveTypeInfo primitiveTypeInfo = PrimitiveTypeInfo.GetPrimitiveTypeInfo(m_value.GetType());
			if ( primitiveTypeInfo == null )
				return false;

			// Indicate that the value is not null.

			WriteRawValueInfo(writer, m_valueFormat, false);

			BinaryWriteAdaptor adaptor = new BinaryWriteAdaptor(writer);
			adaptor.Write(m_value);
			return true;
		}

        private bool WriteEnumParameter(BinaryWriter writer)
        {
            if (m_value == null || !(m_value is System.Enum))
                return false;

            // Write out the enum as a string.

            WriteValueInfo(writer, new ClassInfo(m_value.GetType()), ValueFormat.String);
            BinaryWriteAdaptor adaptor = new BinaryWriteAdaptor(writer);
            adaptor.Write(m_value.ToString());
            return true;
        }

        private void WriteParameter(BinaryWriter writer)
		{
			if ( WriteInstrumentableParameter(writer) )
				return;

			// Try system serialization first, then LinkMe IBinarySerializable. DataObject fails to deserialize
			// when user code directly calls IBinarySerializable.

			if ( WriteSystemBinaryParameter(writer) )
				return;

			if ( WriteLinkMeBinaryParameter(writer) )
				return;

			if ( WriteLinkMeXmlParameter(writer) )
				return;

			if ( WriteSystemXmlParameter(writer) )
				return;

			WriteStringParameter(writer);
		}

		private bool WriteLinkMeBinaryParameter(BinaryWriter writer)
		{
			// Ask the object to serialize itself using the IBinarySerializable interface.
			
			IBinarySerializable serializable = m_value as IBinarySerializable;
			if ( serializable == null )
				return false;

			// Write to a stream.

			MemoryStream stream = new MemoryStream();
			using ( BinaryWriter valueWriter = new BinaryWriter(stream) )
			{
				serializable.Write(valueWriter);
				WriteValueInfo(writer, new ClassInfo(m_value.GetType()), ValueFormat.LinkMeBinary);
				BinaryWriteAdaptor adaptor = new BinaryWriteAdaptor(writer);
				adaptor.Write(stream.ToArray());
			}

			return true;
		}

		private bool WriteSystemBinaryParameter(BinaryWriter writer)
		{
			System.Type type = m_value.GetType();
			if (!type.IsSerializable)
				return false;

			try
			{
				using ( MemoryStream stream = new MemoryStream() )
				{
					BinaryFormatter formatter = new BinaryFormatter();
					formatter.Serialize(stream, m_value);
					WriteValueInfo(writer, new ClassInfo(type), ValueFormat.SystemBinary);
					BinaryWriteAdaptor adaptor = new BinaryWriteAdaptor(writer);
					adaptor.Write(stream.ToArray());
				}

				return true;
			}
			catch ( System.Exception )
			{
				return false;
			}
		}

		private bool WriteLinkMeXmlParameter(BinaryWriter writer)
		{
			// Ask the object to serialize itself using the IXmlSerializable interface.
			
			IXmlSerializable xmlSerializable = m_value as IXmlSerializable;
			if ( xmlSerializable == null )
				return false;

			WriteValueInfo(writer, new ClassInfo(m_value.GetType()), ValueFormat.LinkMeXml);
			writer.Write(XmlSerializer.Serialize(xmlSerializable));
			return true;
		}

		private bool WriteSystemXmlParameter(BinaryWriter writer)
		{
			// Try to serialize it using the serializer.

			try
			{
				// Try it.

				StringBuilder builder = new StringBuilder();
				XmlWriter xmlWriter = new XmlTextWriter(new StringWriter(builder));
				System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(m_value.GetType());
				xmlSerializer.Serialize(xmlWriter, m_value);

				// If it gets to here can assume it is OK.

				WriteValueInfo(writer, new ClassInfo(m_value.GetType()), ValueFormat.SystemXml);
				writer.Write(builder.ToString());

				return true;
			}
			catch ( System.Exception )
			{
				return false;
			}
		}

		private bool WriteInstrumentableParameter(BinaryWriter writer)
		{
			// Ask it.
			
			IInstrumentable instrumentable = m_value as IInstrumentable;
			if ( instrumentable == null )
				return false;

			// Ask it to save its details.

			InstrumentationDetails details = new InstrumentationDetails();
			instrumentable.Write(details);

			// Write to a stream.

			IBinarySerializable serializable = details;
			MemoryStream stream = new MemoryStream();
			using ( BinaryWriter valueWriter = new BinaryWriter(stream) )
			{
				serializable.Write(valueWriter);
				WriteValueInfo(writer, new ClassInfo(typeof(InstrumentationDetails)), ValueFormat.LinkMeBinary);
				BinaryWriteAdaptor adaptor = new BinaryWriteAdaptor(writer);
				adaptor.Write(stream.ToArray());
			}

			return true;
		}

		private void WriteStringParameter(BinaryWriter writer)
		{
			// Write out a default ToString element.
			
			WriteValueInfo(writer, new ClassInfo(m_value.GetType()), ValueFormat.String);
			writer.Write(m_value.ToString());
		}

		internal static EventParameter Read(BinaryReader reader)
		{
			string name = reader.ReadString();
			ValueFormat format = (ValueFormat) reader.ReadInt32();

			switch ( format )
			{
				case ValueFormat.SystemXml:
					return ReadSystemXmlParameter(reader, name);

				case ValueFormat.LinkMeXml:
					return ReadLinkMeXmlParameter(reader, name);

				case ValueFormat.SystemBinary:
					return ReadSystemBinaryParameter(reader, name);

				case ValueFormat.LinkMeBinary:
					return ReadLinkMeBinaryParameter(reader, name);

				case ValueFormat.String:
					return ReadStringParameter(reader, name);

				default:
					return ReadRawParameter(reader, name);
			}
		}

		private static EventParameter ReadLinkMeBinaryParameter(BinaryReader reader, string name)
		{
			BinaryReadAdaptor adaptor = new BinaryReadAdaptor(reader);
			string className = adaptor.ReadString();
			return new EventParameter(name, className, adaptor.ReadBytes(), ValueFormat.LinkMeBinary);
		}

		private static EventParameter ReadSystemBinaryParameter(BinaryReader reader, string name)
		{
			BinaryReadAdaptor adaptor = new BinaryReadAdaptor(reader);
			string className = adaptor.ReadString();
			return new EventParameter(name, className, adaptor.ReadBytes(), ValueFormat.SystemBinary);
		}

		private static EventParameter ReadLinkMeXmlParameter(BinaryReader reader, string name)
		{
			string className = reader.ReadString();
			return new EventParameter(name, className, reader.ReadString(), ValueFormat.LinkMeXml);
		}

		private static EventParameter ReadSystemXmlParameter(BinaryReader reader, string name)
		{
			string className = reader.ReadString();
			return new EventParameter(name, className, reader.ReadString(), ValueFormat.SystemXml);
		}

		private static EventParameter ReadStringParameter(BinaryReader reader, string name)
		{
			string className = reader.ReadString();
			return new EventParameter(name, className, reader.ReadString(), ValueFormat.String);
		}

		private static EventParameter ReadRawParameter(BinaryReader reader, string name)
		{
			EventParameter parameter = ReadNullParameter(reader, name);
			if ( parameter != null )
				return parameter;

			return ReadPrimitiveTypeParameter(reader, name);
		}

		private static EventParameter ReadNullParameter(BinaryReader reader, string name)
		{
			bool isNull = reader.ReadBoolean();
			if ( isNull )
				return new EventParameter(name, null);
			else
				return null;
		}

		private static EventParameter ReadPrimitiveTypeParameter(BinaryReader reader, string name)
		{
			BinaryReadAdaptor adaptor = new BinaryReadAdaptor(reader);
			return new EventParameter(name, adaptor.ReadObject());
		}

		#endregion

        #region IInternable

        public void Intern(Interner interner)
        {
            const string method = "Intern";

            if (interner == null)
                throw new NullParameterException(GetType(), method, "interner");

            interner.Intern(ref m_name);
            interner.Intern(ref m_class);
            interner.TryIntern(ref m_value);
        }

        #endregion

        #region Persistence for MssqlMessageHandler

        internal static EventParameter CreateFromStringAndBinaryValues(string name, string className,
			ValueFormat format, string stringValue, byte[] binaryValue)
		{
			const string method = "CreateFromStringAndBinaryValues";

			if (name == null)
				throw new NullParameterException(typeof(EventParameter), method, "name");

			if (stringValue == null && binaryValue == null)
				return new EventParameter(name, null);

			if (className == null)
				throw new NullParameterException(typeof(EventParameter), method, "className");

			switch (format)
			{
				case ValueFormat.LinkMeXml:
				case ValueFormat.SystemXml:
					return CreateFromXml(name, className, format, stringValue);

				case ValueFormat.LinkMeBinary:
				case ValueFormat.SystemBinary:
					// Don't deserialize until the value is actually needed, just store the binary.

					return new EventParameter(name, className, binaryValue, format);

				case ValueFormat.String:
					// Just store the string - nothing else can be done with it.

					return new EventParameter(name, className, stringValue, format);

				default:
					throw new InvalidParameterValueException(typeof(EventParameter), method, "format",
						typeof(ValueFormat), format);
			}
		}

		private static EventParameter CreateFromXml(string name, string className, ValueFormat format, string xml)
		{
			System.Type type = ClassInfo.GetTypeFromAssemblyQualifiedName(className);
			Debug.Assert(type != null, "type != null");

			// Is it a primitive type? If so, the data is the raw XML value, no elements, etc. so convert it
			// to an object.

			PrimitiveTypeInfo primitiveInfo = PrimitiveTypeInfo.GetPrimitiveTypeInfo(type, false);
			if (primitiveInfo != null)
				return new EventParameter(name, TypeXmlConvert.ToType(xml, primitiveInfo.PrimitiveType));

			if (format == ValueFormat.SystemXml)
			{
				if (type == typeof(ulong))
					return new EventParameter(name, XmlConvert.ToUInt64(xml));
                else if (type == typeof(uint))
					return new EventParameter(name, XmlConvert.ToUInt32(xml));
				else if (type == typeof(ushort))
					return new EventParameter(name, XmlConvert.ToUInt16(xml));
				else if (type == typeof(sbyte))
					return new EventParameter(name, XmlConvert.ToSByte(xml));
			}

			// Not a primitive type, so the XML string is the full serialized representation. Don't deserialize it
			// until needed.

			return new EventParameter(name, className, xml, format);
		}

		internal ValueFormat GetStringAndBinaryValues(out string stringValue, out byte[] binaryValue)
		{
			if (m_value == null)
			{
				stringValue = null;
				binaryValue = null;
				return ValueFormat.Raw;
			}

			switch (m_valueFormat)
			{
				case ValueFormat.Raw:
					return GetDataFromRawValue(out stringValue, out binaryValue);

				case ValueFormat.LinkMeBinary:
				case ValueFormat.SystemBinary:
					return GetDataFromBinary(out stringValue, out binaryValue);

				case ValueFormat.LinkMeXml:
				case ValueFormat.SystemXml:
				case ValueFormat.String:
					// XML or string representation is the best we have. (An earlier attempt to serialize to
					// binary must have failed, so don't even try again.)

					stringValue = (string)m_value;
					binaryValue = null;
					return m_valueFormat;

				default:
					throw new System.ApplicationException("Unexpected value of m_valueFormat: " + m_valueFormat.ToString());
			}
		}

		private ValueFormat GetDataFromRawValue(out string stringValue, out byte[] binaryValue)
		{
			Debug.Assert(m_value != null, "m_value != null");

			// Do we need the binary value, the string value or both?

			System.Type type = m_value.GetType();
			PrimitiveTypeInfo primitiveTypeInfo = PrimitiveTypeInfo.GetPrimitiveTypeInfo(type, false);
			if (primitiveTypeInfo != null)
			{
				// Primitive type - store only the XML.

				stringValue = TypeXmlConvert.ToString(m_value);
				binaryValue = null;
				return (type.Namespace == m_systemNamespace ? ValueFormat.SystemXml : ValueFormat.LinkMeXml);
			}

			// Also check for the types that are not LinkMe primtive types, but can still be stored as a string
			// and searched.

			if (m_value is ulong || m_value is uint || m_value is ushort || m_value is sbyte)
			{
				stringValue = ((System.IFormattable)m_value).ToString(null, NumberFormatInfo.InvariantInfo);
				binaryValue = null;
				return ValueFormat.SystemXml;
			}

			// Try to write to binary. Try system serialization first, then LinkMe IBinarySerializable.
			// DataObject fails to deserialize when user code directly calls IBinarySerializable.
			// The byte stream will be read by UnpackSystemBinaryParameter() or UnpackLinkMeBinaryParameter().

			ValueFormat format = ValueFormat.Raw;
			binaryValue = null;
			IBinarySerializable serializable;

			if (type.IsSerializable)
			{
				using (MemoryStream stream = new MemoryStream())
				{
					try
					{
						BinaryFormatter formatter = new BinaryFormatter();
						formatter.Serialize(stream, m_value);
						binaryValue = stream.ToArray();
						format = ValueFormat.SystemBinary;
					}
					catch (System.Exception)
					{
					}
				}
			}
			else if ((serializable = m_value as IBinarySerializable) != null)
			{
				using (MemoryStream stream = new MemoryStream())
				{
					try
					{
						BinaryWriter writer = new BinaryWriter(stream);
						serializable.Write(writer);
						binaryValue = stream.ToArray();
						format = ValueFormat.LinkMeBinary;
					}
					catch (System.Exception)
					{
					}
				}
			}

			if (format != ValueFormat.Raw)
			{
				// All details should be in the binary data, but write the string as well for certain types,
				// so that they can be searched.

				if (m_value is System.Exception)
				{
					stringValue = m_value.ToString();
				}
				else
				{
					stringValue = null;
				}
			}
			else
			{
				// We don't have the binary data, so export XML or at least the ToString() value.

				try
				{
					stringValue = XmlSerializer.Serialize(m_value);
					format = ValueFormat.LinkMeXml; // May actually be SystemXml, but doesn't matter in this case.
				}
				catch (System.Exception)
				{
					stringValue = m_value.ToString();
					format = ValueFormat.String;
				}
			}

			Debug.Assert(format != ValueFormat.Raw, "format != ValueFormat.Raw");
			return format;
		}

		private ValueFormat GetDataFromBinary(out string stringValue, out byte[] binaryValue)
		{
			// We already have the binary data, but for a primitive type we want to store only the XML. In that
			// case unpack the value (deserialize from binary) and serialize to XML.

			System.Type type = ClassInfo.GetTypeFromAssemblyQualifiedName(m_class);
			Debug.Assert(type != null, "type != null");

			if (PrimitiveTypeInfo.GetPrimitiveTypeInfo(type, false) != null)
			{
				stringValue = TypeXmlConvert.ToString(Value);
				binaryValue = null;
				return (type.Namespace == m_systemNamespace ? ValueFormat.SystemXml : ValueFormat.LinkMeXml);
			}
			else if (type == typeof(ulong) || type == typeof(uint) || type == typeof(ushort) || type == typeof(sbyte))
			{
				stringValue = ((System.IFormattable)Value).ToString(null, NumberFormatInfo.InvariantInfo);
				binaryValue = null;
				return ValueFormat.SystemXml;
			}

			// Not a primitive type - store the binary data.

			stringValue = null;
			binaryValue = (byte[])m_value;
			ValueFormat format = m_valueFormat;

			// Do we want the string representation as well?

			if (type.IsSubclassOf(typeof(System.Exception)) || type == typeof(System.Exception))
			{
				stringValue = Value.ToString();
			}

			return format;
		}

		#endregion

		private void Unpack()
		{
			switch ( m_valueFormat )
			{
				case ValueFormat.LinkMeXml:
					UnpackTypeXml();
					break;

				case ValueFormat.SystemXml:
					UnpackSystemXml();
					break;

				case ValueFormat.LinkMeBinary:
					UnpackTypeBinary();
					break;

				case ValueFormat.SystemBinary:
					UnpackSystemBinary();
					break;
			}
		}

		private void UnpackTypeXml()
		{
			// Create an instance.

			ClassInfo info = new ClassInfo(m_class);
			IXmlSerializable value = info.CreateInstance<IXmlSerializable>();

			// Ask the object to unserialize itself using the IXmlSerializable interface.

			XmlSerializer.Deserialize(value, (string) m_value);

			// Update.

			m_class = null;
			m_value = value;
			m_valueFormat = ValueFormat.Raw;
		}

		private void UnpackSystemXml()
		{
			// Try to unserialize it using the serializer.

			ClassInfo info = new ClassInfo(m_class);
			object value = XmlSerializer.Deserialize(info.GetNetType(), (string) m_value);

			m_class = null;
			m_value = value;
			m_valueFormat = ValueFormat.Raw;
		}

		private void UnpackTypeBinary()
		{
			// Create an instance.

			ClassInfo info = new ClassInfo(m_class);
			IBinarySerializable value = info.CreateInstance<IBinarySerializable>();

			// Ask the object to unserialize itself using the IBinarySerializable interface.

			MemoryStream stream = new MemoryStream((byte[]) m_value);
			using ( BinaryReader reader = new BinaryReader(stream) )
			{
				value.Read(reader);
			}

			m_class = null;
			m_value = value;
			m_valueFormat = ValueFormat.Raw;
		}

		private void UnpackSystemBinary()
		{
			object value = null;
			using ( MemoryStream stream = new MemoryStream((byte[]) m_value) )
			{
				BinaryFormatter formatter = new BinaryFormatter();
				value = formatter.Deserialize(stream);
			}

			m_class = null;
			m_value = value;
			m_valueFormat = ValueFormat.Raw;
		}

        private static bool ByteArraysEqual(byte[] data1, byte[] data2)
        {
            if (ReferenceEquals(data1, data2))
                return true;

            if (data1 == null || data2 == null)
                return false;
            if (data1.Length != data2.Length)
                return false;

            for (int i = 0; i < data1.Length; i++)
            {
                if (data1[i] != data2[i])
                    return false;
            }

            return true;
        }

		private string m_name;
		private string m_class;
		private object m_value;
		private ValueFormat m_valueFormat;
	}

	public class EventParameters
	{
		internal EventParameters(EventParameter[] parameters)
		{
			m_parameters = parameters;
		}

		public int Count
		{
			get { return m_parameters.Length; }
		}

		public EventParameter this[int index]
		{
			get { return m_parameters[index]; }
		}

		public IEnumerator GetEnumerator()
		{
			return m_parameters.GetEnumerator();
		}

		private EventParameter[] m_parameters;
	}
}
