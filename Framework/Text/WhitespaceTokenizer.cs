/*
* Licensed to the Apache Software Foundation (ASF) under one
* or more contributor license agreements.  See the NOTICE file
* distributed with this work for additional information
* regarding copyright ownership.  The ASF licenses this file
* to you under the Apache License, Version 2.0 (the
* "License"); you may not use this file except in compliance
* with the License.  You may obtain a copy of the License at
* 
*   http://www.apache.org/licenses/LICENSE-2.0
* 
* Unless required by applicable law or agreed to in writing,
* software distributed under the License is distributed on an
* "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
* KIND, either express or implied.  See the License for the
* specific language governing permissions and limitations
* under the License.
*/
using System.Collections.Generic;
using LinkMe.Framework.Text;

namespace org.apache.uima.annotator
{
	
	public class WhitespaceTokenizer
        : ITextAnnotator
	{
		private const int CH_SPECIAL = 0;
		private const int CH_NUMBER = 1;
		private const int CH_LETTER = 2;
		private const int CH_WHITESPACE = 4;
		private const int CH_PUNCTUATION = 5;
		private const int CH_NEWLINE = 6;
		private const int UNDEFINED = - 1;
		private const int INVALID_CHAR = 0;
	    private static readonly List<char> punctuations = new List<char>(new char[] {'.', '!', '?' });
		
		public void AnnotateText(TextFragment textFragment)
		{
			// get text content from the CAS
		    string textContent = textFragment.Text;
			
			int tokenStart = UNDEFINED;
			int currentCharPos = 0;
			int sentenceStart = 0;
			int nextCharType = UNDEFINED;
			char nextChar = (char) (INVALID_CHAR);
			
			while (currentCharPos < textContent.Length)
			{
				char currentChar = textContent[currentCharPos];
				int currentCharType = getCharacterType(currentChar);
				
				// get character class for current and next character
				if ((currentCharPos + 1) < textContent.Length)
				{
					nextChar = textContent[currentCharPos + 1];
					nextCharType = getCharacterType(nextChar);
				}
				else
				{
					nextCharType = UNDEFINED;
					nextChar = (char) (INVALID_CHAR);
				}
				
				// check if current character is a letter or number
				if (currentCharType == CH_LETTER || currentCharType == CH_NUMBER)
				{
					
					// check if it is the first letter of a token
					if (tokenStart == UNDEFINED)
					{
						// start new token here
						tokenStart = currentCharPos;
					}
				}
				// check if current character is a whitespace character
				else if (currentCharType == CH_WHITESPACE)
				{
					
					// terminate current token
					if (tokenStart != UNDEFINED)
					{
						// end of current word
                        createAnnotation(textFragment, tokenStart, currentCharPos);
						tokenStart = UNDEFINED;
					}
				}
				// check if current character is a special character
				else if (currentCharType == CH_SPECIAL)
				{
					
					// terminate current token
					if (tokenStart != UNDEFINED)
					{
						// end of current word
                        createAnnotation(textFragment, tokenStart, currentCharPos);
						tokenStart = UNDEFINED;
					}
					
					// create token for special character
                    createAnnotation(textFragment, currentCharPos, currentCharPos + 1);
				}
				// check if current character is new line character
				else if (currentCharType == CH_NEWLINE)
				{
					// terminate current token
					if (tokenStart != UNDEFINED)
					{
						// end of current word
                        createAnnotation(textFragment, tokenStart, currentCharPos);
						tokenStart = UNDEFINED;
					}
				}
				// check if current character is new punctuation character
				else if (currentCharType == CH_PUNCTUATION)
				{
					
					// terminates the current token
					if (tokenStart != UNDEFINED)
					{
                        createAnnotation(textFragment, tokenStart, currentCharPos);
						tokenStart = UNDEFINED;
					}
					
					// check next token type so see if we have a sentence end
					if (((nextCharType == CH_WHITESPACE) || (nextCharType == CH_NEWLINE)) && (punctuations.Contains(currentChar)))
					{
						// terminate sentence
						//createAnnotation(this.sentenceType, sentenceStart, currentCharPos + 1);
						sentenceStart = currentCharPos + 1;
					}
					// create token for punctuation character
                    createAnnotation(textFragment, currentCharPos, currentCharPos + 1);
				}
				// go to the next token
				currentCharPos++;
			} // end of character loop
			
			// we are at the end of the text terminate open token annotations
			if (tokenStart != UNDEFINED)
			{
				// end of current word
                createAnnotation(textFragment, tokenStart, currentCharPos);
				tokenStart = UNDEFINED;
			}
			
			// we are at the end of the text terminate open sentence annotations
			if (sentenceStart != UNDEFINED)
			{
				// end of current word
				//createAnnotation(this.sentenceType, sentenceStart, currentCharPos);
				sentenceStart = UNDEFINED;
			}
		}
		
		/// <summary> create an annotation of the given type in the CAS using startPos and
		/// endPos.
		/// 
		/// </summary>
        /// <param name="textFragment">text fragment
		/// </param>
		/// <param name="startPos">annotation start position
		/// </param>
		/// <param name="endPos">annotation end position
		/// </param>
        private static void createAnnotation(TextFragment textFragment, int startPos, int endPos)
		{
		    int len = endPos - startPos;
            string token = textFragment.Text.Substring(startPos, len);
			textFragment.Annotations.Add(new Annotation(token, startPos, len));
		}
		
		/// <summary> returns the character type of the given character. Possible character
		/// classes are: CH_LETTER for all letters CH_NUMBER for all numbers
		/// CH_WHITESPACE for all whitespace characters CH_PUNCTUATUATION for all
		/// punctuation characters CH_NEWLINE for all new line characters CH_SPECIAL
		/// for all other characters that are not in any of the groups above
		/// 
		/// </summary>
		/// <param name="character">aCharacter
		/// 
		/// </param>
		/// <returns> returns the character type of the given character
		/// </returns>
		private static int getCharacterType(char character)
		{
			
			switch ((int) System.Char.GetUnicodeCategory(character))
			{
				// letter characters
				case (sbyte) System.Globalization.UnicodeCategory.UppercaseLetter: 
				case (sbyte) System.Globalization.UnicodeCategory.LowercaseLetter: 
				case (sbyte) System.Globalization.UnicodeCategory.TitlecaseLetter: 
				case (sbyte) System.Globalization.UnicodeCategory.ModifierLetter: 
				case (sbyte) System.Globalization.UnicodeCategory.OtherLetter: 
				case (sbyte) System.Globalization.UnicodeCategory.NonSpacingMark: 
				case (sbyte) System.Globalization.UnicodeCategory.EnclosingMark: 
				case (sbyte) System.Globalization.UnicodeCategory.SpacingCombiningMark: 
				case (sbyte) System.Globalization.UnicodeCategory.PrivateUse: 
				case (sbyte) System.Globalization.UnicodeCategory.Surrogate: 
				case (sbyte) System.Globalization.UnicodeCategory.ModifierSymbol: 
					return CH_LETTER;
					
					// number characters
				
				case (sbyte) System.Globalization.UnicodeCategory.DecimalDigitNumber: 
				case (sbyte) System.Globalization.UnicodeCategory.LetterNumber: 
				case (sbyte) System.Globalization.UnicodeCategory.OtherNumber: 
					return CH_NUMBER;
					
					// whitespace characters
				
				case (sbyte) System.Globalization.UnicodeCategory.SpaceSeparator: 
					//case Character.CONNECTOR_PUNCTUATION:
					return CH_WHITESPACE;
				
				
				case (sbyte) System.Globalization.UnicodeCategory.DashPunctuation: 
				case (sbyte) System.Globalization.UnicodeCategory.InitialQuotePunctuation: 
				case (sbyte) System.Globalization.UnicodeCategory.FinalQuotePunctuation: 
				case (sbyte) System.Globalization.UnicodeCategory.OtherPunctuation: 
					return CH_PUNCTUATION;
				
				
				case (sbyte) System.Globalization.UnicodeCategory.LineSeparator: 
				case (sbyte) System.Globalization.UnicodeCategory.ParagraphSeparator: 
					return CH_NEWLINE;
				
				
				case (sbyte) System.Globalization.UnicodeCategory.Control: 
					if (character == '\n' || character == '\r')
					{
						return CH_NEWLINE;
					}
					else
					{
						return CH_SPECIAL;
					}
				
				default: 
					return CH_SPECIAL;
				
			}
		}
	}
}