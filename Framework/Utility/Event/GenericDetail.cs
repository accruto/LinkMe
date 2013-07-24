using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Utility.Event
{
    [Serializable]
    public class GenericDetail
        : IEventDetail,
        ISerializable,
        System.ICloneable,
        IXmlSerializable,
        IBinarySerializable
    {
        private string _name = string.Empty;
        private readonly EventDetailValues _values = new EventDetailValues();

        #region Constructors

		public GenericDetail()
		{
		}

        private GenericDetail(SerializationInfo info, StreamingContext streamingContext)
		{
            _name = info.GetString(Constants.Serialization.GenericDetail.Name);
            _values = (EventDetailValues)info.GetValue(Constants.Serialization.GenericDetail.Values, typeof(EventDetailValues));
		}

		#endregion

		#region Properties

		public EventDetailValues Values
		{
			get { return _values; }
		}

		#endregion

		#region System.Object Members

		public override bool Equals(object other)
		{
			if ( !(other is GenericDetail) )
				return false;
            GenericDetail otherDetails = (GenericDetail)other;

            if (_name != otherDetails._name)
                return false;

            if (_values.Count != otherDetails._values.Count)
                return false;

            for (int index = 0; index < _values.Count; ++index)
            {
                if (!Equals(_values[index], otherDetails._values[index]))
                    return false;
            }

            return true;
		}

		public override int GetHashCode()
		{
            int code = _name.GetHashCode();
            for (int index = 0; index < _values.Count; ++index)
                code ^= _values[index].GetHashCode();
            return code;
		}

		#endregion

		#region IEventDetail Members

		string IEventDetail.Name
		{
			get { return _name ?? typeof(GenericDetail).Name; }
		}

		void IEventDetail.Populate()
		{
            _name = string.Empty;
            _values.Clear();
		}

		#endregion

		#region ISerializable Members

		public void GetObjectData(SerializationInfo info, StreamingContext streamingContext)
		{
            info.AddValue(Constants.Serialization.GenericDetail.Name, _name);
			info.AddValue(Constants.Serialization.GenericDetail.Values, _values);
		}

		#endregion

		#region IXmlSerializable Members

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void WriteOuterXml(XmlWriter writer)
		{
			XmlWriteAdaptor adaptor = new XmlWriteAdaptor(writer, Constants.Xml.Namespace);
			adaptor.WriteStartElement(Constants.Xml.ThreadDetail.Name);
			WriteXml(adaptor);
			adaptor.WriteEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			XmlWriteAdaptor adaptor = new XmlWriteAdaptor(writer, Constants.Xml.Namespace);
			WriteXml(adaptor);
		}

		private void WriteXml(XmlWriteAdaptor adaptor)
		{
			adaptor.WriteNamespace(Constants.Xml.Prefix, Constants.Xml.Namespace);
            adaptor.WriteName(_name);
            foreach (var value in _values)
                adaptor.WriteElement(value.Name, (value.Value ?? string.Empty).ToString());
		}

		public void ReadOuterXml(XmlReader reader)
		{
			XmlReadAdaptor adaptor = new XmlReadAdaptor(reader, Constants.Xml.Namespace);
			if ( adaptor.IsReadingElement(Constants.Xml.ThreadDetail.Name) )
			{
				ReadXml(adaptor);
				adaptor.ReadEndElement();
			}
		}

		public void ReadXml(XmlReader reader)
		{
			XmlReadAdaptor adaptor = new XmlReadAdaptor(reader, Constants.Xml.Namespace);
			ReadXml(adaptor);
		}

		private void ReadXml(XmlReadAdaptor adaptor)
		{
            _values.Clear();

            _name = adaptor.ReadAttributeString(Constants.Xml.NameAttribute, string.Empty);
            if (string.IsNullOrEmpty(_name))
                _name = adaptor.ReadAttributeString(Constants.Xml.EventDetails.ClassAttribute, string.Empty);

            while (adaptor.ReadElement())
            {
                string name = adaptor.Name;
                string value = adaptor.GetValue();
                _values.Add(name, value);
                adaptor.ReadEndElement();
            }
		}

		#endregion

		#region IBinarySerializable Members

		void IBinarySerializable.Write(BinaryWriter writer)
		{
            writer.Write(_name);
            writer.Write(_values.Count);
            foreach (var value in _values)
            {
                writer.Write(value.Name);
                writer.Write((value.Value ?? string.Empty).ToString());
            }
		}

		void IBinarySerializable.Read(BinaryReader reader)
		{
            _name = reader.ReadString();
            _values.Clear();
			int count = reader.ReadInt32();
            for (int index = 0; index < count; ++index)
            {
                string name = reader.ReadString();
                string value = reader.ReadString();
                _values.Add(name, value);
            }
		}

		#endregion

		#region ICloneable Members

		object System.ICloneable.Clone()
		{
			return MemberwiseClone();
		}

		#endregion
    }
}
