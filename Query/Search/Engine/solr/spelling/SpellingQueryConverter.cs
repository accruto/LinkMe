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

namespace org.apache.solr.spelling
{

  using IOException = java.io.IOException;
  using StringReader = java.io.StringReader;
  using ArrayList = java.util.ArrayList;
  using Collection = java.util.Collection;
  using Collections = java.util.Collections;
  using Matcher = java.util.regex.Matcher;
  using Pattern = java.util.regex.Pattern;

  using Token = org.apache.lucene.analysis.Token;
  using TokenStream = org.apache.lucene.analysis.TokenStream;
  using FlagsAttribute = org.apache.lucene.analysis.tokenattributes.FlagsAttribute;
  using PayloadAttribute = org.apache.lucene.analysis.tokenattributes.PayloadAttribute;
  using PositionIncrementAttribute = org.apache.lucene.analysis.tokenattributes.PositionIncrementAttribute;
  using TermAttribute = org.apache.lucene.analysis.tokenattributes.TermAttribute;
  using TypeAttribute = org.apache.lucene.analysis.tokenattributes.TypeAttribute;


  /**
   * Converts the query string to a Collection of Lucene tokens using a regular expression.
   * Boolean operators AND and OR are skipped.
   *
   * @since solr 1.3
   **/
  public class SpellingQueryConverter : QueryConverter  {

    /*
    * The following builds up a regular expression that matches productions
    * of the syntax for NMTOKEN as per the W3C XML Recommendation - with one
    * important exception (see below).
    *
    * http://www.w3.org/TR/2008/REC-xml-20081126/ - version used as reference
    *
    * http://www.w3.org/TR/REC-xml/#NT-Nmtoken
    *
    * An NMTOKEN is a series of one or more NAMECHAR characters, which is an
    * extension of the NAMESTARTCHAR character class.
    *
    * The EXCEPTION referred to above concerns the colon, which is legal in an
    * NMTOKEN, but cannot currently be used as a valid field name within Solr,
    * as it is used to delimit the field name from the query string.
    */

    static readonly string[] NAMESTARTCHAR_PARTS = {
            "A-Z_a-z", "\\xc0-\\xd6", "\\xd8-\\xf6", "\\xf8-\\u02ff",
            "\\u0370-\\u037d", "\\u037f-\\u1fff",
            "\\u200c-\\u200d", "\\u2070-\\u218f",
            "\\u2c00-\\u2fef", "\\u2001-\\ud7ff",
            "\\uf900-\\ufdcf", "\\ufdf0-\\ufffd"
    };
    static readonly string[] ADDITIONAL_NAMECHAR_PARTS = {
            "\\-.0-9\\xb7", "\\u0300-\\u036f", "\\u203f-\\u2040"
    };
    private const string SURROGATE_PAIR = "\\p{Cs}{2}";

    static SpellingQueryConverter() {
      java.lang.StringBuilder sb = new java.lang.StringBuilder();
      foreach (string part in NAMESTARTCHAR_PARTS)
        sb.append(part);
      foreach (string part in ADDITIONAL_NAMECHAR_PARTS)
        sb.append(part);
      string NMTOKEN = "([" + sb.toString() + "]|" + SURROGATE_PAIR + ")+";
      string PATTERN = "(?:(?!(" + NMTOKEN + ":|\\d+)))[\\p{L}_\\-0-9]+";
      QUERY_REGEX = Pattern.compile(PATTERN);
    }

    // previous version: Pattern.compile("(?:(?!(\\w+:|\\d+)))\\w+");
      protected static Pattern QUERY_REGEX;


    /**
     * Converts the original query string to a collection of Lucene Tokens.
     * @param original the original query string
     * @return a Collection of Lucene Tokens
     */
    public override Collection/*<Token>*/ convert(string original) {
      if (original == null) { // this can happen with q.alt = and no query
        return Collections.emptyList();
      }
      Collection/*<Token>*/ result = new ArrayList/*<Token>*/();
      //TODO: Extract the words using a simple regex, but not query stuff, and then analyze them to produce the token stream
      Matcher matcher = QUERY_REGEX.matcher(original);
      TokenStream stream;
      while (matcher.find()) {
        string word = matcher.group(0);
        if (word.Equals("AND") == false && word.Equals("OR") == false) {
          try {
            stream = analyzer.reusableTokenStream("", new StringReader(word));
            // TODO: support custom attributes
            TermAttribute termAtt = (TermAttribute) stream.addAttribute(typeof(TermAttribute));
            FlagsAttribute flagsAtt = (FlagsAttribute) stream.addAttribute(typeof(FlagsAttribute));
            TypeAttribute typeAtt = (TypeAttribute) stream.addAttribute(typeof(TypeAttribute));
            PayloadAttribute payloadAtt = (PayloadAttribute) stream.addAttribute(typeof(PayloadAttribute));
            PositionIncrementAttribute posIncAtt = (PositionIncrementAttribute) stream.addAttribute(typeof(PositionIncrementAttribute));
            stream.reset();
            while (stream.incrementToken()) {
              Token token = new Token();
              token.setTermBuffer(termAtt.termBuffer(), 0, termAtt.termLength());
              token.setStartOffset(matcher.start());
              token.setEndOffset(matcher.end());
              token.setFlags(flagsAtt.getFlags());
              token.setType(typeAtt.type());
              token.setPayload(payloadAtt.getPayload());
              token.setPositionIncrement(posIncAtt.getPositionIncrement());
              result.add(token);
            }
          }
#pragma warning disable 168
          catch (IOException e)
          {
          }
#pragma warning restore 168
        }
      }
      return result;
    }

  }
}