using System;
using System.Text;
using LinkMe.Domain;

namespace LinkMe.Apps.Presentation.Domain
{
    public static class DatesExtensions
    {
        public static string GetAgeDisplayText(this PartialDate? dateOfBirth)
        {
            var age = GetAge(dateOfBirth);
            return (age.HasValue ? age.ToString() : "Unspecified");
        }

        public static string GetDateDisplayText(this DateTime time)
        {
            return time.ToString("ddd, dd MMM yyyy");
        }

        public static string GetMonthDisplayText(this int month)
        {
            return new DateTime(2000, month, 1).ToString("MMM");
        }

        public static string GetMonthFullDisplayText(this int month)
        {
            return new DateTime(2000, month, 1).ToString("MMMM");
        }

        public static string GetTimeDisplayText(this DateTime time)
        {
            return time.ToString("hh:mm tt");
        }
        
        public static bool IsValidTenureForDisplay(this PartialDateRange dateTimeRange)
        {
            if (dateTimeRange == null || dateTimeRange.Start == null)
                return false;
            if (dateTimeRange.Start > dateTimeRange.End)
                return false;

            if (dateTimeRange.End != null)
            {
                try
                {
                    var dtStart = new DateTime(dateTimeRange.Start.Value.Year, dateTimeRange.Start.Value.Month ?? 1, dateTimeRange.Start.Value.Day ?? 1);
                    var dtEnd = new DateTime(dateTimeRange.End.Value.Year, dateTimeRange.End.Value.Month ?? 1, dateTimeRange.End.Value.Day ?? 1);
                    var duration = DateTime.MinValue + (dtEnd - dtStart);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }

        public static string GetTenureDisplayText(this PartialDateRange dateTimeRange)
        {
            if (!IsValidTenureForDisplay(dateTimeRange))
                return "Unknown";

            DateTime? duration;
            var dtStart = new DateTime(dateTimeRange.Start.Value.Year, dateTimeRange.Start.Value.Month ?? 1, dateTimeRange.Start.Value.Day ?? 1);

            if (dateTimeRange.End == null)
            {
                if (DateTime.Now > dtStart)
                {
                    // End = "Current"
                    duration = DateTime.MinValue + (DateTime.Now - dtStart);
                }
                else
                {
                    return "Unknown";
                }
            }
            else
            {
                var dtEnd = new DateTime(dateTimeRange.End.Value.Year, dateTimeRange.End.Value.Month ?? 1, dateTimeRange.End.Value.Day ?? 1);
                duration = DateTime.MinValue + (dtEnd - dtStart);
            }

            var years = duration.Value.Year - 1;
            var months = duration.Value.Month - 1;
            if (years == 0)
            {
                if (months == 0)
                    return "<1 month";
                return string.Format("{0} {1}", months, months == 1 ? "month" : "months");
            }
            return string.Format("{0} {1}", Math.Round((double)(years + months / 12) * 10) / 10, years == 1 ? "year" : "years");
        }

        private static int? GetAge(PartialDate? dateOfBirth)
        {
            if (dateOfBirth == null)
                return null;

            var years = DateTime.Now.Year - dateOfBirth.Value.Year;
            if (dateOfBirth.Value.AddYears(years) > new PartialDate(DateTime.Now))
                years--;
            return years;
        }

        public static string GetResumeAge(this int days)
        {
            if (days == 0) return "Today";
            if (days == 1) return "Yesterday";
            if (days < 30) return string.Format("{0} days", days);
            var month = Math.Round((double)days/30);
            return string.Format("{0} {1}", month, month == 1 ? "month" : "months");
        }

        public static string GetJobPostedDateDisplayText(this DateTime dateTime)
        {
            var age = DateTime.Now - dateTime;

            string rez;
            if (age.Days > 0)
            {
                var text = age.Days + (age.Days == 1 ? " day" : " days");
                if (age.Hours > 0)
                    text += string.Format(" {0} {1}", age.Hours, (age.Hours == 1 ? " hour" : " hours"));
                rez = text + " ago";
            }
            else if (age.Hours > 0)
                rez = age.Hours + (age.Hours == 1 ? " hour" : " hours") + " ago";
            else
                rez = "less than an hour ago";

            return "Posted " + rez;
        }

        public static string GetRecentlyViewedDateDisplayText(this DateTime dateTime)
        {
            return "Viewed " + dateTime.GetDateAgoText();
        }

        public static string GetPostedDateDisplayText(this DateTime dateTime)
        {
            return "Posted " + dateTime.GetDateAgoText();
        }

        public static string GetDateAgoText(this DateTime dateTime)
        {
            var ts = DateTime.Now - dateTime;

            var s = "";
            if (ts.Days >= 1)
                s += ts.Days + " day" + (ts.Days > 1 ? "s" : "");
            else if (ts.Hours >= 1)
                s += ts.Hours + " hour" + (ts.Hours > 1 ? "s" : "");
            else s += ts.Minutes + " minute" + (ts.Minutes > 1 ? "s" : "");
            return s + " ago";
        }
    }
}
