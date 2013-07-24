namespace LinkMe.Apps.Asp.Test.Mvc.Fields
{
    internal class HtmlTextBoxField
        : HtmlField
    {
        public HtmlTextBoxField(string name, string value)
            : base(name, value)
        {
        }

        protected override string InputType
        {
            get { return "text"; }
        }

        protected override string InputClass
        {
            get { return "textbox"; }
        }

        protected override string ControlClass
        {
            get { return "textbox_control"; }
        }

        protected override string FieldClass
        {
            get { return "textbox_field"; }
        }
    }
}