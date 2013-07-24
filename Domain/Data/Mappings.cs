using System;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Data
{
    [Flags]
    public enum DateParts
    {
        Year = 0,
        Month = 1,
        Day = 2
    }

    public interface IHavePartialDateRangeEntity
    {
        DateTime? startDate { get; set; }
        byte? startDateParts { get; set; }
        DateTime? endDate { get; set; }
        byte? endDateParts { get; set; }
        bool isCurrent { get; set; }
    }

    public interface IHavePartialDateEntity
    {
        DateTime? date { get; set; }
        byte? dateParts { get; set; }
    }

    public interface IHavePartialCompletionDateEntity
    {
        DateTime? endDate { get; set; }
        byte? endDateParts { get; set; }
        bool isCurrent { get; set; }
    }

    public static class Mappings
    {
        public static void MapTo(this PartialDate? partialDate, IHavePartialDateEntity entity)
        {
            DateTime? date;
            DateParts? parts;
            partialDate.MapTo(out date, out parts);
            entity.date = date;
            entity.dateParts = (byte?)parts;
        }

        public static PartialDate? Map(this IHavePartialDateEntity entity)
        {
            if (entity == null || entity.date == null || entity.dateParts == null)
                return null;

            return GetPartialDate(entity.date.Value, entity.dateParts.Value);
        }

        public static void MapTo(this PartialDateRange partialDateRange, IHavePartialDateRangeEntity entity)
        {
            if (partialDateRange == null)
            {
                entity.startDate = null;
                entity.startDateParts = null;
                entity.endDate = null;
                entity.endDateParts = null;
                entity.isCurrent = false;
                return;
            }

            // Convert the dates first to deal with errors etc.

            DateTime? startDate;
            DateParts? startDateParts;
            DateTime? endDate;
            DateParts? endDateParts;
            partialDateRange.Start.MapTo(out startDate, out startDateParts);
            partialDateRange.End.MapTo(out endDate, out endDateParts);

            // 1st possibility, no start or end date.

            if (startDate == null && endDate == null)
            {
                entity.startDate = null;
                entity.startDateParts = null;
                entity.endDate = null;
                entity.endDateParts = null;
                entity.isCurrent = true;
                return;
            }

            // 2nd possibility, a start date but no end date.

            if (startDate != null && endDate == null)
            {
                entity.startDate = startDate;
                entity.startDateParts = (byte?)startDateParts;
                entity.endDate = null;
                entity.endDateParts = null;
                entity.isCurrent = true;
                return;
            }

            // 3rd possibility, no start date but does have an end date.

            if (startDate == null)
            {
                entity.startDate = null;
                entity.startDateParts = null;
                entity.endDate = endDate;
                entity.endDateParts = (byte?)endDateParts;
                entity.isCurrent = false;
                return;
            }

            // 4th possibility, start date and end date.

            entity.startDate = startDate;
            entity.startDateParts = (byte?)startDateParts;
            entity.endDate = endDate;
            entity.endDateParts = (byte?)endDateParts;
            entity.isCurrent = false;
        }

        public static PartialDateRange Map(this IHavePartialDateRangeEntity entity)
        {
            if (entity == null)
                return null;

            // 1st possibility, no start or end date but is current.

            if (entity.startDate == null && entity.startDateParts == null
                && entity.endDate == null && entity.endDateParts == null
                && entity.isCurrent)
                return new PartialDateRange();

            // 2nd possibility, a start date but no end date/is current.

            if (entity.startDate != null && entity.startDateParts != null
                && entity.endDate == null && entity.endDateParts == null)
                return new PartialDateRange(GetPartialDate(entity.startDate.Value, entity.startDateParts.Value));

            // 3rd possibility, no start date but does have an end date.

            if (entity.startDate == null && entity.startDateParts == null
                && entity.endDate != null && entity.endDateParts != null)
                return new PartialDateRange(null, GetPartialDate(entity.endDate.Value, entity.endDateParts.Value));

            // 4th possibility, start date and end date.

            if (entity.startDate != null && entity.startDateParts != null
                && entity.endDate != null && entity.endDateParts != null)
                return new PartialDateRange(GetPartialDate(entity.startDate.Value, entity.startDateParts.Value), GetPartialDate(entity.endDate.Value, entity.endDateParts.Value));

            // No other combination supported.

            return null;
        }

        public static void MapTo(this PartialCompletionDate partialCompletionDate, IHavePartialCompletionDateEntity entity)
        {
            if (partialCompletionDate == null)
            {
                entity.endDate = null;
                entity.endDateParts = null;
                entity.isCurrent = false;
                return;
            }

            DateTime? endDate;
            DateParts? endDateParts;
            partialCompletionDate.End.MapTo(out endDate, out endDateParts);

            // 1st possibility, no end date so is current.

            if (endDate == null)
            {
                entity.endDate = null;
                entity.endDateParts = null;
                entity.isCurrent = true;
                return;
            }

            // 2nd possibility, end date.

            entity.endDate = endDate;
            entity.endDateParts = (byte?)endDateParts;
            entity.isCurrent = false;
        }

        public static PartialCompletionDate Map(this IHavePartialCompletionDateEntity entity)
        {
            if (entity == null)
                return null;

            // 1st possibility, no end date but is current.

            if (entity.endDate == null && entity.endDateParts == null
                && entity.isCurrent)
                return new PartialCompletionDate();

            // 2nd possibility, end date.

            if (entity.endDate != null && entity.endDateParts != null)
                return new PartialCompletionDate(GetPartialDate(entity.endDate.Value, entity.endDateParts.Value));

            // No other combination supported.

            return null;
        }

        private static PartialDate GetPartialDate(DateTime date, byte parts)
        {
            var dateParts = (DateParts?)parts ?? (DateParts.Year | DateParts.Month | DateParts.Day);
            return !dateParts.IsFlagSet(DateParts.Month)
                ? new PartialDate(date.Year)
                : !dateParts.IsFlagSet(DateParts.Day)
                    ? new PartialDate(date.Year, date.Month)
                    : new PartialDate(date.Year, date.Month, date.Day);
        }

        private static void MapTo(this PartialDate? partialDate, out DateTime? date, out DateParts? parts)
        {
            if (partialDate == null)
            {
                date = null;
                parts = null;
                return;
            }

            try
            {
                if (partialDate.Value.Month == null)
                {
                    date = new DateTime(partialDate.Value.Year, 1, 1);
                    parts = DateParts.Year;
                    return;
                }

                if (partialDate.Value.Day == null)
                {
                    date = new DateTime(partialDate.Value.Year, partialDate.Value.Month.Value, 1);
                    parts = DateParts.Year | DateParts.Month;
                    return;
                }

                date = new DateTime(partialDate.Value.Year, partialDate.Value.Month.Value, partialDate.Value.Day.Value);
                parts = DateParts.Year | DateParts.Month | DateParts.Day;
            }
            catch (Exception)
            {
                // If there is any problem then simply ignore it.

                date = null;
                parts = null;
                return;
            }
        }
    }
}
