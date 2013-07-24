using java.lang;
using org.apache.lucene.analysis.tokenattributes;
using org.apache.solr.util.plugin;

namespace org.apache.lucene.analysis.ngram

/**
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */


/**
 * Tokenizes the given token into n-grams of given size(s).
 * <p>
 * This {@link TokenFilter} create n-grams from the beginning edge or ending edge of a input token.
 * </p>
 */
{
    public class EdgeNGramTokenFilter
        : TokenFilter
    {
        public static Side DEFAULT_SIDE = Side.FRONT;
        public static int DEFAULT_MAX_GRAM_SIZE = int.MaxValue;
        public static int DEFAULT_MIN_GRAM_SIZE = 1;

        // Replace this with an enum when the Java 1.5 upgrade is made, the impl will be simplified
        /** Specifies which side of the input the n-gram should be generated from */
        public enum Side
        {
            FRONT,
            BACK
        }

        private int minGram;
        private int maxGram;
        private Side side;
        private char[] curTermBuffer;
        private int curTermLength;
        private int curGramSize;
        private int tokStart;

        private TermAttribute termAtt;
        private OffsetAttribute offsetAtt;


        protected EdgeNGramTokenFilter(TokenStream input)
            : base(input)
        {
            termAtt = (TermAttribute)addAttribute(typeof(TermAttribute));
            offsetAtt = (OffsetAttribute)addAttribute(typeof(OffsetAttribute));
        }

        /**
         * Creates EdgeNGramTokenFilter that can generate n-grams in the sizes of the given range
         *
         * @param input {@link TokenStream} holding the input to be tokenized
         * @param side the {@link Side} from which to chop off an n-gram
         * @param minGram the smallest n-gram to generate
         * @param maxGram the largest n-gram to generate
         */
        public EdgeNGramTokenFilter(TokenStream input, Side side, int minGram, int maxGram)
            : base(input)
        {
            if (minGram < 1)
            {
                throw new IllegalArgumentException("minGram must be greater than zero");
            }

            if (minGram > maxGram)
            {
                throw new IllegalArgumentException("minGram must not be greater than maxGram");
            }

            this.minGram = minGram;
            this.maxGram = maxGram;
            this.side = side;
            termAtt = (TermAttribute)addAttribute(typeof(TermAttribute));
            offsetAtt = (OffsetAttribute)addAttribute(typeof(OffsetAttribute));
        }

        public override bool incrementToken()
        {
            while (true)
            {
                if (curTermBuffer == null)
                {
                    if (!input.incrementToken())
                    {
                        return false;
                    }
                    else
                    {
                        curTermBuffer = (char[])termAtt.termBuffer().Clone();
                        curTermLength = termAtt.termLength();
                        curGramSize = minGram;
                        tokStart = offsetAtt.startOffset();
                    }
                }
                if (curGramSize <= maxGram)
                {
                    if (!(curGramSize > curTermLength         // if the remaining input is too short, we can't generate any n-grams
                        || curGramSize > maxGram))
                    {       // if we have hit the end of our n-gram size range, quit
                        // grab gramSize chars from front or back
                        int start = side == Side.FRONT ? 0 : curTermLength - curGramSize;
                        int end = start + curGramSize;
                        clearAttributes();
                        offsetAtt.setOffset(tokStart + start, tokStart + end);
                        termAtt.setTermBuffer(curTermBuffer, start, curGramSize);
                        curGramSize++;
                        return true;
                    }
                }
                curTermBuffer = null;
            }
        }

        public override void reset()
        {
            base.reset();
            curTermBuffer = null;
        }
    }
}
