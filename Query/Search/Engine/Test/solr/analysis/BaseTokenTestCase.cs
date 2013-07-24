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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace org.apache.solr.analysis
{

  using ArrayList = java.util.ArrayList;
  using Arrays = java.util.Arrays;
  using Iterator = java.util.Iterator;
  using List = java.util.List;

  using Token = org.apache.lucene.analysis.Token;
  using TokenStream = org.apache.lucene.analysis.TokenStream;

  /**
   * General token testing helper functions
   */
  public abstract class BaseTokenTestCase
  {
    public static string tsToString(TokenStream input) {
      java.lang.StringBuilder output = new java.lang.StringBuilder();
#pragma warning disable 612
      Token t = input.next();
#pragma warning restore 612
      if (null != t)
        output.append(new string(t.termBuffer(), 0, t.termLength()));

#pragma warning disable 612
      for (t = input.next(); null != t; t = input.next()) {
#pragma warning restore 612
          output.append(" ").append(new string(t.termBuffer(), 0, t.termLength()));
      }
      input.close();
      return output.toString();
    }

    public List/*<String>*/ tok2str(java.lang.Iterable/*<Token>*/ tokLst) {
      ArrayList/*<String>*/ lst = new ArrayList/*<String>*/();
      for ( var iter = tokLst.iterator(); iter.hasNext(); ) {
        Token t = (Token) iter.next();
        lst.add( new string(t.termBuffer(), 0, t.termLength()));
      }
      return lst;
    }


    public void assertTokEqual(List/*<Token>*/ a, List/*<Token>*/ b) {
      assertTokEq(a,b,false);
      assertTokEq(b,a,false);
    }

    public void assertTokEqualOff(List/*<Token>*/ a, List/*<Token>*/ b) {
      assertTokEq(a,b,true);
      assertTokEq(b,a,true);
    }

    private void assertTokEq(List/*<Token>*/ a, List/*<Token>*/ b, bool checkOff) {
      int pos=0;
      for (Iterator iter = a.iterator(); iter.hasNext();) {
        Token tok = (Token)iter.next();
        pos += tok.getPositionIncrement();
        if (!tokAt(b, new string(tok.termBuffer(), 0, tok.termLength()), pos
                , checkOff ? tok.startOffset() : -1
                , checkOff ? tok.endOffset() : -1
                )) 
        {
          Assert.Fail(a + "!=" + b);
        }
      }
    }

    public bool tokAt(List/*<Token>*/ lst, string val, int tokPos, int startOff, int endOff) {
      int pos=0;
      for (Iterator iter = lst.iterator(); iter.hasNext();) {
        Token tok = (Token)iter.next();
        pos += tok.getPositionIncrement();
        if (pos==tokPos && new string(tok.termBuffer(), 0, tok.termLength()).Equals(val)
            && (startOff==-1 || tok.startOffset()==startOff)
            && (endOff  ==-1 || tok.endOffset()  ==endOff  )
             )
        {
          return true;
        }
      }
      return false;
    }


    /***
     * Return a list of tokens according to a test string format:
     * a b c  =>  returns List<Token> [a,b,c]
     * a/b   => tokens a and b share the same spot (b.positionIncrement=0)
     * a,3/b/c => a,b,c all share same position (a.positionIncrement=3, b.positionIncrement=0, c.positionIncrement=0)
     * a,1,10,11  => "a" with positionIncrement=1, startOffset=10, endOffset=11
     */
    public List/*<Token>*/ tokens(string str) {
      string[] arr = str.Split(' ');
      List/*<Token>*/ result = new ArrayList/*<Token>*/();
      for (int i=0; i<arr.Length; i++) {
        string[] toks = arr[i].Split('/');
        string[] @params = toks[0].Split(',');

        int posInc;
        int start;
        int end;

        if (@params.Length > 1) {
          posInc = java.lang.Integer.parseInt(@params[1]);
        } else {
          posInc = 1;
        }

        if (@params.Length > 2) {
          start = java.lang.Integer.parseInt(@params[2]);
        } else {
          start = 0;
        }

        if (@params.Length > 3) {
          end = java.lang.Integer.parseInt(@params[3]);
        } else {
          end = start + @params[0].Length;
        }

        Token t = new Token(@params[0],start,end,"TEST");
        t.setPositionIncrement(posInc);
        
        result.add(t);
        for (int j=1; j<toks.Length; j++) {
          t = new Token(toks[j],0,0,"TEST");
          t.setPositionIncrement(0);
          result.add(t);
        }
      }
      return result;
    }

    //------------------------------------------------------------------------
    // These may be useful beyond test cases...
    //------------------------------------------------------------------------

    static List/*<Token>*/ getTokens(TokenStream tstream) {
      List/*<Token>*/ tokens = new ArrayList/*<Token>*/();
      while (true) {
#pragma warning disable 612
        Token t = tstream.next();
#pragma warning restore 612
        if (t == null) break;
        tokens.add(t);
      }
      return tokens;
    }

    public class IterTokenStream : TokenStream {
      Iterator/*<Token>*/ toks;
      public IterTokenStream(params Token[] toks) {
        this.toks = Arrays.asList(toks).iterator();
      }
      public IterTokenStream(java.lang.Iterable/*<Token>*/ toks) {
        this.toks = toks.iterator();
      }
      public IterTokenStream(Iterator/*<Token>*/ toks) {
        this.toks = toks;
      }
      public IterTokenStream(params string[] text) {
        int off = 0;
        ArrayList/*<Token>*/ t = new ArrayList/*<Token>*/( text.Length );
        foreach( string txt in text ) {
          t.add( new Token( txt, off, off+txt.Length ) );
          off += txt.Length + 2;
        }
        this.toks = t.iterator();
      }
#pragma warning disable 672
      public override Token next() {
        if (toks.hasNext()) {
          return (Token) toks.next();
        }
        return null;
      }
#pragma warning restore 672
    }
  }
}