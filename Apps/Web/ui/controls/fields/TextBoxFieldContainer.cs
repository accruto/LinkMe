namespace LinkMe.Web.UI.Controls.Fields
{
    public abstract class TextBoxFieldContainer
        : LinkMeUserControl
    {
        private bool _compulsorySet;
        protected TextBoxFieldContents ucContents;

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            var s = ucContents.CssClass;

            if (!_compulsorySet)
                Required = false;
        }

        public string Label
        {
            get { return ucContents.Label; }
            set { ucContents.Label = value; }
        }

        public string Text
        {
            get { return ucContents.Text; }
            set { ucContents.Text = value; }
        }

        public string Description
        {
            set { ucContents.Description = value; }
        }

        public bool Enabled
        {
            get { return ucContents.Enabled; }
            set { ucContents.Enabled = value; }
        }

        public bool Required
        {
            get
            {
                return ucContents.Required;
            }
            set
            {
                ucContents.Required = value;
                ucContents.CssClass = value ? CompulsoryCssClass : NonCompulsoryCssClass;
                _compulsorySet = true;
            }
        }

        public int MinLength
        {
            get { return ucContents.MinLength; }
            set { ucContents.MinLength = value; }
        }

        public int MaxLength
        {
            get { return ucContents.MaxLength; }
            set { ucContents.MaxLength = value; }
        }

        public string TooltipText
        {
            get { return ucContents.TooltipText; }
            set { ucContents.TooltipText = value; }
        }

        public string Example
        {
            get { return ucContents.Example; }
            set { ucContents.Example = value; }
        }

        public string TextBoxClientID
        {
            get { return ucContents.TextBoxClientID; }
        }

        protected abstract string CompulsoryCssClass { get; }
        protected abstract string NonCompulsoryCssClass { get; }
    }
}
