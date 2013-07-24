using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using LinkMe.Domain;

namespace LinkMe.Apps.Presentation.Converters
{
    public class JavaScriptValues
        : Values, IGetValues, ISetValues
    {
        private readonly JavaScriptSerializer _serializer;
        private readonly IDictionary<string, object> _dictionary;

        public JavaScriptValues(IDictionary<string, object> dictionary, JavaScriptSerializer serializer)
        {
            _dictionary = dictionary;
            _serializer = serializer;
        }

        string IGetValues.GetStringValue(string key)
        {
            return GetStringValue(key);
        }

        bool? IGetValues.GetBooleanValue(string key)
        {
            return GetBooleanValue(key);
        }

        int? IGetValues.GetIntValue(string key)
        {
            return GetIntValue(key);
        }

        int[] IGetValues.GetIntArrayValue(string key)
        {
            return GetIntArrayValue(key);
        }

        string[] IGetValues.GetStringArrayValue(string key)
        {
            return GetStringArrayValue(key);
        }

        decimal? IGetValues.GetDecimalValue(string key)
        {
            return GetDecimalValue(key);
        }

        Guid? IGetValues.GetGuidValue(string key)
        {
            return GetGuidValue(key);
        }

        Guid[] IGetValues.GetGuidArrayValue(string key)
        {
            return GetGuidArrayValue(key);
        }

        DateTime? IGetValues.GetDateTimeValue(string key)
        {
            return GetDateTimeValue(key);
        }

        PartialDate? IGetValues.GetPartialDateValue(string key)
        {
            return GetPartialDateValue(key);
        }

        TEnum? IGetValues.GetValue<TEnum>(string key)
        {
            return GetValue<TEnum>(key);
        }

        TEnum? IGetValues.GetFlagsValue<TEnum>()
        {
            return GetFlagsValue<TEnum>();
        }

        IList<TValue> IGetValues.GetArrayValue<TValue>(string key)
        {
            return _serializer.Deserialize<IList<TValue>>(_serializer.Serialize(GetValue(key)));
        }

        TValue IGetValues.GetChildValue<TValue>(string key)
        {
            // Is there a better way to do this?

            return _serializer.Deserialize<TValue>(_serializer.Serialize(GetValue(key)));
        }

        void ISetValues.SetValue(string key, string value)
        {
            SetValue(key, value);
        }

        void ISetValues.SetValue(string key, string[] value)
        {
            SetValue(key, value);
        }

        void ISetValues.SetValue(string key, bool? value)
        {
            SetValue(key, value);
        }

        void ISetValues.SetValue(string key, bool value)
        {
            SetValue(key, value);
        }

        void ISetValues.SetValue(string key, int? value)
        {
            SetValue(key, value);
        }

        void ISetValues.SetValue(string key, int value)
        {
            SetValue(key, value);
        }

        void ISetValues.SetValue(string key, decimal? value)
        {
            SetValue(key, value);
        }

        void ISetValues.SetValue(string key, decimal value)
        {
            SetValue(key, value);
        }

        void ISetValues.SetValue(string key, Guid? value)
        {
            SetValue(key, Convert(value));
        }

        void ISetValues.SetValue(string key, Guid value)
        {
            SetValue(key, Convert(value));
        }

        void ISetValues.SetValue(string key, Guid[] value)
        {
            SetValue(key, Convert(value));
        }

        void ISetValues.SetValue(string key, DateTime? value)
        {
            SetValue(key, Convert(value));
        }

        void ISetValues.SetValue(string key, PartialDate? value)
        {
            SetValue(key, Convert(value));
        }

        void ISetValues.SetValue<TEnum>(string key, TEnum value)
        {
            SetValue(key, Convert(value));
        }

        void ISetValues.SetValue<TEnum>(string key, TEnum? value)
        {
            SetValue(key, Convert(value));
        }

        void ISetValues.SetFlagsValue<TEnum>(TEnum value)
        {
            SetValues(ConvertFlags(value));
        }

        void ISetValues.SetFlagsValue<TEnum>(TEnum? value)
        {
            SetValues(ConvertFlags(value));
        }

        void ISetValues.SetArrayValue<TValue>(string key, IList<TValue> value)
        {
            // Is there a better way than this?.

            SetValue(key, _serializer.DeserializeObject(_serializer.Serialize(value)));
        }

        void ISetValues.SetChildValue<TValue>(string key, TValue value)
        {
            // Is there a better way than this?.

            SetValue(key, _serializer.DeserializeObject(_serializer.Serialize(value)));
        }

        private void SetValue(string key, object value)
        {
            _dictionary[key] = value;
        }

        private void SetValues(IEnumerable<KeyValuePair<string, bool>> values)
        {
            if (values != null)
            {
                foreach (var pair in values)
                    SetValue(pair.Key, pair.Value);
            }
        }

        protected override object GetValue(string key)
        {
            object value;
            _dictionary.TryGetValue(key, out value);
            return value;
        }
    }
}