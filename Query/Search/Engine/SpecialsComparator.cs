using System;
using System.Collections.Generic;
using com.browseengine.bobo.sort;
using org.apache.lucene.index;
using org.apache.lucene.search;
using org.apache.lucene.util;

namespace LinkMe.Query.Search.Engine
{
    internal class SpecialsComparatorSource
        : DocComparatorSource
    {
        private readonly bool _exclude;
        private readonly IEnumerable<string> _specials;
        private readonly string _fieldName;

        public SpecialsComparatorSource(bool exclude, IEnumerable<string> specials, string fieldName)
        {
            _exclude = exclude;
            _specials = specials;
            _fieldName = fieldName;
       }

        public override DocComparator getComparator(IndexReader reader, int docBase)
        {
            var bitCount = reader.maxDoc();
            var docValues = new OpenBitSet(bitCount);
            if (_exclude)
                docValues.set(0, bitCount);

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
                        docValues.fastClear(docs[0]);
                    else
                        docValues.fastSet(docs[0]);
                }
            }

            return new SpecialsComparator(docValues);
        }
    }

    internal class SpecialsComparator
        : DocComparator
    {
        private readonly OpenBitSet _docValues;

        public SpecialsComparator(OpenBitSet docValues)
        {
            _docValues = docValues;
        }

        #region Overrides of DocComparator

        public override int compare(ScoreDoc scoreDoc1, ScoreDoc scoreDoc2)
        {
            bool sortValue1 = GetSortValue(scoreDoc1.doc);
            bool sortValue2 = GetSortValue(scoreDoc2.doc);
            return sortValue1.CompareTo(sortValue2);
        }

        public override IComparable value(ScoreDoc scoreDoc)
        {
            bool sortValue = GetSortValue(scoreDoc.doc);
            return sortValue;
        }

        #endregion

        private bool GetSortValue(int doc)
        {
            return _docValues.fastGet(doc);
        }
    }
}
