using System.Collections.Generic;
using System.IO;
using System.Web;
using HtmlAgilityPack;

namespace LinkMe.Apps.Asp.Test.Html
{
    public class HtmlPage
    {
        private readonly string _pageText;
        private HtmlDocument _htmlDocument;
        private IList<HtmlForm> _htmlForms;

        public HtmlPage(string pageText)
        {
            _pageText = pageText;
        }

        public HtmlDocument HtmlDocument
        {
            get { return _htmlDocument ?? (_htmlDocument = ParseHtmlDocument()); }
        }

        public IList<HtmlForm> HtmlForms
        {
            get { return _htmlForms ?? (_htmlForms = ParseHtmlForms(HtmlDocument)); }
        }

        private HtmlDocument ParseHtmlDocument()
        {
            // Fixes case where form node contains no elements.

            HtmlNode.ElementsFlags["form"] = HtmlElementFlag.CanOverlap;
            HtmlNode.ElementsFlags["option"] = HtmlElementFlag.CanOverlap;

            var htmlDocument = new HtmlDocument();
            using (var reader = new StringReader(_pageText))
            {
                htmlDocument.Load(reader);
            }

            return htmlDocument;
        }

        private static IList<HtmlForm> ParseHtmlForms(HtmlDocument htmlDocument)
        {
            var formNodes = htmlDocument.DocumentNode.SelectNodes("//form");
            if (formNodes == null)
                return new List<HtmlForm>();

            var htmlForms = new List<HtmlForm>();
            foreach (var formNode in formNodes)
            {
                var htmlForm = new HtmlForm(formNode.GetAttributeValue("id", ""), formNode.GetAttributeValue("method", ""), HttpUtility.HtmlDecode(formNode.GetAttributeValue("action", "")));
                ParseHtmlForm(htmlForm, formNode, ".//input[@type='file']", null, null);
                ParseHtmlForm(htmlForm, formNode, ".//input[@type='password']", null, null);
                ParseHtmlForm(htmlForm, formNode, ".//input[@type='text']", null, null);
                ParseHtmlForm(htmlForm, formNode, ".//input[@type='hidden']", null, null);
                ParseHtmlForm(htmlForm, formNode, ".//input[@type='radio'][@checked]", null, "on");
                ParseHtmlForm(htmlForm, formNode, ".//input[@type='checkbox'][@checked]", null, "on");
                ParseHtmlForm(htmlForm, formNode, ".//textarea", null, null);
                ParseHtmlForm(htmlForm, formNode, ".//select/option[@selected]", "..", null);
                htmlForms.Add(htmlForm);
            }

            return htmlForms;
        }

        private static void ParseHtmlForm(HtmlForm htmlForm, HtmlNode formNode, string elementPath, string nameElementPath, string defaultValue)
        {
            var elements = formNode.SelectNodes(elementPath);
            if (elements == null)
                return;

            foreach (var element in elements)
            {
                var nameElement = string.IsNullOrEmpty(nameElementPath)
                    ? element
                    : element.SelectSingleNode(nameElementPath);

                var name = nameElement.GetAttributeValue("name", "");
                if (!string.IsNullOrEmpty(name))
                {
                    var value = element.GetAttributeValue("value", defaultValue);
                    if (string.IsNullOrEmpty(value))
                        value = element.InnerText.Trim();
                    htmlForm.AddFormVariable(name, value, element.GetAttributeValue("type", "") == "file");
                }
            }
        }

        public override string ToString()
        {
            return _pageText;
        }
    }
}