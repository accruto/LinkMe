using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkMe.Domain.Users.Employers.Views;

namespace LinkMe.Apps.Presentation.Query.Search.Members
{
    internal class NullResumeHighlighter : IResumeHighlighter
    {
        #region IResumeHighlighter Members

        string IResumeHighlighter.SummarizeContent(EmployerMemberView view)
        {
            return null;
        }

        public IEnumerable<KeyValuePair<string, string>> SummarizeSections(EmployerMemberView view)
        {
            return Enumerable.Empty<KeyValuePair<string, string>>();
        }

        string IResumeHighlighter.HighlightJobTitle(string text)
        {
            return HttpUtility.HtmlEncode(text);
        }

        string IResumeHighlighter.HighlightDesiredJobTitle(string text)
        {
            return HttpUtility.HtmlEncode(text);
        }

        string IResumeHighlighter.HighlightContent(string text)
        {
            return HttpUtility.HtmlEncode(text);
        }

        public string HighlightEmployer(string text)
        {
            return HttpUtility.HtmlEncode(text);
        }

        public string HighlightEducation(string text)
        {
            return HttpUtility.HtmlEncode(text);
        }

        #endregion
    }
}
