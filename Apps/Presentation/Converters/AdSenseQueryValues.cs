using System;
using System.Collections.Generic;
using System.Text;
using LinkMe.Domain;

namespace LinkMe.Apps.Presentation.Converters
{
    public class AdSenseQuerySetValues
        : Values, ISetValues
    {
        private readonly StringBuilder _sb;

        public AdSenseQuerySetValues(StringBuilder sb)
        {
            _sb = sb;
        }

        void ISetValues.SetValue(string key, string value)
        {
            SetValue(value);
        }

        void ISetValues.SetValue(string key, string[] value)
        {
            throw new NotImplementedException();
        }

        void ISetValues.SetValue(string key, bool? value)
        {
            if (value != null)
                SetValue(Convert(value.Value));
        }

        void ISetValues.SetValue(string key, bool value)
        {
            SetValue(Convert(value));
        }

        void ISetValues.SetValue(string key, int? value)
        {
            if (value != null)
                SetValue(Convert(value.Value));
        }

        void ISetValues.SetValue(string key, int value)
        {
            SetValue(Convert(value));
        }

        void ISetValues.SetValue(string key, decimal? value)
        {
            if (value != null)
                SetValue(Convert(value.Value));
        }

        void ISetValues.SetValue(string key, decimal value)
        {
            SetValue(Convert(value));
        }

        void ISetValues.SetValue(string key, Guid? value)
        {
            if (value != null)
                SetValue(Convert(value.Value));
        }

        void ISetValues.SetValue(string key, Guid value)
        {
            SetValue(Convert(value));
        }

        void ISetValues.SetValue(string key, Guid[] value)
        {
            throw new NotImplementedException();
        }

        void ISetValues.SetValue(string key, DateTime? value)
        {
            if (value != null)
                SetValue(Convert(value));
        }

        void ISetValues.SetValue(string key, PartialDate? value)
        {
            if (value != null)
                SetValue(Convert(value));
        }

        void ISetValues.SetValue<TEnum>(string key, TEnum value)
        {
            SetValue(Convert(value));
        }

        void ISetValues.SetValue<TEnum>(string key, TEnum? value)
        {
            SetValue(Convert(value));
        }

        void ISetValues.SetFlagsValue<TEnum>(TEnum value)
        {
            throw new NotImplementedException();
        }

        void ISetValues.SetFlagsValue<TEnum>(TEnum? value)
        {
            throw new NotImplementedException();
        }

        void ISetValues.SetArrayValue<TValue>(string key, IList<TValue> value)
        {
            throw new NotImplementedException();
        }

        void ISetValues.SetChildValue<TValue>(string key, TValue value)
        {
            throw new NotImplementedException();
        }

        private void SetValue(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (_sb.Length != 0)
                    _sb.Append(' ');
                _sb.Append(value.ToLower());
            }
        }
    }
}