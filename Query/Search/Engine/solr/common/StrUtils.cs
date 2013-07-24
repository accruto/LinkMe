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

namespace org.apache.solr.common.util
{

  using List = java.util.List;
  using ArrayList = java.util.ArrayList;
  using LinkMe.Query.Search.Engine;

  /**
   * @version $Id: StrUtils.java 765199 2009-04-15 13:47:58Z koji $
   */
  public class StrUtils {
    public static readonly char[] HEX_DIGITS = { '0', '1', '2', '3', '4', '5', '6',
        '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

    /**
     * Split a string based on a separator, but don't split if it's inside
     * a string.  Assume '\' escapes the next char both inside and
     * outside strings.
     */
    public static List/*<String>*/ splitSmart(string s, char separator) {
      ArrayList/*<String>*/ lst = new ArrayList/*<String>*/(4);
      int pos=0, start=0, end=s.Length;
      char inString=(char) 0;
      char ch=(char) 0;
      while (pos < end) {
        char prevChar=ch;
        ch = s[pos++];
        if (ch=='\\') {    // skip escaped chars
          pos++;
        } else if (inString != 0 && ch==inString) {
          inString=(char) 0;
        } else if (ch=='\'' || ch=='"') {
          // If char is directly preceeded by a number or letter
          // then don't treat it as the start of a string.
          // Examples: 50" TV, or can't
          if (!java.lang.Character.isLetterOrDigit(prevChar)) {
            inString=ch;
          }
        } else if (ch==separator && inString==0) {
          lst.add(java.lang.String.instancehelper_substring(s, start,pos-1));
          start=pos;
        }
      }
      if (start < end) {
        lst.add(java.lang.String.instancehelper_substring(s, start,end));
      }

      /***
      if (SolrCore.log.isLoggable(Level.FINEST)) {
        SolrCore.log.trace("splitCommand=" + lst);
      }
      ***/

      return lst;
    }

    /** Splits a backslash escaped string on the separator.
     * <p>
     * Current backslash escaping supported:
     * <br> \n \t \r \b \f are escaped the same as a Java String
     * <br> Other characters following a backslash are produced verbatim (\c => c)
     *
     * @param s  the string to split
     * @param separator the separator to split on
     * @param decode decode backslash escaping
     */
    public static List/*<String>*/ splitSmart(string s, string separator, bool decode) {
      ArrayList/*<String>*/ lst = new ArrayList/*<String>*/(2);
      java.lang.StringBuilder sb = new java.lang.StringBuilder();
      int pos=0, end=s.Length;
      while (pos < end) {
        if (java.lang.String.instancehelper_startsWith(s, separator,pos)) {
          if (sb.length() > 0) {
            lst.add(sb.toString());
            sb=new java.lang.StringBuilder();
          }
          pos+=separator.Length;
          continue;
        }

        char ch = s[pos++];
        if (ch=='\\') {
          if (!decode) sb.append(ch);
          if (pos>=end) break;  // ERROR, or let it go?
          ch = s[pos++];
          if (decode) {
            switch(ch) {
              case 'n' : ch='\n'; break;
              case 't' : ch='\t'; break;
              case 'r' : ch='\r'; break;
              case 'b' : ch='\b'; break;
              case 'f' : ch='\f'; break;
            }
          }
        }

        sb.append(ch);
      }

      if (sb.length() > 0) {
        lst.add(sb.toString());
      }

      return lst;
    }




    public static List/*<String>*/ splitWS(string s, bool decode) {
      ArrayList/*<String>*/ lst = new ArrayList/*<String>*/(2);
      java.lang.StringBuilder sb = new java.lang.StringBuilder();
      int pos=0, end=s.Length;
      while (pos < end) {
        char ch = s[pos++];
        if (java.lang.Character.isWhitespace(ch)) {
          if (sb.length() > 0) {
            lst.add(sb.toString());
            sb=new java.lang.StringBuilder();
          }
          continue;
        }

        if (ch=='\\') {
          if (!decode) sb.append(ch);
          if (pos>=end) break;  // ERROR, or let it go?
          ch = s[pos++];
          if (decode) {
            switch(ch) {
              case 'n' : ch='\n'; break;
              case 't' : ch='\t'; break;
              case 'r' : ch='\r'; break;
              case 'b' : ch='\b'; break;
              case 'f' : ch='\f'; break;
            }
          }
        }

        sb.append(ch);
      }

      if (sb.length() > 0) {
        lst.add(sb.toString());
      }

      return lst;
    }

    /**
     * Splits file names separated by comma character.
     * File names can contain comma characters escaped by backslash '\'
     *
     * @param fileNames the string containing file names
     * @return a list of file names with the escaping backslashed removed
     */
    public static List/*<String>*/ splitFileNames(string fileNames) {
    if (fileNames == null)
      return java.util.Collections.emptyList();

    List/*<String>*/ result = new ArrayList/*<String>*/();
    foreach (string file in fileNames.split("(?<!\\\\),")) {
      result.add(file.replaceAll("\\\\(?=,)", ""));
    }

    return result;
  }

  }
}