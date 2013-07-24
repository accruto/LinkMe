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

using System.Linq;

namespace org.apache.solr.analysis
{

  using ResourceLoader = org.apache.solr.common.ResourceLoader;
  using StrUtils = org.apache.solr.common.util.StrUtils;
  using ResourceLoaderAware = org.apache.solr.util.plugin.ResourceLoaderAware;
  using StopFilter = org.apache.lucene.analysis.StopFilter;
  using StopAnalyzer = org.apache.lucene.analysis.StopAnalyzer;
  using TokenStream = org.apache.lucene.analysis.TokenStream;
  using CharArraySet = org.apache.lucene.analysis.CharArraySet;

  using List = java.util.List;
  using Set = java.util.Set;
  using IOException = java.io.IOException;

  /**
   * @version $Id: StopFilterFactory.java 761036 2009-04-01 20:07:44Z gsingers $
   */
  public class StopFilterFactory : BaseTokenFilterFactory, ResourceLoaderAware {

    public void inform(ResourceLoader loader) {
      string stopWordFiles = (string) args.get("words");
      ignoreCase = getBoolean("ignoreCase",false);
      enablePositionIncrements = getBoolean("enablePositionIncrements",false);

      if (stopWordFiles != null) {
        try {
          List/*<String>*/ files = StrUtils.splitFileNames(stopWordFiles);
            if (stopWords == null && files.size() > 0){
              //default stopwords list has 35 or so words, but maybe don't make it that big to start
              stopWords = new CharArraySet(files.size() * 10, ignoreCase);
            }
            for (var iter = files.iterator(); iter.hasNext();) {
              string file = (string) iter.next();
              List/*<String>*/ wlist = loader.getLines(file.Trim());
              //TODO: once StopFilter.makeStopSet(List) method is available, switch to using that so we can avoid a toArray() call
              stopWords.addAll(StopFilter.makeStopSet((string[])wlist.toArray(new string[0]), ignoreCase));
            }
        } catch (IOException e) {
            throw new System.ApplicationException("Unexpected exception", e);
        }
      } else {
          //explicitly remove 'it' from the stop words list.
          //add in special case of 'careerone'

#pragma warning disable 612
          var stopWordsList = StopAnalyzer.ENGLISH_STOP_WORDS;
#pragma warning restore 612
          stopWordsList = stopWordsList.Where(s => s != "it").Concat(new[] { "careerone" }).ToArray();
          
          stopWords = (CharArraySet) StopFilter.makeStopSet(stopWordsList, ignoreCase);
      }
    }
    //Force the use of a char array set, as it is the most performant, although this may break things if Lucene ever goes away from it.  See SOLR-1095
    private CharArraySet stopWords;
    private bool ignoreCase;
    private bool enablePositionIncrements;

    public bool isEnablePositionIncrements() {
      return enablePositionIncrements;
    }

    public bool isIgnoreCase() {
      return ignoreCase;
    }

    public Set getStopWords() {
      return stopWords;
    }

    public override TokenStream create(TokenStream input) {
#pragma warning disable 612
      StopFilter stopFilter = new StopFilter(input, stopWords, ignoreCase);
#pragma warning restore 612
      stopFilter.setEnablePositionIncrements(enablePositionIncrements);
      return stopFilter;
    }
  }
}