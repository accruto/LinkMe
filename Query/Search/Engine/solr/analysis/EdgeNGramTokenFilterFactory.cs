using System;
using org.apache.solr.analysis;
using org.apache.solr.common;
using org.apache.solr.util.plugin;

namespace org.apache.lucene.analysis.ngram
{
    class EdgeNGramTokenFilterFactory
        : BaseTokenFilterFactory, ResourceLoaderAware
    {
        private int? _minGram;
        private int? _maxGram;
        private EdgeNGramTokenFilter.Side? _side;

        public void inform(ResourceLoader loader)
        {
            _minGram = (int?) args.get("minGramSize");
            _maxGram = (int?) args.get("maxGramSize");
            _side = (EdgeNGramTokenFilter.Side?)Enum.Parse(typeof(EdgeNGramTokenFilter.Side), args.get("side").ToString());
        }

        public override TokenStream create(TokenStream input)
        {
            var filter = new EdgeNGramTokenFilter(input, _side ?? EdgeNGramTokenFilter.DEFAULT_SIDE, _minGram ?? EdgeNGramTokenFilter.DEFAULT_MIN_GRAM_SIZE, _maxGram ?? EdgeNGramTokenFilter.DEFAULT_MAX_GRAM_SIZE);
            return filter;
        }

    }
}
