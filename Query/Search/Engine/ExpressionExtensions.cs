using System;
using System.Linq;
using LinkMe.Framework.Utility;
using org.apache.lucene.analysis;
using org.apache.lucene.queryParser;
using org.apache.lucene.search;
using LuceneQuery = org.apache.lucene.search.Query;
using LuceneVersion = org.apache.lucene.util.Version;
using LinkMe.Framework.Utility.Expressions;

namespace LinkMe.Query.Search.Engine
{
    public static class ExpressionExtensions
    {
        #region GetLuceneQuery

        public static LuceneQuery GetLuceneQuery(this IExpression expression, string fieldName, Analyzer analyzer)
        {
            if (expression is CommutativeExpression)
                return ((CommutativeExpression)expression).GetLuceneQuery(fieldName, analyzer);
            if (expression is LiteralTerm)
                return ((LiteralTerm)expression).GetLuceneQuery(fieldName, analyzer);
            if (expression is ModifierExpression)
                return ((ModifierExpression)expression).GetLuceneQuery(fieldName, analyzer);
            if (expression is UnaryExpression)
                return ((UnaryExpression)expression).GetLuceneQuery(fieldName, analyzer);
            return null;
        }

        private static LuceneQuery GetLuceneQuery(this CommutativeExpression expression, string fieldName, Analyzer analyzer)
        {
            BooleanClause.Occur defaultOp;
            switch (expression.Operator)
            {
                case BinaryOperator.And:
                    defaultOp = BooleanClause.Occur.MUST;
                    break;

                case BinaryOperator.Or:
                    defaultOp = BooleanClause.Occur.SHOULD;
                    break;

                default:
                    throw new InvalidOperationException();
            }

            var query = new BooleanQuery();
            foreach (var term in expression.Terms)
            {
                var op = defaultOp;
                var innerExpr = term;

                var unaryTerm = term as UnaryExpression;
                if (unaryTerm != null)
                {
                    if (expression.Operator != BinaryOperator.And)
                        continue;

                    op = BooleanClause.Occur.MUST_NOT;
                    innerExpr = unaryTerm.Term;
                }

                var innerQuery = innerExpr.GetLuceneQuery(fieldName, analyzer);
                if (innerQuery != null)
                    query.add(innerQuery, op);
            }

            return query;
        }

        private static LuceneQuery GetLuceneQuery(this LiteralTerm expression, string fieldName, Analyzer analyzer)
        {
            // Parse query string.

            var parser = new QueryParser(LuceneVersion.LUCENE_29, fieldName, analyzer);
            LuceneQuery query;

            try
            {
                query = parser.parse(expression.Value);
            }
            catch (ParseException)
            {
                query = parser.parse(QueryParser.escape(expression.Value));
            }

            // QueryParser may generate empty BooleanQuery when confused.

            var boolQuery = query as BooleanQuery;
            if (boolQuery != null && boolQuery.clauses().size() == 0)
                return null;

            //var query = new PayloadTermQuery(new Term(fieldName, expression.Value), new MaxPayloadFunction());
            return query;
        }

        private static LuceneQuery GetLuceneQuery(this ModifierExpression expression, string fieldName, Analyzer analyzer)
        {
            // treat the expression as a literal when allowing shingling

            LuceneQuery query;

            if (expression.Flags.IsFlagSet(ModificationFlags.AllowShingling))
            {
                var parser = new QueryParser(LuceneVersion.LUCENE_29, fieldName, analyzer);
                parser.setDefaultOperator(QueryParser.Operator.AND);

                //add quotes to the expression to ensure the whitespaceanalyzer doesn't break into multiple tokens
                var expressionValue = expression.Expression is LiteralTerm &&
                                      !expression.Expression.GetUserExpression().Contains(LiteralTerm.Quote)
                                          ? LiteralTerm.Quote + expression.Expression.GetUserExpression() +
                                            LiteralTerm.Quote
                                          : expression.Expression.GetUserExpression();
                query = parser.parse(expressionValue);

                if (query is MultiPhraseQuery)
                {
                    // by default a MultiPhraseQuery only matches where the term order matches ("chief executive" does not match "executive chief")
                    // add slop to match on non-contiguous terms where the order doesn't match
                    ((MultiPhraseQuery)query).setSlop(100);
                }
                
                // QueryParser may generate empty BooleanQuery when confused.

                var boolQuery = query as BooleanQuery;
                if (boolQuery != null && boolQuery.clauses().size() == 0)
                    query = null;
            }
            else
            {
                query = expression.Expression.GetLuceneQuery(fieldName, analyzer);
            }

            return query;
        }

        private static LuceneQuery GetLuceneQuery(this UnaryExpression expression, string fieldName, Analyzer analyzer)
        {
            var innerQuery = expression.Term.GetLuceneQuery(fieldName, analyzer);
            var query = new BooleanQuery();
            query.add(innerQuery, BooleanClause.Occur.MUST_NOT);
            return query;
        }

        #endregion

        #region GetLuceneBoostQuery

        public static LuceneQuery GetLuceneBoostQuery(this IExpression expression, string fieldName, Analyzer analyzer)
        {
            if (expression is CommutativeExpression)
                return ((CommutativeExpression)expression).GetLuceneBoostQuery(fieldName, analyzer);
            if (expression is LiteralTerm)
                return ((LiteralTerm)expression).GetLuceneBoostQuery(fieldName, analyzer);
            if (expression is ModifierExpression)
                return ((ModifierExpression)expression).GetLuceneBoostQuery(fieldName, analyzer);
            if (expression is UnaryExpression)
                return ((UnaryExpression)expression).GetLuceneBoostQuery(fieldName, analyzer);
            return null;
        }

        private static LuceneQuery GetLuceneBoostQuery(this CommutativeExpression expression, string fieldName, Analyzer analyzer)
        {
            var query = new BooleanQuery();

            foreach (var innerQuery in expression.Terms.Select(t => t.GetLuceneBoostQuery(fieldName, analyzer)))
            {
                if (innerQuery != null)
                    query.add(innerQuery, BooleanClause.Occur.SHOULD);
            }

            return query;
        }

        private static LuceneQuery GetLuceneBoostQuery(this LiteralTerm expression, string fieldName, Analyzer analyzer)
        {
            // Escape QueryParser special characters.

            var queryString = QueryParser.escape(expression.Value);
            if (expression.GetRawExpression()[0] == '\"')
                queryString = "\"" + queryString + "\"";

            // Parse query string.

            var parser = new QueryParser(LuceneVersion.LUCENE_29, fieldName, analyzer);
            var parsedQuery = parser.parse(queryString);

            if (parsedQuery is TermQuery)
                return parsedQuery;

            var multiPhraseQuery = parsedQuery as MultiPhraseQuery;
            if (multiPhraseQuery != null)
            {
                multiPhraseQuery.setSlop(100);
                return multiPhraseQuery;
            }

            var phraseQuery = parsedQuery as PhraseQuery;
            if (phraseQuery != null)
            {
                var query = new BooleanQuery();
                foreach (var term in phraseQuery.getTerms())
                    query.add(new TermQuery(term), BooleanClause.Occur.SHOULD);

                return query;
            }

            return null;
        }

        private static LuceneQuery GetLuceneBoostQuery(this ModifierExpression expression, string fieldName, Analyzer analyzer)
        {
            return expression.Expression.GetLuceneBoostQuery(fieldName, analyzer);
        }

        private static LuceneQuery GetLuceneBoostQuery(this UnaryExpression expression, string fieldName, Analyzer analyzer)
        {
            return null;
        }

        #endregion


    }
}
