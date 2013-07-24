using System;

namespace LinkMe.Domain
{
    public struct PartialDate
        : IComparable<PartialDate>, ICloneable
    {
        private readonly int? _day;
        private readonly int? _month;
        private readonly int _year;

        public PartialDate(DateTime dt)
        {
            _year = dt.Year;
            _month = dt.Month;
            _day = dt.Day;
        }

        public PartialDate(int year, int month, int day)
        {
            _year = year;
            _month = month;
            _day = day;
        }

        public PartialDate(int year, int month)
        {
            _year = year;
            _month = month;
            _day = null;
        }

        public PartialDate(int year)
        {
            _year = year;
            _month = null;
            _day = null;
        }

        private PartialDate(int year, int? month, int? day)
        {
            _year = year;
            _month = month;
            _day = day;
        }

        public int? Day
        {
            get { return _day; }
        }

        public int? Month
        {
            get { return _month; }
        }

        public int Year
        {
            get { return _year; }
        }

        public PartialDate AddYears(int years)
        {
            return new PartialDate(_year + years, _month, _day);
        }

        public int CompareTo(PartialDate other)
        {
            if (_year > other._year)
                return 1;
            if (_year < other._year)
                return -1;

            // No month specified is earlier.

            if (_month == null)
                return other._month == null ? 0 : -1;
            if (other._month == null)
                return 1;
            if (_month.Value > other._month.Value)
                return 1;
            if (_month.Value < other._month.Value)
                return -1;

            // No day specified is earlier.

            if (_day == null)
                return other._day == null ? 0 : -1;
            if (other._day == null)
                return 1;
            if (_day.Value > other._day.Value)
                return 1;
            if (_day.Value < other._day.Value)
                return -1;
            return 0;
        }

        public static PartialDate Parse(string value)
        {
            var dt = DateTime.Parse(value);

            // Only pull out month and year.

            return new PartialDate(dt.Year, dt.Month);
        }

        public static bool TryParse(string value, out PartialDate? date)
        {
            DateTime dt;
            if (!DateTime.TryParse(value, out dt))
            {
                date = null;
                return false;
            }

            // Only pull out month and year.

            date = new PartialDate(dt.Year, dt.Month);
            return true;
        }

        public override string ToString()
        {
            var dt = new DateTime(_year, _month == null ? 1 : _month.Value, _day == null ? 1 : _day.Value);
            if (_month == null)
                return dt.ToString("yyyy");
            if (_day == null)
                return dt.ToString("MMMM yyyy");
            return dt.ToString("dd MMMM yyyy");
        }

        public string ToString(string format)
        {
            // At the moment only supports MMMM and yyyy formatters, expand out as needed.

            if (_month == null)
                format = format.Replace("MMMM", "").Trim();

            var dt = new DateTime(_year, _month == null ? 1 : _month.Value, _day == null ? 1 : _day.Value);
            return dt.ToString(format);
        }

        public static bool operator >(PartialDate dt1, PartialDate dt2)
        {
            return dt1.CompareTo(dt2) > 0;
        }

        public static bool operator <(PartialDate dt1, PartialDate dt2)
        {
            return dt1.CompareTo(dt2) < 0;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (obj.GetType() != typeof (PartialDate))
                return false;
            return Equals((PartialDate) obj);
        }

        public bool Equals(PartialDate other)
        {
            return other._day.Equals(_day)
                && other._month.Equals(_month)
                && other._year == _year;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = (_day.HasValue ? _day.Value : 0);
                result = (result*397) ^ (_month.HasValue ? _month.Value : 0);
                result = (result*397) ^ _year;
                return result;
            }
        }

        public PartialDate Clone()
        {
            return new PartialDate(_year, _month, _day);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }

    public class PartialDateRange
        : TimeRange<PartialDate>
    {
        public PartialDateRange(PartialDate? start, PartialDate end)
            : base(start, end)
        {
        }

        public PartialDateRange(PartialDate start)
            : base(start)
        {
        }

        public PartialDateRange()
        {
        }
    }

    public class PartialCompletionDate
        : CompletionTime<PartialDate>
    {
        public PartialCompletionDate(PartialDate end)
            : base(end)
        {
        }

        public PartialCompletionDate()
        {
        }
    }
}
