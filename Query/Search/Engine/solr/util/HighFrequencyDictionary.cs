/*
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

namespace org.apache.solr.util
{

  using IOException = java.io.IOException;
  using Iterator = java.util.Iterator;

  using IndexReader = org.apache.lucene.index.IndexReader;
  using Term = org.apache.lucene.index.Term;
  using TermEnum = org.apache.lucene.index.TermEnum;
  using Dictionary = org.apache.lucene.search.spell.Dictionary;
  using StringHelper = org.apache.lucene.util.StringHelper;

  /**
   * HighFrequencyDictionary: terms taken from the given field
   * of a Lucene index, which appear in a number of documents
   * above a given threshold.
   *
   * When using IndexReader.terms(Term) the code must not call next() on TermEnum
   * as the first call to TermEnum, see: http://issues.apache.org/jira/browse/LUCENE-6
   *
   * Threshold is a value in [0..1] representing the minimum
   * number of documents (of the total) where a term should appear.
   * 
   * Based on LuceneDictionary.
   */
  public class HighFrequencyDictionary : Dictionary {
    private readonly IndexReader reader;
    private readonly string field;
    private readonly float thresh;

    public HighFrequencyDictionary(IndexReader reader, string field, float thresh) {
      this.reader = reader;
      this.field = StringHelper.intern(field);
      this.thresh = thresh;
    }

    public Iterator getWordsIterator() {
      return new HighFrequencyIterator(this);
    }

    class HighFrequencyIterator : TermFreqIterator {
      private readonly HighFrequencyDictionary dict;
      private readonly TermEnum termEnum;
      private Term actualTerm;
      private int actualFreq;
      private bool hasNextCalled;
      private readonly int minNumDocs;

      public HighFrequencyIterator(HighFrequencyDictionary dict)
      {
        this.dict = dict;

        try {
          termEnum = dict.reader.terms(new Term(dict.field, ""));
          minNumDocs = (int)(dict.thresh * (float)dict.reader.numDocs());
        } catch (IOException e) {
          throw new java.lang.RuntimeException(e);
        }
      }

      private bool isFrequent(Term term) {
        try {
          return dict.reader.docFreq(term) >= minNumDocs;
        } catch (IOException e) {
          throw new java.lang.RuntimeException(e);
        }
      }

      public object next() {
        if (!hasNextCalled) {
          hasNext();
        }
        hasNextCalled = false;

        try {
          termEnum.next();
        } catch (IOException e) {
          throw new java.lang.RuntimeException(e);
        }

        return (actualTerm != null) ? actualTerm.text() : null;
      }

      public float freq() {
        return actualFreq;
      }


      public bool hasNext() {
        if (hasNextCalled) {
          return actualTerm != null;
        }
        hasNextCalled = true;

        do {
          actualTerm = termEnum.term();
          actualFreq = termEnum.docFreq();

          // if there are no words return false
          if (actualTerm == null) {
            return false;
          }

          string currentField = actualTerm.field();

          // if the next word doesn't have the same field return false
          if (currentField != dict.field) {   // intern'd comparison
            actualTerm = null;
            return false;
          }

          // got a valid term, does it pass the threshold?
          if (isFrequent(actualTerm)) {
            return true;
          }

          // term not up to threshold
          try {
            termEnum.next();
          } catch (IOException e) {
            throw new java.lang.RuntimeException(e);
          }

        } while (true);
      }

      public void remove() {
        throw new java.lang.UnsupportedOperationException();
      }
    }
  }
}