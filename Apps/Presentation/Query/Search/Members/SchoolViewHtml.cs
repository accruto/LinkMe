using LinkMe.Apps.Presentation.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Presentation.Query.Search.Members
{
	public class SchoolViewHtml
	{
		private readonly string _degree;
		private readonly string _major;
        private readonly string _completionDate;
		private readonly string _institution;
		private readonly string _description;

		internal SchoolViewHtml(ISchool school)
		{
			_degree = school.Degree;
            _major = school.Major;
            _completionDate = school.GetCompletionDateDisplayText();
			_institution = school.Institution;
			_description = HtmlUtil.LineBreaksToHtml(school.Description);
		}

		public string Degree
		{
			get { return _degree; }
		}

		public string Major
		{
			get { return _major; }
		}

		public string CompletionDate
		{
			get { return _completionDate; }
		}

		public string Institution
		{
			get { return _institution; }
		}

		public string Description
		{
			get { return _description; }
		}
	}
}
