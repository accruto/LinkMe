using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace LinkMe.Framework.Utility.Urls
{
    internal class QueryStringCollection
    {
        private readonly NameValueCollection _collection;
        private string _toString;

        public QueryStringCollection()
        {
            _collection = HttpUtility.ParseQueryString(string.Empty);
            _toString = _collection.ToString();
        }

        public QueryStringCollection(QueryStringCollection collection)
        {
            _collection = HttpUtility.ParseQueryString(string.Empty);
            Add(collection);
            _toString = _collection.ToString();
        }

        public QueryStringCollection(NameValueCollection collection)
        {
            _collection = HttpUtility.ParseQueryString(string.Empty);
            Add(collection);
            _toString = _collection.ToString();
        }

        public QueryStringCollection(string queryString)
        {
            _collection = HttpUtility.ParseQueryString(queryString ?? string.Empty);
            _toString = _collection.ToString();
        }

        public QueryStringCollection(params string[] parameters)
        {
            _collection = HttpUtility.ParseQueryString(string.Empty);
            if (parameters != null && parameters.Length != 0)
            {
                if (parameters.Length % 2 != 0)
                    throw new ArgumentException("An even number of arguments must be specified.", "parameters");

                for (var index = 0; index < parameters.Length; index += 2)
                {
                    // The key must not be null or empty.

                    var key = parameters[index];
                    if (string.IsNullOrEmpty(key))
                        throw new ArgumentException("The query parameter name at index " + index + " is null or empty string.");

                    // The value can be null (don't add at all) or empty (add an empty value). This is consistent
                    // with what Request.QueryString["key"] would return.

                    var value = parameters[index + 1];
                    if (value == null)
                        continue;

                    _collection.Add(key, value);
                }
            }

            _toString = _collection.ToString();
        }

        public override string ToString()
        {
            if (_toString == null)
                _toString = _collection.ToString();
            return _toString;
        }

        public void Add(QueryStringCollection collection)
        {
            if (collection != null && collection.Count > 0)
            {
                _collection.Add(collection._collection);
                _toString = null;
            }
        }

        public void Add(NameValueCollection collection)
        {
            if (collection != null && collection.Count > 0)
            {
                _collection.Add(collection);
                _toString = null;
            }
        }

        public void Add(string name, string value)
        {
            _collection.Add(name, value);
            _toString = null;
        }

        public void Set(QueryStringCollection collection)
        {
            if (_collection.Count > 0)
            {
                foreach (string key in collection)
                {
                    _collection.Remove(key);
                }
            }

            Add(collection);
        }

        public void Set(string name, string value)
        {
            _collection[name] = value;
            _toString = null;
        }

        public void Remove(string name)
        {
            _collection.Remove(name);
            _toString = null;
        }

        public void Clear()
        {
            _collection.Clear();
            _toString = null;
        }

        public string Get(string name)
        {
            return _collection[name];
        }

        public string[] GetValues(string name)
        {
            return _collection.GetValues(name);
        }

        public int Count
        {
            get { return _collection.Count; }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return new Enumerator(_collection.Keys.GetEnumerator());
        }

        private class Enumerator
            : IEnumerator<string>
        {
            private readonly IEnumerator _enumerator;

            public Enumerator(IEnumerator enumerator)
            {
                _enumerator = enumerator;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                return _enumerator.MoveNext();
            }

            public void Reset()
            {
                _enumerator.Reset();
            }

            public string Current
            {
                get { return (string) _enumerator.Current; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }
        }
    }

    public class ReadOnlyQueryString
        : IEnumerable<string>
    {
        private readonly QueryStringCollection _collection;

        internal ReadOnlyQueryString(QueryStringCollection collection)
        {
            _collection = collection;
        }

        public ReadOnlyQueryString(params string[] parameters)
        {
            _collection = new QueryStringCollection(parameters);
        }

        public ReadOnlyQueryString(string queryString)
        {
            _collection = new QueryStringCollection(queryString);
        }

        public ReadOnlyQueryString(NameValueCollection queryString)
        {
            _collection = new QueryStringCollection(queryString);
        }

        public int Count
        {
            get { return _collection.Count; }
        }

        public string this[string name]
        {
            get { return _collection.Get(name); }
            protected set { _collection.Set(name, value); }
        }

        public string[] GetValues(string name)
        {
            return _collection.GetValues(name);
        }

        public override string ToString()
        {
            return _collection.ToString();
        }

        internal QueryStringCollection Collection
        {
            get { return _collection; }
        }

        protected void Add(string name, string value)
        {
            _collection.Add(name, value);
        }

        protected void Add(ReadOnlyQueryString queryString)
        {
            _collection.Add(queryString._collection);
        }

        protected void Add(NameValueCollection queryString)
        {
            _collection.Add(queryString);
        }

        protected void Remove(string name)
        {
            _collection.Remove(name);
        }

        protected void Clear()
        {
            _collection.Clear();
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }
    }

    public class QueryString
        : ReadOnlyQueryString
    {
        internal QueryString(QueryStringCollection collection)
            : base(collection)
        {
        }

        public QueryString(NameValueCollection queryString)
            : base(queryString)
        {
        }

        public QueryString(params string[] parameters)
            : base(parameters)
        {
        }

        public QueryString(string queryString)
            : base(queryString)
        {
        }

        public new string this[string name]
        {
            get { return base[name]; }
            set { base[name] = value; }
        }

        public new void Add(string name, string value)
        {
            base.Add(name, value);
        }

        public new void Add(ReadOnlyQueryString queryString)
        {
            base.Add(queryString);
        }

        public new void Add(NameValueCollection queryString)
        {
            base.Add(queryString);
        }

        public new void Remove(string name)
        {
            base.Remove(name);
        }

        public new void Clear()
        {
            base.Clear();
        }
    }
}
