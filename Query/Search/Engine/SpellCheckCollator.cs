using System;
using System.Collections.Generic;
using org.apache.lucene.analysis;
using org.apache.solr.spelling;

namespace LinkMe.Query.Search.Engine
{
    static class SpellCheckCollator
    {
        public static List<SpellCheckCollation> Collate(SpellingResult result, String originalQuery, int maxCollations)
        {
            List<SpellCheckCollation> collations = new List<SpellCheckCollation>();

            int collNo = 0;
            PossibilityIterator possibilityIter = new PossibilityIterator(result.getSuggestions());
            while (collNo < maxCollations && possibilityIter.hasNext())
            {
                RankedSpellPossibility possibility = (RankedSpellPossibility)possibilityIter.next();
                string collationQueryStr = GetCollationQuery(originalQuery, possibility.getCorrections());

                collNo++;
                SpellCheckCollation collation = new SpellCheckCollation();
                collation.CollationQuery = collationQueryStr;
                collation.InternalRank = possibility.getRank();

                var misspellingsAndCorrections = new List<KeyValuePair<string, string>>();
                for (var iter = possibility.getCorrections().iterator(); iter.hasNext(); )
                {
                    SpellCheckCorrection corr = (SpellCheckCorrection)iter.next();
                    misspellingsAndCorrections.Add(new KeyValuePair<string, string>(corr.getOriginal().term(), corr.getCorrection()));
                }

                collation.MisspellingsAndCorrections = misspellingsAndCorrections;
                collations.Add(collation);
            }
            return collations;
        }

        private static string GetCollationQuery(String origQuery,
                                    java.util.List/*<SpellCheckCorrection>*/ corrections)
        {
            var collation = new java.lang.StringBuilder(origQuery);
            int offset = 0;
            for (var iter = corrections.iterator(); iter.hasNext(); )
            {
                SpellCheckCorrection correction = (SpellCheckCorrection)iter.next();
                Token tok = correction.getOriginal();
                // we are replacing the query in order, but injected terms might cause
                // illegal offsets due to previous replacements.
                if (tok.getPositionIncrement() == 0)
                    continue;
                collation.replace(tok.startOffset() + offset, tok.endOffset() + offset,
                    correction.getCorrection());
                offset += correction.getCorrection().Length
                    - (tok.endOffset() - tok.startOffset());
            }
            return collation.toString();
        }
    }
}
