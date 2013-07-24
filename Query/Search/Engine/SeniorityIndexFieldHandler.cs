using LuceneQuery = org.apache.lucene.search.Query;
using org.apache.lucene.search.function;

namespace LinkMe.Query.Search.Engine
{
    internal class SeniorityIndexFieldHandler
    {
        private readonly string _fieldName;

        protected SeniorityIndexFieldHandler(string fieldName)
        {
            _fieldName = fieldName;
        }

        protected LuceneQuery GetQuery(LuceneQuery contentQuery, int? seniorityIndex, float sigma)
        {
            if (!seniorityIndex.HasValue)
                return contentQuery;

            var seniorityQuery = new ValueSourceQuery(new IntFieldSource(_fieldName));
            return new SeniorityIndexQuery(contentQuery, seniorityQuery, seniorityIndex.Value, sigma);
        }
    }
}
