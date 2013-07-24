using System;

namespace LinkMe.Domain
{
    [Serializable]
    public class CompletionTime<T>
        : IComparable<CompletionTime<T>>
        where T : struct, IComparable<T>
    {
        public CompletionTime(T end)
        {
            End = end;
        }

        public CompletionTime()
        {
            End = null;
        }

        public T? End { get; private set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is CompletionTime<T> ? Equals((CompletionTime<T>)obj) : false;
        }

        public int CompareTo(CompletionTime<T> other)
        {
            // No end means indefinite so they come last.

            if (End == null)
                return other.End == null ? 0 : 1;
            return other.End == null
                ? -1
                : End.Value.CompareTo(other.End.Value);
        }

        public override string ToString()
        {
            return End.ToString();
        }

        public bool Equals(CompletionTime<T> other)
        {
            return other.End.Equals(End);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return End.GetHashCode() * 397;
            }
        }
    }
}
