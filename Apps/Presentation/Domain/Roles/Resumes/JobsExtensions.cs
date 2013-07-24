using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Presentation.Domain.Roles.Resumes
{
    public static class JobsExtensions
    {
        private const string HtmlLineBreak = "<br />";
        private const string Current = "Current";

        public static string GetJobsDisplayHtml(this IEnumerable<IJob> jobs)
        {
            return jobs.IsNullOrEmpty()
                ? null
                : string.Join(HtmlLineBreak, (from j in jobs let s = GetJobDisplayHtml(j) where !string.IsNullOrEmpty(s) select s).ToArray());
        }

        public static string[] GetJobTitles(this IEnumerable<IJob> jobs)
        {
            return jobs.IsNullOrEmpty()
                ? null
                : (from j in jobs where !string.IsNullOrEmpty(j.Title) select j.Title).ToArray();
        }

        public static string GetDateRangeDisplayText(this Job job)
        {
            if (job.Dates == null)
                return null;

            if (job.Dates.Start == null)
            {
                if (job.Dates.End == null)
                    return Current;
                return "- " + GetDisplayText(job.Dates.End.Value);
            }

            if (job.Dates.End == null)
                return GetDisplayText(job.Dates.Start.Value) + " - " + Current;
            return GetDisplayText(job.Dates.Start.Value) + " - " + GetDisplayText(job.Dates.End.Value);
        }

        public static string GetStartDisplayText(this Job job)
        {
            return job.Dates == null ? null : GetDisplayText(job.Dates.Start);
        }

        public static string GetEndDisplayText(this Job job)
        {
            return job.Dates == null ? null : GetDisplayText(job.Dates.End);
        }

        public static string GetStartMonthDisplayText(this Job job)
        {
            return job.Dates == null ? null : GetMonthDisplayText(job.Dates.Start);
        }

        public static string GetEndMonthDisplayText(this Job job)
        {
            return job.Dates == null ? null : GetMonthDisplayText(job.Dates.End);
        }

        public static string GetStartYearDisplayText(this Job job)
        {
            return job.Dates == null ? null : GetYearDisplayText(job.Dates.Start);
        }

        public static string GetEndYearDisplayText(this Job job)
        {
            return job.Dates == null ? null : GetYearDisplayText(job.Dates.End);
        }

        private static string GetDisplayText(PartialDate date)
        {
            return date.Month != null ? date.ToString("MMM yyyy") : date.ToString("yyyy");
        }

        private static string GetDisplayText(PartialDate? dt)
        {
            return dt == null ? null : GetDisplayText(dt.Value);
        }

        private static string GetMonthDisplayText(PartialDate? dt)
        {
            return dt == null || dt.Value.Month == null ? null : dt.Value.ToString("MMM");
        }

        private static string GetYearDisplayText(PartialDate? dt)
        {
            return dt == null ? null : dt.Value.ToString("yyyy");
        }

        private static string GetJobDisplayHtml(IJob job)
        {
            if (string.IsNullOrEmpty(job.Title))
                return null;
            return string.IsNullOrEmpty(job.Company)
                ? HtmlUtil.TextToHtml(job.Title)
                : HtmlUtil.TextToHtml(job.Title + " at " + job.Company);
        }
    }
}