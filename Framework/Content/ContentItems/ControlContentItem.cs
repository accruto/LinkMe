namespace LinkMe.Framework.Content.ContentItems
{
    public class ControlContentItem
        : ContentItem
    {
        private const string ControlPathProperty = "ControlPath";

/*        public override string TemplateUrl
        {
            get { return "~/cms/ContentTemplates/ControlContentTemplate.ascx"; }
        }
        */

        public string ControlPath
        {
            get { return GetField<string>(ControlPathProperty); }
            set { SetField(ControlPathProperty, value); }
        }
    }
}
