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

namespace org.apache.solr.analysis
{

    using Encoder = org.apache.commons.codec.Encoder;
    using TokenFilter = org.apache.lucene.analysis.TokenFilter;
    using TokenStream = org.apache.lucene.analysis.TokenStream;
    using TermAttribute = org.apache.lucene.analysis.tokenattributes.TermAttribute;
    using PositionIncrementAttribute = org.apache.lucene.analysis.tokenattributes.PositionIncrementAttribute;

    /**
     * Create tokens for phonetic matches.  See:
     * http://jakarta.apache.org/commons/codec/api-release/org/apache/commons/codec/language/package-summary.html
     *
     * @version $Id: PhoneticFilter.java 804726 2009-08-16 17:28:58Z yonik $
     */
    public class PhoneticFilter : TokenFilter 
    {
      protected bool inject = true; 
      protected Encoder encoder = null;
      protected string name = null;
      
      protected State save = null;
      private readonly TermAttribute termAtt;
      private readonly PositionIncrementAttribute posAtt;

      public PhoneticFilter(TokenStream input, Encoder encoder, string name, bool inject) : base(input) {
        this.encoder = encoder;
        this.name = name;
        this.inject = inject;
        this.termAtt = (TermAttribute) addAttribute(typeof(TermAttribute));
        this.posAtt = (PositionIncrementAttribute) addAttribute(typeof(PositionIncrementAttribute));    
      }

      public override bool incrementToken() {
        if( save != null ) {
          // clearAttributes();  // not currently necessary
          restoreState(save);
          save = null;
          return true;
        }

        if (!input.incrementToken()) return false;

        // pass through zero-length terms
        if (termAtt.termLength()==0) return true;

        string value = termAtt.term();
        string phonetic = null;
        try {
         string v = encoder.encode(value).ToString();
         if (v.Length > 0 && !value.Equals(v)) phonetic = v;
        }
#pragma warning disable 168
        catch (java.lang.Exception ignored) { } // just use the direct text
#pragma warning restore 168

        if (phonetic == null) return true;

        if (!inject) {
          // just modify this token
          termAtt.setTermBuffer(phonetic);
          return true;
        }

        // We need to return both the original and the phonetic tokens.
        // to avoid a orig=captureState() change_to_phonetic() saved=captureState()  restoreState(orig)
        // we return the phonetic alternative first

        int origOffset = posAtt.getPositionIncrement();
        posAtt.setPositionIncrement(0);
        save = captureState();

        posAtt.setPositionIncrement(origOffset);
        termAtt.setTermBuffer(phonetic);
        return true;
      }

      public override void reset() {
        input.reset();
        save = null;
      }
    }
}