using java.lang;

namespace org.apache.solr.spelling
{
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

  using ArrayList = java.util.ArrayList;
  using Collections = java.util.Collections;
  using Iterator = java.util.Iterator;
  using LinkedHashMap = java.util.LinkedHashMap;
  using List = java.util.List;
  using Map = java.util.Map;
  using NoSuchElementException = java.util.NoSuchElementException;

  using Token = org.apache.lucene.analysis.Token;

  /**
   * <p>
   * Given a list of possible Spelling Corrections for multiple mis-spelled words
   * in a query, This iterator returns Possible Correction combinations ordered by
   * reasonable probability that such a combination will return actual hits if
   * re-queried. This implementation simply ranks the Possible Combinations by the
   * sum of their component ranks.
   * </p>
   * 
   */
  public class PossibilityIterator : Iterator/*<RankedSpellPossibility>*/ {
	  private readonly List/*<List<SpellCheckCorrection>>*/ possibilityList = new ArrayList/*<List<SpellCheckCorrection>>*/();
	  private readonly List/*<RankedSpellPossibility>*/ rankedPossibilityList = new ArrayList/*<RankedSpellPossibility>*/();
	  private readonly Iterator/*<RankedSpellPossibility>*/ rankedPossibilityIterator;
	  private readonly int[] correctionIndex;
	  private bool done;

	  /**
	   * <p>
	   * We assume here that the passed-in inner LinkedHashMaps are already sorted
	   * in order of "Best Possible Correction".
	   * </p>
	   * 
	   * @param suggestions
	   */
	  public PossibilityIterator(Map/*<Token, LinkedHashMap<String, Integer>>*/ suggestions) {
		  for (var iter = suggestions.entrySet().iterator(); iter.hasNext();) {
		    Map.Entry/*<Token, LinkedHashMap<String, Integer>>*/ entry = (Map.Entry) iter.next();
			  Token token = (Token) entry.getKey();
			  List/*<SpellCheckCorrection>*/ possibleCorrections = new ArrayList/*<SpellCheckCorrection>*/();
			  for (var iter1 = ((LinkedHashMap)entry.getValue()).entrySet().iterator(); iter1.hasNext();) {
			    Map.Entry /*<String, Integer>*/ entry1 = (Map.Entry) iter1.next();
				  SpellCheckCorrection correction = new SpellCheckCorrection();
				  correction.setOriginal(token);
				  correction.setCorrection((string) entry1.getKey());
				  correction.setNumberOfOccurences((int) entry1.getValue());
				  possibleCorrections.add(correction);
			  }
			  possibilityList.add(possibleCorrections);
		  }

		  int wrapSize = possibilityList.size();
		  if (wrapSize == 0) {
			  done = true;
		  } else {
			  correctionIndex = new int[wrapSize];
			  for (int i = 0; i < wrapSize; i++) {
				  int suggestSize = ((List)possibilityList.get(i)).size();
				  if (suggestSize == 0) {
					  done = true;
					  break;
				  }
				  correctionIndex[i] = 0;
			  }
		  }

		  while (internalHasNext()) {
			  rankedPossibilityList.add(internalNext());
		  }
		  Collections.sort(rankedPossibilityList);
		  rankedPossibilityIterator = rankedPossibilityList.iterator();
	  }

	  private bool internalHasNext() {
		  return !done;
	  }

	  /**
	   * <p>
	   * This method is converting the independent LinkHashMaps containing various
	   * (silo'ed) suggestions for each mis-spelled word into individual
	   * "holistic query corrections", aka. "Spell Check Possibility"
	   * </p>
	   * <p>
	   * Rank here is the sum of each selected term's position in its respective
	   * LinkedHashMap.
	   * </p>
	   * 
	   * @return
	   */
	  private RankedSpellPossibility internalNext() {
		  if (done) {
			  throw new NoSuchElementException();
		  }

		  List/*<SpellCheckCorrection>*/ possibleCorrection = new ArrayList/*<SpellCheckCorrection>*/();
		  int rank = 0;
		  for (int i = 0; i < correctionIndex.Length; i++) {
			  List/*<SpellCheckCorrection>*/ singleWordPossibilities = (List) possibilityList.get(i);
			  SpellCheckCorrection singleWordPossibility = (SpellCheckCorrection) singleWordPossibilities.get(correctionIndex[i]);
			  rank += correctionIndex[i];

			  if (i == correctionIndex.Length - 1) {
				  correctionIndex[i]++;
				  if (correctionIndex[i] == singleWordPossibilities.size()) {
					  correctionIndex[i] = 0;
					  if (correctionIndex.Length == 1) {
						  done = true;
					  }
					  for (int ii = i - 1; ii >= 0; ii--) {
						  correctionIndex[ii]++;
						  if (correctionIndex[ii] >= ((List)possibilityList.get(ii)).size() && ii > 0) {
							  correctionIndex[ii] = 0;
						  } else {
							  break;
						  }
					  }
				  }
			  }
			  possibleCorrection.add(singleWordPossibility);
		  }
  		
		  if(correctionIndex[0] == ((List)possibilityList.get(0)).size())
		  {
			  done = true;
		  }

		  RankedSpellPossibility rsl = new RankedSpellPossibility();
		  rsl.setCorrections(possibleCorrection);
		  rsl.setRank(rank);
		  return rsl;
	  }

	  public bool hasNext() {
		  return rankedPossibilityIterator.hasNext();
	  }

	  public object next() {
		  return rankedPossibilityIterator.next();
	  }

	  public void remove() {
		  throw new UnsupportedOperationException();
	  }

  }
}