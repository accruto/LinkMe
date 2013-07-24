using System.Globalization;

namespace LinkMe.Framework.Text
{
    public class SpecialTermAnnotator
        : ITextAnnotator
    {
        void ITextAnnotator.AnnotateText(TextFragment textFragment)
        {
            // Replace "NET" with ".NET" where applicable.

            var annotations = textFragment.Annotations;
            var count = annotations.Count;
            for (var i = 0; i < count; ++i)
            {
                var annotation = annotations[i];

                if (Annotation.CompareInfo.Compare(annotation.Term, "NET", CompareOptions.IgnoreCase) == 0
                    && annotation.SourcePos > 0
                    && textFragment.Text[annotation.SourcePos - 1] == '.')
                {
                    annotations[i] = new Annotation(".NET", annotation.SourcePos, annotation.SourceLen);
                }
            }
        }
    }
}