using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Framework.Utility.Linq
{
    public class ReadOnlyDictionary<TKey, TValue>
        : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private readonly IDictionary<TKey, TValue> _dictionary;

        public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
        {
            _dictionary = dictionary;
        }

        public TValue this[TKey key]
        {
            get { return _dictionary[key]; }
        }

        public ICollection<TKey> Keys
        {
            get { return _dictionary.Keys; }
        }

        public ICollection<TValue> Values
        {
            get { return _dictionary.Values; }
        }

        public int Count
        {
            get { return _dictionary.Count; }
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IDictionary<TKey, TValue> ToDictionary()
        {
            return this.ToDictionary(d => d.Key, d => d.Value);
        }
    }
}
