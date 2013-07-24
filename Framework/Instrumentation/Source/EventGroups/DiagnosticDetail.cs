using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Xml;
using LinkMe.Framework.Utility.Event;

namespace LinkMe.Framework.Instrumentation.Message
{
	[System.Serializable]
	public sealed class DiagnosticDetail
		:	IEventDetail,
		ISerializable,
		System.ICloneable,
		IXmlSerializable,
		IBinarySerializable,
        IInternable
	{
		#region Constructors

		public DiagnosticDetail()
		{
		}

		private DiagnosticDetail(SerializationInfo info, StreamingContext streamingContext)
		{
			_stackTrace = info.GetString("StackTrace");
		}

		#endregion

		#region Properties

		public string StackTrace
		{
			get { return _stackTrace; }
		}

		#endregion

		#region System.Object Members

		public override bool Equals(object other)
		{
			if ( !(other is DiagnosticDetail) )
				return false;
			var otherDetails = (DiagnosticDetail) other;
			return _stackTrace == otherDetails._stackTrace;
		}

		public override int GetHashCode()
		{
			return _stackTrace.GetHashCode();
		}

		#endregion

		#region IEventDetail Members

		string IEventDetail.Name
		{
			get { return typeof(DiagnosticDetail).Name; }
		}

        EventDetailValues IEventDetail.Values
        {
            get
            {
                var values = new EventDetailValues {{"StackTrace", _stackTrace}};
                return values;
            }
        }

        void IEventDetail.Populate()
		{
			_stackTrace = System.Environment.StackTrace;
		}

		#endregion

		#region ISerializable Members

		public void GetObjectData(SerializationInfo info, StreamingContext streamingContext)
		{
			info.AddValue("StackTrace", _stackTrace);
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
			adaptor.WriteStartElement(Constants.Xml.DiagnosticDetail.Name);
			adaptor.WriteNamespace(Constants.Xml.Namespace);
			WriteXml(adaptor);
			adaptor.WriteEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			var adaptor = new XmlWriteAdaptor(writer, Constants.Xml.Namespace);
			adaptor.WriteNamespace(Constants.Xml.Namespace);
			WriteXml(adaptor);
		}

		private void WriteXml(XmlWriteAdaptor adaptor)
		{
			adaptor.WriteElement(Constants.Xml.DiagnosticDetail.StackTraceElement, _stackTrace);
		}

		public void ReadOuterXml(XmlReader reader)
		{
			var adaptor = new XmlReadAdaptor(reader, Constants.Xml.Namespace);
            if (adaptor.IsReadingElement(Constants.Xml.DiagnosticDetail.Name))
            {
                ReadXml(adaptor);
                adaptor.ReadEndElement();
            }
		}

		public void ReadXml(XmlReader reader)
		{
			var adaptor = new XmlReadAdaptor(reader, Constants.Xml.Namespace);
			ReadXml(adaptor);
		}

		private void ReadXml(XmlReadAdaptor adaptor)
		{
			_stackTrace = adaptor.ReadElementString(Constants.Xml.DiagnosticDetail.StackTraceElement);
		}

		#endregion

		#region IBinarySerializable Members

		void IBinarySerializable.Write(BinaryWriter writer)
		{
			writer.Write(_stackTrace);
		}

		void IBinarySerializable.Read(BinaryReader reader)
		{
			_stackTrace = reader.ReadString();
		}

		#endregion

		#region ICloneable Members

		object System.ICloneable.Clone()
		{
			return MemberwiseClone();
		}

		#endregion

        #region IInternable Members

        public void Intern(Interner interner)
        {
            const string method = "Intern";

            if (interner == null)
                throw new NullParameterException(GetType(), method, "interner");

            interner.Intern(ref _stackTrace);
        }

        #endregion

		#region Member Variables

		private string _stackTrace = string.Empty;

		#endregion
	}

	public class MessageDiagnosticDetailFactory
		:	IEventDetailFactory
	{
		public string Name
		{
			get { return typeof(DiagnosticDetail).Name; }
		}

		public IEventDetail CreateInstance()
		{
			return new DiagnosticDetail();
		}
	}
}
