using System.Collections.Generic;
using LinkMe.Domain.Users.Employers.Views;

namespace LinkMe.Apps.Presentation.Query.Search.Members
{
    public interface IResumeHighlighter
    {
        string SummarizeContent(EmployerMemberView view);
        IEnumerable<KeyValuePair<string, string>> SummarizeSections(EmployerMemberView view);

        string HighlightJobTitle(string text);
        string HighlightDesiredJobTitle(string text);
        string HighlightContent(string text);
        string HighlightEmployer(string text);
        string HighlightEducation(string text);
    }
}