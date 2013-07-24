using System;
using System.Collections;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Content
{
    public abstract class Reference
    {
        private readonly string _path;
        private readonly ContentUrl _url;

        protected Reference(Version version, string path)
        {
            _path = path;
            _url = new ContentUrl(version, path);
        }

        protected Reference(string path)
        {
            _path = path;
            _url = new ContentUrl(path);
        }

        public string Path
        {
            get { return _path; }
        }

        public ReadOnlyApplicationUrl Url
        {
            get { return _url; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Reference))
                return false;
            return _path.Equals(((Reference) obj)._path);
        }

        public override int GetHashCode()
        {
            return _path.GetHashCode();
        }

        protected static string GetPath(bool minified, string path, string extension)
        {
            return minified ? GetMinifiedPath(path, extension) : path;
        }

        private static string GetMinifiedPath(string path, string extension)
        {
            if (path.EndsWith(extension, StringComparison.InvariantCultureIgnoreCase) && !path.EndsWith(".min" + extension, StringComparison.InvariantCultureIgnoreCase))
                return path.Substring(0, path.Length - extension.Length) + ".min" + extension;
            return path;
        }
    }

    public abstract class References<R>
        : IEnumerable<R>
        where R : Reference
    {
        private readonly IList<R> _references = new List<R>();

        public void Add(R reference)
        {
            _references.Add(reference);
        }

        public void Insert(R reference)
        {
            Insert(reference, 0);
        }

        public void Insert(R reference, int index)
        {
            _references.Insert(index, reference);
        }

        public void Remove(R reference)
        {
            _references.Remove(reference);
        }

        IEnumerator<R> IEnumerable<R>.GetEnumerator()
        {
            var enumerator = _references.GetEnumerator();
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _references.GetEnumerator();
        }
    }
}