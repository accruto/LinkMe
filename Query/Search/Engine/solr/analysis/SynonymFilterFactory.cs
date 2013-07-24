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

using java.io;

namespace org.apache.solr.analysis
{

  using Token = org.apache.lucene.analysis.Token;
  using TokenStream = org.apache.lucene.analysis.TokenStream;
  using ResourceLoader = org.apache.solr.common.ResourceLoader;
  using StrUtils = org.apache.solr.common.util.StrUtils;
  using ResourceLoaderAware = org.apache.solr.util.plugin.ResourceLoaderAware;

  using Reader = java.io.Reader;
  using StringReader = java.io.StringReader;
  using ArrayList = java.util.ArrayList;
  using List = java.util.List;

  /**
   * @version $Id: SynonymFilterFactory.java 712457 2008-11-09 01:24:11Z koji $
   */
  public class SynonymFilterFactory : BaseTokenFilterFactory, ResourceLoaderAware {
    public void inform(ResourceLoader loader) {
      string synonyms = (string) args.get("synonyms");

      bool ignoreCase = getBoolean("ignoreCase", false);
      bool expand = getBoolean("expand", true);

      //String tf = args.get("tokenizerFactory");
      //TokenizerFactory tokFactory = null;
      //if( tf != null ){
      //  tokFactory = loadTokenizerFactory( loader, tf, args );
      //}

      if (synonyms != null) {
        List/*<String>*/ wlist=null;
        try {
          File synonymFile = new File(synonyms);
          if (synonymFile.exists()) {
            wlist = loader.getLines(synonyms);
          } else  {
            List/*<String>*/ files = StrUtils.splitFileNames(synonyms);
            wlist = new ArrayList/*<String>*/();
            for (var iter = files.iterator(); iter.hasNext();) {
              string file = (string) iter.next();
              List/*<String>*/ lines = loader.getLines(file.Trim());
              wlist.addAll(lines);
            }
          }
        } catch (IOException e) {
            throw new System.ApplicationException("Unexpected exception", e);
        }
        synMap = new SynonymMap(ignoreCase);
        parseRules(wlist, synMap, "=>", ",", expand,null);
      }
    }

    private SynonymMap synMap;

    public static void parseRules(List/*<String>*/ rules, SynonymMap map, string mappingSep,
      string synSep, bool expansion, TokenizerFactory tokFactory) {
      int count=0;
      for ( var iter = rules.iterator(); iter.hasNext();) {
        // To use regexes, we need an expression that specifies an odd number of chars.
        // This can't really be done with string.split(), and since we need to
        // do unescaping at some point anyway, we wouldn't be saving any effort
        // by using regexes.

        string rule = (string)iter.next();
        List/*<String>*/ mapping = StrUtils.splitSmart(rule, mappingSep, false);

        List/*<List<String>>*/ source;
        List/*<List<String>>*/ target;

        if (mapping.size() > 2) {
            throw new System.ApplicationException("Invalid Synonym Rule:" + rule);
        } else if (mapping.size()==2) {
          source = getSynList((string) mapping.get(0), synSep, tokFactory);
          target = getSynList((string) mapping.get(1), synSep, tokFactory);
        } else {
          source = getSynList((string) mapping.get(0), synSep, tokFactory);
          if (expansion) {
            // expand to all arguments
            target = source;
          } else {
            // reduce to first argument
            target = new ArrayList/*<List<String>>*/(1);
            target.add(source.get(0));
          }
        }

        bool includeOrig=false;
        for (var fromIter = source.iterator(); fromIter.hasNext();) {
          List /*<String>*/ fromToks = (List) fromIter.next();
          count++;
          for (var toIter = target.iterator(); toIter.hasNext();) {
            List /*<String>*/ toToks = (List) toIter.next();
            map.add(fromToks,
                    SynonymMap.makeTokens(toToks),
                    includeOrig,
                    true
            );
          }
        }
      }
    }

    // a , b c , d e f => [[a],[b,c],[d,e,f]]
    private static List/*<List<String>>*/ getSynList(string str, string separator, TokenizerFactory tokFactory) {
      List/*<String>*/ strList = StrUtils.splitSmart(str, separator, false);
      // now split on whitespace to get a list of token strings
      List/*<List<String>>*/ synList = new ArrayList/*<List<String>>*/();
      for (var iter = strList.iterator(); iter.hasNext();) {
        string toks = (string) iter.next();
        List/*<String>*/ tokList = tokFactory == null ?
          StrUtils.splitWS(toks, true) : splitByTokenizer(toks, tokFactory);
        synList.add(tokList);
      }
      return synList;
    }

    private static List/*<String>*/ splitByTokenizer(string source, TokenizerFactory tokFactory){
      StringReader reader = new StringReader( source );
      TokenStream ts = loadTokenizer(tokFactory, reader);
      List/*<String>*/ tokList = new ArrayList/*<String>*/();
      try {
#pragma warning disable 612
          for (Token token = ts.next(); token != null; token = ts.next()) {
#pragma warning restore 612
          string text = new string(token.termBuffer(), 0, token.termLength());
          if( text.Length > 0 )
            tokList.add( text );
        }
      } catch (IOException e) {
        throw new System.ApplicationException("Unexpected exception.", e);
      }
      finally{
        reader.close();
      }
      return tokList;
    }

    private static TokenStream loadTokenizer(TokenizerFactory tokFactory, Reader reader){
      return tokFactory.create( reader );
    }

    public SynonymMap getSynonymMap() {
      return synMap;
    }

    public override TokenStream create(TokenStream input) {
      return new SynonymFilter(input,synMap);
    }
  }
}