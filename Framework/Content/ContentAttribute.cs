using System;

namespace LinkMe.Framework.Content
{
    public class ContentAttribute
        : Attribute
    {
        private readonly string _displayName;

        public ContentAttribute(string displayName)
        {
            _displayName = displayName;
        }

        public string DisplayName
        {
            get { return _displayName; }
        }
    }

    public class TextContentAttribute
        : ContentAttribute
    {
        public TextContentAttribute(string displayName)
            : base(displayName)
        {
        }
    }
}
