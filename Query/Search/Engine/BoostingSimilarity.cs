using System;
using org.apache.lucene.search;

namespace LinkMe.Query.Search.Engine
{
    class BoostingSimilarity
        : DefaultSimilarity
    {
        public override float scorePayload(int docId, string fieldName, int start, int end, byte[] payload, int offset, int length)
        {
            if (payload == null)
                return 1.0f;

            var boost = decodeNorm(payload[offset]);
            return boost;
        }
    }
}
