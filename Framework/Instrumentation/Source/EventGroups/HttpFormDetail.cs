using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization;
using System.Web;
using LinkMe.Framework.Tools.Net;
using LinkMe.Framework.Type;
using LinkMe.Framework.Type.Tools.Net;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Xml;
using DateTime = LinkMe.Framework.Type.DateTime;
using TimeZone = LinkMe.Framework.Type.TimeZone;

namespace LinkMe.Framework.Instrumentation.Message
{
    [Serializable]
    public class HttpFormVariableDetail
        : IBinarySerializable,
        ISerializable,
        ICloneable,
        IInternable
    {
        private string _key;
        private string[] _values;

        internal HttpFormVariableDetail()
        {
        }

        private HttpFormVariableDetail(SerializationInfo info, StreamingContext context)
        {
            BinarySerializer.ReadObjectDataForBinarySerializable(this, info);
        }

        public string Key
        {
            get { return _key; }
        }

        public string[] Values
        {
            get { return _values; }
        }

        #region IBinarySerializable Members

        public void Write(BinaryWriter writer)
        {
            var adaptor = new BinaryWriteAdaptor(writer);
            adaptor.WriteNullable(_key);
            adaptor.Write(_values.Length);
            for (var index = 0; index < _values.Length; ++index)
                adaptor.WriteNullable(_values[index]);
        }

        public void Read(BinaryReader reader)
        {
            var adaptor = new BinaryReadAdaptor(reader);
            _key = adaptor.ReadStringNullable();
            var count = adaptor.ReadInt32();
            _values = new string[count];
            for (var index = 0; index < count; ++index)
                _values[index] = adaptor.ReadStringNullable();
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

            _key = interner.Intern(_key);
            _values = interner.Intern(_values);
        }

        #endregion

        public override bool Equals(object obj)
        {
            var other = obj as HttpFormVariableDetail;
            if (other == null)
                return false;

            return _key == other._key
                && _values.NullableCollectionEqual(other._values);
        }

        public override int GetHashCode()
        {
            return (_key ?? string.Empty).GetHashCode();
        }

        internal HttpFormVariableDetail Clone()
        {
            return new HttpFormVariableDetail
            {
                _key = _key,
                _values = (string[])_values.Clone(),
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

        public void Set(string key, string[] values)
        {
            _key = key;
            _values = values ?? new string[0];
        }
    }

    [Serializable]
    public class HttpFormDetail
        : IBinarySerializable,
        ISerializable,
        ICloneable,
        IInternable
    {
        private List<HttpFormVariableDetail> _variables = new List<HttpFormVariableDetail>();

        internal HttpFormDetail()
        {
        }

        private HttpFormDetail(SerializationInfo info, StreamingContext context)
        {
            BinarySerializer.ReadObjectDataForBinarySerializable(this, info);
        }

        public int Count
        {
            get { return _variables.Count; }
        }

        #region IBinarySerializable Members

        public void Write(BinaryWriter writer)
        {
            var adaptor = new BinaryWriteAdaptor(writer);

            adaptor.Write(_variables.Count);
            for (var index = 0; index < _variables.Count; ++index)
                _variables[index].Write(writer);
        }

        public void Read(BinaryReader reader)
        {
            _variables.Clear();
            var adaptor = new BinaryReadAdaptor(reader);
            var count = adaptor.ReadInt32();

            for (var index = 0; index < count; index++)
            {
                var variable = new HttpFormVariableDetail();
                variable.Read(reader);
                _variables.Add(variable);
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

            if (_variables.Count > 0)
            {
                var newVariables = new List<HttpFormVariableDetail>();
                foreach (var variable in _variables)
                {
                    var newVariable = variable.Clone();
                    newVariable.Intern(interner);
                    newVariables.Add(newVariable);
                }

                _variables = newVariables;
            }
        }

        #endregion

        public override bool Equals(object obj)
        {
            var other = obj as HttpFormDetail;
            if (other == null || _variables.Count != other._variables.Count)
                return false;

            for (var index = 0; index < _variables.Count; ++index)
            {
                var variable = _variables[index];
                var otherVariable = other._variables[index];
                if (!variable.Equals(otherVariable))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return _variables.Count.GetHashCode();
        }

        internal HttpFormDetail Clone()
        {
            var form = new HttpFormDetail { _variables = new List<HttpFormVariableDetail>() };
            foreach (var variable in _variables)
                form._variables.Add(variable.Clone());
            return form;
        }

        internal void WriteXml(XmlWriteAdaptor adaptor, string elementName)
        {
            if (_variables.Count == 0)
                return; // Don't write an empty element.

            adaptor.WriteStartElement(elementName);

            for (var index = 0; index < _variables.Count; ++index)
                _variables[index].WriteXml(adaptor, Constants.Xml.HttpDetail.Form.VariableElement);

            adaptor.WriteEndElement();
        }

        internal void ReadXml(XmlReadAdaptor adaptor, string elementName)
        {
            _variables.Clear();

            if (adaptor.ReadElement(elementName))
            {
                while (adaptor.IsReadingElement(Constants.Xml.HttpDetail.Form.VariableElement))
                {
                    var variable = new HttpFormVariableDetail();
                    variable.ReadXml(adaptor, Constants.Xml.HttpDetail.Form.VariableElement);
                    _variables.Add(variable);
                }

                adaptor.ReadEndElement();
            }
        }

        public void Set(NameValueCollection variables)
        {
            _variables = new List<HttpFormVariableDetail>();
            if (variables != null)
            {
                for (var index = 0; index < variables.Count; ++index)
                {
                    var variable = new HttpFormVariableDetail();
                    var key = variables.GetKey(index);
                    var values = variables.GetValues(index);
                    variable.Set(key, values);
                    _variables.Add(variable);
                }
            }
        }

        public HttpFormVariableDetail this[int index]
        {
            get { return _variables[index]; }
        }
    }

    internal class HttpFormMemberWrapper
        : MemberWrapper
    {
        private readonly HttpFormDetail _value;

        internal HttpFormMemberWrapper(HttpFormDetail value)
        {
            _value = value;
        }

        public override string Name
        {
            get { return "Form"; }
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
            return new HttpFormGenericWrapper(_value);
        }
    }

    internal class HttpFormGenericWrapper : GenericWrapper
    {
        internal HttpFormGenericWrapper(HttpFormDetail value)
            : base(value)
        {
        }

        public override MemberWrappers GetMembers()
        {
            var cookies = (HttpFormDetail)GetWrappedObject();
            var members = new MemberWrappers();

            for (int index = 0; index < cookies.Count; ++index)
                members.Add(new HttpFormVariableMemberWrapper(cookies[index]));

            return members;
        }
    }

    internal class HttpFormVariableMemberWrapper
    : MemberWrapper
    {
        private readonly HttpFormVariableDetail _value;

        internal HttpFormVariableMemberWrapper(HttpFormVariableDetail value)
        {
            _value = value;
        }

        public override string Name
        {
            get { return _value.Key; }
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
            return new GenericWrapper(_value.Values);
        }
    }
}
