namespace LinkMe.Apps.Asp.Test.Html
{
    public class HtmlFileTester
        : HtmlInputTester
    {
        public HtmlFileTester(HttpClient httpClient, string id)
            : base(httpClient, id)
        {
        }

        public string FilePath
        {
            get { return GetOptionalAttributeValue("value") ?? ""; }
            set { SetValue(GetAttributeValue("name"), value); }
        }

        protected override string InputType
        {
            get { return "file"; }
        }
    }
}