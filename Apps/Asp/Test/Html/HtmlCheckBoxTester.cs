namespace LinkMe.Apps.Asp.Test.Html
{
    public class HtmlCheckBoxTester
        : HtmlInputTester
    {
        private readonly bool _isMvcCheckBox;

        public HtmlCheckBoxTester(HttpClient httpClient, string id, bool isMvcCheckBox)
            : base(httpClient, id)
        {
            _isMvcCheckBox = isMvcCheckBox;
        }

        public HtmlCheckBoxTester(HttpClient httpClient, string id)
            : this(httpClient, id, true)
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
                var name = GetAttributeValue("name");
                if (_isMvcCheckBox)
                {
                    if (value)
                    {
                        SetValue(name, "true");
                        AddValue(name, "false");
                    }
                    else
                    {
                        SetValue(name, "false");
                    }
                }
                else
                {
                    if (value)
                        SetValue(name, GetOptionalAttributeValue("value") ?? "on");
                    else
                        RemoveValue(name);
                }
            }
        }

        public string Value
        {
            get { return GetOptionalAttributeValue("value"); }
        }

        protected override string InputType
        {
            get { return "checkbox"; }
        }
    }
}
