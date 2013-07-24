using System;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Content
{
    public class JavaScriptReference
        : Reference
    {
        public JavaScriptReference(Version version, bool minified, string path)
            : base(version, GetPath(minified, path, ".js"))
        {
        }

        public JavaScriptReference(string path)
            : base(path)
        {
        }

        public override string ToString()
        {
            var builder = new TagBuilder("script");
            builder.MergeAttribute("type", "text/javascript");
            builder.MergeAttribute("src", Url.ToString());
            return builder.ToString();
        }
    }

    public class JavaScriptReferences
        : References<JavaScriptReference>
    {
    }
}