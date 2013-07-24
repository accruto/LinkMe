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

namespace org.apache.solr.util
{

    using java.util;
    using Character = java.lang.Character;
    using CharSequence = java.lang.CharSequence;
    using Serializable = java.io.Serializable;

    /**
     * A simple class that stores key Strings as char[]'s in a
     * hash table. Note that this is not a general purpose
     * class.  For example, it cannot remove items from the
     * map, nor does it resize its hash table to be smaller,
     * etc.  It is designed to be quick to retrieve items
     * by char[] keys without the necessity of converting
     * to a String first.
     */

    public class CharArrayMap/*<V>*/ : AbstractMap/*<String, V>*/, Map, 
      java.lang.Cloneable.__Interface, Serializable.__Interface
    {
      private const int INIT_SIZE = 2;
      private char[][] keys;
#pragma warning disable 108
      private object[] values;
#pragma warning restore 108
      private int count;
      private readonly bool _ignoreCase;

      /** Create map with enough capacity to hold startSize
       *  terms */
      public CharArrayMap(int initialCapacity, bool ignoreCase) {
        _ignoreCase = ignoreCase;
        int size = INIT_SIZE;
        // load factor of .75, inverse is 1.25, or x+x/4
        initialCapacity = initialCapacity + (initialCapacity >>2);
        while(size <= initialCapacity)
          size <<= 1;
        keys = new char[size][];
        values = new object[size];
      }

      public bool ignoreCase() {
        return _ignoreCase;
      }

      public object get(char[] key) {
        return get(key, 0, key.Length);
      }

      public object get(char[] key, int off, int len) {
        return values[getSlot(key, off, len)];
      }

      public object get(CharSequence key) {
        return values[getSlot(key)];
      }

      public override object get(object key) {
        return values[getSlot(key)];
      }

      public override bool containsKey(object s) {
        return keys[getSlot(s)] != null; 
      }

      public override bool containsValue(object value) {
        if (value == null) {
          // search for key with a null value
          for (int i=0; i<keys.Length; i++) {
            if (keys[i] != null && values[i] == null) return true;
          }
          return false;
        }

        for (int i=0; i<values.Length; i++) {
          object val = values[i];
          if (val != null && value.Equals(val)) return true;
        }
        return false;
      }


      private int getSlot(object key) {
        if (key is char[]) {
          char[] keyc = (char[])key;
          return getSlot(keyc, 0, keyc.Length);
        }
        return getSlot((CharSequence)key);
      }

      private int getSlot(char[] key, int off, int len) {
        int code = getHashCode(key, len);
        int pos = code & (keys.Length-1);
        char[] key2 = keys[pos];
        if (key2 != null && !equals(key, off, len, key2)) {
          int inc = ((code>>8)+code)|1;
          do {
            code += inc;
            pos = code & (keys.Length-1);
            key2 = keys[pos];
          } while (key2 != null && !equals(key, off, len, key2));
        }
        return pos;
      }

      /** Returns true if the String is in the set */
      private int getSlot(CharSequence key) {
        int code = getHashCode(key);
        int pos = code & (keys.Length-1);
        char[] key2 = keys[pos];
        if (key2 != null && !equals(key, key2)) {
          int inc = ((code>>8)+code)|1;
          do {
            code += inc;
            pos = code & (keys.Length-1);
            key2 = keys[pos];
          } while (key2 != null && !equals(key, key2));
        }
        return pos;
      }

      public object put(CharSequence key, object val) {
        return put(key.toString(), val); // could be more efficient
      }

      public object put(string key, object val) {
        return put(key.ToCharArray(), val);
      }

      /** Add this key,val pair to the map.
       * The char[] key is directly used, no copy is made.
       * If ignoreCase is true for this Map, the key array will be directly modified.
       * The user should never modify the key after calling this method.
       */
      public object put(char[] key, object val) {
        if (_ignoreCase)
          for(int i=0;i< key.Length;i++)
            key[i] = Character.toLowerCase(key[i]);
        int slot = getSlot(key, 0, key.Length);
        if (keys[slot] == null) count++;
        object prev = values[slot];
        keys[slot] = key;
        values[slot] = val;

        if (count + (count>>2) >= keys.Length) {
          rehash();
        }

        return prev;
      }
      

      private bool equals(char[] text1, int off, int len, char[] text2) {
        if (len != text2.Length)
          return false;
        if (_ignoreCase) {
          for(int i=0;i<len;i++) {
            if (Character.toLowerCase(text1[off+i]) != text2[i])
              return false;
          }
        } else {
          for(int i=0;i<len;i++) {
            if (text1[off+i] != text2[i])
              return false;
          }
        }
        return true;
      }

      private bool equals(CharSequence text1, char[] text2) {
        int len = text1.length();
        if (len != text2.Length)
          return false;
        if (_ignoreCase) {
          for(int i=0;i<len;i++) {
            if (Character.toLowerCase(text1.charAt(i)) != text2[i])
              return false;
          }
        } else {
          for(int i=0;i<len;i++) {
            if (text1.charAt(i) != text2[i])
              return false;
          }
        }
        return true;
      }

      private void rehash() {
        int newSize = 2* keys.Length;
        char[][] oldEntries = keys;
        object[] oldValues = values;
        keys = new char[newSize][];
        values = new object[newSize];

        for(int i=0;i<oldEntries.Length;i++) {
          char[] key = oldEntries[i];
          if (key != null) {
            // todo: could be faster... no need to compare keys on collision
            // since they are unique
            int newSlot = getSlot(key,0,key.Length);
            keys[newSlot] = key;
            values[newSlot] = oldValues[i];
          }
        }
      }

      private int getHashCode(char[] text, int len) {
        int code = 0;
        if (_ignoreCase) {
          for (int i=0; i<len; i++) {
            code = code*31 + Character.toLowerCase(text[i]);
          }
        } else {
          for (int i=0; i<len; i++) {
            code = code*31 + text[i];
          }
        }
        return code;
      }

      private int getHashCode(CharSequence text) {
        int code;
        if (_ignoreCase) {
          code = 0;
          int len = text.length();
          for (int i=0; i<len; i++) {
            code = code*31 + Character.toLowerCase(text.charAt(i));
          }
        } else {
#pragma warning disable 184
          if (false && text is string)
#pragma warning restore 184
          {
            code = text.GetHashCode();
          } else {
            code = 0;
            int len = text.length();
            for (int i=0; i<len; i++) {
              code = code*31 + text.charAt(i);
            }
          }
        }
        return code;
      }

      public override int size() {
        return count;
      }

      public override bool isEmpty() {
        return count==0;
      }

      public override void clear() {
        count = 0;
        Arrays.fill(keys,null);
        Arrays.fill(values,null);
      }

      public override Set entrySet()
      {
        return new EntrySet(this);
      }

      /** Returns an EntryIterator over this Map. */
      public EntryIterator iterator() {
        return new EntryIterator(this);
      }

      /** public iterator class so efficient methods are exposed to users */
      public class EntryIterator : Iterator/*<Map.Entry<String,V>>*/ {
        int pos=-1;
        int lastPos;
        private readonly CharArrayMap map;

        public EntryIterator(CharArrayMap map)
        {
          goNext();
          this.map = map;
        }

        private void goNext() {
          lastPos = pos;
          pos++;
          while (pos < map.keys.Length && map.keys[pos] == null) pos++;
        }

        public bool hasNext() {
          return pos < map.keys.Length;
        }

        /** gets the next key... do not modify the returned char[] */
        public char[] nextKey() {
          goNext();
          return map.keys[lastPos];
        }

        /** gets the next key as a newly created String object */
        public string nextKeyString() {
          return new string(nextKey());
        }

        /** returns the value associated with the last key returned */
        public object currentValue() {
          return map.values[lastPos];
        }

        /** sets the value associated with the last key returned */    
        public object setValue(object value) {
          object old = map.values[lastPos];
          map.values[lastPos] = value;
          return old;      
        }

        /** Returns an Entry<String,V> object created on the fly...
         * use nextCharArray() + currentValie() for better efficiency. */
        public object/*<String,V>*/ next() {
          goNext();
          return new MapEntry(lastPos, map);
        }

        public void remove() {
          throw new java.lang.UnsupportedOperationException();
        }
      }


      private class MapEntry : Map.Entry/*<String,V>*/ {
        readonly int pos;
        private readonly CharArrayMap _map;

        public MapEntry(int pos, CharArrayMap map)
        {
          this.pos = pos;
          _map = map;
        }

        public char[] getCharArr() {
          return _map.keys[pos];
        }

        public object getKey() {
          return new string(getCharArr());
        }

        public object getValue() {
          return _map.values[pos];
        }

        public object setValue(object value) {
          object old = _map.values[pos];
          _map.values[pos] = value;
          return old;
        }

        public bool equals(object obj)
        {
          throw new System.NotImplementedException();
        }

        public int hashCode()
        {
          throw new System.NotImplementedException();
        }

        public string toString() {
          return getKey() + "=" + getValue();
        }
      }



      private class EntrySet : AbstractSet/*<Map.Entry<String, V>>*/ {
        private readonly CharArrayMap _map;

        public EntrySet(CharArrayMap map)
        {
          _map = map;
        }

        public override Iterator iterator() {
          return new EntryIterator(_map);
        }
        public override bool contains(object o) {
          if (!(o is Map.Entry))
            return false;
          Map.Entry e = (Map.Entry)o;
          object key = e.getKey();
          if (key==null) return false;  // we don't support null keys
          object val = e.getValue();
          object v = _map.get(key);
          return v==null ? val==null : v.Equals(val);
        }
        public override bool remove(object o) {
          throw new java.lang.UnsupportedOperationException();
        }
        public override int size() {
          return _map.count;
        }
        public override void clear() {
          _map.clear();
        }
      }

      protected override object clone() {
        CharArrayMap map = null;
        try {
          map = (CharArrayMap)base.clone();
          map.keys = (char[][]) keys.Clone();
          map.values = (object[]) values.Clone();
        } catch (java.lang.CloneNotSupportedException) {
          // impossible
        }
        return map;
      }
    }
}