using System;
using System.Data.Linq;
using System.Linq.Expressions;

namespace LinkMe.Framework.Utility.Data.Linq
{
    public static class DataOptions
    {
        public static DataLoadOptions CreateLoadOptions<T>(Expression<Func<T, object>> loadWith)
        {
            var options = new DataLoadOptions();
            options.LoadWith(loadWith);
            return options;
        }

        public static DataLoadOptions CreateLoadOptions<T1, T2>(Expression<Func<T1, object>> loadWith1, Expression<Func<T2, object>> loadWith2)
        {
            var options = new DataLoadOptions();
            options.LoadWith(loadWith1);
            options.LoadWith(loadWith2);
            return options;
        }

        public static DataLoadOptions CreateLoadOptions<T1, T2, T3>(Expression<Func<T1, object>> loadWith1, Expression<Func<T2, object>> loadWith2, Expression<Func<T3, object>> loadWith3)
        {
            var options = new DataLoadOptions();
            options.LoadWith(loadWith1);
            options.LoadWith(loadWith2);
            options.LoadWith(loadWith3);
            return options;
        }
    }
}
