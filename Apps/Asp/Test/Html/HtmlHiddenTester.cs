namespace LinkMe.Apps.Asp.Test.Html
{
    public class HtmlHiddenTester
        : HtmlInputTester
    {
        public HtmlHiddenTester(HttpClient httpClient, string id)
            : base(httpClient, id)
        {
        }

        public string Text
        {
            get { return GetOptionalAttributeValue("value") ?? ""; }
            set { SetValue(GetAttributeValue("name"), value); }
        }

        protected override string InputType
        {
            get { return "hidden"; }
        }
    }
}