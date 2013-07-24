using System.Collections.Generic;
using org.apache.lucene.analysis;
using org.apache.lucene.index;
using org.apache.solr.spelling;

namespace LinkMe.Query.Search.Engine
{
    class SpellCheckHandler
    {
        private readonly QueryConverter _queryConverter;
        private readonly SpellCheckerWrapper _spellChecker;

        public float ThresholdTokenFrequency
        {
            set { _spellChecker.Threshold = value; }
        }

        public SpellCheckHandler(Analyzer analyzer, string fieldName)
        {
            _queryConverter = new SpellingQueryConverter();
            _queryConverter.setAnalyzer(analyzer);

            _spellChecker = new SpellCheckerWrapper(fieldName, null);
        }

        public void Build(IndexReader reader)
        {
            _spellChecker.Build(reader);
        }

        public IList<SpellCheckCollation> GetSpellingSuggestions(string queryString, IndexReader reader, int maxSpellings, int maxCollations, bool onlyMorePopular)
        {
            var tokens = _queryConverter.convert(queryString);
            if (tokens == null || tokens.isEmpty())
                return new SpellCheckCollation[0];

            var options = new SpellingOptions(tokens, reader, maxSpellings, onlyMorePopular, true, java.lang.Float.MIN_VALUE);
            var spellingResult = _spellChecker.GetSuggestions(options);
            if (spellingResult == null)
                return new SpellCheckCollation[0];

            var collations = SpellCheckCollator.Collate(spellingResult, queryString, maxCollations);

            collations.Sort();

            return collations;
        }
    }
}
