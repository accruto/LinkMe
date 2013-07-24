using System.Collections.Generic;

namespace LinkMe.Framework.Content.Templates
{
    public class ViewPartContentItem
        : ContentItem
    {
        private const string TextProperty = "Text";
        private const string TextDisplayName = "Text";

        [Content(TextDisplayName)]
        public string Text
        {
            get { return GetField<string>(TextProperty); }
            set { SetField(TextProperty, value); }
        }
    }

    public class ViewContentItem
        : ContentItem
    {
        private const string MimeTypeProperty = "MimeType";
        private const string MimeTypeDisplayName = "Mime type";
        private const string PartsDisplayName = "Parts";

        [TextContent(MimeTypeDisplayName)]
        public string MimeType
        {
            get { return GetField<string>(MimeTypeProperty); }
            set { SetField(MimeTypeProperty, value); }
        }

        [Content(PartsDisplayName)]
        public IList<ViewPartContentItem> Parts
        {
            get { return GetChildren<ViewPartContentItem>(); }
            set { SetChildren(value); }
        }
    }

    public class TemplateBaseContentItem
        : ContentItem
    {
        private const string SubjectProperty = "Subject";
        private const string SubjectDisplayName = "Subject";
        private const string ViewsDisplayName = "Views";

        [Content(SubjectDisplayName)]
        public string Subject
        {
            get { return GetField<string>(SubjectProperty); }
            set { SetField(SubjectProperty, value); }
        }

        [Content(ViewsDisplayName)]
        public IList<ViewContentItem> Views
        {
            get { return GetChildren<ViewContentItem>(); }
            set { SetChildren(value); }
        }
    }

    public class TemplateContentItem
        : TemplateBaseContentItem
    {
        private const string MasterProperty = "Master";
        private const string MasterDisplayName = "Master";

        [TextContent(MasterDisplayName)]
        public string Master
        {
            get { return GetField<string>(MasterProperty); }
            set { SetField(MasterProperty, value); }
        }
    }
    
    public class MasterTemplateContentItem
        : TemplateBaseContentItem
    {
    }
}