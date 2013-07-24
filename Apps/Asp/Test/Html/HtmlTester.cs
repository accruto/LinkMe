using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace LinkMe.Apps.Asp.Test.Html
{
    public abstract class HtmlTester
    {
        private readonly HttpClient _httpClient;
        private readonly string _id;

        private const RegexOptions Options = RegexOptions.IgnoreCase | RegexOptions.Compiled;
        private static readonly Regex[] PostBackPatterns = new[]
        {
            new Regex(@"__doPostBack\('(?<target>.*?)','(?<argument>.*?)'\)", Options),
            new Regex(@"setTimeout\('__doPostBack\(\\'(?<target>.*?)\\',\\'(?<argument>.*?)\\'\)", Options),
            new Regex(@"WebForm_DoPostBackWithOptions\(new\sWebForm_PostBackOptions\(\""(?<target>.*?)\"",\s\""(?<argument>.*?)\"",", Options)
        };

        protected HtmlTester(HttpClient httpClient, string id)
        {
            _httpClient = httpClient;
            _id = id;
        }

        public bool IsVisible
        {
            get { return _httpClient.CurrentHtml.DocumentNode.SelectSingleNode("//*[@id='" + _id + "']") != null; }
        }

        public bool IsEnabled
        {
            get { return !HasAttribute(Node, "disabled"); }
        }

        public bool IsReadOnly
        {
            get { return HasAttribute(Node, "readonly"); }
        }

        protected abstract void AssertNode(HtmlNode node);

        protected string Id
        {
            get { return _id; }
        }

        protected void Submit()
        {
            _httpClient.Submit(FormId);
        }

        protected void Get(string url)
        {
            _httpClient.Get(HttpStatusCode.OK, url);
        }

        private HtmlNode Node
        {
            get
            {
                var node = _httpClient.CurrentHtml.DocumentNode.SelectSingleNode("//*[@id='" + _id + "']");
                if (node == null)
                    throw new ApplicationException("Cannot find HTML element with id '" + _id + "'.");
                AssertNode(node);
                return node;
            }
        }

        private string FormId
        {
            get
            {
                var formNode = Node.Ancestors("form").FirstOrDefault();
                return formNode == null
                    ? null
                    : GetAttributeValue(formNode, "id");
            }
        }

        protected HtmlNodeCollection GetNodes(string path)
        {
            return Node.SelectNodes(path);
        }

        protected string GetInnerText()
        {
            return Node.InnerText;
        }

        protected string GetAttributeValue(string name)
        {
            return GetAttributeValue(Node, name);
        }

        private static string GetAttributeValue(HtmlNode node, string name)
        {
            var attribute = node.Attributes[name];
            if (attribute == null)
                throw new ArgumentException("Cannot find attribute with name '" + name + "'.");
            return attribute.Value;
        }

        protected string GetOptionalAttributeValue(string name)
        {
            return GetOptionalAttributeValue(Node, name);
        }

        private static string GetOptionalAttributeValue(HtmlNode node, string name)
        {
            var attribute = node.Attributes[name];
            return attribute == null ? null : attribute.Value;
        }

        protected bool HasAttribute(string name)
        {
            return Node.Attributes[name] != null;
        }

        private static bool HasAttribute(HtmlNode node, string name)
        {
            return node.Attributes[name] != null;
        }

        protected void SetValue(string name, string value)
        {
            _httpClient.SetFormVariable(FormId, name, value, GetOptionalAttributeValue(Node, "type") == "file");
        }

        protected void AddValue(string name, string value)
        {
            _httpClient.AddFormVariable(FormId, name, value, GetOptionalAttributeValue(Node, "type") == "file");
        }

        protected void RemoveValue(string name)
        {
            _httpClient.ClearFormVariable(FormId, name);
        }

        protected bool IsPostBack(string href)
        {
            var match = GetPostbackMatch(href);
            return match != null;
        }

        protected void PostBack(string href)
        {
            // Look for post back parameters.

            var match = GetPostbackMatch(href);
            if (match == null)
                throw new InvalidOperationException("Cannot find postback parameters.");

            // Do the post back.

            SetValue("__EVENTTARGET", match.Groups["target"].Captures[0].Value);
            SetValue("__EVENTARGUMENT", match.Groups["argument"].Captures[0].Value);
            Submit();
        }

        private static Match GetPostbackMatch(string postBackScript)
        {
            foreach (var regex in PostBackPatterns)
            {
                var match = regex.Match(postBackScript);
                if (match.Success)
                    return match;
            }

            return null;
        }
    }
}
