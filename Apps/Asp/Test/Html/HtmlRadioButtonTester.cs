using System;

namespace LinkMe.Apps.Asp.Test.Html
{
    public class CannotUncheckException
        : InvalidOperationException
    {
        public CannotUncheckException()
            : base("Cannot uncheck radio button, check another one in the same group instead.")
        {
        }
    }

    public class HtmlRadioButtonTester
        : HtmlInputTester
    {
        public HtmlRadioButtonTester(HttpClient httpClient, string id)
            : base(httpClient, id)
        {
        }

        public bool IsChecked
        {
            get
            {
                return GetOptionalAttributeValue("checked") != null;
            }
            set
            {
                if (value)
                {
                    var name = GetAttributeValue("name");
                    RemoveValue(name);
                    SetValue(name, GetAttributeValue("value"));
                }
                else
                {
                    throw new CannotUncheckException();
                }
            }
        }

        protected override string InputType
        {
            get { return "radio"; }
        }
    }
}
