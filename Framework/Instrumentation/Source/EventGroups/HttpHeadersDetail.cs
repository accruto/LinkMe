using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using LinkMe.Framework.Tools.Net;
using LinkMe.Framework.Type;
using LinkMe.Framework.Type.Tools.Net;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Instrumentation.Message
{
    [System.Serializable]
    public class HttpHeadersDetail
        : IBinarySerializable,
        ISerializable,
        System.ICloneable,
        IInternable
    {
        private NameValueCollection m_headers = new NameValueCollection();

        internal HttpHeadersDetail()
        {
        }

        private HttpHeadersDetail(SerializationInfo info, StreamingContext context)
        {
            BinarySerializer.ReadObjectDataForBinarySerializable(this, info);
        }

        public int Count
        {
            get { return m_headers.Count; }
        }

        #region IBinarySerializable Members

        public void Write(BinaryWriter writer)
        {
            BinaryWriteAdaptor adaptor = new BinaryWriteAdaptor(writer);

            adaptor.Write(m_headers.Count);
            for (int index = 0; index < m_headers.Count; ++index)
            {
                adaptor.Write(m_headers.GetKey(index));
                string[] values = m_headers.GetValues(index);
                if (values == null)
                {
                    adaptor.Write(0);
                }
                else
                {
                    adaptor.Write(values.Length);
                    for (int valueIndex = 0; valueIndex < values.Length; ++valueIndex)
                        adaptor.Write(values[valueIndex]);
                }
            }
        }

        public void Read(BinaryReader reader)
        {
            m_headers.Clear();
            BinaryReadAdaptor adaptor = new BinaryReadAdaptor(reader);
            int count = adaptor.ReadInt32();

            for (int index = 0; index < count; index++)
            {
                string key = adaptor.ReadString();
                int valueCount = adaptor.ReadInt32();
                if (valueCount == 0)
                {
                    m_headers.Add(key, null);
                }
                else
                {
                    for (int valueIndex = 0; valueIndex < valueCount; ++valueIndex)
                        m_headers.Add(key, adaptor.ReadString());
                }
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

            // Build a new collection using interned keys and values and then replace the underlying collection
            // with the new one - should be thread-safe without locking.

            if (m_headers.Count > 0)
            {
                NameValueCollection newHeaders = new NameValueCollection();

                for (int index = 0; index < m_headers.Count; ++index)
                {
                    string key = interner.Intern(m_headers.GetKey(index));
                    string[] values = m_headers.GetValues(index);
                    if (values != null)
                    {
                        foreach (string value in values)
                            newHeaders.Add(key, interner.Intern(value));
                    }
                    else
                    {
                        newHeaders.Add(key, null);
                    }
                }

                m_headers = newHeaders;
            }
        }

        #endregion

        public override bool Equals(object obj)
        {
            HttpHeadersDetail other = obj as HttpHeadersDetail;
            if (other == null || m_headers.Count != other.m_headers.Count)
                return false;

            for (int index = 0; index < m_headers.Count; ++index)
            {
                string key = m_headers.GetKey(index);
                string otherKey = other.m_headers.GetKey(index);
                if (key != otherKey)
                    return false;

                string[] values = m_headers.GetValues(index);
                string[] otherValues = other.m_headers.GetValues(index);

                if (values == null)
                {
                    if (otherValues != null)
                        return false;
                }
                else
                {
                    if (otherValues == null)
                        return false;

                    if (values.Length != otherValues.Length)
                        return false;

                    for (int valueIndex = 0; valueIndex < values.Length; ++valueIndex)
                    {
                        if (values[valueIndex] != otherValues[valueIndex])
                            return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return m_headers.Count.GetHashCode();
        }

        internal HttpHeadersDetail Clone()
        {
            HttpHeadersDetail cloned = new HttpHeadersDetail();
            cloned.m_headers = new NameValueCollection(m_headers);
            return cloned;
        }

        internal void WriteXml(XmlWriteAdaptor adaptor, string elementName)
        {
            if (m_headers.Count == 0)
                return; // Don't write an empty element.

            adaptor.WriteStartElement(elementName);

            for (int index = 0; index < m_headers.Count; ++index)
            {
                adaptor.WriteStartElement(Constants.Xml.HttpDetail.Headers.HeaderElement);
                adaptor.WriteAttribute(Constants.Xml.HttpDetail.Headers.NameAttribute, m_headers.GetKey(index));

                string[] values = m_headers.GetValues(index);
                if (values == null)
                {
                    adaptor.WriteXsiNilAttribute(true);
                }
                else
                {
                    foreach (string value in values)
                        adaptor.WriteElement(Constants.Xml.HttpDetail.Headers.ValueElement, value);
                }

                adaptor.WriteEndElement();
            }

            adaptor.WriteEndElement();
        }

        internal void ReadXml(XmlReadAdaptor adaptor, string elementName)
        {
            m_headers.Clear();

            if (adaptor.ReadElement(elementName))
            {
                while (adaptor.ReadElement(Constants.Xml.HttpDetail.Headers.HeaderElement))
                {
                    string key = adaptor.ReadAttributeString(Constants.Xml.HttpDetail.Headers.NameAttribute, null);
                    bool isNull = adaptor.ReadXsiNilAttribute();
                    if (isNull)
                    {
                        m_headers.Add(key, null);
                    }
                    else
                    {
                        while (adaptor.ReadElement(Constants.Xml.HttpDetail.Headers.ValueElement))
                        {
                            string value = adaptor.ReadString();
                            m_headers.Add(key, value);
                            adaptor.ReadEndElement();
                        }
                    }

                    adaptor.ReadEndElement();
                }

                adaptor.ReadEndElement();
            }
        }

        public void Set(NameValueCollection headers)
        {
            m_headers = new NameValueCollection(headers);
        }

        public string GetKey(int index)
        {
            return m_headers.GetKey(index);
        }

        public string GetValue(int index)
        {
            string[] values = m_headers.GetValues(index);
            if (values == null || values.Length == 0)
                return null;

            StringBuilder sb = new StringBuilder();
            sb.Append(values[0]);
            for (int valueIndex = 1; valueIndex < values.Length; ++valueIndex)
                sb.Append("; ").Append(values[valueIndex]);
            return sb.ToString();
        }
    }

    internal class HttpHeadersMemberWrapper : MemberWrapper
    {
        private readonly HttpHeadersDetail m_value;

        internal HttpHeadersMemberWrapper(HttpHeadersDetail value)
        {
            m_value = value;
        }

        public override string Name
        {
            get { return "Headers"; }
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
            return m_value;
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
            return new HttpHeadersGenericWrapper(m_value);
        }
    }

    internal class HttpHeadersGenericWrapper : GenericWrapper
    {
        internal HttpHeadersGenericWrapper(HttpHeadersDetail value)
            : base(value)
        {
        }

        public override MemberWrappers GetMembers()
        {
            HttpHeadersDetail headers = (HttpHeadersDetail)GetWrappedObject();
            MemberWrappers members = new MemberWrappers();

            for (int index = 0; index < headers.Count; ++index)
                members.Add(new ConstantMemberWrapper(headers.GetKey(index), headers.GetValue(index)));

            return members;
        }
    }
}
