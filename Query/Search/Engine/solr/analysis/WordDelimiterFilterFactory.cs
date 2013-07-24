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
  using CharArraySet = org.apache.lucene.analysis.CharArraySet;

  using ResourceLoaderAware = org.apache.solr.util.plugin.ResourceLoaderAware;
  using ResourceLoader = org.apache.solr.common.ResourceLoader;
  using StrUtils = org.apache.solr.common.util.StrUtils;


  using Map = java.util.Map;
  using File = java.io.File;
  using List = java.util.List;
  using IOException = java.io.IOException;


  /**
   * @version $Id: WordDelimiterFilterFactory.java 793090 2009-07-10 19:40:33Z yonik $
   */
  public class WordDelimiterFilterFactory : BaseTokenFilterFactory, ResourceLoaderAware {
    public static readonly string PROTECTED_TOKENS = "protected";

    public void inform(ResourceLoader loader) {
      string wordFiles = (string) args.get(PROTECTED_TOKENS);
      if (wordFiles != null) {  
        try {
          File protectedWordFiles = new File(wordFiles);
          if (protectedWordFiles.exists()) {
            List/*<String>*/ wlist = loader.getLines(wordFiles);
            //This cast is safe in Lucene
            protectedWords = new CharArraySet(wlist, false);//No need to go through StopFilter as before, since it just uses a List internally
          } else  {
            List/*<String>*/ files = StrUtils.splitFileNames(wordFiles);
            for (var iter = files.iterator(); iter.hasNext();) {
              string file = (string) iter.next();
              List/*<String>*/ wlist = loader.getLines(file.Trim());
              if (protectedWords == null)
                protectedWords = new CharArraySet(wlist, false);
              else
                protectedWords.addAll(wlist);
            }
          }
        } catch (IOException e) {
            throw new System.ApplicationException("Unexpected exception.", e);
        }
      }
    }

    private CharArraySet protectedWords = null;

    int generateWordParts=0;
    int generateNumberParts=0;
    int catenateWords=0;
    int catenateNumbers=0;
    int catenateAll=0;
    int splitOnCaseChange=0;
    int splitOnNumerics=0;
    int preserveOriginal=0;
    int stemEnglishPossessive=0;

    public override void init(Map/*<String, String>*/ args) {
      base.init(args);
      generateWordParts = getInt("generateWordParts", 1);
      generateNumberParts = getInt("generateNumberParts", 1);
      catenateWords = getInt("catenateWords", 0);
      catenateNumbers = getInt("catenateNumbers", 0);
      catenateAll = getInt("catenateAll", 0);
      splitOnCaseChange = getInt("splitOnCaseChange", 1);
      splitOnNumerics = getInt("splitOnNumerics", 1);
      preserveOriginal = getInt("preserveOriginal", 0);
      stemEnglishPossessive = getInt("stemEnglishPossessive", 1);
    }

    public override TokenStream create(TokenStream input) {
      return new WordDelimiterFilter(input,
                                     generateWordParts, generateNumberParts,
                                     catenateWords, catenateNumbers, catenateAll,
                                     splitOnCaseChange, preserveOriginal,
                                     splitOnNumerics, stemEnglishPossessive, protectedWords);
    }
  }
}