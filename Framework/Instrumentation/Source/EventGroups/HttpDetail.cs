using System.Runtime.Serialization;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using LinkMe.Framework.Tools.Net;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Xml;
using LinkMe.Framework.Utility.Event;

namespace LinkMe.Framework.Instrumentation.Message
{
    [System.Serializable]
    public sealed class HttpDetail
        : IEventDetail,
        ISerializable,
        System.ICloneable,
        IXmlSerializable,
        IBinarySerializable,
        IInternable,
        ICustomWrapper
    {
        #region Constructors

        public HttpDetail()
        {
        }

        private HttpDetail(SerializationInfo info, StreamingContext streamingContext)
        {
            _request = ((HttpRequestDetail)info.GetValue("Request", typeof(HttpRequestDetail))) ?? new HttpRequestDetail();
            _session = ((HttpSessionDetail)info.GetValue("Session", typeof(HttpSessionDetail))) ?? new HttpSessionDetail();
        }

        #endregion

        #region Properties

        public HttpRequestDetail Request
        {
            get { return _request; }
        }

        public HttpSessionDetail Session
        {
            get { return _session; }
        }

        #endregion

        #region System.Object Members

        public override bool Equals(object other)
        {
            if (!(other is HttpDetail))
                return false;
            var otherDetails = (HttpDetail)other;
            return _request.Equals(otherDetails._request)
                && _session.Equals(otherDetails._session);
        }

        public override int GetHashCode()
        {
            return _request.GetHashCode()
                ^ _session.GetHashCode();
        }

        #endregion

        #region IEventDetail Members

        string IEventDetail.Name
        {
            get { return typeof(HttpDetail).Name; }
        }

        EventDetailValues IEventDetail.Values
        {
            get
            {
                var values = new EventDetailValues();

                // Need to fill in.

                return values;
            }
        }

        void IEventDetail.Populate()
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                var request = context.Request;

                _request.Content.EncodingName = request.ContentEncoding.EncodingName;
                _request.Content.Length = request.ContentLength;
                _request.Content.Type = request.ContentType;

                _request.User.HostName = request.UserHostName;
                _request.User.HostAddress = request.UserHostAddress;
                _request.User.Agent = request.UserAgent;

                _request.HttpMethod = request.HttpMethod;
                _request.Url = request.Url.AbsoluteUri;
                _request.RawUrl = request.RawUrl;
                _request.UrlReferrer = request.UrlReferrer == null ? string.Empty : request.UrlReferrer.AbsoluteUri;

                _request.IsAuthenticated = request.IsAuthenticated;
                _request.IsLocal = request.IsLocal;
                _request.IsSecureConnection = request.IsSecureConnection;

                _request.Headers.Set(request.Headers);
                _request.Cookies.Set(request.Cookies);
                _request.Form.Set(request.Form);

                var session = context.Session;
                if (session != null)
                    _session.Id = session.SessionID;
            }
        }

        #endregion

        #region ICustomWrapper Members

        bool ICustomWrapper.MayHaveChildren
        {
            get { return true; }
        }

        GenericWrapper ICustomWrapper.CreateWrapper()
        {
            return new HttpDetailGenericWrapper(this);
        }

        #endregion

        #region ISerializable Members

        public void GetObjectData(SerializationInfo info, StreamingContext streamingContext)
        {
            info.AddValue("Request", _request);
            info.AddValue("Session", _session);
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
            adaptor.WriteStartElement(Constants.Xml.HttpDetail.Name);
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
            _request.WriteXml(adaptor, Constants.Xml.HttpDetail.Request.Name);
            _session.WriteXml(adaptor, Constants.Xml.HttpDetail.Session.Name);
        }

        public void ReadOuterXml(XmlReader reader)
        {
            var adaptor = new XmlReadAdaptor(reader, Constants.Xml.Namespace);
            if (adaptor.IsReadingElement(Constants.Xml.HttpDetail.Name))
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
            _request.ReadXml(adaptor, Constants.Xml.HttpDetail.Request.Name);
            _session.ReadXml(adaptor, Constants.Xml.HttpDetail.Session.Name);
        }

        #endregion

        #region IBinarySerializable Members

        void IBinarySerializable.Write(BinaryWriter writer)
        {
            _request.Write(writer);
            _session.Write(writer);
        }

        void IBinarySerializable.Read(BinaryReader reader)
        {
            _request.Read(reader);
            _session.Read(reader);
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

            _request.Intern(interner);
            _session.Intern(interner);
        }

        #endregion

        #region Member Variables

        private readonly HttpRequestDetail _request = new HttpRequestDetail();
        private readonly HttpSessionDetail _session = new HttpSessionDetail();

        #endregion
    }

    internal class HttpDetailGenericWrapper : GenericWrapper
    {
        internal HttpDetailGenericWrapper(HttpDetail value)
            : base(value)
        {
        }

        public override MemberWrappers GetMembers()
        {
            var detail = (HttpDetail)GetWrappedObject();
            return new MemberWrappers
            {
                new HttpRequestMemberWrapper(detail.Request),
                new HttpSessionMemberWrapper(detail.Session)
            };
        }
    }

    public class MessageHttpDetailFactory
        : IEventDetailFactory
    {
        public string Name
        {
            get { return typeof(HttpDetail).Name; }
        }

        public IEventDetail CreateInstance()
        {
            return new HttpDetail();
        }
    }
}
