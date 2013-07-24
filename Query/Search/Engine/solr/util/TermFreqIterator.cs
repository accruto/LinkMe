﻿/*
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

  using Iterator = java.util.Iterator;

  public interface TermFreqIterator : Iterator/*<String>*/ {

    float freq();
    
    //public static class TermFreqIteratorWrapper implements TermFreqIterator {
    //  private Iterator wrapped;
      
    //  public TermFreqIteratorWrapper(Iterator wrapped) {
    //    this.wrapped = wrapped;
    //  }

    //  public float freq() {
    //    return 1.0f;
    //  }

    //  public boolean hasNext() {
    //    return wrapped.hasNext();
    //  }

    //  public String next() {
    //    return wrapped.next().toString();
    //  }

    //  public void remove() {
    //    throw new UnsupportedOperationException();
    //  }
      
    //}
  }
}