namespace LinkMe.Apps.Asp.Test.Html
{
    public class HtmlPasswordTester
        : HtmlTextBoxTester
    {
        public HtmlPasswordTester(HttpClient httpClient, string id)
            : base(httpClient, id)
        {
        }

        protected override string InputType
        {
            get { return "password"; }
        }
    }
}