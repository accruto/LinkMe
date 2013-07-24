namespace LinkMe.Framework.Content.ContentItems
{
    public class TextContentItem
        : ContentItem
    {
        private const string TextProperty = "Text";

        public string Text
        {
            get { return GetField<string>(TextProperty); }
            set { SetField(TextProperty, value); }
        }
    }
}
