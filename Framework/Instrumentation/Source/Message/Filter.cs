using System.Collections;
using System.Collections.Generic;

using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Instrumentation.Message
{
	public abstract class Filter
	{
		private readonly string _name;
		private readonly Pattern _pattern;

        protected Filter(string name, PatternType patternType, string pattern)
        {
            _name = name;
            _pattern = new Pattern(patternType, pattern);
        }

        public string Name
		{
			get { return _name; }
		}

		public Pattern Pattern
		{
			get { return _pattern; }
		}

		public override string ToString()
		{
			return Name;
		}

        public override bool Equals(object obj)
        {
            var other = obj as Filter;
            if (other == null)
                return false;
	        return Equals(other.GetType(), GetType()) && Equals(other._name, _name) && Equals(other._pattern, _pattern);
	    }

	    public override int GetHashCode()
	    {
            return (_name != null ? _name.GetHashCode() : 0) ^ (_pattern != null ? _pattern.GetHashCode() : 0);
	    }
	}

    public class SourceFilter
        : Filter
    {
        public SourceFilter(PatternType patternType, string pattern)
            : base(Constants.Filters.Source, patternType, pattern)
        {
        }
    }

    public class EventFilter
        : Filter
    {
        public EventFilter(PatternType patternType, string pattern)
            : base(Constants.Filters.Event, patternType, pattern)
        {
        }
    }

    public class TypeFilter
    : Filter
    {
        public TypeFilter(PatternType patternType, string pattern)
            : base(Constants.Filters.Type, patternType, pattern)
        {
        }
    }

    public class MethodFilter
    : Filter
    {
        public MethodFilter(PatternType patternType, string pattern)
            : base(Constants.Filters.Method, patternType, pattern)
        {
        }
    }

    public class MessageFilter
    : Filter
    {
        public MessageFilter(PatternType patternType, string pattern)
            : base(Constants.Filters.Message, patternType, pattern)
        {
        }
    }

    public class DetailFilter
        : Filter
    {
        public DetailFilter(string name, PatternType patternType, string pattern)
            : base(name, patternType, pattern)
        {
        }
    }

    public class ParameterFilter
        : Filter
    {
        public ParameterFilter(string name, PatternType patternType, string pattern)
            : base(name, patternType, pattern)
        {
        }
    }

    public class Filters
        : IEnumerable<Filter>
	{
        private readonly List<Filter> _filters = new List<Filter>();

        public int Count
        {
            get { return _filters.Count; }
        }

		public Filter this[string name]
		{
			get
			{
				foreach ( Filter filter in _filters )
				{
					if ( filter.Name == name )
						return filter;
				}

				return null;
			}
		}

		#region IEnumerable Members

		public IEnumerator<Filter> GetEnumerator()
		{
			return _filters.GetEnumerator();
		}

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

		public void Add(Filter filter)
		{
			if (filter == null)
				throw new NullParameterException(GetType(), "Add", "filter");

			_filters.Add(filter);
		}

		public void Clear()
		{
			_filters.Clear();
		}

        public override bool Equals(object obj)
        {
            var other = obj as Filters;
            if (other == null)
                return false;

            if (Count != other.Count)
                return false;

            // Work through this.

            foreach (var filter in _filters)
            {
                bool found = false;
                foreach (var otherFilter in other._filters)
                {
                    if (filter.Equals(otherFilter))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    return false;
            }

            // Work through other.

            foreach (var otherFilter in other._filters)
            {
                bool found = false;
                foreach (var filter in _filters)
                {
                    if (filter.Equals(otherFilter))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    return false;
            }

            return true;
        }

	    public override int GetHashCode()
	    {
	        return (_filters != null ? _filters.GetHashCode() : 0);
	    }
	}
}
