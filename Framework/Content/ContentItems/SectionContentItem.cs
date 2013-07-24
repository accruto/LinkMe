namespace LinkMe.Framework.Content.ContentItems
{
    public class SectionContentItem
        : ContentItem
    {
        private const string SectionTitleProperty = "SectionTitle";
        private const string SectionTitleDisplayName = "Section title";
        private const string SectionContentProperty = "SectionContent";
        private const string SectionContentDisplayName = "Section content";

        [TextContent(SectionTitleDisplayName)]
        public string SectionTitle
        {
            get { return GetField<string>(SectionTitleProperty); }
            set { SetField(SectionTitleProperty, value); }
        }

        [Content(SectionContentDisplayName)]
        public ContentItem SectionContent
        {
            get { return GetChild<ContentItem>(SectionContentProperty); }
            set { SetChild(SectionContentProperty, value); }
        }
    }
}
