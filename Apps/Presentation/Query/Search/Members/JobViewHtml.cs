using System;
using System.Text.RegularExpressions;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Presentation.Query.Search.Members
{
	public class JobViewHtml
	{
		private readonly string _title;
		private readonly string _employer;
		private readonly string _description;
		private PartialDate? _start;
        private PartialDate? _end;
        private readonly bool _isRecentJob;

		public JobViewHtml(IJob job, bool isRecentJob)
		{
		    if (job == null)
		        throw new ArgumentNullException("job");

			_title = job.Title;
            _employer = job.Company;
            _isRecentJob = isRecentJob;

            _description = HtmlUtil.LineBreaksToHtml(job.Description);
            _start = job.Dates == null ? null : job.Dates.Start;
            _end = job.Dates == null ? null : job.Dates.End;
		}

        public static JobViewHtml[] GetJobViews(IResume resume)
        {
            var jobs = resume.Jobs;

            var views = new JobViewHtml[(jobs == null ? 0 : jobs.Count)];

            if (jobs != null)
            {
                var previousJob = resume.PreviousJob;

                for (int index = 0; index < views.Length; index++)
                {
                    var job = jobs[index];
                    views[index] = new JobViewHtml(job, (job.Dates != null && job.Dates.End == null) || job == previousJob);
                }
            }

            return views;
        }

		public string Title
		{
			get { return _title; }
		}

		public string Employer
		{
			get { return _employer; }
		}

		public string Description
		{
			get { return _description; }
		}

        /// <summary>
        /// Is the job a "current" or "previous" job, which may need to be hidden in some cases?
        /// </summary>
        public bool IsRecentJob
	    {
	        get { return _isRecentJob; }
	    }

        private static string GetYear(PartialDate? dt)
		{
            return dt == null ? null : dt.Value.ToString("yyyy");
		}

		public string GetDateRange()
		{
			if (_start == null && _end == null)
                return string.Format("{0} - {0}", "N/A");

		    var start = _start == null ? "N/A" : GetDate(_start.Value);
            var end = _end == null ? "Current" : GetDate(_end.Value);

            return string.Format("{0} - {1}", start, end);
		}

		public string GetDateRangeAbbrevMonth()
		{
            // Formats the output from GetDateRange()

		    string fullDateRange = GetDateRange();

            if (string.IsNullOrEmpty(fullDateRange))
				return string.Empty;

            string[] dateParts = fullDateRange.Split(new[] {" - "}, StringSplitOptions.None);

            if (dateParts.Length > 2)
                return fullDateRange; 
			
            if(dateParts.Length == 1)
                throw new ApplicationException(String.Format("Datepart was {0}", fullDateRange));
				
            string startPart = MonthAbbreviator(dateParts[0]);
            string endPart = MonthAbbreviator(dateParts[1]);
            
            return string.Format("{0} - {1}", startPart, endPart);
        }

        public string MonthAbbreviator(string date)
        {
            // Splits a string of 'January 2007' type and returns the month 
            // in an abbreviated format.
			
            string[] dateParts = date.Trim().Split(new[] { ' ' }, StringSplitOptions.None);
            string formattedMonth;
            string year;
            var validYear = new Regex("[1-2][0-9]{3}", RegexOptions.Compiled);
            var validMonth = new Regex("^(jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec)", (RegexOptions.Compiled | RegexOptions.IgnoreCase));

            if(dateParts.Length == 2)
            {
                if (validYear.IsMatch(dateParts[1]))
                {
                    if (validMonth.IsMatch(dateParts[0]))
                    {
                        // The expected input
                        formattedMonth = dateParts[0].Trim().Substring(0, 3);
                        year = dateParts[1].Trim();

                        date = String.Format("{0} {1}", formattedMonth, year);
                    }
                }
            }

            if(dateParts.Length == 3)
			{
                if (validYear.IsMatch(dateParts[2]))
                {
                    if (validMonth.IsMatch(dateParts[1]))
                    {
                        // The user may have specified a day
                        formattedMonth = dateParts[1].Trim().Substring(0, 3);
                        year = dateParts[2].Trim();

                        date = String.Format("{0} {1}", formattedMonth, year);
                    }
                }
			}

            return date;
            
		}

		public string GetYearRange()
		{
			var startYear = GetYear(_start);
			var endYear = GetYear(_end);

			if (!string.IsNullOrEmpty(endYear))
			{
			    if (startYear == endYear)								// start  = end 
					return string.Format("{0}:", startYear);

                return !string.IsNullOrEmpty(startYear)
                    ? string.Format("{0}-{1}:", startYear, endYear)
                    : endYear;
			}
		    
            // no ending date

		    return !string.IsNullOrEmpty(startYear) ? string.Format("{0}:", startYear) : string.Empty;
		}

        private static string GetDate(PartialDate date)
        {
            return date.Month != null ? date.ToString("MMM yyyy") : date.ToString("yyyy");
        }
	}
}
