using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Roles.Communications.Settings
{
    public enum Timing
    {
        Periodic = 0,
        Notification = 1
    }

    public enum Frequency
    {
        Immediately = 1,
        Daily = 2,
        Weekly = 0,
        Monthly = 3,
        Never = 4
    }

    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Timing Timing { get; set; }
        public Frequency DefaultFrequency { get; set; }
        public IList<Frequency> AvailableFrequencies { get; set; }
        public UserType UserTypes { get; set; }
    }

    public static class FrequencyExtensions
    {
        public static TimeSpan ToTimeSpan(this Frequency frequency)
        {
            switch (frequency)
            {
                case Frequency.Immediately:
                    return TimeSpan.Zero;

                case Frequency.Daily:
                    return TimeSpan.FromDays(1);

                case Frequency.Weekly:
                    return TimeSpan.FromDays(7);

                case Frequency.Monthly:
                    return TimeSpan.FromDays(30);

                case Frequency.Never:
                    return TimeSpan.MaxValue;

                default:
                    throw new ArgumentOutOfRangeException("frequency");
            }
        }
    }
}
