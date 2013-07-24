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

  using TokenStream = org.apache.lucene.analysis.TokenStream;
  using CharStream = org.apache.lucene.analysis.CharStream;
  using CharReader = org.apache.lucene.analysis.CharReader;
  using Tokenizer = org.apache.lucene.analysis.Tokenizer;

  using Reader = java.io.Reader;

  /**
   * @version $Id: TokenizerChain.java 805263 2009-08-18 02:50:49Z yonik $
   */

  //
  // An analyzer that uses a tokenizer and a list of token filters to
  // create a TokenStream.
  //
  public class TokenizerChain : SolrAnalyzer {
    readonly private CharFilterFactory[] charFilters;
    readonly private TokenizerFactory tokenizer;
    readonly private TokenFilterFactory[] filters;

    public TokenizerChain(TokenizerFactory tokenizer, TokenFilterFactory[] filters) : this(null,tokenizer,filters){
      
    }

    public TokenizerChain(CharFilterFactory[] charFilters, TokenizerFactory tokenizer, TokenFilterFactory[] filters) {
      this.charFilters = charFilters;
      this.tokenizer = tokenizer;
      this.filters = filters;
    }

    public CharFilterFactory[] getCharFilterFactories() { return charFilters; }
    public TokenizerFactory getTokenizerFactory() { return tokenizer; }
    public TokenFilterFactory[] getTokenFilterFactories() { return filters; }

    public override Reader charStream(Reader reader){
      if( charFilters != null && charFilters.Length > 0 ){
        CharStream cs = CharReader.get( reader );
        for (int i=0; i<charFilters.Length; i++) {
          cs = charFilters[i].create(cs);
        }
        reader = cs;
      }
      return reader;
    }

    public override TokenStreamInfo getStream(string fieldName, Reader reader) {
      Tokenizer tk = tokenizer.create(charStream(reader));
      TokenStream ts = tk;
      for (int i=0; i<filters.Length; i++) {
        ts = filters[i].create(ts);
      }
      return new TokenStreamInfo(tk,ts);
    }

    public override string toString() {
      java.lang.StringBuilder sb = new java.lang.StringBuilder("TokenizerChain(");
      foreach (CharFilterFactory filter in charFilters) {
        sb.append(filter);
        sb.append(", ");
      }
      sb.append(tokenizer);
      foreach (TokenFilterFactory filter in filters) {
        sb.append(", ");
        sb.append(filter);
      }
      sb.append(')');
      return sb.toString();
    }

  }
}