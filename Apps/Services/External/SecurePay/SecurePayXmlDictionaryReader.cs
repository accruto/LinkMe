using System;
using System.Xml;

namespace LinkMe.Apps.Services.External.SecurePay
{
    public class SecurePayXmlDictionaryReader
        : XmlDictionaryReader
    {
        private readonly XmlDictionaryReader _reader;

        public SecurePayXmlDictionaryReader(XmlDictionaryReader reader)
        {
            _reader = reader;
        }

        public override string GetAttribute(string name)
        {
            return _reader.GetAttribute(name);
        }

        public override string GetAttribute(string name, string namespaceURI)
        {
            return _reader.GetAttribute(name, namespaceURI);
        }

        public override string GetAttribute(int i)
        {
            return _reader.GetAttribute(i);
        }

        public override bool MoveToAttribute(string name)
        {
            return _reader.MoveToAttribute(name);
        }

        public override bool MoveToAttribute(string name, string ns)
        {
            return _reader.MoveToAttribute(name, ns);
        }

        public override bool MoveToFirstAttribute()
        {
            return _reader.MoveToFirstAttribute();
        }

        public override bool MoveToNextAttribute()
        {
            return _reader.MoveToNextAttribute();
        }

        public override bool MoveToElement()
        {
            return _reader.MoveToElement();
        }

        public override bool ReadAttributeValue()
        {
            return _reader.ReadAttributeValue();
        }

        public override bool Read()
        {
            return _reader.Read();
        }

        public override void Close()
        {
            _reader.Close();
        }

        public override string LookupNamespace(string prefix)
        {
            return _reader.LookupNamespace(prefix);
        }

        public override void ResolveEntity()
        {
            _reader.ResolveEntity();
        }

        public override XmlNodeType NodeType
        {
            get { return _reader.NodeType; }
        }

        public override string LocalName
        {
            get { return _reader.LocalName; }
        }

        public override string NamespaceURI
        {
            get { return _reader.NamespaceURI; }
        }

        public override string Prefix
        {
            get { return _reader.Prefix; }
        }

        public override bool HasValue
        {
            get { return _reader.HasValue; }
        }

        public override string Value
        {
            get { return _reader.Value; }
        }

        public override int Depth
        {
            get { return _reader.Depth; }
        }

        public override string BaseURI
        {
            get { return _reader.BaseURI; }
        }

        public override bool IsEmptyElement
        {
            get { return _reader.IsEmptyElement; }
        }

        public override int AttributeCount
        {
            get { return _reader.AttributeCount; }
        }

        public override bool EOF
        {
            get { return _reader.EOF; }
        }

        public override ReadState ReadState
        {
            get { return _reader.ReadState; }
        }

        public override XmlNameTable NameTable
        {
            get { return _reader.NameTable; }
        }
    }
}
