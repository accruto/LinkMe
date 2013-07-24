using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Framework.Utility.Results
{
    public interface ITimeable
    {
        DateTime Time { get; }
    }

    public class ReverseComparer<T>
        : IComparer<T> where T : IComparable<T>
    {
        public int Compare(T t1, T t2)
        {
            return -1 * t1.CompareTo(t2);
        }
    }

    public class TimedResults<T>
        : IEnumerable<Tuple<TimeSpan, IList<T>>>
        where T : ITimeable
    {
        private readonly IDictionary<TimeSpan, IList<T>> _results;

        internal TimedResults(IComparer<TimeSpan> comparer)
        {
            _results = new SortedList<TimeSpan, IList<T>>(comparer);
        }

        public int Count
        {
            get { return _results.Count; }
        }

        internal void Add(T t)
        {
            IList<T> list;
            if (!_results.TryGetValue(t.Time.TimeOfDay, out list))
            {
                list = new List<T>();
                _results[t.Time.TimeOfDay] = list;
            }

            list.Add(t);
        }

        internal void Remove(T t)
        {
            IList<T> list;
            if (_results.TryGetValue(t.Time.TimeOfDay, out list))
            {
                list.Remove(t);
                if (list.Count == 0)
                    _results.Remove(t.Time.TimeOfDay);
            }
        }

        public IList<T> this[TimeSpan time]
        {
            get
            {
                IList<T> list;
                _results.TryGetValue(time, out list);
                return list;
            }
        }

        IEnumerator<Tuple<TimeSpan, IList<T>>> IEnumerable<Tuple<TimeSpan, IList<T>>>.GetEnumerator()
        {
            return _results.Select(pair => new Tuple<TimeSpan, IList<T>>(pair.Key, pair.Value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _results.Select(pair => new Tuple<TimeSpan, IList<T>>(pair.Key, pair.Value)).GetEnumerator();
        }
    }

    public class DatedResults<T>
        : IEnumerable<Tuple<DateTime, TimedResults<T>>>
        where T : ITimeable
    {
        private readonly SortedList<DateTime, TimedResults<T>> _results;
        private readonly IComparer<TimeSpan> _timeComparer;

        public DatedResults(IComparer<DateTime> dateComparer, IComparer<TimeSpan> timeComparer)
        {
            _results = new SortedList<DateTime, TimedResults<T>>(dateComparer);
            _timeComparer = timeComparer;
        }

        public int Count
        {
            get { return _results.Count; }
        }

        public void Add(T t)
        {
            TimedResults<T> timedResults;
            if (!_results.TryGetValue(t.Time.Date, out timedResults))
            {
                timedResults = new TimedResults<T>(_timeComparer);
                _results.Add(t.Time.Date, timedResults);
            }

            timedResults.Add(t);
        }

        public void Remove(T t)
        {
            TimedResults<T> timedResults;
            if (_results.TryGetValue(t.Time.Date, out timedResults))
            {
                timedResults.Remove(t);
                if (timedResults.Count == 0)
                    _results.Remove(t.Time.Date);
            }
        }

        public TimedResults<T> this[DateTime time]
        {
            get
            {
                TimedResults<T> timedResults;
                _results.TryGetValue(time.Date, out timedResults);
                return timedResults;
            }
        }

        IEnumerator<Tuple<DateTime, TimedResults<T>>> IEnumerable<Tuple<DateTime, TimedResults<T>>>.GetEnumerator()
        {
            return _results.Select(pair => new Tuple<DateTime, TimedResults<T>>(pair.Key, pair.Value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _results.Select(pair => new Tuple<DateTime, TimedResults<T>>(pair.Key, pair.Value)).GetEnumerator();
        }
    }
}
