using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;


namespace LinkMe.Framework.Utility.Xml
{
	/// <summary>
	/// Provides static methods for serializing objects that support the IXmlSerializable interface.
	/// </summary>
	public sealed class XmlSerializer
	{
		/// <summary>
		/// Private construtor so that an instance cannot be created.
		/// </summary>
		private XmlSerializer()
		{
		}

		public static void Serialize(object source, XmlWriter writer)
		{
			const string method = "Serialize";

			if (source == null)
				throw new Exceptions.NullParameterException(typeof(XmlSerializer), method, "source");
			if (writer == null)
				throw new Exceptions.NullParameterException(typeof(XmlSerializer), method, "writer");

			// Look for IXmlSerializable first.

			if ( source is IXmlSerializable )
			{
				// Let it do it.

				IXmlSerializable xmlSerializable = source as IXmlSerializable;
				xmlSerializable.WriteOuterXml(writer);
			}
			else
			{
				// Fall back to the system serializer.

				System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(source.GetType());
				xmlSerializer.Serialize(writer, source);
			}
		}

		public static string Serialize(object source)
		{
			const string method = "Serialize";

			if (source == null)
				throw new Exceptions.NullParameterException(typeof(XmlSerializer), method, "source");

			// Create a writer for the output.

			StringBuilder builder = new StringBuilder();
			XmlWriter writer = new XmlTextWriter(new StringWriter(builder));
			Serialize(source, writer);
			return builder.ToString();
		}

		public static string Serialize(IXmlSerializable source)
		{
			const string method = "Serialize";

			if (source == null)
				throw new Exceptions.NullParameterException(typeof(XmlSerializer), method, "source");

			// Create a writer for the output.

			StringBuilder builder = new StringBuilder();
			XmlWriter writer = new XmlTextWriter(new StringWriter(builder));
			source.WriteOuterXml(writer);
			return builder.ToString();
		}

		public static void Deserialize(IXmlSerializable source, string xml)
		{
			if ( source != null && xml != null && xml.Length > 0 )
				Deserialize(source, new XmlTextReader(new StringReader(xml)));
		}

		public static void Deserialize(IXmlSerializable source, XmlReader reader)
		{
			const string method = "Deserialize";

			if (reader == null)
				throw new Exceptions.NullParameterException(typeof(XmlSerializer), method, "reader");

			// IXmlSerializable.ReadOuterXml() should read the entire element on which the reader is position,
			// including the EndElement node, which some implementations don't do. Check for this common problem.

			if ( source != null )
			{
#if DEBUG
				Debug.Assert(reader.NodeType == XmlNodeType.None || reader.NodeType == XmlNodeType.Element,
					string.Format("The reader node is {0} '{1}' before calling ReadOuterXml() on '{2}'.",
					reader.NodeType, reader.LocalName, source.GetType().FullName),
					"Before ReadOuterXml() is called the reader should be positioned at the start of the element"
					+ " to be read by that method.");
				int depth = reader.Depth;
#endif

				source.ReadOuterXml(reader);

#if DEBUG
				Debug.Assert(depth == 0 || reader.Depth == depth - 1, string.Format("The reader node is {0} '{1}'"
					+ " after calling ReadOuterXml() on '{2}'.", reader.NodeType, reader.LocalName, source.GetType().FullName),
					"After ReadOuterXml() is called the reader should be positioned immediately AFTER the element"
					+ " read by that method (NOT on the EndElement node of the element that was read).");
#endif
			}
		}

		public static object Deserialize(System.Type type, XmlReader reader)
		{
			const string method = "Deserialize";

			if (type == null)
				throw new Exceptions.NullParameterException(typeof(XmlSerializer), method, "type");
			if (reader == null)
				throw new Exceptions.NullParameterException(typeof(XmlSerializer), method, "reader");

			// Look for IXmlSerializable first.

			System.Type interfaceType = type.GetInterface(typeof(IXmlSerializable).FullName);
			if ( interfaceType == typeof(IXmlSerializable) )
			{
				// Create an instance.

				IXmlSerializable xmlSerializable = (IXmlSerializable)System.Activator.CreateInstance(type, true);

				// Let it do it.

				Deserialize(xmlSerializable, reader);

				return xmlSerializable;
			}
			else
			{
				// Fall back to the system serializer.

				System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(type);
				return xmlSerializer.Deserialize(reader);
			}
		}

		public static object Deserialize(System.Type type, string xml)
		{
			if (xml == null || xml.Length == 0)
				return null;

			return Deserialize(type, new XmlTextReader(new StringReader(xml)));
		}

		public static bool IsTypeSerializable(System.Type type)
		{
			const string method = "IsTypeSerializable";

			if (type == null)
				throw new Exceptions.NullParameterException(typeof(XmlSerializer), method, "type");

			// Check if the type supports IXmlSerializable.

			if (type.GetInterface(typeof(IXmlSerializable).FullName, false) == typeof(IXmlSerializable))
				return true;

			// Can it be used with the system XmlSerializer? Trying to create an XmlSerializer and catching
			// the exception all the time is slow, so do some preliminary checks first.

			if (type.IsNotPublic || type.IsInterface)
				return false;

			if (type.IsClass && !type.IsAbstract && !type.IsArray && type.GetConstructor(System.Type.EmptyTypes) == null)
				return false;
	
			// Check if a system XmlSerializer can be created for it.

			try
			{
				new System.Xml.Serialization.XmlSerializer(type);
			}
			catch (System.Exception)
			{
				return false;
			}

			return true;
		}
	}
}
