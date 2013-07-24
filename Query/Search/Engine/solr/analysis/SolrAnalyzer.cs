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

  using org.apache.lucene.analysis;

  using Reader = java.io.Reader;

  /**
   * @version $Id: SolrAnalyzer.java 804726 2009-08-16 17:28:58Z yonik $
   */
  public abstract class SolrAnalyzer : Analyzer {
    int posIncGap=0;
    
    public void setPositionIncrementGap(int gap) {
      posIncGap=gap;
    }

    public override int getPositionIncrementGap(string fieldName) {
      return posIncGap;
    }

    /** wrap the reader in a CharStream, if appropriate */
    public virtual Reader charStream(Reader reader){
      return reader;
    }

    public override TokenStream tokenStream(string fieldName, Reader reader) {
      return getStream(fieldName, reader).getTokenStream();
    }

    public class TokenStreamInfo {
      private readonly Tokenizer tokenizer;
      private readonly TokenStream tokenStream;
      public TokenStreamInfo(Tokenizer tokenizer, TokenStream tokenStream) {
        this.tokenizer = tokenizer;
        this.tokenStream = tokenStream;
      }
      public Tokenizer getTokenizer() { return tokenizer; }
      public TokenStream getTokenStream() { return tokenStream; }
    }


    public abstract TokenStreamInfo getStream(string fieldName, Reader reader);

    public override TokenStream reusableTokenStream(string fieldName, Reader reader) {
      // if (true) return tokenStream(fieldName, reader);
      TokenStreamInfo tsi = (TokenStreamInfo)getPreviousTokenStream();
      if (tsi != null) {
        tsi.getTokenizer().reset(charStream(reader));
        // the consumer will currently call reset() on the TokenStream to hit all the filters.
        // this isn't necessarily guaranteed by the APIs... but is currently done
        // by lucene indexing in DocInverterPerField, and in the QueryParser
        return tsi.getTokenStream();
      } else {
        tsi = getStream(fieldName, reader);
        setPreviousTokenStream(tsi);
        return tsi.getTokenStream();
      }
    }
  }
}