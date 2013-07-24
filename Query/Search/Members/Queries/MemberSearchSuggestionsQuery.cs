using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using LinkMe.Framework.Utility.Expressions;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Query.Members;
using org.apache.lucene.analysis;
using org.apache.lucene.index;
using org.apache.lucene.queryParser;
using LuceneVersion = org.apache.lucene.util.Version;

namespace LinkMe.Query.Search.Members.Queries
{
    public class MemberSearchSuggestionsQuery
        : IMemberSearchSuggestionsQuery
    {
        private readonly IChannelManager<IMemberSearchService> _serviceManager;
        private int _maxCollations = 5;

        public int MaxCollations
        {
            set { _maxCollations = value; }
        }

        public MemberSearchSuggestionsQuery(IChannelManager<IMemberSearchService> serviceManager)
        {
            _serviceManager = serviceManager;
        }

        IList<SpellingSuggestion> IMemberSearchSuggestionsQuery.GetSpellingSuggestions(MemberSearchCriteria criteria)
        {
            var queryString = ToQueryString(criteria);
            SpellCheckCollation[] collations;
            var service = _serviceManager.Create();
            try
            {
                collations = service.GetSpellingSuggestions(queryString, _maxCollations);
            }
            catch (Exception)
            {
                _serviceManager.Abort(service);
                throw;
            }
            _serviceManager.Close(service);

            var suggestions = new List<SpellingSuggestion>(collations.Length);
            foreach (var collation in collations)
            {
                var suggestedCriteria = criteria.Clone();
                ApplyQueryString(suggestedCriteria, collation.CollationQuery);

                suggestions.Add(new SpellingSuggestion
                {
                    Criteria = suggestedCriteria,
                    Corrections = collation.MisspellingsAndCorrections
                });
            }
    
            return suggestions;
        }

        IList<MemberSearchSuggestion> IMemberSearchSuggestionsQuery.GetMoreResultsSuggestions(MemberSearchCriteria criteria)
        {
            var keywords = criteria.GetKeywords();

            var suggestions = new List<MemberSearchSuggestion>();
            if (string.IsNullOrEmpty(criteria.JobTitle))
            {
                if (!string.IsNullOrEmpty(keywords))
                    suggestions.Add(new KeywordsSuggestion { Criteria = criteria.Clone() });
            }
            else
            {
                if (string.IsNullOrEmpty(keywords))
                    suggestions.Add(new JobTitleAsKeywordsSuggestion { Criteria = criteria.ChangeJobTitleToKeywords() });

                if (criteria.JobTitlesToSearch != JobsToSearch.AllJobs)
                {
                    var newCriteria = criteria.Clone();
                    newCriteria.JobTitlesToSearch = JobsToSearch.AllJobs;
                    suggestions.Add(new AllJobsSuggestion { Criteria = newCriteria });
                }
            }

            if (!string.IsNullOrEmpty(keywords))
            {
                var keywordsExpression = Expression.Parse(keywords);
                var ored = Expression.Flatten(keywordsExpression, BinaryOperator.Or);

                if (!Equals(keywordsExpression, ored))
                {
                    var newCriteria = criteria.Clone();
                    newCriteria.SetKeywords(ored.GetUserExpression());
                    suggestions.Add(new OrKeywordsSuggestion { Criteria = newCriteria });
                }
            }

            return suggestions.ToList();
        }

        IList<MemberSearchSuggestion> IMemberSearchSuggestionsQuery.GetLessResultsSuggestions(MemberSearchCriteria criteria)
        {
            return new List<MemberSearchSuggestion>();
        }   

        private static string ToQueryString(MemberSearchCriteria criteria)
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(criteria.AllKeywords))
                sb.AppendFormat("AllKeywords:({0}) ", criteria.AllKeywords);

            if (!string.IsNullOrEmpty(criteria.AnyKeywords))
                sb.AppendFormat("AnyKeywords:({0}) ", criteria.AnyKeywords);

            if (!string.IsNullOrEmpty(criteria.ExactPhrase))
                sb.AppendFormat("ExactPhrase:({0}) ", criteria.ExactPhrase);

            if (!string.IsNullOrEmpty(criteria.CompanyKeywords))
                sb.AppendFormat("CompanyKeywords:({0}) ", criteria.CompanyKeywords);

            if (!string.IsNullOrEmpty(criteria.EducationKeywords))
                sb.AppendFormat("EducationKeywords:({0}) ", criteria.EducationKeywords);

            if (!string.IsNullOrEmpty(criteria.JobTitle))
                sb.AppendFormat("JobTitle:({0}) ", criteria.JobTitle);

            return sb.ToString();
        }

        private static void ApplyQueryString(MemberSearchCriteria criteria, string queryString)
        {
            // Parse the returned Lucene query string.

            var queryParser = new QueryParser(LuceneVersion.LUCENE_29, string.Empty, new SimpleAnalyzer());
            var query = queryParser.parse(queryString);

            // Arrange terms by field.

            var terms = new java.util.LinkedHashSet(); // preserves order
            query.extractTerms(terms);

            var coll = new NameValueCollection();
            for (var iter = terms.iterator(); iter.hasNext(); )
            {
                var term = (Term)iter.next();
                coll.Add(term.field(), term.text());
            }

            // Extarct criteria parts.

            string allKeywords = null;
            var values = coll.GetValues("AllKeywords");
            if (values != null)
                allKeywords = string.Join(" ", values);

            string anyKeywords = null;
            values = coll.GetValues("AnyKeywords");
            if (values != null)
                anyKeywords = string.Join(" ", values);

            string exactPhrase = null;
            values = coll.GetValues("ExactPhrase");
            if (values != null)
                exactPhrase = string.Join(" ", values);

            criteria.SetKeywords(allKeywords, exactPhrase, anyKeywords, null);

            values = coll.GetValues("CompanyKeywords");
            if (values != null)
                criteria.CompanyKeywords = string.Join(" ", values);

            values = coll.GetValues("EducationKeywords");
            if (values != null)
                criteria.EducationKeywords = string.Join(" ", values);

            values = coll.GetValues("JobTitle");
            if (values != null)
                criteria.JobTitle = string.Join(" ", values);
        }
    }
}