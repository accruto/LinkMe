using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Web;
using LinkMe.Framework.Tools.Net;
using LinkMe.Framework.Type;
using LinkMe.Framework.Type.Tools.Net;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Xml;
using DateTime=LinkMe.Framework.Type.DateTime;
using TimeZone=LinkMe.Framework.Type.TimeZone;

namespace LinkMe.Framework.Instrumentation.Message
{
    [System.Serializable]
    public class HttpCookieDetail
        : IBinarySerializable,
        ISerializable,
        System.ICloneable,
        IInternable
    {
        private string _domain;
        private System.DateTime _expires;
        private bool _httpOnly;
        private string _name;
        private string _path;
        private bool _secure;
        private string _value;

        internal HttpCookieDetail()
        {
        }

        private HttpCookieDetail(SerializationInfo info, StreamingContext context)
        {
            BinarySerializer.ReadObjectDataForBinarySerializable(this, info);
        }

        public string Domain
        {
            get { return _domain; }
        }

        public System.DateTime Expires
        {
            get { return _expires; }
        }

        public bool HttpOnly
        {
            get { return _httpOnly; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Path
        {
            get { return _path; }
        }

        public bool Secure
        {
            get { return _secure; }
        }

        public string Value
        {
            get { return _value; }
        }

        #region IBinarySerializable Members

        public void Write(BinaryWriter writer)
        {
            var adaptor = new BinaryWriteAdaptor(writer);
            adaptor.WriteNullable(_domain);
            adaptor.Write(DateTime.FromSystemDateTime(_expires, TimeZone.UTC));
            adaptor.Write(_httpOnly);
            adaptor.WriteNullable(_name);
            adaptor.WriteNullable(_path);
            adaptor.Write(_secure);
            adaptor.WriteNullable(_value);
        }

        public void Read(BinaryReader reader)
        {
            var adaptor = new BinaryReadAdaptor(reader);
            _domain = adaptor.ReadStringNullable();
            _expires = adaptor.ReadDateTime().ToSystemDateTime();
            _httpOnly = adaptor.ReadBoolean();
            _name = adaptor.ReadStringNullable();
            _path = adaptor.ReadStringNullable();
            _secure = adaptor.ReadBoolean();
            _value = adaptor.ReadStringNullable();
        }

        #endregion

        #region ISerializable Members

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            BinarySerializer.WriteObjectDataForBinarySerializable(this, info);
        }

        #endregion

        #region ICloneable Members

        object ICloneable.Clone()
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

            _domain = interner.Intern(_domain);
            _name = interner.Intern(_name);
            _path = interner.Intern(_path);
        }

        #endregion

        public override bool Equals(object obj)
        {
            var other = obj as HttpCookieDetail;
            if (other == null)
                return false;

            return _domain == other._domain
                   && _expires == other._expires
                   && _httpOnly == other._httpOnly
                   && _name == other._name
                   && _path == other._path
                   && _secure == other._secure
                   && _value == other._value;
        }

        public override int GetHashCode()
        {
            return (_domain ?? string.Empty).GetHashCode()
                   ^ _expires.GetHashCode()
                   ^ _httpOnly.GetHashCode()
                   ^ (_name ?? string.Empty).GetHashCode()
                   ^ (_path ?? string.Empty).GetHashCode()
                   ^ _secure.GetHashCode()
                   ^ (_value ?? string.Empty).GetHashCode();
        }

        internal HttpCookieDetail Clone()
        {
            return new HttpCookieDetail
            {
                _domain = _domain,
                _expires = _expires,
                _httpOnly = _httpOnly,
                _name = _name,
                _path = _path,
                _secure = _secure,
                _value = _value
            };
        }

        internal void WriteXml(XmlWriteAdaptor adaptor, string elementName)
        {
            adaptor.WriteStartElement(elementName);

            adaptor.WriteEndElement();
        }

        internal void ReadXml(XmlReadAdaptor adaptor, string elementName)
        {
            if (adaptor.ReadElement(elementName))
            {
                adaptor.ReadEndElement();
            }
        }

        public void Set(HttpCookie cookie)
        {
            _domain = cookie.Domain;
            _expires = cookie.Expires;
            _httpOnly = cookie.HttpOnly;
            _name = cookie.Name;
            _path = cookie.Path;
            _secure = cookie.Secure;
            _value = cookie.Value;
        }
    }

    [Serializable]
    public class HttpCookiesDetail
        : IBinarySerializable,
        ISerializable,
        ICloneable,
        IInternable
    {
        private List<HttpCookieDetail> _cookies = new List<HttpCookieDetail>();

        internal HttpCookiesDetail()
        {
        }

        private HttpCookiesDetail(SerializationInfo info, StreamingContext context)
        {
            BinarySerializer.ReadObjectDataForBinarySerializable(this, info);
        }

        public int Count
        {
            get { return _cookies.Count; }
        }

        #region IBinarySerializable Members

        public void Write(BinaryWriter writer)
        {
            var adaptor = new BinaryWriteAdaptor(writer);

            adaptor.Write(_cookies.Count);
            for (var index = 0; index < _cookies.Count; ++index)
                _cookies[index].Write(writer);
        }

        public void Read(BinaryReader reader)
        {
            _cookies.Clear();
            var adaptor = new BinaryReadAdaptor(reader);
            var count = adaptor.ReadInt32();

            for (var index = 0; index < count; index++)
            {
                var cookie = new HttpCookieDetail();
                cookie.Read(reader);
                _cookies.Add(cookie);
            }
        }

        #endregion

        #region ISerializable Members

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            BinarySerializer.WriteObjectDataForBinarySerializable(this, info);
        }

