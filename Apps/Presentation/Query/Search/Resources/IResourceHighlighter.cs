namespace LinkMe.Apps.Presentation.Query.Search.Resources
{
    public interface IResourceHighlighter
    {
        string SummarizeContent(string text, bool htmlEncodeOutput);

        string HighlightTitle(string text, bool htmlEncodeOutput);
        string HighlightContent(string text, bool htmlEncodeOutput);
    }
}
