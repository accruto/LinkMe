using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Presentation.Converters
{
    public class QueryStringSetValues
        : Values, ISetValues
    {
        private readonly QueryString _queryString;

        public QueryStringSetValues(QueryString queryString)
        {
            _queryString = queryString;
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
            if (value != null)
                SetValue(key, Convert(value.Value));
        }

        void ISetValues.SetValue(string key, bool value)
        {
            SetValue(key, Convert(value));
        }

        void ISetValues.SetValue(string key, int? value)
        {
            if (value != null)
                SetValue(key, Convert(value.Value));
        }

        void ISetValues.SetValue(string key, int value)
        {
            SetValue(key, Convert(value));
        }

        void ISetValues.SetValue(string key, decimal? value)
        {
            if (value != null)
                SetValue(key, Convert(value.Value));
        }

        void ISetValues.SetValue(string key, decimal value)
        {
            SetValue(key, Convert(value));
        }

        void ISetValues.SetValue(string key, Guid? value)
        {
            if (value != null)
                SetValue(key, Convert(value.Value));
        }

        void ISetValues.SetValue(string key, Guid value)
        {
            SetValue(key, Convert(value));
        }

        void ISetValues.SetValue(string key, Guid[] value)
        {
            if (value != null)
                SetValue(key, (from g in value select Convert(g)).ToArray());
        }

        void ISetValues.SetValue(string key, DateTime? value)
        {
            if (value != null)
                SetValue(key, Convert(value));
        }

        void ISetValues.SetValue(string key, PartialDate? value)
        {
            if (value != null)
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
            throw new NotImplementedException();
        }

        void ISetValues.SetChildValue<TValue>(string key, TValue value)
        {
            throw new NotImplementedException();
        }

        private void SetValue(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
                _queryString.Add(key, value);
        }

        private void SetValues(IEnumerable<KeyValuePair<string, bool>> values)
        {
            if (values != null)
            {
                foreach (var pair in values)
                    SetValue(pair.Key, Convert(pair.Value));
            }
        }

        private void SetValue(string key, ICollection<string> value)
        {
            if (value != null && value.Count > 0)
            {
                foreach (var v in value)
                    _queryString.Add(key, v);
            }
        }
    }

    public class QueryStringGetValues
        : Values, IGetValues
    {
        private readonly ReadOnlyQueryString _queryString;

        public QueryStringGetValues(ReadOnlyQueryString queryString)
        {
            _queryString = queryString;
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
            throw new NotImplementedException();
        }

        TValue IGetValues.GetChildValue<TValue>(string key)
        {
            throw new NotImplementedException();
        }

        protected override object GetValue(string key)
        {
            return _queryString.GetValues(key);
        }
    }
}