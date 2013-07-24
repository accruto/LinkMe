using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Framework.Utility.Data.Linq
{
    public class SplitList<T>
    {
        public static readonly string Delimiter = new string(',', 1);
        private readonly T[] _list;

        public SplitList(IEnumerable<T> list)
        {
            _list = list == null
                ? new T[0]
                : list.ToArray();
        }

        public override string ToString()
        {
            var strings = new string[_list.Length];
            for (var index = 0; index < _list.Length; ++index)
                strings[index] = _list[index].ToString();
            return string.Join(Delimiter, strings);
        }
    }
}

