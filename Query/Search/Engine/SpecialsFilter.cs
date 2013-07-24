using System;
using System.Collections.Generic;
using System.Linq;
using org.apache.lucene.index;
using org.apache.lucene.search;
using org.apache.lucene.util;

namespace LinkMe.Query.Search.Engine
{
    class SpecialsFilter : Filter
    {
        private readonly string _fieldName;
        private readonly bool _exclude;
        private readonly List<string> _specials;

        public SpecialsFilter(string fieldName, bool exclude, IEnumerable<string> specials)
        {
            _fieldName = fieldName;
            _exclude = exclude;
            _specials = specials.ToList();
        }

        public override DocIdSet getDocIdSet(IndexReader reader)
        {
            var bitCount = reader.maxDoc();
            var bits = new OpenBitSet(bitCount);
            if (_exclude)
                bits.set(0, bitCount);

            var docs = new int[1];
            var freqs = new int[1];
            var termTemplate = new Term(_fieldName);

            foreach (string special in _specials)
            {
                TermDocs termDocs = reader.termDocs(termTemplate.createTerm(special));
                int count = termDocs.read(docs, freqs);
                if (count == 1)
                {
                    if (_exclude)
                        bits.fastClear(docs[0]);
                    else
                        bits.fastSet(docs[0]);
                }
            }

            return bits;
        }
    }
}
