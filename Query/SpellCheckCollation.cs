using System;
using System.Collections.Generic;

namespace LinkMe.Query
{
    [Serializable]
    public class SpellCheckCollation
        : IComparable<SpellCheckCollation>
    {
        public string CollationQuery { get; set; }
        public IList<KeyValuePair<string, string>> MisspellingsAndCorrections { get; set; }
        public int Hits { get; set; }
        public int InternalRank { get; set; }

        int IComparable<SpellCheckCollation>.CompareTo(SpellCheckCollation other)
        {
            var c = InternalRank.CompareTo(other.InternalRank);
            return c != 0
                ? c
                : CollationQuery.CompareTo(other.CollationQuery);
        }
    }
}
