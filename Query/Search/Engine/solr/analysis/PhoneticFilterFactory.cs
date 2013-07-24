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

using System;
using ikvm.extensions;
//using Exception=System.Exception;

namespace org.apache.solr.analysis
{

  using Method = java.lang.reflect.Method;
  using HashMap = java.util.HashMap;
  using Map = java.util.Map;

  using Encoder = org.apache.commons.codec.Encoder;
  using DoubleMetaphone = org.apache.commons.codec.language.DoubleMetaphone;
  using Metaphone = org.apache.commons.codec.language.Metaphone;
  using RefinedSoundex = org.apache.commons.codec.language.RefinedSoundex;
  using Soundex = org.apache.commons.codec.language.Soundex;
  using TokenStream = org.apache.lucene.analysis.TokenStream;

  /**
   * Create tokens based on phonetic encoders
   * 
   * http://jakarta.apache.org/commons/codec/api-release/org/apache/commons/codec/language/package-summary.html
   * 
   * This takes two arguments:
   *  "encoder" required, one of "DoubleMetaphone", "Metaphone", "Soundex", "RefinedSoundex"
   * 
   * "inject" (default=true) add tokens to the stream with the offset=0
   * 
   * @version $Id: PhoneticFilterFactory.java 764276 2009-04-12 02:24:01Z yonik $
   * @see PhoneticFilter
   */
  public class PhoneticFilterFactory : BaseTokenFilterFactory 
  {
    public const string ENCODER = "encoder";
    public const string INJECT = "inject"; // boolean
    
    private static Map/*<String, Class<? extends Encoder>>*/ registry;
    static PhoneticFilterFactory() {
      registry = new HashMap/*<String, Class<? extends Encoder>>*/();
      registry.put( "DoubleMetaphone".ToUpper(), typeof(DoubleMetaphone));
      registry.put( "Metaphone".ToUpper(),       typeof(Metaphone));
      registry.put( "Soundex".ToUpper(),         typeof(Soundex));
      registry.put( "RefinedSoundex".ToUpper(),  typeof(RefinedSoundex));
    }
    
    protected bool inject = true;
    protected string name = null;
    protected Encoder encoder = null;

    public override void init(Map/*<String,String>*/ args) {
      base.init( args );

      inject = getBoolean(INJECT, true);
      
      string name = (string)args.get( ENCODER );
      if( name == null ) {
        throw new InvalidOperationException("Missing required parameter: "+ENCODER+" ["+registry.keySet()+"]" );
      }
      java.lang.Class/*<? extends Encoder>*/ clazz = (Type)registry.get(name.ToUpper());
      if( clazz == null ) {
        throw new InvalidOperationException("Unknown encoder: "+name +" ["+registry.keySet()+"]" );
      }
      
      try {
        encoder = (Encoder)clazz.newInstance();
        
        // Try to set the maxCodeLength
        string v = (string) args.get( "maxCodeLength" );
        if( v != null ) {
          Method setter = encoder.getClass().getMethod( "setMaxCodeLen", typeof(int));
          setter.invoke( encoder, java.lang.Integer.parseInt( v ) );
        }
      } 
      catch (Exception e) {
        throw new InvalidOperationException("Error initializing: " + name + "/" + clazz, e);
      }
    }
    
    public override TokenStream create(TokenStream input) {
      return new PhoneticFilter(input,encoder,name,inject);
    }
  }
}