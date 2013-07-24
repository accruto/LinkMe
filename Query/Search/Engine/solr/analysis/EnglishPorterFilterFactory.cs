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

  using CharArraySet = org.apache.lucene.analysis.CharArraySet;
  using TokenStream = org.apache.lucene.analysis.TokenStream;
  using ResourceLoader = org.apache.solr.common.ResourceLoader;
  using StrUtils = org.apache.solr.common.util.StrUtils;
  using ResourceLoaderAware = org.apache.solr.util.plugin.ResourceLoaderAware;

  using IOException = java.io.IOException;
  using File = java.io.File;
  using List = java.util.List;

  /**
   * @version $Id: EnglishPorterFilterFactory.java 804726 2009-08-16 17:28:58Z yonik $
   *
   * @deprecated Use SnowballPorterFilterFactory with language="English" instead
   */
  public class EnglishPorterFilterFactory : BaseTokenFilterFactory, ResourceLoaderAware {
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

    public override TokenStream create(TokenStream input) {
      return new EnglishPorterFilter(input, protectedWords);
    }

  }


  /**
   * English Porter2 filter that doesn't use reflection to
   * adapt lucene to the snowball stemmer code.
   */
  //@Deprecated
  class EnglishPorterFilter : SnowballPorterFilter {
    public EnglishPorterFilter(TokenStream source, CharArraySet protWords) :
      base(source, new org.tartarus.snowball.ext.EnglishStemmer(), protWords)
    {}
  }
}