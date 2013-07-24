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

    using IOException = java.io.IOException;
    using List = java.util.List;
    using Set = java.util.Set;

    using CharArraySet = lucene.analysis.CharArraySet;
    using StopAnalyzer = lucene.analysis.StopAnalyzer;
    using TokenStream = lucene.analysis.TokenStream;
    using ResourceLoader = common.ResourceLoader;
    using StrUtils = common.util.StrUtils;
    using ResourceLoaderAware = util.plugin.ResourceLoaderAware;

    /**
     * Constructs a CommonGramsFilter
     */

    /*
     * This is pretty close to a straight copy from StopFilterFactory
     */
    public class CommonGramsFilterFactory : BaseTokenFilterFactory,
        ResourceLoaderAware
    {

        public void inform(ResourceLoader loader)
        {
            string commonWordFiles = (string)args.get("words");
            ignoreCase = getBoolean("ignoreCase", false);

            if (commonWordFiles != null)
            {
                try
                {
                    List/*<String>*/ files = StrUtils.splitFileNames(commonWordFiles);
                    if (commonWords == null && files.size() > 0)
                    {
                        //default stopwords list has 35 or so words, but maybe don't make it that big to start
                        commonWords = new CharArraySet(files.size() * 10, ignoreCase);
                    }
                    for (var iter = files.iterator(); iter.hasNext(); )
                    {
                        string file = (string)iter.next();
                        List/*<String>*/ wlist = loader.getLines(file.Trim());
                        //TODO: once StopFilter.makeStopSet(List) method is available, switch to using that so we can avoid a toArray() call
                        commonWords.addAll(CommonGramsFilter.makeCommonSet((string[])wlist.toArray(new string[0]), ignoreCase));
                    }
                }
                catch (IOException e)
                {
                    throw new System.ApplicationException("Unexpected exception", e);
                }
            }
            else
            {
#pragma warning disable 612
                commonWords = (CharArraySet)CommonGramsFilter.makeCommonSet(StopAnalyzer.ENGLISH_STOP_WORDS, ignoreCase);
#pragma warning restore 612
            }
        }

        //Force the use of a char array set, as it is the most performant, although this may break things if Lucene ever goes away from it.  See SOLR-1095
        private CharArraySet commonWords;
        private bool ignoreCase;

        public bool isIgnoreCase()
        {
            return ignoreCase;
        }

        public Set getCommonWords()
        {
            return commonWords;
        }

        public override TokenStream create(TokenStream input)
        {
            CommonGramsFilter commonGrams = new CommonGramsFilter(input, commonWords, ignoreCase);
            return commonGrams;
        }
    }
}