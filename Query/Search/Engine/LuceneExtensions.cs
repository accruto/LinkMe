using System;
using System.Linq;
using org.apache.lucene.search;
using LuceneQuery = org.apache.lucene.search.Query;

namespace LinkMe.Query.Search.Engine
{
    public static class LuceneExtensions
    {
        public static string ToFieldValue(this Guid id)
        {
            return id.ToString("N");
        }

        public static int ToFieldValue(this DateTime dateTime, TimeGranularity timeGranularity)
        {
            // Convert to number of <timegranularity> since 1 Jan 0001, 00:00:00

            switch (timeGranularity)
            {
                case TimeGranularity.Day:
                    return (int) (dateTime.Ticks/10000000/3600/24);

                case TimeGranularity.Hour:
                    return (int) (dateTime.Ticks/10000000/3600);

                case TimeGranularity.Minute:
                    return (int) (dateTime.Ticks/10000000/60);
            }

            throw new NotImplementedException();
        }

        public static LuceneQuery CombineQueries(this LuceneQuery[] queries, BooleanClause.Occur occur)
        {
            BooleanQuery query = null;

            foreach (var subQuery in queries.Where(q => q != null))
            {
                if (query == null)
                    query = new BooleanQuery();
                query.add(subQuery, occur);
            }

            return query;
        }
    }
}
