using System;
using System.Threading;

namespace LinkMe.Framework.Text
{
    public class ConcurrentInterner
    {
        private const int _numSlots = 1 << 20;
        private readonly Entry[] _slots = new Entry[_numSlots];

        public unsafe string Intern(char* term, int len)
        {
            uint h = one_at_a_time_hash(((byte*)term), 2 * len);
            uint slot = h % _numSlots;

            while (true)
            {
                Entry root = _slots[slot];

                for (Entry e = root; e != null; e = e.Next)
                    if (UnsafeEquals(e.Term, term, len))
                        return e.Term;

                var entry = new Entry(new string(term, 0, len), root);
                if (ReferenceEquals(Interlocked.CompareExchange(ref _slots[slot], entry, root), root))
                    return entry.Term;
            }
        }

        private unsafe static uint one_at_a_time_hash(byte* key, int key_len) // By Bob Jenkins
        {
            uint hash = 0;
            int i;

            for (i = 0; i < key_len; i++)
            {
                hash += key[i];
                hash += hash << 10;
                hash ^= hash >> 6;
            }
            hash += hash << 3;
            hash ^= hash >> 11;
            hash += hash << 15;
            return hash;
        }

        private unsafe static bool UnsafeEquals(string s, char* term, int len)
        {
            if (s.Length != len) return false;
            for (int i = 0; i < len; ++i)
                if (s[i] != *(term + i))
                    return false;
            return true;
        }

        private class Entry
        {
            public readonly string Term;
            public readonly Entry Next;

            public Entry(string term, Entry next)
            {
                Term = term;
                Next = next;
            }
        }
    }
}
