using System.Collections.Generic;

namespace LinkMe.Framework.Text
{
    public class EmailAddressRemover
        : ITextAnnotator
    {
        void ITextAnnotator.AnnotateText(TextFragment textFragment)
        {
            var start = 0;
            while (start < textFragment.Annotations.Count)
            {
                int first;
                int last;
                if (RecognizeEmailAddress(textFragment.Text, textFragment.Annotations, start, out first, out last))
                {
                    textFragment.Annotations.RemoveRange(first, last - first + 1);
                    start = first;
                }
                else
                {
                    start++;
                }
            }
        }

        private static bool RecognizeEmailAddress(string source, IList<Annotation> annotations, int start, out int first, out int last)
        {
            first = start;
            last = start;

            if (start == 0)
                return false;

            var sourcePos = annotations[start].SourcePos;
            if (sourcePos == 0 || source[sourcePos - 1] != '@')
                return false;

            // Go left.

            while (first > 0)
            {
                var current = annotations[first];
                var left = annotations[first - 1];
                if (left.SourcePos + left.SourceLen + 1 != current.SourcePos)
                    break;

                first--; // "left" is part of the email address

                var prevPos = left.SourcePos - 1;
                if (prevPos < 0 || source[prevPos] != '.')
                    break;
            }

            if (first == start)
                return false;

            // Go right.

            while (last < annotations.Count - 1)
            {
                var current = annotations[last];
                var right = annotations[last + 1];
                if (current.SourcePos + current.SourceLen + 1 != right.SourcePos)
                    break;

                var nextPos = current.SourcePos + current.SourceLen;
                if (nextPos >= source.Length || source[nextPos] != '.')
                    break;

                last++; // "right" is part of the email address
            }

            return first != last;
        }
    }
}