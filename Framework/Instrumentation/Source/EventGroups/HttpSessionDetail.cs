using System.IO;
using System.Runtime.Serialization;
using LinkMe.Framework.Tools.Net;
using LinkMe.Framework.Type.Tools.Net;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Instrumentation.Message
{
    public class HttpSessionDetail
        : IBinarySerializable,
        ISerializable,
        System.ICloneable,
        IInternable
    {
        private string _id = string.Empty;

        internal HttpSessionDetail()
        {
        }

        protected HttpSessionDetail(SerializationInfo info, StreamingContext context)
        {
            BinarySerializer.ReadObjectDataForBinarySerializable(this, info);
        }

        #region Properties

        public string Id
        {
            get { return _id; }
            internal set { _id = value ?? string.Empty; }
        }

        #endregion

        #region IBinarySerializable Members

        public void Write(BinaryWriter writer)
        {
            writer.Write(_id);
        }

        public void Read(BinaryReader reader)
        {
            _id = reader.ReadString();
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

            interner.Intern(ref _id);
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (!(obj is HttpSessionDetail))
                return false;
            var other = (HttpSessionDetail)obj;
            return _id == other._id;
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        internal HttpSessionDetail Clone()
        {
            return new HttpSessionDetail
            {
                _id = _id,
            };
        }

        internal void WriteXml(XmlWriteAdaptor adaptor, string elementName)
        {
            adaptor.WriteStartElement(elementName);
            adaptor.WriteElement(Constants.Xml.HttpDetail.Session.IdElement, _id);
            adaptor.WriteEndElement();
        }

        internal void ReadXml(XmlReadAdaptor adaptor, string elementName)
        {
            if (adaptor.ReadElement(elementName))
            {
                _id = adaptor.ReadElementString(Constants.Xml.HttpDetail.Session.IdElement);
                adaptor.ReadEndElement();
            }
        }
    }

    internal class HttpSessionMemberWrapper : MemberWrapper
    {
        private readonly HttpSessionDetail _value;

        internal HttpSessionMemberWrapper(HttpSessionDetail value)
        {
            _value = value;
        }

        public override string Name
        {
            get { return "Session"; }
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
            return new HttpSessionGenericWrapper(_value);
        }
    }

    internal class HttpSessionGenericWrapper : GenericWrapper
    {
        internal HttpSessionGenericWrapper(HttpSessionDetail value)
            : base(value)
        {
        }

        public override MemberWrappers GetMembers()
        {
            var session = (HttpSessionDetail)GetWrappedObject();
            return new MemberWrappers
            {
                new ConstantMemberWrapper("Id", session.Id),
            };
        }
    }
}
