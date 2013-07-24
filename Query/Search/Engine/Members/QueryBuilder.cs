using LinkMe.Query.Members;
using org.apache.lucene.analysis;
using org.apache.lucene.queryParser;
using org.apache.lucene.search;
using LuceneQuery = org.apache.lucene.search.Query;
using LuceneVersion = org.apache.lucene.util.Version;

namespace LinkMe.Query.Search.Engine.Members
{
    public class QueryBuilder
    {
        private readonly Analyzer _queryAnalyzer;
        private readonly IMemberSearchBooster _booster;

        public QueryBuilder(Analyzer queryAnalyzer, IMemberSearchBooster booster)
        {
            _queryAnalyzer = queryAnalyzer;
            _booster = booster;
        }

        public LuceneQuery GetContentQuery(MemberSearchQuery query)
        {
            return query.Keywords.GetLuceneQuery(query.IncludeSynonyms ? FieldName.Content : FieldName.Content_Exact, _queryAnalyzer);
        }

        public LuceneQuery GetJobTitleQuery(MemberSearchQuery query)
        {
            string fieldName;
            switch (query.JobTitlesToSearch)
            {
                case JobsToSearch.AllJobs:
                    fieldName = query.IncludeSynonyms ? FieldName.JobTitle : FieldName.JobTitle_Exact;
                    break;

                case JobsToSearch.LastJob:
                    fieldName = query.IncludeSynonyms ? FieldName.JobTitleLast : FieldName.JobTitleLast_Exact;
                    break;

                case JobsToSearch.RecentJobs:
                    fieldName = query.IncludeSynonyms ? FieldName.JobTitleRecent : FieldName.JobTitleRecent_Exact;
                    break;

                default:
                    return null;
            }

            return query.JobTitle.GetLuceneQuery(fieldName, _queryAnalyzer);
        }

        public LuceneQuery GetEmployerQuery(MemberSearchQuery query)
        {
            string fieldName;
            switch (query.CompaniesToSearch)
            {
                case JobsToSearch.AllJobs:
                    fieldName = query.IncludeSynonyms ? FieldName.Employer : FieldName.Employer_Exact;
                    break;

                case JobsToSearch.LastJob:
                    fieldName = query.IncludeSynonyms ? FieldName.EmployerLast : FieldName.EmployerLast_Exact;
                    break;

                case JobsToSearch.RecentJobs:
                    fieldName = query.IncludeSynonyms ? FieldName.EmployerRecent : FieldName.EmployerRecent_Exact;
                    break;

                default:
                    return null;
            }

            return query.CompanyKeywords.GetLuceneQuery(fieldName, _queryAnalyzer);
        }

        public LuceneQuery GetDesiredJobTitleQuery(MemberSearchQuery query)
        {
            return query.DesiredJobTitle.GetLuceneQuery(query.IncludeSynonyms ? FieldName.DesiredJobTitle : FieldName.DesiredJobTitle_Exact, _queryAnalyzer);
        }

        public LuceneQuery GetEducationQuery(MemberSearchQuery query)
        {
            return query.EducationKeywords.GetLuceneQuery(query.IncludeSynonyms ? FieldName.Education : FieldName.Education_Exact, _queryAnalyzer);
        }

        public LuceneQuery GetNameQuery(MemberSearchQuery query)
        {
            if (string.IsNullOrEmpty(query.Name))
                return null;

            var fieldName = query.IncludeSimilarNames ? FieldName.Name : FieldName.Name_Exact;
            var queryParser = new QueryParser(LuceneVersion.LUCENE_29, fieldName, _queryAnalyzer);
            queryParser.setDefaultOperator(QueryParser.Operator.AND);

            return queryParser.parse(query.Name);
        }

        public LuceneQuery GetQuery(MemberSearchQuery searchQuery)
        {
            var fieldQueries = CreateFieldQueries(searchQuery);
            var query = fieldQueries.CombineQueries(BooleanClause.Occur.MUST) ?? new MatchAllDocsQuery();

            return searchQuery.SortOrder == MemberSortOrder.Relevance
                ? _booster.GetRecencyBoostingQuery(query)
                : query;
        }

        private LuceneQuery[] CreateFieldQueries(MemberSearchQuery query)
        {
            return new[]
            {
                BoostContentQuery(GetContentQuery(query), query),
                GetJobTitleQuery(query),
                GetEmployerQuery(query),
                GetDesiredJobTitleQuery(query),
                GetEducationQuery(query),
                GetNameQuery(query)
            };
        }

        private LuceneQuery BoostContentQuery(LuceneQuery contentQuery, MemberSearchQuery query)
        {
            if (contentQuery == null || query.SortOrder != MemberSortOrder.Relevance)
                return contentQuery;

            // Boost recent job titles.

            var jobTitleQuery = query.Keywords.GetLuceneBoostQuery(query.IncludeSynonyms ? FieldName.JobTitleRecent : FieldName.JobTitleRecent_Exact, _queryAnalyzer);
            return _booster.GetJobTitleBoostingQuery(contentQuery, jobTitleQuery);
        }
    }
}
