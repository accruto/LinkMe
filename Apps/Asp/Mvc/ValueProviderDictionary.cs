using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc
{
    public class ValueProviderDictionary
        : IDictionary<string, string>
    {
        private readonly IValueProvider _valueProvider;

        public ValueProviderDictionary(IValueProvider valueProvider)
        {
            _valueProvider = valueProvider;
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<string, string> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        public void Add(string key, string value)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out string value)
        {
            value = null;
            var result = _valueProvider.GetValue(key);
            if (result == null)
                return false;
            value = result.AttemptedValue;
            return !string.IsNullOrEmpty(value);
        }

        public string this[string key]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public ICollection<string> Keys
        {
            get { throw new NotImplementedException(); }
        }

        public ICollection<string> Values
        {
            get { throw new NotImplementedException(); }
        }
    }
}
