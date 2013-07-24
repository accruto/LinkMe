using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yahoo.Yui.Compressor;
using System.Text.RegularExpressions;
using System.IO;

namespace AspMinifier
{
    class Minifier
    {
        Dictionary<string, Func<string, string>> compressors;

        public Minifier()
        {
            compressors = new Dictionary<string, Func<string, string>>();
            compressors["js"] = minify_javascript;
            compressors["css"] = minify_css;

            compressors["aspx"] = minify_aspnet;
            compressors["master"] = minify_aspnet;
            compressors["ascx"] = minify_aspnet;
            compressors["html"] = minify_aspnet;
            compressors["htm"] = minify_aspnet;
        }

        /// <summary>
        /// If set to true, a file.backup will be created for each file
        /// </summary>
        public bool Backup { get; set; }

        /// <summary>
        /// Minifies a file given its file path
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Bytes saved</returns>
        public int Minify(string file)
        {
            StreamReader sr = null;
            if (Backup)
            {
                System.IO.File.Delete(file + ".backup");
                System.IO.File.Move(file, file + ".backup");
                sr = new StreamReader(file + ".backup");
            }
            else
            {
                sr = new StreamReader(file);
            }

            string data = sr.ReadToEnd();
            string extension = file.Split('.').Last();
            int initialSize = data.Length;
            if (compressors.ContainsKey(extension))
            {
                data = compressors[extension](data);
            }
            sr.Close();

            StreamWriter sw = new StreamWriter(file);
            sw.Write(data);
            sw.Close();

            return initialSize - data.Length;
        }

        /// <summary>
        /// Compress javascript using Yahoo javascript compressor
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string minify_javascript(string data)
        {
            try
            {
                return JavaScriptCompressor.Compress(data, false);
            }
            catch { }
            return data;
        }

        /// <summary>
        /// Compress css using Yahoo Css Compressor
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string minify_css(string data)
        {
            try
            {
                return CssCompressor.Compress(data);
            }
            catch { }
            return data;
        }

        /// <summary>
        /// Finds inlined Javascript & CSS and minifies them using Yahoo compressor.
        /// Everything else thats not in the script/style tag gets their extra white spaces removed.
        /// </summary>
        /// <param name="html">Html code to be compressed</param>
        /// <returns>Compressed html</returns>
        string minify_aspnet(string html)
        {
            // Minify inline javascript & css
            var tags = new[]
            {
                new
                {
                    start = new string[] { "<script type=\"text/javascript\">", "<script>" },
                    end = "</script>",
                    func = new Func<string, string>(minify_javascript)
                },
                
                new
                {
                    start = new string[] { "<style type=\"text/css\">", "<style>" },
                    end = "</style>",
                    func = new Func<string, string>(minify_css)
                }
            };

            // Pointers to the string
            int currentIndex = 0, endIndex = 0;
            while (true)
            {
                currentIndex = endIndex;
                string currentTagKey = "";

                // Specifies the current tag object were working with
                var currentTag = tags[0];

                // Go through each tag and try to find it in the html, see which tag is closest to the currentIndex
                int newIndex = -1;
                foreach (var tag in tags)
                {
                    foreach (var tagkey in tag.start)
                    {
                        int index = html.IndexOf(tagkey, currentIndex, StringComparison.CurrentCultureIgnoreCase);
                        if (index != -1 && (newIndex == -1 || index < newIndex))
                        {
                            newIndex = index;
                            currentTagKey = tagkey;
                            currentTag = tag;
                        }
                    }
                }
                currentIndex = newIndex;

                // Whenever we're not in a script/style tag, we're in pure HTML mode.
                // Remove all empty spaces between 2 tags, but only do it within html.
                if (currentIndex == -1 || endIndex < currentIndex)
                {
                    if (currentIndex == -1) currentIndex = html.Length;

                    // Take out the html code we want to minify
                    string newHTML = html.Substring(endIndex, currentIndex - endIndex);

                    // Use simple regex to remove all extra white spaces between tag (its not a smart one)
                    newHTML = Regex.Replace(newHTML, @"\s+<", " <", RegexOptions.Singleline);
                    newHTML = Regex.Replace(newHTML, @">\s+", "> ", RegexOptions.Singleline);
                    //newHTML = Regex.Replace(newHTML, @"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}", string.Empty);

                    // Replace the existing html code with the new one
                    html = html.Remove(endIndex, currentIndex - endIndex);
                    html = html.Insert(endIndex, newHTML);

                    // Move the pointer to the end of this new html code
                    currentIndex = endIndex + newHTML.Length;

                    // If we reached the end of the whole html, exit
                    if (currentIndex == html.Length) break;
                }

                // If we didn't find any more tags to work with, exit
                if (currentIndex == -1) break;

                // Find the end tag
                endIndex = html.IndexOf(currentTag.end, currentIndex, StringComparison.CurrentCultureIgnoreCase);
                if (endIndex == -1) break;

                // Take out the script/style code
                string script = html.Substring(currentIndex + currentTagKey.Length, endIndex - currentIndex - currentTagKey.Length);

                // Check if the script/style includes any server side code, that will make things messy, better avoid
                if (script.Contains("<%")) continue;

                // Generate the compressed script and insert it into the html.
                string newScript = currentTag.func(script);
                html = html.Remove(currentIndex + currentTagKey.Length, endIndex - currentIndex - currentTagKey.Length);
                html = html.Insert(currentIndex + currentTagKey.Length, newScript);

                // Move the end pointer to the end of the </script> tag
                endIndex = currentIndex + currentTagKey.Length + newScript.Length + currentTag.end.Length;
            }

            return html;
        }
    }
}
