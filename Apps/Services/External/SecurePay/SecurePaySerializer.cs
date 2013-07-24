using System;
using System.Runtime.Serialization;
using System.Xml;

namespace LinkMe.Apps.Services.External.SecurePay
{
    public class SecurePaySerializer
        : XmlObjectSerializer
    {
        private readonly DataContractSerializer _serializer;

        public SecurePaySerializer(Type type)
        {
            _serializer = new DataContractSerializer(type);
        }

        public override void WriteStartObject(XmlDictionaryWriter writer, object graph)
        {
            _serializer.WriteStartObject(new SecurePayXmlDictionaryWriter(writer), graph);
        }

        public override void WriteObjectContent(XmlDictionaryWriter writer, object graph)
        {
            _serializer.WriteObjectContent(new SecurePayXmlDictionaryWriter(writer), graph);
        }

        public override void WriteEndObject(XmlDictionaryWriter writer)
        {
            _serializer.WriteEndObject(new SecurePayXmlDictionaryWriter(writer));
        }

        public override object ReadObject(XmlDictionaryReader reader, bool verifyObjectName)
        {
            return _serializer.ReadObject(new SecurePayXmlDictionaryReader(reader), verifyObjectName);
        }

        public override bool IsStartObject(XmlDictionaryReader reader)
        {
            return _serializer.IsStartObject(new SecurePayXmlDictionaryReader(reader));
        }
    }
}