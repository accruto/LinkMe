using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using LinkMe.Framework.Tools.Net;
using LinkMe.Framework.Type;
using LinkMe.Framework.Type.Tools.Net;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Instrumentation.Message
{
    [System.Serializable]
    public class HttpRequestContentDetail
        : IBinarySerializable,
        ISerializable,
        System.ICloneable,
        IInternable
    {
        private string _encodingName = string.Empty;
        private int _length;
        private string _type = string.Empty;

        internal HttpRequestContentDetail()
        {
        }

        private HttpRequestContentDetail(SerializationInfo info, StreamingContext context)
        {
            BinarySerializer.ReadObjectDataForBinarySerializable(this, info);
        }

        #region Properties

        public string EncodingName
        {
            get { return _encodingName; }
            internal set { _encodingName = value ?? string.Empty; }
        }

        public int Length
        {
            get { return _length; }
            internal set { _length = value; }
        }

        public string Type
        {
            get { return _type; }
            internal set { _type = value ?? string.Empty; }
        }

        #endregion

        #region IBinarySerializable Members

        public void Write(BinaryWriter writer)
        {
            var adaptor = new BinaryWriteAdaptor(writer);
            adaptor.Write(_encodingName);
            adaptor.Write(_length);
            adaptor.Write(_type);
        }

        public void Read(BinaryReader reader)
        {
            var adaptor = new BinaryReadAdaptor(reader);
            _encodingName = adaptor.ReadString();
            _length = adaptor.ReadInt32();
            _type = adaptor.ReadString();
        }

        #endregion

        #region ISerializable Members

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            BinarySerializer.WriteObjectDataForBinarySerializable(this, info);
        }

        #endregion

        #region ICloneable Members

        object System.ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

        #region IInternable Members

        public void Intern(Interner interner)
        {
            const string method = "Intern";

            if (interner == null)
                throw new NullParameterException(GetType(), method, "interner");

            interner.Intern(ref _encodingName);
            interner.Intern(ref _type);
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (!(obj is HttpRequestContentDetail))
                return false;
            var other = (HttpRequestContentDetail)obj;
            return _encodingName == other._encodingName
                   && _length == other._length
                   && _type == other._type;
        }

        public override int GetHashCode()
        {
            return _encodingName.GetHashCode()
                ^ _length.GetHashCode()
                ^ _type.GetHashCode();
        }

        internal HttpRequestContentDetail Clone()
        {
            return new HttpRequestContentDetail
            {
                _encodingName = _encodingName,
                _length = _length,
                _type = _type
            };
        }

        internal void WriteXml(XmlWriteAdaptor adaptor, string elementName)
        {
            adaptor.WriteStartElement(elementName);
            adaptor.WriteElement(Constants.Xml.HttpDetail.Request.Content.EncodingNameElement, _encodingName);
            adaptor.WriteElement(Constants.Xml.HttpDetail.Request.Content.LengthElement, XmlConvert.ToString(_length));
            adaptor.WriteElement(Constants.Xml.HttpDetail.Request.Content.TypeElement, _type);
            adaptor.WriteEndElement();
        }

        internal void ReadXml(XmlReadAdaptor adaptor, string elementName)
        {
            if (adaptor.IsReadingElement(elementName))
            {
                _encodingName = adaptor.ReadElementString(Constants.Xml.HttpDetail.Request.Content.EncodingNameElement, string.Empty);
                _length = XmlConvert.ToInt32(adaptor.ReadElementString(Constants.Xml.HttpDetail.Request.Content.LengthElement, "0"));
                _type = adaptor.ReadElementString(Constants.Xml.HttpDetail.Request.Content.TypeElement, string.Empty);
                adaptor.ReadEndElement();
            }
        }
    }

    internal class HttpRequestContentMemberWrapper
        : MemberWrapper
    {
        private readonly HttpRequestContentDetail _value;

        internal HttpRequestContentMemberWrapper(HttpRequestContentDetail value)
        {
            _value = value;
        }

        public override string Name
        {
            get { return "Content"; }
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override System.Type GetMemberType()
        {
            return null;
        }

        protected override object GetValueImpl()
        {
            return _value;
        }

        protected override void SetValueImpl(object value)
        {
            throw new System.NotSupportedException();
        }

        public override bool MayHaveChildren
        {
            get { return true; }
        }

        public override GenericWrapper CreateWrapper()
        {
            return new HttpRequestContentGenericWrapper(_value);
        }
    }

    internal class HttpRequestContentGenericWrapper : GenericWrapper
    {
        internal HttpRequestContentGenericWrapper(HttpRequestContentDetail value)
            : base(value)
        {
        }

        public override MemberWrappers GetMembers()
        {
            var content = (HttpRequestContentDetail)GetWrappedObject();
            return new MemberWrappers
            {
                new ConstantMemberWrapper("EncodingName", content.EncodingName),
                new ConstantMemberWrapper("Length", content.Length),
                new ConstantMemberWrapper("Type", content.Type)
            };
        }
    }

    [System.Serializable]
    public class HttpRequestUserDetail
        : IBinarySerializable,
        ISerializable,
        System.ICloneable,
        IInternable
    {
        private string _hostName = string.Empty;
        private string _hostAddress = string.Empty;
        private string _agent = string.Empty;

        internal HttpRequestUserDetail()
        {
        }

        private HttpRequestUserDetail(SerializationInfo info, StreamingContext context)
        {
            BinarySerializer.ReadObjectDataForBinarySerializable(this, info);
        }

        #region Properties

        public string HostName
        {
            get { return _hostName; }
            internal set { _hostName = value ?? string.Empty; }
        }

        public string HostAddress
        {
            get { return _hostAddress; }
            internal set { _hostAddress = value ?? string.Empty; }
        }

        public string Agent
        {
            get { return _agent; }
            internal set { _agent = value ?? string.Empty; }
        }

        #endregion

        #region IBinarySerializable Members

        public void Write(BinaryWriter writer)
        {
            writer.Write(_hostName);
            writer.Write(_hostAddress);
            writer.Write(_agent);
        }

        public void Read(BinaryReader reader)
        {
            _hostName = reader.ReadString();
            _hostAddress = reader.ReadString();
            _agent = reader.ReadString();
        }

        #endregion

        #region ISerializable Members

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            BinarySerializer.WriteObjectDataForBinarySerializable(this, info);
        }

        #endregion

        #region ICloneable Members

        object System.ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

        #region IInternable Members

        public void Intern(Interner interner)
        {
            const string method = "Intern";

            if (interner == null)
                throw new NullParameterException(GetType(), method, "interner");

            interner.Intern(ref _hostName);
            interner.Intern(ref _hostAddress);
            interner.Intern(ref _agent);
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (!(obj is HttpRequestUserDetail))
                return false;
            var other = (HttpRequestUserDetail)obj;
            return _hostName == other._hostName
                   && _hostAddress == other._hostAddress
                   && _agent == other._agent;
        }

        public override int GetHashCode()
        {
            return _hostName.GetHashCode()
                ^ _hostAddress.GetHashCode()
                ^ _agent.GetHashCode();
        }

        internal HttpRequestUserDetail Clone()
        {
            return new HttpRequestUserDetail
            {
                _hostName = _hostName,
                _hostAddress = _hostAddress,
                _agent = _agent
            };
        }

        internal void WriteXml(XmlWriteAdaptor adaptor, string elementName)
        {
            adaptor.WriteStartElement(elementName);
            adaptor.WriteElement(Constants.Xml.HttpDetail.Request.User.HostNameElement, _hostName);
            adaptor.WriteElement(Constants.Xml.HttpDetail.Request.User.HostAddressElement, _hostAddress);
            adaptor.WriteElement(Constants.Xml.HttpDetail.Request.User.AgentElement, _agent);
            adaptor.WriteEndElement();
        }

        internal void ReadXml(XmlReadAdaptor adaptor, string elementName)
        {
            if (adaptor.IsReadingElement(elementName))
            {
                _hostName = adaptor.ReadElementString(Constants.Xml.HttpDetail.Request.User.HostNameElement);
                _hostAddress = adaptor.ReadElementString(Constants.Xml.HttpDetail.Request.User.HostAddressElement);
                _agent = adaptor.ReadElementString(Constants.Xml.HttpDetail.Request.User.AgentElement);
                adaptor.ReadEndElement();
            }
        }
    }

    internal class HttpRequestUserMemberWrapper
        : MemberWrapper
    {
        private readonly HttpRequestUserDetail _value;

        internal HttpRequestUserMemberWrapper(HttpRequestUserDetail value)
        {
            _value = value;
        }

        public override string Name
        {
            get { return "User"; }
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override System.Type GetMemberType()
        {
            return null;
        }

        protected override object GetValueImpl()
        {
            return _value;
        }

        protected override void SetValueImpl(object value)
        {
            throw new System.NotSupportedException();
        }

        public override bool MayHaveChildren
        {
            get { return true; }
        }

        public override GenericWrapper CreateWrapper()
        {
            return new HttpRequestUserGenericWrapper(_value);
        }
    }

    internal class HttpRequestUserGenericWrapper : GenericWrapper
    {
        internal HttpRequestUserGenericWrapper(HttpRequestUserDetail value)
            : base(value)
        {
        }

        public override MemberWrappers GetMembers()
        {
            var user = (HttpRequestUserDetail)GetWrappedObject();
            return new MemberWrappers
            {
                new ConstantMemberWrapper("HostName", user.HostName),
                new ConstantMemberWrapper("HostAddress", user.HostAddress),
                new ConstantMemberWrapper("Agent", user.Agent)
            };
        }
    }

    public class HttpRequestDetail
        : IBinarySerializable,
        ISerializable,
        System.ICloneable,
        IInternable
    {
        private string _httpMethod = string.Empty;
        private string _url = string.Empty;
        private string _rawUrl = string.Empty;
        private string _urlReferrer = string.Empty;
        private bool _isAuthenticated;
        private bool _isLocal;
        private bool _isSecureConnection;
        private HttpHeadersDetail _headers = new HttpHeadersDetail();
        private HttpCookiesDetail _cookies = new HttpCookiesDetail();
        private HttpFormDetail _form = new HttpFormDetail();
        private HttpRequestContentDetail _content = new HttpRequestContentDetail();
        private HttpRequestUserDetail _user = new HttpRequestUserDetail();

        internal HttpRequestDetail()
        {
        }

        protected HttpRequestDetail(SerializationInfo info, StreamingContext context)
        {
            BinarySerializer.ReadObjectDataForBinarySerializable(this, info);
        }

        #region Properties

        public string HttpMethod
        {
            get { return _httpMethod; }
            internal set { _httpMethod = value ?? string.Empty; }
        }

        public string Url
        {
            get { return _url; }
            internal set { _url = value ?? string.Empty; }
        }

        public string RawUrl
        {
            get { return _rawUrl; }
            internal set { _rawUrl = value ?? string.Empty; }
        }

        public string UrlReferrer
        {
            get { return _urlReferrer; }
            internal set { _urlReferrer = value ?? string.Empty; }
        }

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
            internal set { _isAuthenticated = value; }
        }

        public bool IsLocal
        {
            get { return _isLocal; }
            internal set { _isLocal = value; }
        }

        public bool IsSecureConnection
        {
            get { return _isSecureConnection; }
            internal set { _isSecureConnection = value; }
        }

        public HttpHeadersDetail Headers
        {
            get { return _headers; }
        }

        public HttpCookiesDetail Cookies
        {
            get { return _cookies; }
        }

        public HttpFormDetail Form
        {
            get { return _form; }
        }

        public HttpRequestContentDetail Content
        {
            get { return _content; }
        }

        public HttpRequestUserDetail User
        {
            get { return _user; }
        }

        #endregion

        #region IBinarySerializable Members

        public void Write(BinaryWriter writer)
        {
            writer.Write(_httpMethod);
            writer.Write(_url);
            writer.Write(_rawUrl);
            writer.Write(_urlReferrer);
            writer.Write(_isAuthenticated);
            writer.Write(_isLocal);
            writer.Write(_isSecureConnection);
            _headers.Write(writer);
            _cookies.Write(writer);
            _form.Write(writer);
            _content.Write(writer);
            _user.Write(writer);
        }

        public void Read(BinaryReader reader)
        {
            _httpMethod = reader.ReadString();
            _url = reader.ReadString();
            _rawUrl = reader.ReadString();
            _urlReferrer = reader.ReadString();
            _isAuthenticated = reader.ReadBoolean();
            _isLocal = reader.ReadBoolean();
            _isSecureConnection = reader.ReadBoolean();
            _headers.Read(reader);
            _cookies.Read(reader);
            _form.Read(reader);
            _content.Read(reader);
            _user.Read(reader);
        }

        #endregion

        #region ISerializable Members

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            BinarySerializer.WriteObjectDataForBinarySerializable(this, info);
        }

        #endregion

        #region ICloneable Members

        object System.ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

        #region IInternable Members

        public void Intern(Interner interner)
        {
            const string method = "Intern";

            if (interner == null)
                throw new NullParameterException(GetType(), method, "interner");

            interner.Intern(ref _httpMethod);
            interner.Intern(ref _url);
            interner.Intern(ref _rawUrl);
            interner.Intern(ref _urlReferrer);
            _headers.Intern(interner);
            _cookies.Intern(interner);
            _form.Intern(interner);
            _content.Intern(interner);
            _user.Intern(interner);
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (!(obj is HttpRequestDetail))
                return false;
            var other = (HttpRequestDetail)obj;
            return _httpMethod == other._httpMethod
                && _url == other._url
                && _rawUrl == other._rawUrl
                && _urlReferrer == other._urlReferrer
                && _isAuthenticated == other._isAuthenticated
                && _isLocal == other._isLocal
                && _isSecureConnection == other._isSecureConnection
                && _headers.Equals(other._headers)
                && _cookies.Equals(other._cookies)
                && _form.Equals(other._form)
                && _content.Equals(other._content)
                && _user.Equals(other._user);
        }

        public override int GetHashCode()
        {
            return _httpMethod.GetHashCode()
                ^ _url.GetHashCode()
                ^ _rawUrl.GetHashCode()
                ^ _urlReferrer.GetHashCode()
                ^ _isAuthenticated.GetHashCode()
                ^ _isLocal.GetHashCode()
                ^ _isSecureConnection.GetHashCode()
                ^ _headers.GetHashCode()
                ^ _cookies.GetHashCode()
                ^ _form.GetHashCode()
                ^ _content.GetHashCode()
                ^ _user.GetHashCode();
        }

        internal HttpRequestDetail Clone()
        {
            return new HttpRequestDetail
            {
                _httpMethod = _httpMethod,
                _url = _url,
                _rawUrl = _rawUrl,
                _urlReferrer = _urlReferrer,
                _isAuthenticated = _isAuthenticated,
                _isLocal = _isLocal,
                _isSecureConnection = _isSecureConnection,
                _headers = _headers.Clone(),
                _cookies = _cookies.Clone(),
                _form = _form.Clone(),
                _content = _content.Clone(),
                _user = _user.Clone()
            };
        }

        internal void WriteXml(XmlWriteAdaptor adaptor, string elementName)
        {
            adaptor.WriteStartElement(elementName);
            adaptor.WriteElement(Constants.Xml.HttpDetail.HttpMethodElement, _httpMethod);
            adaptor.WriteElement(Constants.Xml.HttpDetail.UrlElement, _url);
            adaptor.WriteElement(Constants.Xml.HttpDetail.RawUrlElement, _rawUrl);
            adaptor.WriteElement(Constants.Xml.HttpDetail.UrlReferrerElement, _urlReferrer);
            adaptor.WriteElement(Constants.Xml.HttpDetail.IsAuthenticatedElement, XmlConvert.ToString(_isAuthenticated));
            adaptor.WriteElement(Constants.Xml.HttpDetail.IsLocalElement, XmlConvert.ToString(_isLocal));
            adaptor.WriteElement(Constants.Xml.HttpDetail.IsSecureConnectionElement, XmlConvert.ToString(_isSecureConnection));
            _headers.WriteXml(adaptor, Constants.Xml.HttpDetail.Headers.Name);
            _cookies.WriteXml(adaptor, Constants.Xml.HttpDetail.Cookies.Name);
            _form.WriteXml(adaptor, Constants.Xml.HttpDetail.Form.Name);
            _content.WriteXml(adaptor, Constants.Xml.HttpDetail.Request.Content.Name);
            _user.WriteXml(adaptor, Constants.Xml.HttpDetail.Request.User.Name);
            adaptor.WriteEndElement();
        }

        internal void ReadXml(XmlReadAdaptor adaptor, string elementName)
        {
            if (adaptor.ReadElement(elementName))
            {
                _httpMethod = adaptor.ReadElementString(Constants.Xml.HttpDetail.HttpMethodElement);
                _url = adaptor.ReadElementString(Constants.Xml.HttpDetail.UrlElement);
                _rawUrl = adaptor.ReadElementString(Constants.Xml.HttpDetail.RawUrlElement);
                _urlReferrer = adaptor.ReadElementString(Constants.Xml.HttpDetail.UrlReferrerElement);
                _isAuthenticated = XmlConvert.ToBoolean(adaptor.ReadElementString(Constants.Xml.HttpDetail.IsAuthenticatedElement));
                _isLocal = XmlConvert.ToBoolean(adaptor.ReadElementString(Constants.Xml.HttpDetail.IsLocalElement));
                _isSecureConnection = XmlConvert.ToBoolean(adaptor.ReadElementString(Constants.Xml.HttpDetail.IsSecureConnectionElement));
                _headers.ReadXml(adaptor, Constants.Xml.HttpDetail.Headers.Name);
                _cookies.ReadXml(adaptor, Constants.Xml.HttpDetail.Cookies.Name);
                _form.ReadXml(adaptor, Constants.Xml.HttpDetail.Form.Name);
                _content.ReadXml(adaptor, Constants.Xml.HttpDetail.Request.Content.Name);
                _user.ReadXml(adaptor, Constants.Xml.HttpDetail.Request.User.Name);
                adaptor.ReadEndElement();
            }
        }
    }

    internal class HttpRequestMemberWrapper : MemberWrapper
    {
        private readonly HttpRequestDetail _value;

        internal HttpRequestMemberWrapper(HttpRequestDetail value)
        {
            _value = value;
        }

        public override string Name
        {
            get { return "Request"; }
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override System.Type GetMemberType()
        {
            return null;
        }

        protected override object GetValueImpl()
        {
            return _value;
        }

        protected override void SetValueImpl(object value)
        {
            throw new System.NotSupportedException();
        }

        public override bool MayHaveChildren
        {
            get { return true; }
        }

        public override GenericWrapper CreateWrapper()
        {
            return new HttpRequestGenericWrapper(_value);
        }
    }

    internal class HttpRequestGenericWrapper : GenericWrapper
    {
        internal HttpRequestGenericWrapper(HttpRequestDetail value)
            : base(value)
        {
        }

        public override MemberWrappers GetMembers()
        {
            var request = (HttpRequestDetail)GetWrappedObject();
            return new MemberWrappers
            {
                new ConstantMemberWrapper("HttpMethod", request.HttpMethod),
                new ConstantMemberWrapper("Url", request.Url),
                new ConstantMemberWrapper("RawUrl", request.RawUrl),
                new ConstantMemberWrapper("UrlReferrer", request.UrlReferrer),
                new ConstantMemberWrapper("IsAuthenticated", request.IsAuthenticated),
                new ConstantMemberWrapper("IsLocal", request.IsLocal),
                new ConstantMemberWrapper("IsSecureConnection", request.IsSecureConnection),
                new HttpHeadersMemberWrapper(request.Headers),
                new HttpCookiesMemberWrapper(request.Cookies),
                new HttpFormMemberWrapper(request.Form),
                new HttpRequestContentMemberWrapper(request.Content),
                new HttpRequestUserMemberWrapper(request.User)
            };
        }
    }
}
