using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace LinkMe.Utility.Utilities
{
    /// <summary>
    /// Load a file into an object (using a delegate that knows how) and
    /// cache it for a specified expiry period. After the expiry window
    /// elapses, reload the object iff the original file's timestamp has
    /// changed.
    /// </summary>
    /// <typeparam name="T">The type of object returned by the loading delegate.</typeparam>
    public class CachingFileLoader<T>
    {
        #region private
        private readonly string _filename;
        private readonly Func<Stream, T> _load;
        private TimeSpan _expiry;
        private T _value;
        private DateTime _expires = DateTime.MinValue;
        private DateTime _lastMod = DateTime.MinValue;
        #endregion

        private static IEnumerable<string> FileLines(TextReader reader)
        {
            while (reader.Peek() >= 0)
                yield return reader.ReadLine();
        }

        /// <summary>
        /// Construct a caching file loader.
        /// </summary>
        /// <param name="filename">The file to load</param>
        /// <param name="load">The delegate that loads the file and returns the corresponding object</param>
        public CachingFileLoader(string filename, Func<Stream, T> load)
        {
            _filename = HttpContext.Current == null ? filename : HttpContext.Current.Server.MapPath(filename);
            _expiry = TimeSpan.FromSeconds(5.0);
            _load = load;
        }

        public static CachingFileLoader<T> FromTextReader(string filename, Func<TextReader, T> load)
        {
            return new CachingFileLoader<T>(filename, stream => load(new StreamReader(stream)));
        }

        public static CachingFileLoader<T> FromLines(string filename, Func<IEnumerable<string>, T> load)
        {
            return FromTextReader(filename, reader => load(FileLines(reader)));
        }

        public TimeSpan Expiry
        {
            get { return _expiry; }
            set { _expiry = value; }
        }

        /// <summary>
        /// The loaded object (checks for staleness and refreshes if necessary).
        /// </summary>
        public T Value
        {
            get
            {
                // We don't want to check the timestamp on every request.
                if (_expires < DateTime.Now)
                {
                    lock (this)
                    {
                        if (_expires < DateTime.Now)
                        {
                            DateTime mod = File.GetLastWriteTimeUtc(_filename);

                            if (_lastMod < mod)
                            {
                                using (var fs = new FileStream(_filename, FileMode.Open, FileAccess.Read))
                                {
                                    _value = _load(fs);
                                }

                                // Only set once loaded successfully.
                                _lastMod = mod;
                            }

                            _expires = DateTime.Now.Add(_expiry);
                        }
                    }
                }
                return _value;
            }
        }
    }
}
