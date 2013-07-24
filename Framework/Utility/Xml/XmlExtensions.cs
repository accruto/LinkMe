using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace LinkMe.Framework.Utility.Xml
{
    public static class XmlExtensions
    {
        private static readonly Encoding _defaultEncoding = new UTF8Encoding(false); // Not the same as Encoding.UTF8

        /// <summary>
        /// Use UTF8 without the two identifier bytes.
        /// </summary>
        public static Encoding DefaultEncoding
        {
            get { return _defaultEncoding; }
        }

        public static void ValidateXml(this Stream input, XmlSchema schema)
        {
            if (input == null)
                throw new ArgumentException("input");
            if (schema == null)
                throw new ArgumentException("schema");

            var settings = new XmlReaderSettings {ValidationType = ValidationType.Schema};
            settings.Schemas.Add(schema);

            var validator = XmlReader.Create(input, settings);
            while (validator.Read())
            {
            }
        }

        public static bool IsValidCharForXmlText(this char c)
        {
            // According to http://www.w3.org/TR/REC-xml/#charsets valid characters are:
            // 0x9, 0xA, 0xD, 0x20-0xD7FF, 0xE000-0xFFF0 and 0x10000-0x10FFFF (the last one is out of range for char).

            if (c < 0x20)
                return (c == '\t' || c == '\n' || c == '\r');
            if (c <= 0xD7FF)
                return true;
            return (c >= 0xE000 && c <= 0xFFF0);
        }

        public static string StripInvalidCharsFromXml(this string xml)
        {
            //Use string instead of StringBuilder
            //Somewhate counter-intuitively it's faster for most strings
            // when there aren't a large number of invalid characters 
            // in the xml string

            if (string.IsNullOrEmpty(xml))
                return xml;

            var i = 0;
            while (i < xml.Length)
            {
                if (IsValidCharForXmlText(xml.ToCharArray(i, 1)[0]))
                    ++i;
                else
                    xml = xml.Remove(i, 1);
            }

            return xml;
        }

        public static string StripInvalidAsciiCharsForXml(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            // First strip out characters that just aren't valid in XML text, ever.

            value = StripInvalidCharsFromXml(value);

            // The utf encoding method returns question marks where it doesn't understand some
            // chars, so we need to strip these out.

            var parts = value.Split('?');
            var sb = new StringBuilder();

            for (var i = 0; i < parts.Length; i++)
            {
                var encoded = Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(parts[i]));
                sb.Append(encoded.Replace("?", ""));

                // Replace the question marks we split on
                if (i < parts.Length - 1)
                    sb.Append('?');
            }

            return sb.ToString();
        }

        public static void WriteNonEmptyAttributeString(this XmlWriter writer, string localName, string value)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (string.IsNullOrEmpty(localName))
                throw new ArgumentException("The attribute name must be specified.", "localName");

            if (!string.IsNullOrEmpty(value))
                writer.WriteAttributeString(localName, value);
        }

        public static void WriteNonEmptyElementString(this XmlWriter writer, string localName, string value)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (string.IsNullOrEmpty(localName))
                throw new ArgumentException("The attribute name must be specified.", "localName");

            if (!string.IsNullOrEmpty(value))
                writer.WriteElementString(localName, value);
        }

        public static string SerializeEnum(this Enum value)
        {
            return value.ToString().Replace(", ", " ");
        }

        public static T DeserializeEnum<T>(this string value)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Type argument T must be an Enum type.");
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("The " + typeof(T).Name + " value to deserialize must be specified.", "value");

            var parts = value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            long intValue = 0;
            foreach (var part in parts)
            {
                var partValue = Enum.Parse(typeof(T), part);
                intValue |= Convert.ToInt64(partValue);
            }

            return (T)Enum.ToObject(typeof(T), intValue);
        }

        public static List<string> ReadStringList(this XmlReader reader, string elementName)
        {
            var items = new List<string>();
            while (reader.IsStartElement(elementName))
                items.Add(reader.ReadElementString(elementName));
            return items;
        }

        public static string ReadOptionalElementString(this XmlReader reader, string elementName)
        {
            return !reader.IsStartElement(elementName) ? null : reader.ReadElementString(elementName);
        }

        public static void ReadEndElement(this XmlReader reader, string elementName)
        {
            // XmlReader itself handles this really poorly: if you call ReadStartElement on "<test> </test>"
            // you must call ReadEndElement(), but if you call ReadStartElement on "<test />" then you must
            // NOT call it or it will read the next outer end element tag!

            if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == elementName)
                reader.ReadEndElement();
        }
    }
}
