using System;

namespace LinkMe.Domain
{
    [Serializable]
    public class TimeRange<T>
        : IComparable<TimeRange<T>>
        where T : struct, IComparable<T>
    {
        public TimeRange(T? start, T end)
        {
            // Make sure start comes before end.

            if (start != null && start.Value.CompareTo(end) > 1)
            {
                Start = end;
                End = start.Value;
            }
            else
            {
                Start = start;
                End = end;
            }
        }

        public TimeRange(T start)
        {
            Start = start;
            End = null;
        }

        public TimeRange()
        {
            Start = null;
            End = null;
        }

        public T? Start { get; private set; }
        public T? End { get; private set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is TimeRange<T> ? Equals((TimeRange<T>) obj) : false;
        }

        public int CompareTo(TimeRange<T> other)
        {
            // Compare the end date first, then start date.
            // If they have the same end date then those with the
            // earlier start date come first.

            var result = CompareTo(End, other.End);
            return result != 0
                ? result
                : CompareTo(Start, other.Start);
        }

        private static int CompareTo(T? dt1, T? dt2)
        {
            if (dt1 == null)
                return dt2 == null ? 0 : 1;
            return dt2 == null
                ? -1
                : dt1.Value.CompareTo(dt2.Value);
        }

        public override string ToString()
        {
            return Start + " - " + End;
        }

        public bool Equals(TimeRange<T> other)
        {
            return other.Start.Equals(Start)
                && other.End.Equals(End);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Start.GetHashCode() * 397) ^ End.GetHashCode();
            }
        }
    }

    [Serializable]
    public class DateTimeRange
        : TimeRange<DateTime>
    {
        public DateTimeRange(DateTime? start, DateTime end)
            : base(start, end)
        {
        }

        public DateTimeRange(DateTime start)
            : base(start)
        {
        }

        public DateTimeRange()
        {
        }
    }

    [Serializable]
    public class DateRange
        : DateTimeRange
    {
        public DateRange(DateTime? start, DateTime end)
            : base(start == null ? (DateTime?)null : start.Value.Date, end.Date)
        {
        }

        public DateRange(DateTime start)
            : base(start.Date)
        {
        }

        public DateRange()
        {
        }

        public override string ToString()
        {
            return Start.Value.ToShortDateString() + " - " + End.Value.ToShortDateString();
        }
    }

    [Serializable]
    public class DayRange
        : DateRange
    {
        public DayRange(DateTime date)
            : base(date, date.AddDays(1))
        {
        }

        public static DayRange Today
        {
            get { return new DayRange(DateTime.Today); }
        }

        public static DayRange Yesterday
        {
            get { return new DayRange(DateTime.Today.AddDays(-1)); }
        }
    }

    [Serializable]
    public class MonthRange
        : DateTimeRange
    {
        public MonthRange(DateTime date)
            : base(new DateTime(date.Year, date.Month, 1), new DateTime(date.Year, date.Month, 1).AddMonths(1))
        {
        }

        public override string ToString()
        {
            return Start.Value.ToShortDateString() + " - " + End.Value.ToShortDateString();
        }

        public static MonthRange CurrentMonth
        {
            get { return new MonthRange(DateTime.Today); }
        }

        public static MonthRange LastMonth
        {
            get { return new MonthRange(DateTime.Today.AddMonths(-1)); }
        }
    }
}
