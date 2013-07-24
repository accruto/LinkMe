using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;

using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Net;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Utility.Event
{
    [Serializable]
    public class EventDetailValue
    {
        private readonly string _name;
        private readonly object _value;

        public EventDetailValue(string name, object value)
        {
            _name = name;
            _value = value;
        }

        public string Name
        {
            get { return _name; }
        }

        public object Value
        {
            get { return _value; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EventDetailValue))
                return false;
            var otherValue = (EventDetailValue)obj;
            return _name == otherValue._name && Equals(_value, otherValue._value);
        }

        public override int GetHashCode()
        {
            return (_name ?? string.Empty).GetHashCode() ^ (_value ?? string.Empty).GetHashCode();
        }
    }

    [Serializable]
    public class EventDetailValues
        : IEnumerable<EventDetailValue>
    {
        private readonly List<EventDetailValue> _values = new List<EventDetailValue>();

        public int Count
        {
            get { return _values.Count; }
        }

        public EventDetailValue this[int index]
        {
            get { return _values[index]; }
        }

        public void Add(string name, object value)
        {
            _values.Add(new EventDetailValue(name, value));
        }

        public void Clear()
        {
            _values.Clear();
        }

        IEnumerator<EventDetailValue> IEnumerable<EventDetailValue>.GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }
    }

	public interface IEventDetail
	{
		string Name { get; }
        EventDetailValues Values { get; }
		void Populate();
	}

	public interface IEventDetailFactory
	{
		string Name { get; }
		IEventDetail CreateInstance();
	}

	[Serializable]
	public class EventDetails
		:	ISerializable,
			IBinarySerializable,
			IXmlSerializable,
			IInternable,
            IEnumerable<IEventDetail>
	{
		public EventDetails()
		{
			_details = new List<IEventDetail>();
		}

		protected EventDetails(SerializationInfo info, StreamingContext context)
		{
            _details = (List<IEventDetail>)info.GetValue(Constants.Serialization.Details, typeof(List<IEventDetail>));
		}

        private EventDetails(List<IEventDetail> details)
		{
			_details = details;
		}

		public void Add(IEventDetail detail)
		{
			_details.Add(detail);
		}

		public int Count
		{
			get { return _details.Count; }
		}

		public IEventDetail this[int index]
		{
			get { return _details[index]; }
		}

		public EventDetails Clone()
		{
			return new EventDetails(new List<IEventDetail>(_details));
		}

		#region ISerializable Members

		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Constants.Serialization.Details, _details);
		}

		#endregion

		#region System.Object Members

	    IEnumerator<IEventDetail> IEnumerable<IEventDetail>.GetEnumerator()
	    {
            return _details.GetEnumerator();
	    }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _details.GetEnumerator();
        }

        public override bool Equals(object other)
		{
			if ( !(other is EventDetails) )
				return false;
			var otherDetails = (EventDetails) other;
			if ( _details.Count != otherDetails._details.Count )
				return false;
			for ( int index = 0; index < _details.Count; ++index )
			{
				if ( !_details[index].Equals(otherDetails._details[index]) )
					return false;
			}

			return true;
		}

		public override int GetHashCode()
		{
			return _details.GetHashCode();
		}

		#endregion

		#region IBinarySerializable

		void IBinarySerializable.Write(BinaryWriter writer)
		{
			writer.Write(_details.Count);
			foreach ( IEventDetail detail in _details )
			{
				writer.Write(detail.GetType().AssemblyQualifiedName);
				((IBinarySerializable) detail).Write(writer);
			}
		}

		void IBinarySerializable.Read(BinaryReader reader)
		{
            _details = new List<IEventDetail>();
			int count = reader.ReadInt32();
			for ( int index = 0; index < count; ++index )
			{
				var classInfo = new ClassInfo(reader.ReadString());
				var serializable = classInfo.CreateInstance<IBinarySerializable>();
				serializable.Read(reader);
				_details.Add(serializable as IEventDetail);
			}
		}

		#endregion

		#region IXmlSerializable Members

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void WriteOuterXml(XmlWriter writer)
		{
			var adaptor = new XmlWriteAdaptor(writer, Constants.Xml.Namespace);
			adaptor.WriteStartElement(Constants.Xml.EventDetails.RootElement);
			adaptor.WriteNamespace(Constants.Xml.Prefix, Constants.Xml.Namespace);
			adaptor.WriteNamespace(Constants.Xsi.Prefix, Constants.Xsi.Namespace);
			WriteXml(adaptor);
			adaptor.WriteEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			var adaptor = new XmlWriteAdaptor(writer, Constants.Xml.Namespace);
			adaptor.WriteNamespace(Constants.Xml.Prefix, Constants.Xml.Namespace);
			adaptor.WriteNamespace(Constants.Xsi.Prefix, Constants.Xsi.Namespace);
			WriteXml(adaptor);
		}

		private void WriteXml(XmlWriteAdaptor adaptor)
		{
			foreach (IEventDetail eventDetail in _details)
			{
				var serializable = eventDetail as IXmlSerializable;
				if (serializable != null)
				{
					adaptor.WriteStartElement(Constants.Xml.EventDetails.EventDetailElement);
					adaptor.WriteAttribute(Constants.Xml.ClassAttribute,
						eventDetail.GetType().AssemblyQualifiedName);
					serializable.WriteOuterXml(adaptor.XmlWriter);
					adaptor.WriteEndElement();
				}
				else
				{
					Debug.Fail("Event detail type '" + eventDetail.GetType().FullName + "' is not IXmlSerializable.");
				}
			}
		}

		public void ReadOuterXml(XmlReader reader)
		{
			var adaptor = new XmlReadAdaptor(reader, Constants.Xml.Namespace);
			if ( adaptor.IsReadingElement(Constants.Xml.EventDetails.RootElement) )
				ReadXml(adaptor);
		}

		public void ReadXml(XmlReader reader)
		{
			var adaptor = new XmlReadAdaptor(reader, Constants.Xml.Namespace);
			ReadXml(adaptor);
		}

		private void ReadXml(XmlReadAdaptor adaptor)
		{
			const string method = "ReadXml";

			while ( adaptor.ReadElement(Constants.Xml.EventDetails.EventDetailElement, true) )
			{
				string type = adaptor.ReadAttributeString(Constants.Xml.ClassAttribute, true);

                object instance = null;
				var classInfo = new ClassInfo(type);

                try
                {
                    instance = classInfo.CreateInstance();
                }
                catch (Exception)
                {
                }
                
                IEventDetail eventDetail;
                IXmlSerializable serializable;
                if (instance != null)
                {
                    eventDetail = instance as IEventDetail;
                    if (eventDetail == null)
                        throw new TypeDoesNotImplementInterfaceException(GetType(), method, instance.GetType(), typeof(IEventDetail));

                    serializable = instance as IXmlSerializable;
                    if (serializable == null)
                        throw new TypeDoesNotImplementInterfaceException(GetType(), method, instance.GetType(), typeof(IXmlSerializable));
                }
                else
                {
                    eventDetail = new GenericDetail();
                    serializable = eventDetail as IXmlSerializable;
                }

                if (adaptor.ReadElement())
                    serializable.ReadOuterXml(adaptor.XmlReader);
				Add(eventDetail);

				adaptor.ReadEndElement();
			}
		}

		#endregion

		#region IInternable Members

		public void Intern(Interner interner)
		{
			const string method = "Intern";

			if (interner == null)
				throw new NullParameterException(GetType(), method, "interner");

			foreach (IEventDetail detail in _details)
			{
				var internable = detail as IInternable;
				if (internable != null)
				{
					internable.Intern(interner);
				}
			}
		}

		#endregion

		private List<IEventDetail> _details;
	}

	public class EventDetailFactories
	{
		public EventDetails CreateDetails()
		{
			if ( _factories == null )
				return null;

			var eventDetails = new EventDetails();
			foreach ( IEventDetailFactory eventDetailFactory in _factories.Values )
			{
				IEventDetail eventDetail = eventDetailFactory.CreateInstance();
				eventDetail.Populate();
				eventDetails.Add(eventDetail);
			}

			return eventDetails;
		}

		public void Add(IEventDetailFactory factory)
		{
			if ( _factories == null )
				_factories = new SortedList<string, IEventDetailFactory>();
			_factories[factory.Name] = factory;
		}

		public IEventDetailFactory this[string name]
		{
			get
			{
				if ( _factories == null )
                    _factories = new SortedList<string, IEventDetailFactory>();
				return _factories[name];
			}
			set
			{
				if ( _factories == null )
                    _factories = new SortedList<string, IEventDetailFactory>();
				_factories[name] = value;
			}
		}

        public bool Contains(string name)
        {
            return _factories == null ? false : _factories.ContainsKey(name);
        }

		public IEnumerator GetEnumerator()
		{
			if ( _factories == null )
                _factories = new SortedList<string, IEventDetailFactory>();
			return _factories.Values.GetEnumerator();
		}

		private SortedList<string, IEventDetailFactory> _factories;
	}
}
