using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Web;

namespace LinkMe.Framework.Content.Templates
{
    internal class MergeParser
    {
        private static readonly Regex Regex = new Regex(@"^[a-zA-Z0-9\.]+$", RegexOptions.Compiled);

        public static string Parse(string text, string mimeType, string method, TemplateProperties properties, MergeSettings settings)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream, System.Text.Encoding.UTF8))
                {
                    WriteUsings(writer, mimeType, settings.Usings);

                    // Class declaration.

                    writer.Write(@"
using System;

namespace LinkMe.Utility.Templates.Generated
{
    public class Engine
    {
");

                    // Fields.

                    WriteFields(writer, settings.Fields);

                    // Methods.

                    WriteMethods(writer, settings.Methods);

                    // The actual merge method.

                    writer.Write(@"
        public static string ");

                    writer.Write(method);
                    writer.Write("(");
                    
                    WriteParameters(writer, properties);

                    writer.Write(@")
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(stream, System.Text.Encoding.UTF8))
                {
");

                    writer.Write(@"                    // Start of template.

");

                    WriteText(writer, text, mimeType);

                    writer.Write(@"                    // End of template.

");

                    writer.Write(@"
                    writer.Flush();
                    stream.Position = 0;
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}");

                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        private static void WriteMethods(TextWriter writer, IEnumerable<string> methods)
        {
            foreach (string method in methods)
            {
                writer.WriteLine();
                writer.WriteLine(method);
                writer.WriteLine();
            }
        }

        private static void WriteUsings(TextWriter writer, string mimeType, MergeUsings usings)
        {
            if (mimeType == MediaTypeNames.Text.Html)
            {
                if (!usings.Contains(typeof(HttpUtility).Namespace))
                {
                    writer.Write("using ");
                    writer.Write(typeof(HttpUtility).Namespace);
                    writer.WriteLine(";");
                }
            }

            // Each specified namespace needs a using statement.

            foreach (string ns in usings)
            {
                writer.Write("using ");
                writer.Write(ns);
                writer.WriteLine(";");
            }
        }

        private static void WriteFields(TextWriter writer, IEnumerable<MergeField> fields)
        {
            if (fields != null)
            {
                foreach (MergeField field in fields)
                {
                    writer.Write("        private static readonly ");
                    writer.Write(field.Type.FullName + " ");
                    writer.Write(field.Name);
                    writer.Write(" = ");
                    writer.Write(field.InitialValue);
                    writer.WriteLine(";");
                }
            }
        }

        private static void WriteParameters(TextWriter writer, IEnumerable<TemplateProperty> properties)
        {
            if (properties != null)
            {
                bool first = true;
                foreach (TemplateProperty property in properties)
                {
                    if (first)
                        first = false;
                    else 
                        writer.Write(", ");

                    writer.Write(property.Type.FullName);
                    writer.Write(" ");
                    writer.Write(property.Name);
                }
            }
        }

        public static void WriteText(TextWriter writer, string text, string mimeType)
        {
            // Work through the text looking for '<%= ... %>' and '<% ... %>' tags to replace.

            int start = text.IndexOf("<%");
            if (start == -1)
            {
                // Nothing to replace so just write out the text.

                if (text.Length != 0)
                {
                    writer.WriteLine("                    writer.Write(@\"" + text.Replace("\"", "\"\"") + "\");");
                    writer.WriteLine();
                }
                return;
            }

            // Keep track of where the parsing is up to.

            int last = 0;

            while (start != -1)
            {
                if (start != -1)
                {
                    // Write out the text before the start tag.

                    string beforeText = text.Substring(last, start - last).Replace("\"", "\"\"");
                    beforeText = Clean(beforeText, mimeType);

                    if (beforeText.Length != 0)
                    {
                        writer.WriteLine("                    writer.Write(@\"" + beforeText + "\");");
                        writer.WriteLine();
                    }
                }

                // Find the end tag.

                int end = text.IndexOf("%>", start);
                if (end == -1)
                    break;

                string tag = text.Substring(start, end - start + 2);
                if (tag[2] == '=')
                {
                    // '<%= ... %>' tag.  Write out the contents of the tag.

                    writer.WriteLine("                    " + GetWriteStatement(mimeType == MediaTypeNames.Text.Html, tag.Substring(3, tag.Length - 5)) + ";");
                    writer.WriteLine();
                }
                else
                {
                    // '<% ... %>' tag.  Put the code block directly into the generated code.

                    writer.Write("                    ");
                    writer.WriteLine(tag.Substring(2, tag.Length - 4));
                    writer.WriteLine();
                }

                // Update the control variables.

                last = end + 2;

                // Look for the next tag.

                start = text.IndexOf("<%", last);
                if (start == -1)
                {
                    // None found, so write out the final text after the last tag.

                    string afterText = text.Substring(last, text.Length - last).Replace("\"", "\"\"");
                    afterText = Clean(afterText, mimeType);

                    if (afterText.Length != 0)
                    {
                        writer.WriteLine("                    writer.Write(@\"" + afterText + "\");");
                        writer.WriteLine();
                    }
                }
            }
        }

        private static string Clean(string text, string mimeType)
        {
            // Carriage returns are significant in plain text.

            if (mimeType == MediaTypeNames.Text.Plain)
                return text;

            // Remove extra \r\n from the start and end of the text.

            while (text.Length > 2 && text.StartsWith("\r\n\r\n"))
                text = text.Substring(2);

            while (text.Length > 2 && text.EndsWith("\r\n\r\n"))
                text = text.Substring(0, text.Length - 2);

            return text;
        }

        private static string GetWriteStatement(bool htmlEncode, string text)
        {
            if (htmlEncode)
            {
                // If the text refers directly to an object or is a property of an object then encode it.

                if (Regex.Match(text).Success)
                    return "writer.Write(LinkMe.Framework.Utility.HtmlUtil.LineBreaksToHtml(HttpUtility.HtmlEncode(" + text + ".ToString())))";
            }

            return "writer.Write(" + text + ")";
        }
    }
}