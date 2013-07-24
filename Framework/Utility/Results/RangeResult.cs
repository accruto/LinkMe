using System.Collections.Generic;

namespace LinkMe.Framework.Utility.Results
{
    public class Range
    {
        private readonly int _skip;
        private readonly int? _take;

        public Range(int skip, int take)
        {
            _skip = skip;
            _take = take;
        }

        public Range(int skip)
        {
            _skip = skip;
        }

        public Range()
        {
            _skip = 0;
            _take = null;
        }

        public int Skip
        {
            get { return _skip; }
        }

        public int? Take
        {
            get { return _take; }
        }
    }

    public struct RangeResult<T>
    {
        private readonly int _totalItems;
        private readonly IList<T> _rangeItems;

        public RangeResult(int totalItems, IList<T> rangeItems)
        {
            _totalItems = totalItems;
            _rangeItems = rangeItems;
        }

        public int TotalItems
        {
            get { return _totalItems; }
        }

        public IList<T> RangeItems
        {
            get { return _rangeItems; }
        }
    }
}
