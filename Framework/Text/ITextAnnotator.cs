using System.Collections.Generic;
using System.Globalization;

namespace LinkMe.Framework.Text
{
    public struct Annotation
    {
        public static readonly TextInfo TextInfo = CultureInfo.InvariantCulture.TextInfo;
        public static readonly CompareInfo CompareInfo = CultureInfo.InvariantCulture.CompareInfo;

        public readonly string Term;
        public readonly int SourcePos;
        public readonly int SourceLen;

        //unsafe public Annotation(char* term, int start, int len, int sourcePos, int sourceLen)
        //    : this(Intern(term + start, len), sourcePos, sourceLen)
        //{
        //}

        public Annotation(string term, int sourcePos, int sourceLen)
        {
            // Bug 8336 - the system word-breaker (called by CiWordBreaker) sometimes converts uppercase
            // words to lowercase, so convert to uppercase again here.

            Term = TextInfo.ToUpper(term);
            SourcePos = sourcePos;
            SourceLen = sourceLen;
        }

        public override string ToString()
        {
            return Term + " (" + SourcePos + " - " + (SourcePos + SourceLen) + ")";
        }
    }

    public class TextFragment
    {
        public string Text;
        public List<Annotation> Annotations = new List<Annotation>();

        public TextFragment(string text)
        {
            Text = text;
        }
    }

    public interface ITextAnnotator
    {
        void AnnotateText(TextFragment textFragment);
    }
}