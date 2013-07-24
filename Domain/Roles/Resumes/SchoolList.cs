using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Domain.Roles.Resumes
{
    /// <summary>
    /// Ensures that schools are kept in date order, ie current schools followeed by recent schools followed by schools with no dates.
    /// </summary>
    internal class SchoolList
        : IList<School>
    {
        private class CompletionTimeComparer<T>
            : IComparer<CompletionTime<T>>
            where T : struct, IComparable<T>
        {
            public int Compare(CompletionTime<T> x, CompletionTime<T> y)
            {
                // Want to show more recent schools first, so reverse the default order.

                if (x == null)
                    return y == null ? 0 : 1;
                return y == null
                    ? -1
                    : -1 * x.CompareTo(y);
            }
        }

        private static readonly CompletionTimeComparer<PartialDate> DateComparer = new CompletionTimeComparer<PartialDate>();
        private IList<School> _list;

        public SchoolList(IEnumerable<School> schools)
        {
            _list = Sort(schools);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        IEnumerator<School> IEnumerable<School>.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        void ICollection<School>.Add(School school)
        {
            // Add the school and then resort.

            _list.Add(school);
            _list = Sort(_list);
        }

        void ICollection<School>.Clear()
        {
            _list.Clear();
        }

        bool ICollection<School>.Contains(School school)
        {
            return _list.Contains(school);
        }

        void ICollection<School>.CopyTo(School[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        bool ICollection<School>.Remove(School school)
        {
            if (_list.Remove(school))
            {
                // Need to resort.

                _list = Sort(_list);
                return true;
            }

            return false;
        }

        int ICollection<School>.Count
        {
            get { return _list.Count; }
        }

        bool ICollection<School>.IsReadOnly
        {
            get { return false; }
        }

        int IList<School>.IndexOf(School school)
        {
            return _list.IndexOf(school);
        }

        void IList<School>.Insert(int index, School item)
        {
            throw new NotImplementedException();
        }

        void IList<School>.RemoveAt(int index)
        {
            _list.RemoveAt(index);
            _list = Sort(_list);
        }

        School IList<School>.this[int index]
        {
            get { return _list[index]; }
            set { throw new NotImplementedException(); }
        }

        private static List<School> Sort(IEnumerable<School> schools)
        {
            return schools.OrderBy(s => s.CompletionDate, DateComparer).ToList();
        }
    }
}