using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace LinkMe.Framework.Utility.Wcf
{
    /// <summary>
    /// Implementation of XmlObjectSerializer using XmlSerializer.
    /// Borrowed from System.ServiceModel.Dispatcher.XmlSerializerObjectSerializer.
    /// </summary>
    public class XmlSerializerObjectSerializer : XmlObjectSerializer
    {
        #region Fields

        private bool _isSerializerSetExplicit;
        private string _rootName;
        private string _rootNamespace;
        private Type _rootType;
        private XmlSerializer _serializer;

        #endregion

        #region Construction

        public XmlSerializerObjectSerializer(Type type)
        {
            Initialize(type, null, null, null);
        }

        public XmlSerializerObjectSerializer(Type type, XmlQualifiedName qualifiedName, XmlSerializer xmlSerializer)
        {
            if (qualifiedName == null)
                throw new ArgumentNullException("qualifiedName");

            Initialize(type, qualifiedName.Name, qualifiedName.Namespace, xmlSerializer);
        }

        private void Initialize(Type type, string rootName, string rootNamespace, XmlSerializer xmlSerializer)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            _rootType = type;
            _rootName = rootName;
            _rootNamespace = rootNamespace ?? string.Empty;
            _serializer = xmlSerializer;

            if (_serializer == null)
            {
                if (_rootName == null)
                {
                    _serializer = new XmlSerializer(type);
                }
                else
                {
                    var root = new XmlRootAttribute {ElementName = _rootName, Namespace = _rootNamespace};
                    _serializer = new XmlSerializer(type, root);
                }
            }
            else
            {
                _isSerializerSetExplicit = true;
            }

            if (_rootName == null)
            {
                XmlTypeMapping mapping = new XmlReflectionImporter().ImportTypeMapping(_rootType);
                _rootName = mapping.ElementName;
                _rootNamespace = mapping.Namespace;
            }
        }

        #endregion

        #region Overrides

        public override bool IsStartObject(XmlDictionaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            reader.MoveToElement();
            if (_rootName != null)
                return reader.IsStartElement(_rootName, _rootNamespace);

            return reader.IsStartElement();
        }

        public override object ReadObject(XmlDictionaryReader reader, bool verifyObjectName)
        {
            if (!_isSerializerSetExplicit)
                return _serializer.Deserialize(reader);

            var objArray = (object[])_serializer.Deserialize(reader);
            if ((objArray != null) && (objArray.Length > 0))
                return objArray[0];

            return null;
        }

        public override void WriteEndObject(XmlDictionaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public override void WriteObject(XmlDictionaryWriter writer, object graph)
        {
            if (_isSerializerSetExplicit)
                _serializer.Serialize(writer, new[] {graph});
            else
                _serializer.Serialize(writer, graph);
        }

        public override void WriteObjectContent(XmlDictionaryWriter writer, object graph)
        {
            throw new NotImplementedException();
        }

        public override void WriteStartObject(XmlDictionaryWriter writer, object graph)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}