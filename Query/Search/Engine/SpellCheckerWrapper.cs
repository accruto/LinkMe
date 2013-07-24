using org.apache.lucene.analysis;
using org.apache.lucene.index;
using org.apache.lucene.search.spell;
using org.apache.lucene.store;
using org.apache.solr.spelling;
using org.apache.solr.util;


namespace LinkMe.Query.Search.Engine
{
    using Arrays = java.util.Arrays;
    using List = java.util.List;

    class SpellCheckerWrapper
    {
        private readonly string field;
        private readonly SpellChecker spellChecker;

        public const int DEFAULT_SUGGESTION_COUNT = 5;
        private float accuracy = 0.5f;

        private float threshold = 0.001f;

        public float Accuracy
        {
            set
            {
                accuracy = value;
                spellChecker.setAccuracy(accuracy);
            }
        }

        public float Threshold
        {
            set { threshold = value; }
        }

        public SpellCheckerWrapper(string field, StringDistance sd)
        {
            this.field = field;

            if (sd == null)
                sd = new LevensteinDistance();

            spellChecker = new SpellChecker(new RAMDirectory(), sd);
            spellChecker.setAccuracy(accuracy);
        }

        public void Build(IndexReader reader)
        {
            spellChecker.clearIndex();
            spellChecker.indexDictionary(new HighFrequencyDictionary(reader, field, threshold));
        }

        public SpellingResult GetSuggestions(SpellingOptions options)
        {
            SpellingResult result = new SpellingResult(options.tokens);
            bool haveSuggestions = false;

            IndexReader reader = options.reader;
            Term term = field != null ? new Term(field, "") : null;
            //float theAccuracy = (options.accuracy == java.lang.Float.MIN_VALUE) ? spellChecker.getAccuracy() : options.accuracy;

            int count = java.lang.Math.max(options.count, DEFAULT_SUGGESTION_COUNT);
            for (var iter = options.tokens.iterator(); iter.hasNext(); )
            {
                Token token = (Token)iter.next();
                string tokenText = new string(token.termBuffer(), 0, token.termLength());
                string[] suggestions = spellChecker.suggestSimilar(tokenText,
                      count,
                      field != null ? reader : null,
                      field,
                      options.onlyMorePopular/*, theAccuracy*/);
                if (suggestions.Length == 1 && suggestions[0].Equals(tokenText))
                {
                    //These are spelled the same, continue on
                    continue;
                }

                if (options.extendedResults && reader != null && field != null)
                {
                    term = term.createTerm(tokenText);
                    var termFreq = reader.docFreq(term);
                    result.add(token, termFreq);

                    // AC: add the original term to suggestions if it's frequent enough
                    // TODO: make the treshold configurable
                    if (termFreq > 100)
                        result.add(token, tokenText, termFreq);

                    int countLimit = java.lang.Math.min(options.count, suggestions.Length);
                    if (countLimit > 0)
                    {
                        for (int i = 0; i < countLimit; i++)
                        {
                            term = term.createTerm(suggestions[i]);
                            result.add(token, suggestions[i], reader.docFreq(term));
                            haveSuggestions = true;
                        }
                    }
                }
                else
                {
                    if (suggestions.Length > 0)
                    {
                        List/*<String>*/ suggList = Arrays.asList(suggestions);
                        if (suggestions.Length > options.count)
                        {
                            suggList = suggList.subList(0, options.count);
                        }
                        result.add(token, suggList);
                        haveSuggestions = true;
                    }
                }
            }
            return haveSuggestions? result : null;
        }
    }
}
