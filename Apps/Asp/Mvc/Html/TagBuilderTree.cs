using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Html
{
    public class TagBuilderTree
    {
        private readonly TagBuilder _tagBuilder;
        private readonly IList<TagBuilderTree> _childTagBuilders = new List<TagBuilderTree>();

        public TagBuilderTree(string tagName)
        {
            _tagBuilder = new TagBuilder(tagName);
        }

        private TagBuilderTree(TagBuilder tagBuilder)
        {
            _tagBuilder = tagBuilder;
        }

        public void AddCssClass(string value)
        {
            _tagBuilder.AddCssClass(value);
        }

        public void AddTag(TagBuilder tagBuilder)
        {
            _childTagBuilders.Add(new TagBuilderTree(tagBuilder));
        }

        public void AddTag(TagBuilderTree tagBuilder)
        {
            _childTagBuilders.Add(tagBuilder);
        }

        public void MergeAttribute(string key, string value)
        {
            _tagBuilder.MergeAttribute(key, value);
        }

        public override string ToString()
        {
            return ToString(TagRenderMode.Normal);
        }

        public string ToString(TagRenderMode mode)
        {
            // Set the inner html to the children.

            if (_childTagBuilders.Count > 0)
            {
                var sb = new StringBuilder();
                foreach (var tagBuilder in _childTagBuilders)
                    sb.AppendLine(tagBuilder.ToString(TagRenderMode.Normal));
                _tagBuilder.InnerHtml = sb.ToString();
            }

            return _tagBuilder.ToString(mode);
        }
    }
}
