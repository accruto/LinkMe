using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkMe.Framework.Utility.Linq
{
    public class KeyedCollection<TKey, TItem>
        : IEnumerable<TItem>
        where TItem : class
    {
        private readonly Func<TItem, TKey> _keySelector;
        private readonly SortedList<TKey, TItem> _dictionary;

        public KeyedCollection(Func<TItem, TKey> keySelector)
        {
            _keySelector = keySelector;
            _dictionary = new SortedList<TKey, TItem>();
        }

        public void Add(TItem item)
        {
            _dictionary[_keySelector(item)] = item;
        }

        public void Add(IEnumerable<TItem> items)
        {
            foreach (var item in items)
                Add(item);
        }

        public int Count
        {
            get { return _dictionary.Count; }
        }

        public TItem this[TKey key]
        {
            get
            {
                if (Equals(key, default(TKey)))
                    return null;
                TItem value;
                return _dictionary.TryGetValue(key, out value) ? value : null;
            }
        }

        IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator()
        {
            return _dictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dictionary.Values.GetEnumerator();
        }
    }
}
