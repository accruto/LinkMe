using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using LinkMe.Framework.Utility;

namespace LinkMe.Utility.Utilities
{
    public static class IdUtil
    {
        public static List<I> GetIds<T, I>(IEnumerable<T> values)
            where T : IHasId<I>
            where I : struct, IEquatable<I>
        {
            List<I> ids = new List<I>();

            if (values == null)
                return ids;

            foreach (IHasId<I> value in values)
            {
                ids.Add(value.Id);
            }

            return ids;
        }

        public static List<I> GetIds<I>(IEnumerable values)
            where I : struct, IEquatable<I>
        {
            List<I> ids = new List<I>();

            if (values == null)
                return ids;

            foreach (IHasId<I> value in values)
            {
                ids.Add(value.Id);
            }

            return ids;
        }

        public static string GetIdList<T, I>(string separator, IEnumerable<T> values)
            where T : IHasId<I>
            where I : struct, IEquatable<I>
        {
            if (values == null)
                return "";

            IEnumerator<T> enumerator = values.GetEnumerator();
            if (!enumerator.MoveNext())
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(enumerator.Current.Id);

            while (enumerator.MoveNext())
            {
                sb.Append(separator);
                sb.Append(enumerator.Current.Id);
            }

            return sb.ToString();
        }
    }
}
