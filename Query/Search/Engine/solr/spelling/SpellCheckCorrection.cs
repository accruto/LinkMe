﻿namespace org.apache.solr.spelling
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

  using Token = org.apache.lucene.analysis.Token;

  public class SpellCheckCorrection {
	  private Token original;
	  private string correction;
	  private int numberOfOccurences;

	  public Token getOriginal() {
		  return original;
	  }

	  public void setOriginal(Token original) {
		  this.original = original;
	  }

	  public string getCorrection() {
		  return correction;
	  }

	  public void setCorrection(string correction) {
		  this.correction = correction;
	  }

	  public int getNumberOfOccurences() {
		  return numberOfOccurences;
	  }

	  public void setNumberOfOccurences(int numberOfOccurences) {
		  this.numberOfOccurences = numberOfOccurences;
	  }

  }
}