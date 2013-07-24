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

using java.io;
using org.apache.lucene.analysis;

namespace org.apache.solr.analysis
{

  using Map = java.util.Map;


  /**
   * Simple abstract implementation that handles init arg processing.
   * 
   * @version $Id: BaseTokenizerFactory.java 696539 2008-09-18 02:16:26Z ryan $
   */
  public abstract class BaseTokenizerFactory : TokenizerFactory {
    
    /** The init args */
    protected Map/*<String,String>*/ args;
    
    public void init(Map/*<String,String>*/ args) {
      this.args=args;
    }
    
    public Map/*<String,String>*/ getArgs() {
      return args;
    }

    public abstract Tokenizer create(Reader input);
  }
}