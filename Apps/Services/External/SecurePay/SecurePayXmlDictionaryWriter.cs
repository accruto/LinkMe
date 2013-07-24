using System.Linq;
using System.Xml;

namespace LinkMe.Apps.Services.External.SecurePay
{
    public class SecurePayXmlDictionaryWriter
        : XmlDictionaryWriter
    {
        private static readonly string[] ElementsAsAttributes = new [] {"count", "ID"};

        private readonly XmlDictionaryWriter _writer;
        private bool _ignoreEndAttribute;
        private bool _writeAsAttribute;

        public SecurePayXmlDictionaryWriter(XmlDictionaryWriter writer)
        {
            _writer = writer;
        }

        public override void WriteStartDocument()
        {
            _writer.WriteStartDocument();
        }

        public override void WriteStartDocument(bool standalone)
        {
            _writer.WriteStartDocument(standalone);
        }

        public override void WriteEndDocument()
        {
            _writer.WriteEndDocument();
        }

        public override void WriteDocType(string name, string pubid, string sysid, string subset)
        {
            _writer.WriteDocType(name, pubid, sysid, subset);
        }

        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            if (ElementsAsAttributes.Contains(localName))
            {
                _writeAsAttribute = true;
                WriteStartAttribute(prefix, localName, ns);
            }
            else
            {
                _writer.WriteStartElement(prefix, localName, null);
            }
        }

        public override void WriteEndElement()
        {
            if (_writeAsAttribute)
            {
                WriteEndAttribute();
                _writeAsAttribute = false;
            }
            else
            {
                _writer.WriteEndElement();
            }
        }

        public override void WriteFullEndElement()
        {
            if (_writeAsAttribute)
            {
                WriteEndAttribute();
                _writeAsAttribute = false;
            }
            else
            {
                _writer.WriteEndElement();
            }
        }

        public override void WriteStartAttribute(string prefix, string localName, string ns)
        {
            // Ignore all namespace declarations.

            if (prefix == "xmlns")
                _ignoreEndAttribute = true;
            else
                _writer.WriteStartAttribute(prefix, localName, null);
        }

        public override void WriteEndAttribute()
        {
            if (_ignoreEndAttribute)
                _ignoreEndAttribute = false;
            else
                _writer.WriteEndAttribute();
        }

        public override void WriteCData(string text)
        {
            _writer.WriteCData(text);
        }

        public override void WriteComment(string text)
        {
            _writer.WriteComment(text);
        }

        public override void WriteProcessingInstruction(string name, string text)
        {
            _writer.WriteProcessingInstruction(name, text);
        }

        public override void WriteEntityRef(string name)
        {
            _writer.WriteEntityRef(name);
        }

        public override void WriteCharEntity(char ch)
        {
            _writer.WriteCharEntity(ch);
        }

        public override void WriteWhitespace(string ws)
        {
            _writer.WriteWhitespace(ws);
        }

        public override void WriteString(string text)
        {
            if (!_ignoreEndAttribute)
                _writer.WriteString(text);
        }

        public override void WriteSurrogateCharEntity(char lowChar, char highChar)
        {
            _writer.WriteSurrogateCharEntity(lowChar, highChar);
        }

        public override void WriteChars(char[] buffer, int index, int count)
        {
            _writer.WriteChars(buffer, index, count);
        }

        public override void WriteRaw(char[] buffer, int index, int count)
        {
            _writer.WriteRaw(buffer, index, count);
        }

        public override void WriteRaw(string data)
        {
            _writer.WriteRaw(data);
        }

        public override void WriteBase64(byte[] buffer, int index, int count)
        {
            _writer.WriteBase64(buffer, index, count);
        }

        public override void Close()
        {
            _writer.Close();
        }

        public override void Flush()
        {
            _writer.Flush();
        }

        public override string LookupPrefix(string ns)
        {
            return _writer.LookupPrefix(ns);
        }

        public override WriteState WriteState
        {
            get { return _writer.WriteState; }
        }
    }
}
