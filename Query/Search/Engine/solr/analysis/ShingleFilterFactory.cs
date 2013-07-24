using org.apache.solr.analysis;
using java.util;

namespace org.apache.lucene.analysis.shingle
{
    public class ShingleFilterFactory
        : BaseTokenFilterFactory
    {
        private int? _maxShingleSize;

        public override void init(Map args)
         {
            base.init(args);
            _maxShingleSize = (int?) args.get("maxShingleSize");
        }

        public override TokenStream create(TokenStream input)
        {
            var shingle = new ShingleFilter(input, _maxShingleSize ?? ShingleFilter.DEFAULT_MAX_SHINGLE_SIZE );
            return shingle;
        }
    }
}
