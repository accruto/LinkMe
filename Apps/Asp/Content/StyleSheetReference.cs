using System;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Content
{
    public class StyleSheetReference
        : Reference
    {
        private readonly string _media;

        public StyleSheetReference(Version version, bool minified, string path, string media)
            : base(version, GetPath(minified, path, ".css"))
        {
            _media = media;
        }

        public StyleSheetReference(Version version, bool minified, string path)
            : base(version, GetPath(minified, path, ".css"))
        {
        }

        public StyleSheetReference(string path, string media)
            : base(path)
        {
            _media = media;
        }

        public StyleSheetReference(string path)
            : base(path)
        {
        }

        public string Media
        {
            get { return _media; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is StyleSheetReference))
                return false;
            return base.Equals(obj) && _media == ((StyleSheetReference)obj)._media;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ (_media ?? string.Empty).GetHashCode();
        }

        public override string ToString()
        {
            var builder = new TagBuilder("link");
            builder.MergeAttribute("href", Url.ToString());
            builder.MergeAttribute("rel", "stylesheet");
            if (!string.IsNullOrEmpty(Media))
                builder.MergeAttribute("media", Media);
            return builder.ToString(TagRenderMode.StartTag);
        }
    }

    public class StyleSheetReferences
        : References<StyleSheetReference>
    {
    }
}