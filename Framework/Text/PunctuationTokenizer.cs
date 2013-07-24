using System;
using System.Globalization;

namespace LinkMe.Framework.Text
{
    public class PunctuationTokenizer
        : ITextAnnotator
    {
        private string _punctuation;

        public string Punctuation
        {
            set { _punctuation = value; }
        }

        #region Implementation of ITextAnnotator

        public void AnnotateText(TextFragment textFragment)
        {
            string text = textFragment.Text;
            if (text == null)
                throw new ArgumentException();

            int start = 0;

            while (start < text.Length)
            {
                int end = FindNextPunctuation(text, start);
                if (end == -1)
                {
                    textFragment.Annotations.Add(new Annotation(text.Substring(start), start, text.Length - start));
                    break;
                }

                int length = end - start;
                if (length > 0)
                    textFragment.Annotations.Add(new Annotation(text.Substring(start, length), start, length));

                start = end + 1;
            }
        }

        #endregion

        private int FindNextPunctuation(string text, int start)
        {
            for (int i = start; i < text.Length; i++)
            {
                if (IsPunctuation(text[i]))
                {
                    if (char.GetUnicodeCategory(text[i]) == UnicodeCategory.DashPunctuation &&
                        i > 0 && char.IsLetterOrDigit(text[i - 1]) &&
                        i < text.Length - 1 && char.IsLetterOrDigit(text[i + 1]))
                    {
                        // Dashes immediately between letters/numbers are not punctuation marks. 
                        continue;
                    }

                    return i;
                }
            }

            return -1;
        }

        private bool IsPunctuation(char ch)
        {
            if (string.IsNullOrEmpty(_punctuation))
                return char.IsPunctuation(ch);

            return (_punctuation.IndexOf(ch) != -1);
        }
    }
}