        #endregion

        #region ICloneable Members

        object ICloneable.Clone()
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

            // Build a new collection using interned keys and values and then replace the underlying collection
            // with the new one - should be thread-safe without locking.

            if (_cookies.Count > 0)
            {
                var newCookies = new List<HttpCookieDetail>();
                foreach (var cookie in _cookies)
                {
                    var newCookie = cookie.Clone();
                    newCookie.Intern(interner);
                    newCookies.Add(newCookie);
                }

                _cookies = newCookies;
            }
        }

        #endregion

        public override bool Equals(object obj)
        {
            var other = obj as HttpCookiesDetail;
            if (other == null || _cookies.Count != other._cookies.Count)
                return false;

            for (var index = 0; index < _cookies.Count; ++index)
            {
                var cookie = _cookies[index];
                var otherCookie = other._cookies[index];
                if (!cookie.Equals(otherCookie))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return _cookies.Count.GetHashCode();
        }

        internal HttpCookiesDetail Clone()
        {
            var cookies = new HttpCookiesDetail {_cookies = new List<HttpCookieDetail>()};
            foreach (var cookie in _cookies)
                cookies._cookies.Add(cookie.Clone());
            return cookies;
        }

        internal void WriteXml(XmlWriteAdaptor adaptor, string elementName)
        {
            if (_cookies.Count == 0)
                return; // Don't write an empty element.

            adaptor.WriteStartElement(elementName);

            for (var index = 0; index < _cookies.Count; ++index)
                _cookies[index].WriteXml(adaptor, Constants.Xml.HttpDetail.Cookies.CookieElement);

            adaptor.WriteEndElement();
        }

        internal void ReadXml(XmlReadAdaptor adaptor, string elementName)
        {
            _cookies.Clear();

            if (adaptor.ReadElement(elementName))
            {
                while (adaptor.IsReadingElement(Constants.Xml.HttpDetail.Cookies.CookieElement))
                {
                    var cookie = new HttpCookieDetail();
                    cookie.ReadXml(adaptor, Constants.Xml.HttpDetail.Cookies.CookieElement);
                    _cookies.Add(cookie);
                }

                adaptor.ReadEndElement();
            }
        }

        public void Set(HttpCookieCollection cookies)
        {
            _cookies = new List<HttpCookieDetail>();
            for (var index = 0; index < cookies.Count; ++index)
            {
                var cookie = new HttpCookieDetail();
                cookie.Set(cookies[index]);
                _cookies.Add(cookie);
            }
        }

        public HttpCookieDetail this[int index]
        {
            get { return _cookies[index]; }
        }
    }

    internal class HttpCookiesMemberWrapper
        : MemberWrapper
    {
        private readonly HttpCookiesDetail _value;

        internal HttpCookiesMemberWrapper(HttpCookiesDetail value)
        {
            _value = value;
        }

        public override string Name
        {
            get { return "Cookies"; }
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
            throw new NotSupportedException();
        }

        public override bool MayHaveChildren
        {
            get { return true; }
        }

        public override GenericWrapper CreateWrapper()
        {
            return new HttpCookiesGenericWrapper(_value);
        }
    }

    internal class HttpCookiesGenericWrapper : GenericWrapper
    {
        internal HttpCookiesGenericWrapper(HttpCookiesDetail value)
            : base(value)
        {
        }

        public override MemberWrappers GetMembers()
        {
            var cookies = (HttpCookiesDetail)GetWrappedObject();
            var members = new MemberWrappers();

            for (int index = 0; index < cookies.Count; ++index)
                members.Add(new HttpCookieMemberWrapper(cookies[index]));

            return members;
        }
    }

    internal class HttpCookieMemberWrapper
    : MemberWrapper
    {
        private readonly HttpCookieDetail _value;

        internal HttpCookieMemberWrapper(HttpCookieDetail value)
        {
            _value = value;
        }

        public override string Name
        {
            get { return _value.Name; }
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
            return new HttpCookieGenericWrapper(_value);
        }
    }

    internal class HttpCookieGenericWrapper : GenericWrapper
    {
        internal HttpCookieGenericWrapper(HttpCookieDetail value)
            : base(value)
        {
        }

        public override MemberWrappers GetMembers()
        {
            var cookie = (HttpCookieDetail)GetWrappedObject();
            return new MemberWrappers
            {
                new ConstantMemberWrapper("Domain", cookie.Domain),
                new ConstantMemberWrapper("Expires", cookie.Expires),
                new ConstantMemberWrapper("HttpOnly", cookie.HttpOnly),
                new ConstantMemberWrapper("Path", cookie.Path),
                new ConstantMemberWrapper("Secure", cookie.Secure),
                new ConstantMemberWrapper("Value", cookie.Value),
            };
        }
    }
}
