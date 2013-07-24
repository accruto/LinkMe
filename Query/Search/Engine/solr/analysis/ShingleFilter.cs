using System.Diagnostics.CodeAnalysis;
using java.lang;
using java.util;

namespace org.apache.lucene.analysis.shingle
{
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
     * <p>A ShingleFilter constructs shingles (token n-grams) from a token stream.
     * In other words, it creates combinations of tokens as a single token.
     *
     * <p>For example, the sentence "please divide this sentence into shingles"
     * might be tokenized into shingles "please divide", "divide this",
     * "this sentence", "sentence into", and "into shingles".
     *
     * <p>This filter handles position increments > 1 by inserting filler tokens
     * (tokens with termtext "_"). It does not handle a position increment of 0.
     */
    public class ShingleFilter
        : TokenFilter
    {

        private LinkedList shingleBuf = new LinkedList();
        private LinkedList outputBuf = new LinkedList();
        private LinkedList tokenBuf = new LinkedList();
        private StringBuffer[] shingles;
        private string tokenType = "shingle";

        /**
         * filler token for when positionIncrement is more than 1
         */
        public static char[] FILLER_TOKEN = { '_' };


        /**
         * default maximum shingle size is 2.
         */
        public static int DEFAULT_MAX_SHINGLE_SIZE = 2;

        /**
         * The string to use when joining adjacent tokens to form a shingle
         */
        public static string TOKEN_SEPARATOR = "|";

        /**
         * By default, we output unigrams (individual tokens) as well as shingles
         * (token n-grams).
         */
        private bool outputUnigrams = true;

        /**
         * maximum shingle size (number of tokens)
         */
        private int maxShingleSize;

        /**
         * Constructs a ShingleFilter with the specified single size from the
         * TokenStream <code>input</code>
         *
         * @param input input stream
         * @param maxShingleSize maximum shingle size produced by the filter.
         */
        public ShingleFilter(TokenStream input, int maxShingleSize)
            : base(input)
        {
            setMaxShingleSize(maxShingleSize);
        }

        /**
       * Construct a ShingleFilter with default shingle size.
       *
       * @param input input stream
       */
        public ShingleFilter(TokenStream input)
            : base(input)
        {
            setMaxShingleSize(DEFAULT_MAX_SHINGLE_SIZE);
        }

        /**
         * Construct a ShingleFilter with the specified token type for shingle tokens.
         *
         * @param input input stream
         * @param tokenType token type for shingle tokens
         */
        public ShingleFilter(TokenStream input, string tokenType)
            : this(input)
        {
            setMaxShingleSize(DEFAULT_MAX_SHINGLE_SIZE);
            setTokenType(tokenType);
        }

        /**
       * Set the type of the shingle tokens produced by this filter.
       * (default: "shingle")
       *
       * @param tokenType token tokenType
       */
        public void setTokenType(string tokenType)
        {
            this.tokenType = tokenType;
        }

        /**
       * Shall the output stream contain the input tokens (unigrams) as well as
       * shingles? (default: true.)
       *
       * @param outputUnigrams Whether or not the output stream shall contain
       * the input tokens (unigrams)
       */
        public void setOutputUnigrams(bool outputUnigrams)
        {
            this.outputUnigrams = outputUnigrams;
        }

        /**
       * Set the max shingle size (default: 2)
       *
       * @param maxShingleSize max size of output shingles
       */
        public void setMaxShingleSize(int maxShingleSize)
        {
            if (maxShingleSize < 2)
            {
                throw new IllegalArgumentException("Max shingle size must be >= 2");
            }
            shingles = new StringBuffer[maxShingleSize];
            for (int i = 0; i < shingles.Length; i++)
            {
                shingles[i] = new StringBuffer();
            }
            this.maxShingleSize = maxShingleSize;
        }

        /**
       * Clear the StringBuffers that are used for storing the output shingles.
       */
        private void clearShingles()
        {
            for (int i = 0; i < shingles.Length; i++)
            {
                shingles[i].setLength(0);
            }
        }

        /* (non-Javadoc)
         * @see org.apache.lucene.analysis.TokenStream#next()
         */
#pragma warning disable 672
        public override Token next(Token reusableToken)
        {
            if (outputBuf.isEmpty())
            {
                fillOutputBuf(reusableToken);
            }
            Token nextToken = null;
            if (!outputBuf.isEmpty())
            {
                nextToken = (Token)outputBuf.removeFirst();
            }
            return nextToken;
        }
#pragma warning restore 672

        /**
       * Get the next token from the input stream and push it on the token buffer.
       * If we encounter a token with position increment > 1, we put filler tokens
       * on the token buffer.
       * <p/>
       * Returns null when the end of the input stream is reached.
       * @return the next token, or null if at end of input stream
       * @throws IOException if the input stream has a problem
       */
        private Token getNextToken(Token reusableToken)
        {
            if (tokenBuf.isEmpty())
            {
#pragma warning disable 612
                Token nextToken = input.next(reusableToken);
#pragma warning restore 612
                if (nextToken != null)
                {
                    for (int i = 1; i < nextToken.getPositionIncrement(); i++)
                    {
                        Token fillerToken = (Token)nextToken.clone();
                        // A filler token occupies no space
                        fillerToken.setEndOffset(fillerToken.startOffset());
                        fillerToken.setTermBuffer(FILLER_TOKEN, 0, FILLER_TOKEN.Length);
                        tokenBuf.add(fillerToken);
                    }
                    tokenBuf.add(nextToken.clone());
                    return getNextToken(nextToken);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return (Token)tokenBuf.removeFirst();
            }
        }

        /**
       * Fill the output buffer with new shingles.
       *
       * @throws IOException if there's a problem getting the next token
       */
        private void fillOutputBuf(Token token)
        {
            bool addedToken = false;
            /*
             * Try to fill the shingle buffer.
             */
            do
            {
                token = getNextToken(token);
                if (token != null)
                {
                    shingleBuf.add(token.clone());
                    if (shingleBuf.size() > maxShingleSize)
                    {
                        shingleBuf.removeFirst();
                    }
                    addedToken = true;
                }
                else
                {
                    break;
                }
            } while (shingleBuf.size() < maxShingleSize);

            /*
             * If no new token could be added to the shingle buffer, we have reached
             * the end of the input stream and have to discard the least recent token.
             */
            if (!addedToken)
            {
                if (shingleBuf.isEmpty())
                {
                    return;
                }
                else
                {
                    shingleBuf.removeFirst();
                }
            }

            clearShingles();

            int[] endOffsets = new int[shingleBuf.size()];
            for (int j = 0; j < endOffsets.Length; j++)
            {
                endOffsets[j] = 0;
            }

            int i = 0;
            Token shingle = null;
            for (Iterator it = shingleBuf.iterator(); it.hasNext(); )
            {
                shingle = (Token)it.next();
                for (int j = i; j < shingles.Length; j++)
                {
                    if (shingles[j].length() != 0)
                    {
                        shingles[j].append(TOKEN_SEPARATOR);
                    }
                    shingles[j].append(shingle.termBuffer(), 0, shingle.termLength());
                }

                endOffsets[i] = shingle.endOffset();
                i++;
            }

            if ((!shingleBuf.isEmpty()) && outputUnigrams)
            {
                Token unigram = (Token)shingleBuf.getFirst();
                unigram.setPositionIncrement(1);
                outputBuf.add(unigram);
            }

            /*
             * Push new tokens to the output buffer.
             */
            if (!shingleBuf.isEmpty())
            {
                Token firstShingle = (Token)shingleBuf.getFirst();
                shingle = (Token)firstShingle.clone();
                shingle.setType(tokenType);
            }
            for (int j = 1; j < shingleBuf.size(); j++)
            {
                shingle.setEndOffset(endOffsets[j]);
                StringBuffer buf = shingles[j];
                int termLength = buf.length();
                char[] termBuffer = shingle.termBuffer();
                if (termBuffer.Length < termLength)
                    termBuffer = shingle.resizeTermBuffer(termLength);
                buf.getChars(0, termLength, termBuffer, 0);
                shingle.setTermLength(termLength);
                if ((!outputUnigrams) && j == 1)
                {
                    shingle.setPositionIncrement(1);
                }
                else
                {
                    shingle.setPositionIncrement(0);
                }
                outputBuf.add(shingle.clone());
            }
        }
    }
}