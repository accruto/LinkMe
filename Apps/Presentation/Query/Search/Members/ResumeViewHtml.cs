using System;
using System.Collections;
using System.Text;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Presentation.Query.Search.Members
{
	/// <summary>
	/// Stores the text of the resume in HTML in a format that can be directly shown on a web page.
	/// </summary>
	public class ResumeViewHtml
	{
        private const string HtmlLineBreak = "<br />";
        private readonly string _objective;
		private readonly string _summary;
		private readonly string _skills;
		private readonly string _professional;
		private readonly string _interests;
		private readonly string _referees;
	    private readonly string _citizenship;
		private readonly string _affiliations;
		private readonly string _other;
		private readonly string[] _courses;
		private readonly string[] _awards;
		private readonly JobViewHtml[] _jobs;
		private readonly SchoolViewHtml[] _schools;

		public ResumeViewHtml(IResume resume)
		{
			if (resume == null)
				throw new ArgumentNullException("resume");

			_objective			= HtmlUtil.LineBreaksToHtml(resume.Objective);
			_summary				= HtmlUtil.LineBreaksToHtml(resume.Summary);
			_skills				= HtmlUtil.LineBreaksToHtml(resume.Skills);
			_professional		= HtmlUtil.LineBreaksToHtml(resume.Professional);
			_interests			= HtmlUtil.LineBreaksToHtml(resume.Interests);
			_referees			= HtmlUtil.LineBreaksToHtml(resume.Referees);
			_citizenship 		= HtmlUtil.LineBreaksToHtml(resume.Citizenship);
			_affiliations		= HtmlUtil.LineBreaksToHtml(resume.Affiliations);
			_other				= HtmlUtil.LineBreaksToHtml(resume.Other);

            _jobs = JobViewHtml.GetJobViews(resume);

            if (resume.Schools == null)
            {
                _schools = new SchoolViewHtml[0];
            }
            else
            {
			    _schools = new SchoolViewHtml[resume.Schools.Count];

                for (int index = 0; index < _schools.Length; index++)
                    _schools[index] = new SchoolViewHtml(resume.Schools[index]);
            }

            if (resume.Courses == null)
            {
                _courses = new string[0];
            }
            else
            {
			    _courses = new string[resume.Courses.Count];

                for (int index = 0; index < _courses.Length; index++)
                    _courses[index] = HtmlUtil.LineBreaksToHtml(resume.Courses[index]);
            }

            if (resume.Awards == null)
            {
                _awards = new string[0];
            }
            else
            {
			    _awards = new string[resume.Awards.Count];

                for (int index = 0; index < _awards.Length; index++)
                    _awards[index] = HtmlUtil.LineBreaksToHtml(resume.Awards[index]);
            }
		}

		public string Objective
		{
			get { return _objective; }
		}

		public string Summary
		{
			get { return _summary; }
		}

		public string Skills
		{
			get { return _skills; }
		}

		public string Professional
		{
			get { return _professional; }
		}

		public string Interests
		{
			get { return _interests; }
		}

		public string Referees
		{
			get { return _referees; }
		}

		public string Citizenship
		{
			get { return _citizenship; }
		}

		public string Affiliations
		{
			get { return _affiliations; }
		}

		public string Other
		{
			get { return _other; }
		}

		public IList Jobs
		{
			get { return _jobs; }
		}

        public IList Schools
		{
			get { return _schools; }
		}

		public IList Courses
		{
			get { return _courses; }
		}

		public IList Awards
		{
			get { return _awards; }
		}

		/// <summary>
		/// Get the text that is searched in an employer search AND displayed in Full Candidate View.
		/// The order must be the same as in Full Candidate View. Currently used for keywords in context.
		/// </summary>
        /// <param name="hideRecentEmployers">False to include employers for current and previous jobs,
        /// true to omit them.</param>
		/// <returns>All displayed sections of the resume separated by HTML linebreaks.</returns>
		/// <param name="hideReferees">False to include referees, true to omit them.</param>
		public string GetSearchedTextToDisplay(bool hideRecentEmployers, bool hideReferees)
		{
			var sb = new StringBuilder();

			sb.Append(_objective);
			sb.Append(HtmlLineBreak);
			sb.Append(_summary);
			sb.Append(HtmlLineBreak);

			foreach (JobViewHtml job in Jobs)
			{
                AppendJobText(sb, job, hideRecentEmployers);
			}

			sb.Append(_skills);
			sb.Append(HtmlLineBreak);

			foreach (SchoolViewHtml school in Schools)
			{
				sb.Append(school.Degree);
				sb.Append(HtmlLineBreak);
				sb.Append(school.Major);
				sb.Append(HtmlLineBreak);
				sb.Append(school.Institution);
				sb.Append(HtmlLineBreak);
				sb.Append(school.Description);
				sb.Append(HtmlLineBreak);
			}

            foreach (string course in Courses)
            {
                sb.Append(course);
                sb.Append(HtmlLineBreak);
            }

            foreach (string course in Awards)
            {
                sb.Append(course);
                sb.Append(HtmlLineBreak);
            }

            sb.Append(_professional);
			sb.Append(HtmlLineBreak);
			sb.Append(_interests);
			sb.Append(HtmlLineBreak);
			sb.Append(_citizenship);
			sb.Append(HtmlLineBreak);
			sb.Append(_affiliations);
			sb.Append(HtmlLineBreak);
			sb.Append(_other);

            if (!hideReferees)
            {
                sb.Append(HtmlLineBreak);
                sb.Append(_referees);
            }

		    return sb.ToString();
		}

		private static void AppendJobText(StringBuilder sb, JobViewHtml job, bool hideRecentEmployers)
		{
			// Don't include the dates, titles or employers for the last 2 jobs.
	
			if (!string.IsNullOrEmpty(job.Title))
			{
			    string yearRange = job.GetYearRange();
			    if (!string.IsNullOrEmpty(yearRange))
			    {
			        sb.Append(yearRange);
			        sb.Append(" ");
			    }

			    if (!string.IsNullOrEmpty(job.Title))
			    {
			        sb.Append(job.Title);
			        sb.Append(", ");
			    }
			}

            if (!string.IsNullOrEmpty(job.Employer) && (!hideRecentEmployers || !job.IsRecentJob))
			{
				sb.Append(job.Employer);
			}

            if (sb.Length > 0)
            {
				sb.Append(HtmlLineBreak);
			}

			sb.Append(job.Description);
			sb.Append(HtmlLineBreak);
		}
	}
}
