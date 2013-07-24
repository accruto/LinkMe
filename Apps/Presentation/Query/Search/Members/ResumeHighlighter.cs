using System.Collections.Generic;
using LinkMe.Domain.Users.Employers.Views;
using org.apache.lucene.analysis;
using LuceneQuery = org.apache.lucene.search.Query;

namespace LinkMe.Apps.Presentation.Query.Search.Members
{
    internal class ResumeHighlighter
        : Highlighter, IResumeHighlighter
    {
        private readonly LuceneQuery _contentQuery;
        private readonly LuceneQuery _jobTitleQuery;
        private readonly LuceneQuery _desiredJobTitleQuery;
        private readonly LuceneQuery _employerQuery;
        private readonly LuceneQuery _educationQuery;

        public ResumeHighlighter(LuceneQuery contentQuery, LuceneQuery jobTitleQuery, LuceneQuery desiredJobTitleQuery, LuceneQuery employerQuery, LuceneQuery educationQuery, Analyzer contentAnalyzer, HighlighterConfiguration configuration)
            : base(contentAnalyzer, configuration)
        {
            _contentQuery = contentQuery;
            _jobTitleQuery = jobTitleQuery;
            _desiredJobTitleQuery = desiredJobTitleQuery;
            _employerQuery = employerQuery;
            _educationQuery = educationQuery;
        }

        string IResumeHighlighter.SummarizeContent(EmployerMemberView view)
        {
            if (view.Resume == null || _contentQuery == null)
                return string.Empty;

            var resumeView = new ResumeViewText(view);
            var resumeText = resumeView.RenderForSummarizing();
            return Summarize(_contentQuery, resumeText, true);
        }

        IEnumerable<KeyValuePair<string, string>> IResumeHighlighter.SummarizeSections(EmployerMemberView view)
        {
            var resumeView = new ResumeViewText(view);
            var summaries = new List<KeyValuePair<string, string>>();

            AddSection(summaries, _contentQuery, "Objective", resumeView.Objective);
            AddSection(summaries, _contentQuery, "Summary", resumeView.SelfSummary);
            AddSection(summaries, _contentQuery, "Skills", resumeView.Skills);
            AddSection(summaries, _educationQuery, "Education", resumeView.EducationDetails);
            AddSection(summaries, _contentQuery, "Professional", resumeView.ProfessionalDetails);
            AddSection(summaries, _contentQuery, "Personal", resumeView.PersonalDetails);
            AddSection(summaries, _contentQuery, "Employment", resumeView.EmploymentDetails);

            return summaries;
        }

        string IResumeHighlighter.HighlightJobTitle(string text)
        {
            return Highlight(_jobTitleQuery, text, true);
        }

        string IResumeHighlighter.HighlightDesiredJobTitle(string text)
        {
            return Highlight(_desiredJobTitleQuery, text, true);
        }

        string IResumeHighlighter.HighlightContent(string text)
        {
            return Highlight(_contentQuery, text, true);
        }

        string IResumeHighlighter.HighlightEmployer(string text)
        {
            return Highlight(_employerQuery, text, true);
        }

        string IResumeHighlighter.HighlightEducation(string text)
        {
            return Highlight(_educationQuery, text, true);
        }

        private void AddSection(ICollection<KeyValuePair<string, string>> summaries, LuceneQuery query, string sectionName, string sectionText)
        {
            var sectionSummary = Summarize(query, sectionText, true);
            if (!string.IsNullOrEmpty(sectionSummary))
                summaries.Add(new KeyValuePair<string, string>(sectionName, sectionSummary));
        }
    }
}
