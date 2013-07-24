using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using LinkMe.Framework.Utility.Collections;
using LinkMe.Framework.Utility.Event;
using LinkMe.Framework.Utility.Net;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Utility.Exceptions
{
	/// <summary>
	/// Contains information about an exception that can displayed in the ExceptionViewer control and
	/// serialized to XML. Does not store the exception itself. All properties values are stored as strings
	/// and accessing them should never throw exceptions.
	/// </summary>
	[Serializable]
	public class ExceptionInfo
		:	ICloneable,
            IXmlSerializable,
            IBinarySerializable,
            IInternable
	{
		private const string ExceptionFormatString = "<error: an exception of type '{0}' was thrown>";
        private const string OuterElement = "exception";
        private const string NullValue = "<null>";
        private const uint BinaryVersion = 20080818;
        private const int MaxPrimitiveElementsToShow = 50;
        private const int MaxComplexElementsToShow = 10;

		private ExceptionInfo _innerException;
		private string _helpLink;
		private string _hResult;
		private string _message;
		private string _source;
		private string _stackTrace;
		private string _targetSite;
		private string _toString;
		private ClassInfo _type;
		private IDictionary _properties = CreateDictionary();

	    #region Constructors

		public ExceptionInfo(Exception exception, IErrorHandler errorHandler)
		{
			if (exception == null)
				throw new NullParameterException(typeof(ExceptionInfo), "ctor", "exception");
            if (errorHandler == null)
                errorHandler = new DefaultErrorHandler();

			// Type

			Type type = exception.GetType();
			_type = new ClassInfo(type.FullName, type.Assembly.FullName);

			// HelpLink

			try
			{
				_helpLink = exception.HelpLink;
			}
			catch (Exception ex)
			{
				_helpLink = string.Format(ExceptionFormatString, ex.GetType().FullName);
			}

			// Replace LF with CRLF in every property that is likely to have new lines, so that it displays
			// properly in a multi-line textbox.

			// Message

			try
			{
                if (exception is UserException)
                    _message = TextUtil.ReplaceLfWithCrlf(errorHandler.FormatErrorMessage((UserException)exception));
                else
                    _message = TextUtil.ReplaceLfWithCrlf(exception.Message);
			}
			catch (Exception ex)
			{
				_message = string.Format(ExceptionFormatString, ex.GetType().FullName);
			}

			// Source

			try
			{
				_source = exception.Source;
			}
			catch (Exception ex)
			{
				_source = string.Format(ExceptionFormatString, ex.GetType().FullName);
			}

			// StackTrace

			try
			{
				// The stack trace may be null if the exception is not actually thrown (just instantiated).
				// SqlException also has a bug that loses the stack trace when deserializing.

				_stackTrace = TextUtil.ReplaceLfWithCrlf(exception.StackTrace);
			}
			catch (Exception ex)
			{
				_stackTrace = string.Format(ExceptionFormatString, ex.GetType().FullName);
			}

			// TargetSite

			try
			{
				_targetSite = (exception.TargetSite == null ? null : exception.TargetSite.ToString());
			}
			catch (Exception ex)
			{
				_targetSite = string.Format(ExceptionFormatString, ex.GetType().FullName);
			}

			// HResult

			try
			{
				PropertyInfo propertyHr = exception.GetType().GetProperty("HResult",
					BindingFlags.NonPublic | BindingFlags.Instance);
				object tempValue = propertyHr.GetValue(exception, null);
				_hResult = (tempValue == null ? null : string.Format("0x{0:x}", tempValue));
			}
			catch (Exception ex)
			{
				_hResult = string.Format(ExceptionFormatString, ex.GetType().FullName);
			}

            // Other properties

			InitialiseAdditionalProperties(exception);

		    // ToString()

			InitialiseToString(exception);

		    // InnerException

			_innerException = (exception.InnerException == null ? null : new ExceptionInfo(exception.InnerException, errorHandler));
		}

	    private ExceptionInfo()
		{
		}

	    #endregion

		#region ICloneable members

		object ICloneable.Clone()
		{
			return Clone();
		}

	    #endregion

        #region IBinarySerializable members

        public void Write(BinaryWriter writer)
        {
            writer.Write(BinaryVersion);

            writer.Write(_type != null);
            if (_type != null)
            {
                _type.Write(writer);
            }

            WriteNullable(writer, _helpLink);
            WriteNullable(writer, _message);
            WriteNullable(writer, _source);
            WriteNullable(writer, _stackTrace);
            WriteNullable(writer, _targetSite);
            WriteNullable(writer, _hResult);

            if (_properties == null)
            {
                writer.Write(0);
            }
            else
            {
                var stream = new MemoryStream();
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, _properties);

                var length = (int)stream.Length;
                writer.Write(length);
                writer.Write(stream.GetBuffer(), 0, length);
            }

            writer.Write(_innerException != null);
            if (_innerException != null)
            {
                _innerException.Write(writer);
            }
        }

	    public void Read(BinaryReader reader)
        {
            uint version = reader.ReadUInt32();
            if (version != BinaryVersion)
            {
                throw new ArgumentException(string.Format("Expected version {0}, but read version {1}.",
                    BinaryVersion, version));
            }

            Reset();

            if (reader.ReadBoolean())
            {
                _type = new ClassInfo();
                _type.Read(reader);
            }

            _helpLink = ReadNullable(reader);
            _message = ReadNullable(reader);
            _source = ReadNullable(reader);
            _stackTrace = ReadNullable(reader);
            _targetSite = ReadNullable(reader);
            _hResult = ReadNullable(reader);

            int length = reader.ReadInt32();
            if (length != 0)
            {
                byte[] bytes = reader.ReadBytes(length);

                var formatter = new BinaryFormatter();
                _properties = (IDictionary)formatter.Deserialize(new MemoryStream(bytes));
            }

            if (reader.ReadBoolean())
            {
                _innerException = new ExceptionInfo();
                _innerException.Read(reader);
            }
        }

	    #endregion

        #region IInternable members

        public void Intern(Interner interner)
        {
            interner.Intern(_type);
            interner.Intern(ref _helpLink);
            interner.Intern(ref _hResult);
            interner.Intern(ref _message);
            interner.Intern(ref _source);
            interner.Intern(ref _stackTrace);
            interner.Intern(ref _targetSite);
            interner.Intern(ref _toString);
            _properties = interner.Intern(_properties);
            interner.Intern(_innerException);
        }

	    #endregion

        #region Static methods

        public static ExceptionInfo FromXmlFile(string filePath)
		{
            using (var streamReader = new StreamReader(filePath))
            {
                return FromXml(new XmlReadAdaptor(streamReader));
            }
		}

	    public static ExceptionInfo FromXmlString(string xml)
        {
            using (var stringReader = new StringReader(xml))
            {
                return FromXml(new XmlReadAdaptor(stringReader));
            }
        }

        public static ExceptionInfo FromXml(XmlReadAdaptor reader)
        {
            return FromXml(reader, OuterElement);
        }

	    public static ExceptionInfo FromXml(XmlReadAdaptor reader, string topElementName)
        {
            if (!reader.ReadElement(topElementName))
                return null;

            var value = new ExceptionInfo();
            value.ReadXml(reader);
            reader.ReadEndElement();

            return value;
        }

	    public static ExceptionInfo FromBinary(BinaryReader reader)
        {
            var value = new ExceptionInfo();
            ((IBinarySerializable)value).Read(reader);
            return value;
        }

	    public static string GetMessageTree(Exception ex)
		{
			if (ex == null)
				return null;

			return (ex.InnerException == null ? ex.Message :
				ex.Message + System.Environment.NewLine + "--> " + GetMessageTree(ex.InnerException));
		}

	    private static void ReadDictionaryFromXml(XmlReadAdaptor reader, IDictionary dictionary)
		{
            while (reader.ReadElement())
            {
                string name = XmlConvert.DecodeName(reader.Name);
                object value;

                if (reader.ReadAttributeBoolean("null", false))
                {
                    value = null;
                }
                else if (reader.IsEmptyElement)
                {
                    value = "";
                }
                else
                {
                    reader.MoveToElement();

                    if (reader.HasValue)
                    {
                        value = reader.ReadString();
                    }
                    else
                    {
                        // Read sub-elements into a dictionary.

                        IDictionary subDictionary = CreateDictionary();
                        ReadDictionaryFromXml(reader, subDictionary);
                        value = subDictionary;
                    }
                }

                reader.ReadEndElement();

                dictionary.Add(name, value);
            }
		}

	    private static void WriteDictionaryToXml(XmlWriteAdaptor writer, IDictionary dictionary, string elementName)
		{
			if (dictionary.Count > 0)
			{
				writer.WriteStartElement(elementName);

				IDictionaryEnumerator enumerator = dictionary.GetEnumerator();
				while (enumerator.MoveNext())
				{
                    string childElementName = XmlConvert.EncodeName((string)enumerator.Key);

					if (enumerator.Value == null)
					{
                        writer.WriteStartElement(childElementName);
						writer.WriteAttribute("null", "true");
						writer.WriteEndElement();
					}
					else if (enumerator.Value is IDictionary)
					{
                        WriteDictionaryToXml(writer, (IDictionary)enumerator.Value, childElementName);
					}
					else
					{
						Debug.Assert(enumerator.Value is string, "enumerator.Value is string");
                        writer.WriteElement(childElementName, (string)enumerator.Value);
					}
				}

				writer.WriteEndElement();
			}
		}

	    private static IDictionary GetEventDetailsValue(IEnumerable<IEventDetail> details)
        {
            IDictionary detailsTable = CreateDictionary();

            foreach (IEventDetail detail in details)
            {
                // Add an entry for each set of details.

                IDictionary detailsValue = CreateDictionary();
                detailsTable[detail.GetType().Name] = detailsValue;

                // Iterate through properties.

                PropertyInfo[] detailsProperties = detail.GetType().GetProperties(
                    BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo detailsProperty in detailsProperties)
                {
                    if (detailsProperty.CanRead)
                    {
                        object detailsPropertyValue = detailsProperty.GetValue(detail, null);
                        detailsValue[detailsProperty.Name] =
                            detailsPropertyValue == null ? null : detailsPropertyValue.ToString();
                    }
                }
            }

            return detailsTable;
        }

        private static object GetArrayValue(IList array)
        {
            Type type = array.GetType().GetElementType();

            if (array.Count == 0)
                return type.FullName + "[0]";

            if (System.Type.GetTypeCode(type) != TypeCode.Object)
                return string.Format("{0}[{1}] {2}", type, array.Count, GetPrimitiveListValue(array));
            return GetComplexListValue(array);
        }

        private static object GetListValue(IList list)
        {
            if (list.Count == 0)
                return "{ Count = 0 }";

            // Try to guess whether it's a "primitive" list or not by using the first non-null element.

            object element = null;
            int maxI = Math.Min(list.Count, MaxComplexElementsToShow);
            for (int i = 0; i < maxI; i++)
            {
                element = list[i];
                if (element != null)
                    break;
            }

            if (element != null && System.Type.GetTypeCode(element.GetType()) != TypeCode.Object)
                return GetPrimitiveListValue(list);
            return GetComplexListValue(list);
        }

	    private static string GetPrimitiveListValue(IList list)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("{{{0}", GetElementValue(list, 0));

            int length = Math.Min(list.Count, MaxPrimitiveElementsToShow);

            for (int index = 1; index < length; index++)
            {
                sb.Append(", ");
                sb.Append(GetElementValue(list, index));
            }

            if (list.Count > MaxPrimitiveElementsToShow)
            {
                sb.Append(", ...");
            }

            sb.Append("}");

            return sb.ToString();
        }

	    private static IDictionary GetComplexListValue(IList list)
	    {
	        // Simply convert the list to a dictionary with indexes as keys.

            IDictionary dictionary = CreateDictionary();

            int length = Math.Min(list.Count, MaxComplexElementsToShow);
            for (int index = 0; index < length; index++)
            {
                dictionary[index.ToString()] = GetElementValue(list, index);
            }

            return dictionary;
	    }

        private static IDictionary GetDictionaryValue(IDictionary raw)
        {
            IDictionary strings = CreateDictionary();

            int count = 0;
            IDictionaryEnumerator enumerator = raw.GetEnumerator();
            while (enumerator.MoveNext() && count++ < MaxComplexElementsToShow)
            {
                // The key should never be null in a correct IDictionary implementation, but just in case...
                string keyAsString = (enumerator.Key == null ? NullValue : enumerator.Key.ToString());
                string valueAsString = (enumerator.Value == null ? NullValue : enumerator.Value.ToString());
                strings.Add(keyAsString, valueAsString);
            }

            return strings;
        }

        private static string GetElementValue(IList list, int index)
	    {
	        object value = list[index];
	        return value == null ? NullValue : value.ToString();
	    }

	    private static void WriteNullable(BinaryWriter writer, string value)
        {
            writer.Write(value != null);
            if (value != null)
            {
                writer.Write(value);
            }
        }

        private static string ReadNullable(BinaryReader reader)
        {
            if (reader.ReadBoolean())
                return reader.ReadString();
            return null;
        }

	    private static IDictionary CreateDictionary()
        {
            // TODO: To really save memory and time in deserializing implement a custom dictionary class
            // with a small footprint and IBinarySerializable support.
            return new OrderedDictionary();
        }

	    private static IDictionary CopyDictionary(IDictionary dictionary)
        {
            if (dictionary == null)
                return null;

            // Perform a deep copy, assuming values are either primitive types or IDictionary.

            IDictionary copy = CreateDictionary();

            IDictionaryEnumerator enumerator = dictionary.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var subDictionary = enumerator.Value as IDictionary;
                object value = (subDictionary == null ? enumerator.Value : CopyDictionary(subDictionary));
                copy.Add(enumerator.Key, value);
            }

            return copy;
        }

	    private static object FormatPropertyValue(object rawValue)
	    {
	        if (rawValue == null)
                return null;

            if (rawValue is Array)
	            return GetArrayValue((Array)rawValue);
            if (rawValue is IList)
                return GetListValue((IList)rawValue);
            if (rawValue is EventDetails)
	            return GetEventDetailsValue((EventDetails)rawValue);
            if (rawValue is IDictionary)
                return GetDictionaryValue((IDictionary)rawValue);

            return TextUtil.ReplaceLfWithCrlf(rawValue.ToString());
	    }

	    #endregion

		#region Instance properties

	    public IDictionary AdditionalProperties
		{
			get { return _properties; }
		}

	    public string HelpLink
		{
			get { return _helpLink; }
		}

	    public string HResult
		{
			get { return _hResult; }
		}

	    public ExceptionInfo InnerException
		{
			get { return _innerException; }
		}

	    public string Message
		{
			get { return _message; }
		}

	    public string Source
		{
			get { return _source; }
		}

	    public string StackTrace
		{
			get { return _stackTrace; }
		}

	    public string TargetSite
		{
			get { return _targetSite; }
		}

	    public ClassInfo Type
		{
			get { return _type; }
		}

	    #endregion

		#region Instance methods

	    public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            var other = obj as ExceptionInfo;
            if (other == null)
                return false;

            return (Message == other.Message && Source == other.Source && HResult == other.HResult
                && HelpLink == other.HelpLink && StackTrace == other.StackTrace
                && TargetSite == other.TargetSite && Equals(Type, other.Type)
                && CollectionUtil.DictionariesEqual(AdditionalProperties, other.AdditionalProperties)
                && Equals(InnerException, other.InnerException));
        }

	    public override int GetHashCode()
        {
            int hashCode = _properties.Count.GetHashCode();

            if (_message != null)
            {
                hashCode ^= _message.GetHashCode();
            }
            if (_stackTrace != null)
            {
                hashCode ^= _stackTrace.GetHashCode();
            }
            if (_innerException != null)
            {
                hashCode ^= _innerException.GetHashCode();
            }

            return hashCode;
        }

	    public string GetMessageTree()
		{
			return (InnerException == null ? Message :
				Message + System.Environment.NewLine + "--> " + InnerException.GetMessageTree());
		}

	    public ExceptionInfo Clone()
		{
			var cloned = new ExceptionInfo
            {
                _helpLink = _helpLink,
                _hResult = _hResult,
                _innerException = (_innerException == null ? null : _innerException.Clone()),
                _message = _message,
                _properties = CopyDictionary(_properties),
                _source = _source,
                _stackTrace = _stackTrace,
                _targetSite = _targetSite,
                _type = (_type == null ? null : _type.Clone())
            };

	        return cloned;
		}

	    public override string ToString()
		{
			return (_toString ?? DefaultToString());
		}

	    public void WriteOuterXml(XmlWriter writer)
		{
            var adaptor = new XmlWriteAdaptor(writer);
            adaptor.WriteStartElement(OuterElement);
			WriteXml(adaptor);
			writer.WriteEndElement(); // </exception>
		}

	    public void WriteXml(XmlWriter writer)
        {
            if (writer.WriteState != WriteState.Element)
            {
                throw new System.InvalidOperationException(typeof(ExceptionInfo).Name + ".WriteXml() expects the start"
                     + " of the outer element to have been written. Did you mean to call WriteOuterXml() instead?");
            }

            var adaptor = new XmlWriteAdaptor(writer);
            WriteXml(adaptor);
        }

	    private void WriteXml(XmlWriteAdaptor writer)
		{
            Debug.Assert(writer.XmlWriter.WriteState == WriteState.Element, "Invalid WriteState: " + writer.XmlWriter.WriteState);

			writer.WriteClass(Type);

			// XML element names should normally being with a lowercase letter, but we want to preserve the
			// case of all the property names.

			if (HelpLink != null)
			{
				writer.WriteElement("HelpLink", HelpLink);
			}
			if (Message != null)
			{
				writer.WriteElement("Message", Message);
			}
			if (Source != null)
			{
				writer.WriteElement("Source", Source);
			}
			if (StackTrace != null)
			{
				writer.WriteElement("StackTrace", StackTrace);
			}
			if (TargetSite != null)
			{
				writer.WriteElement("TargetSite", TargetSite);
			}
			writer.WriteElement("HResult", HResult);

			// Additional properties (defined on derived exception classes)

			WriteDictionaryToXml(writer, _properties, "additionalProperties");

			// Inner exception

			if (InnerException != null)
			{
				writer.WriteStartElement("innerException");
				InnerException.WriteXml(writer);
				writer.WriteEndElement(); // </innerException>
			}
		}

	    public void ReadOuterXml(XmlReader xmlReader)
        {
            Reset();

            // Create an adaptor.

            var adaptor = new XmlReadAdaptor(xmlReader);
            if (adaptor.IsReadingElement(OuterElement))
                ReadXml(adaptor);
        }

	    public XmlSchema GetSchema()
	    {
            return null;
	    }

	    public void ReadXml(XmlReader xmlReader)
        {
            Reset();

            // Create an adaptor.

            var adaptor = new XmlReadAdaptor(xmlReader);
            ReadXml(adaptor);
        }

	    private void Reset()
	    {
            _helpLink = null;
            _hResult = null;
            _innerException = null;
            _message = null;
            _properties = CreateDictionary();
            _source = null;
            _stackTrace = null;
            _targetSite = null;
            _toString = null;
            _type = null;
	    }

	    private void ReadXml(XmlReadAdaptor reader)
        {
            _type = reader.ReadClass();
            reader.MoveToElement();

            _helpLink = reader.ReadElementString("HelpLink", false, null);
            _message = reader.ReadElementString("Message", false, null);
            _source = reader.ReadElementString("Source", false, null);
            _stackTrace = reader.ReadElementString("StackTrace", false, null);
            _targetSite = reader.ReadElementString("TargetSite", false, null);
            _hResult = reader.ReadElementString("HResult", false, null);

            // Additional properties

            if (reader.ReadElement("additionalProperties", false))
            {
                ReadDictionaryFromXml(reader, _properties);
                reader.ReadEndElement();
            }

	        // Inner exception

            if (reader.ReadElement("innerException", false))
            {
                _innerException = new ExceptionInfo();
                _innerException.ReadXml(reader);
                reader.ReadEndElement();
            }
        }

	    private string DefaultToString()
		{
            string typeName = (Type == null ? "<unknown type>" : Type.FullName);
			string result = (string.IsNullOrEmpty(Message) ? typeName : typeName + ": " + Message);

			if (InnerException != null)
			{
				result += " ---> " + InnerException + System.Environment.NewLine
					+ "   --- End of inner exception stack trace ---";
			}

			if (StackTrace != null)
			{
				result += System.Environment.NewLine + StackTrace;
			}

			return result;
		}

	    private void InitialiseToString(Exception exception)
	    {
	        MethodInfo toStringMethod = exception.GetType().GetMethod("ToString", System.Type.EmptyTypes);
	        Debug.Assert(toStringMethod != null, "Exception type '" + exception.GetType().FullName
	                                             + "' doesn't seem to have a ToString() method.");

	        if (toStringMethod.DeclaringType == typeof(Exception))
	        {
	            _toString = null; // Default ToString() implementation, so we can compute the value ourselves as needed.
	        }
	        else
	        {
	            // The derived exception class overrides ToString(), so we have to store the result.

	            try
	            {
	                _toString = exception.ToString();
	            }
	            catch (Exception ex)
	            {
	                _toString = string.Format(ExceptionFormatString, ex.GetType().FullName);
	            }
	        }
	    }

	    private void InitialiseAdditionalProperties(Exception exception)
	    {
	        // Add properties defined on derived exceptions using Reflection.

	        PropertyInfo[] properties = exception.GetType().GetProperties(
	            BindingFlags.Public | BindingFlags.Instance);

	        foreach (PropertyInfo property in properties)
	        {
	            string propertyName = property.Name;
	            if (property.CanRead && property.Name != "Message" && propertyName != "StackTrace"
	                && propertyName != "Source" && propertyName != "HResult" && propertyName != "HelpLink"
	                && propertyName != "InnerException" && propertyName != "TargetSite")
	            {
	                object propertyValue;
	                try
	                {
	                    propertyValue = FormatPropertyValue(property.GetValue(exception, null));
	                }
	                catch (TargetInvocationException ex)
	                {
	                    propertyValue = string.Format(ExceptionFormatString, ex.InnerException.GetType().FullName);
	                }
	                catch (Exception ex)
	                {
	                    propertyValue = string.Format(ExceptionFormatString, ex.GetType().FullName);
	                }

	                _properties[propertyName] = propertyValue;
	            }
	        }

            // Also add public fields - some exceptions do use those.

            FieldInfo[] fields = exception.GetType().GetFields(
                BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                string fieldName = field.Name;

                {
                    object fieldValue;
                    try
                    {
                        fieldValue = FormatPropertyValue(field.GetValue(exception));
                    }
                    catch (Exception ex)
                    {
                        fieldValue = string.Format(ExceptionFormatString, ex.GetType().FullName);
                    }

                    _properties[fieldName] = fieldValue;
                }
            }
        }

	    #endregion
	}
}
