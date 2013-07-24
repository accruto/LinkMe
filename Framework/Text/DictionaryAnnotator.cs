using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Framework.Text
{
    public class DictionaryAnnotator
        : ITextAnnotator
    {
        private readonly ILookup<string, TextFragment> _dictionary; // lookup by 1st dictionary entry term

        public DictionaryAnnotator(ITextAnnotator termAnnotator, IEnumerable<string> phrases)
        {
            _dictionary = phrases.Select(ph =>
                                             {
                                                 var frag = new TextFragment(ph);
                                                 termAnnotator.AnnotateText(frag);
                                                 return frag;
                                             })
                .Where(frag => frag.Annotations.Count > 0)
                .ToLookup(frag => frag.Annotations[0].Term);
        }

        #region Implementation of ITextAnnotator

        public void AnnotateText(TextFragment textFragment)
        {
            // Get a copy of token annotations that already must be in textFragment.

            var tokens = new Annotation[textFragment.Annotations.Count];
            textFragment.Annotations.CopyTo(tokens);
            textFragment.Annotations.Clear();

            // Search for dictionary phrases in the text. Get the longest possible match.

            for (var i = 0; i < tokens.Length; i++)
            {
                // Try to find the dictionary match starting from the current position.

                TextFragment bestMatch = null;
                var matches = _dictionary[tokens[i].Term];

                foreach (var match in matches)
                {
                    var found = true;

                    for (var j = 1; j < match.Annotations.Count; j++)
                    {
                        if ((i + j) >= tokens.Length ||
                            !string.Equals(tokens[i + j].Term, match.Annotations[j].Term, StringComparison.InvariantCultureIgnoreCase))
                        {
                            found = false;
                            break;
                        }
                    }

                    if (found && (bestMatch == null || bestMatch.Annotations.Count < match.Annotations.Count))
                    {
                        bestMatch = match;
                    }
                }

                if (bestMatch == null)
                    continue;

                // Add new dictionary annotation.

                var bestMatchTerm = string.Join(" ", bestMatch.Annotations.Select(ann => ann.Term).ToArray());
                var firstAnn = tokens[i];
                var lastAnn = tokens[i + bestMatch.Annotations.Count - 1];

                textFragment.Annotations.Add(new Annotation(
                                                 bestMatchTerm, firstAnn.SourcePos, lastAnn.SourcePos + lastAnn.SourceLen - firstAnn.SourcePos));

                // Advance the current position.

                i += bestMatch.Annotations.Count - 1;
            }
        }

        #endregion
    }
}