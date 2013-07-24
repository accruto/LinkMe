using System.Web;

namespace LinkMe.Apps.Asp.Test.Html
{
    public class HtmlTextBoxTester
        : HtmlInputTester
    {
        public HtmlTextBoxTester(HttpClient httpClient, string id)
            : base(httpClient, id)
        {
        }

        public string Text
        {
            get { return HttpUtility.HtmlDecode(GetOptionalAttributeValue("value") ?? ""); }
            set { SetValue(GetAttributeValue("name"), value); }
        }

        protected override string InputType
        {
            get { return "text"; }
        }
    }
}