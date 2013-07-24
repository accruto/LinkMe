namespace LinkMe.Apps.Asp.Test.Html
{
    public class HtmlButtonTester
        : HtmlInputTester
    {
        public HtmlButtonTester(HttpClient httpClient, string id)
            : base(httpClient, id)
        {
        }

        public void Click()
        {
            var name = GetOptionalAttributeValue("name");
            if (!string.IsNullOrEmpty(name))
                SetValue(name, GetAttributeValue("value"));
            Submit();
        }

        protected override string InputType
        {
            get { return "submit"; }
        }
    }
}