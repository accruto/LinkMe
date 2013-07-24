using System;
using org.apache.lucene.analysis;
using org.apache.lucene.analysis.tokenattributes;
using org.apache.lucene.index;
using org.apache.lucene.search;

namespace LinkMe.Query.Search.Engine
{
    class BoostingTokenFilter
        : TokenFilter
    {
        private readonly int[] _startOffsets;
        private readonly float[] _boosts;
        private readonly OffsetAttribute _offsetAttr;
        private readonly PayloadAttribute _payloadAttr;

        public BoostingTokenFilter(TokenStream input, int[] startOffsets, float[] boosts) 
            : base(input)
        {
            _startOffsets = startOffsets;
            _boosts = boosts;
            _offsetAttr = (OffsetAttribute)base.addAttribute(typeof(OffsetAttribute));
            _payloadAttr = (PayloadAttribute)base.addAttribute(typeof(PayloadAttribute));
        }

        public override bool incrementToken()
        {
            if (!input.incrementToken())
                return false;

            var startOffset = _offsetAttr.startOffset();
            var boost = GetBoost(startOffset);

            if (float.IsNaN(boost))
            {
                _payloadAttr.setPayload(null);
            }
            else
            {
                var boostByte = Similarity.encodeNorm(boost);
                var boostPayload = new Payload(new[] { boostByte });
                _payloadAttr.setPayload(boostPayload);
            }

            return true;
        }

        private float GetBoost(int startOffset)
        {
            var index = Array.BinarySearch(_startOffsets, startOffset);
            if (index >= 0)
                return _boosts[index];

            index = ~index;
            if (index == 0)
                return float.NaN;

            return _boosts[index - 1];
        }
    }
}
