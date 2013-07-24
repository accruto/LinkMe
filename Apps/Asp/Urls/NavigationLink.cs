using System.Text;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Urls
{
    public class Link
    {
        private readonly string _url;
        private readonly string _text;
        private readonly string _title;
        private readonly string _cssClass;
        private readonly string _target;
        private readonly string _id;

        public Link(ReadOnlyUrl url, string text, string title, string cssClass, string target, string id)
        {
            _url = url.ToString();
            _text = text;
            _title = title;
            _cssClass = cssClass;
            _target = target;
            _id = id;
        }

        public Link(string url, string text, string title, string cssClass, string target, string id)
        {
            _url = url;
            _text = text;
            _title = title;
            _cssClass = cssClass;
            _target = target;
            _id = id;
        }

        public string Url
        {
            get { return _url; }
        }

        public string Text
        {
            get { return _text; }
        }

        public string Title
        {
            get { return _title; }
        }

        public string CssClass
        {
            get { return _cssClass; }
        }

        public string Target
        {
            get { return _target; }
        }

        public string Id
        {
            get { return _id; }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("<a");
            if (!string.IsNullOrEmpty(_id))
                sb.Append(" id=\"").Append(_id).Append("\"");
            if (!string.IsNullOrEmpty(_cssClass))
                sb.Append(" class=\"").Append(_cssClass).Append("\"");
            if (_url != null)
                sb.Append(" href=\"").Append(_url).Append("\"");
            if (!string.IsNullOrEmpty(_title))
                sb.Append(" title=\"").Append(_title).Append("\"");
            if (!string.IsNullOrEmpty(_target))
                sb.Append(" target=\"").Append(_target).Append("\"");
            sb.Append(">").Append(_text).Append("</a>");
            return sb.ToString();
        }
    }
}
