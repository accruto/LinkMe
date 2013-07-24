using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Domain.Roles.Resumes
{
    /// <summary>
    /// Ensures that jobs are kept in date order, ie current jobs followeed by recent jobs followeed by jobs with no dates.
    /// </summary>
    internal class JobList
        : IList<Job>
    {
        private class TimeRangeComparer<T>
            : IComparer<TimeRange<T>>
            where T : struct, IComparable<T>
        {
            public int Compare(TimeRange<T> x, TimeRange<T> y)
            {
                // Want to show more recent jobs first, so reverse the default order.

                if (x == null)
                    return y == null ? 0 : 1;
                return y == null
                    ? -1
                    : -1 * x.CompareTo(y);
            }
        }

        private static readonly TimeRangeComparer<PartialDate> DatesComparer = new TimeRangeComparer<PartialDate>();
        private IList<Job> _list;

        public JobList(IEnumerable<Job> jobs)
        {
            _list = Sort(jobs);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        IEnumerator<Job> IEnumerable<Job>.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        void ICollection<Job>.Add(Job job)
        {
            // Add the job and then resort.

            _list.Add(job);
            _list = Sort(_list);
        }

        void ICollection<Job>.Clear()
        {
            _list.Clear();
        }

        bool ICollection<Job>.Contains(Job job)
        {
            return _list.Contains(job);
        }

        void ICollection<Job>.CopyTo(Job[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        bool ICollection<Job>.Remove(Job job)
        {
            if (_list.Remove(job))
            {
                // Need to resort.

                _list = Sort(_list);
                return true;
            }

            return false;
        }

        int ICollection<Job>.Count
        {
            get { return _list.Count; }
        }

        bool ICollection<Job>.IsReadOnly
        {
            get { return false; }
        }

        int IList<Job>.IndexOf(Job job)
        {
            return _list.IndexOf(job);
        }

        void IList<Job>.Insert(int index, Job item)
        {
            throw new NotImplementedException();
        }

        void IList<Job>.RemoveAt(int index)
        {
            _list.RemoveAt(index);
            _list = Sort(_list);
        }

        Job IList<Job>.this[int index]
        {
            get { return _list[index]; }
            set { throw new NotImplementedException(); }
        }

        private static List<Job> Sort(IEnumerable<Job> jobs)
        {
            return jobs.OrderBy(j => j.Dates, DatesComparer).ToList();
        }
    }
}