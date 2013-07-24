using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HtmlAgilityPack;

namespace LinkMe.Framework.Tools.Performance.Http
{
    internal abstract class PostData
    {
    }

    internal class RawPostData
        : PostData
    {
        public string Value { get; set; }
    }

    public abstract class PostValue
    {
        private readonly string _name;

        protected PostValue(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

        internal abstract void Write(BinaryWriter writer, Encoding encoding, string boundary);
        internal abstract void Write(StringBuilder sb, bool first);
    }

    internal class PostDataValues
        : PostData, IEnumerable<PostValue>
    {
        private readonly IList<PostValue> _postValues = new List<PostValue>();

        public void Add(PostValue value)
        {
            _postValues.Add(value);
        }

        IEnumerator<PostValue> IEnumerable<PostValue>.GetEnumerator()
        {
            return _postValues.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _postValues.GetEnumerator();
        }
    }

    public class HttpContent
    {
        private readonly string _contentType;
        private readonly string _text;
        private HtmlDocument _document;
        private PostData _postData;

        internal HttpContent(string contentType, string text)
        {
            _contentType = contentType;
            _text = text;
        }

        public string Text
        {
            get { return _text; }
        }

        internal HtmlDocument Document
        {
            get
            {
                if (_document == null)
                    ParsePageText();
                return _document;
            }
        }

        internal RawPostData RawPostData
        {
            get
            {
                if (_postData == null || _postData is PostDataValues)
                    _postData = new RawPostData();
                return (RawPostData)_postData;
            }
        }

        internal PostDataValues PostDataValues
        {
            get
            {
                if (_postData == null || _postData is RawPostData)
                    ParsePageText();
                if (_postData == null)
                    _postData = new PostDataValues();
                return (PostDataValues) _postData;
            }
        }

        internal PostData PostData
        {
            get { return _postData ?? PostDataValues; }
        }

        private void ParsePageText()
        {
            _postData = new PostDataValues();

            if (!string.IsNullOrEmpty(_text) && _contentType != "application/json")
            {
                _document = new HtmlDocument();
                _document.Load(new StringReader(_text));
                ParsePostData();
            }
        }

        private void ParsePostData()
        {
            ParsePostData("//form//input[@type='file']", "@name", "");
            ParsePostData("//form//input[@type='password']", "@name", "");
            ParsePostData("//form//input[@type='text']", "@name", "");
            ParsePostData("//form//input[@type='hidden']", "@name", "");
            ParsePostData("//form//input[@type='radio'][@checked]", "@name", "on");
            ParsePostData("//form//input[@type='checkbox'][@checked]", "@name", "on");
            ParsePostData("//form//textarea", "@name", null);
            ParsePostData("//form//select/option[@selected]", "../@name", null);
        }

        private void ParsePostData(string path, string namePath, string defaultValue)
        {
            foreach (var node in Document.DocumentNode.SelectNodes(path))
            {
                var nameNode = node.SelectSingleNode(namePath);
                if (nameNode != null)
                {
                    var valueNode = node.Attributes["value"];
                    var value = valueNode == null ? null : valueNode.Value;
                    if (string.IsNullOrEmpty(value))
                        value = defaultValue ?? node.InnerText.Trim();

                    SetPostValue(node, nameNode.InnerText, value);
                }
            }
        }

        private void SetPostValue(HtmlNode node, string name, string value)
        {
            var typeAttribute = node.Attributes["type"];
            var type = typeAttribute == null ? null : typeAttribute.Value;
            if (type == "file")
                PostDataValues.Add(new FilePostValue(name, value, null));
            else
                PostDataValues.Add(new SimplePostValue(name, value));
        }
    }
}
