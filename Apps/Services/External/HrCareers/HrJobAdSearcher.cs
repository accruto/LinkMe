using System;
using System.Linq;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Expressions;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;

namespace LinkMe.Apps.Services.External.HrCareers
{
    class HrJobAdSearcher
    {
        private static readonly List<CategorySearchCriteria> _searchList = new List<CategorySearchCriteria>();
        private readonly IExecuteJobAdSearchCommand _jobAdSearch;

        static HrJobAdSearcher()
        {
            _searchList.Add(new CategorySearchCriteria(15, new JobAdSearchCriteria
            {
                AdTitle = "Industrial relations"
            }));

            _searchList.Add(new CategorySearchCriteria(19, new JobAdSearchCriteria
            {
                AdTitle = 
                    Expression.Combine(BinaryOperator.And,
                        Expression.Combine(BinaryOperator.Or,
                            new LiteralTerm("HR"),
                            new LiteralTerm("employment"),
                            new LiteralTerm("career")),
                        new UnaryExpression(UnaryOperator.Not, 
                            new LiteralTerm("driver"))).GetRawExpression()
            }));

            _searchList.Add(new CategorySearchCriteria(20, new JobAdSearchCriteria
            {
                AdTitle = 
                    Expression.Combine(BinaryOperator.Or,
                        new LiteralTerm("recruiter"),
                        new LiteralTerm("recruitment"),
                        new LiteralTerm("candidate"),
                        new LiteralTerm("recruitment consultant"),
                        new LiteralTerm("recruitment advisor"),
                        new LiteralTerm("candidate resourcer")).GetRawExpression()
            }));

            _searchList.Add(new CategorySearchCriteria(21, new JobAdSearchCriteria
            {
                AdTitle = 
                    Expression.Combine(BinaryOperator.And,
                        Expression.Combine(BinaryOperator.Or,
                            new LiteralTerm("HR Manager"),
                            new LiteralTerm("Change manager"),
                            new LiteralTerm("recruitment manager")),
                        new UnaryExpression(UnaryOperator.Not,
                            new LiteralTerm("driver"))).GetRawExpression()
            }));

            _searchList.Add(new CategorySearchCriteria(22, new JobAdSearchCriteria
            {
                AdTitle = 
                    Expression.Combine(BinaryOperator.Or,
                        new LiteralTerm("Trainer"),
                        new LiteralTerm("Learning and development"),
                        new LiteralTerm("Learning Development"),
                        new LiteralTerm("L D")).GetRawExpression()
            }));

            _searchList.Add(new CategorySearchCriteria(23, new JobAdSearchCriteria
            {
                AdTitle = 
                    Expression.Combine(BinaryOperator.Or,
                        new LiteralTerm("Organisation design"),
                        new LiteralTerm("development"),
                        new LiteralTerm("change manager"),
                        new LiteralTerm("change management")).GetRawExpression()
            }));

            _searchList.Add(new CategorySearchCriteria(24, new JobAdSearchCriteria
            {
                AdTitle = 
                    Expression.Combine(BinaryOperator.Or,
                        new LiteralTerm("OH S"),
                        new LiteralTerm("Occupational health & safety"),
                        new LiteralTerm("health and safety")).GetRawExpression()
            }));

            _searchList.Add(new CategorySearchCriteria(24, new JobAdSearchCriteria
            {
                AdTitle = 
                    Expression.Combine(BinaryOperator.Or,
                        new LiteralTerm("OH S"),
                        new LiteralTerm("Occupational health & safety"),
                        new LiteralTerm("health and safety")).GetRawExpression()
            }));

            _searchList.Add(new CategorySearchCriteria(25, new JobAdSearchCriteria
            {
                AdTitle = 
                    Expression.Combine(BinaryOperator.Or,
                        new LiteralTerm("Compliance"),
                        new LiteralTerm("Diversity")).GetRawExpression()
            }));

            _searchList.Add(new CategorySearchCriteria(26, new JobAdSearchCriteria
            {
                AdTitle = 
                    Expression.Combine(BinaryOperator.Or,
                        new LiteralTerm("Case manager"),
                        new LiteralTerm("graduate")).GetRawExpression()
            }));

            _searchList.Add(new CategorySearchCriteria(27, new JobAdSearchCriteria
            {
                AdTitle = 
                    Expression.Combine(BinaryOperator.Or,
                        new LiteralTerm("Compensation"),
                        new LiteralTerm("return to work")).GetRawExpression()
            }));

            _searchList.Add(new CategorySearchCriteria(28, new JobAdSearchCriteria
            {
                AdTitle = 
                    Expression.Combine(BinaryOperator.Or,
                        new LiteralTerm("Remuneration"),
                        new LiteralTerm("benefits")).GetRawExpression()
            }));

            _searchList.Add(new CategorySearchCriteria(29, new JobAdSearchCriteria
            {
                AdTitle =
                    new LiteralTerm("HRIS").GetRawExpression()
            }));

            _searchList.Add(new CategorySearchCriteria(30, new JobAdSearchCriteria
            {
                AdTitle =
                    new LiteralTerm("Payroll").GetRawExpression()
            }));

            _searchList.Add(new CategorySearchCriteria(31, new JobAdSearchCriteria
            {
                AdTitle = 
                    Expression.Combine(BinaryOperator.And,
                        Expression.Combine(BinaryOperator.Or,
                            new LiteralTerm("HR Manager"),
                            new LiteralTerm("HR Coordinator"),
                            new LiteralTerm("HR Officer")),
                        new UnaryExpression(UnaryOperator.Not,
                            new LiteralTerm("driver"))).GetRawExpression()
            }));

            _searchList.Add(new CategorySearchCriteria(32, new JobAdSearchCriteria
            {
                AdTitle = 
                    Expression.Combine(BinaryOperator.And,
                        Expression.Combine(BinaryOperator.Or,
                            new LiteralTerm("HR Director"),
                            new LiteralTerm("HR Manager")),
                        new UnaryExpression(UnaryOperator.Not,
                            new LiteralTerm("driver"))).GetRawExpression()
            }));
        }

        public HrJobAdSearcher(IExecuteJobAdSearchCommand jobAdSearch)
        {
            _jobAdSearch = jobAdSearch;
        }

        public ILookup<Guid, int> Search()
        {
            var hrResults = Enumerable.Empty<KeyValuePair<Guid, int>>();

            foreach (var search in _searchList)
            {
                var categoryId = search.CategoryId;
                var execution = _jobAdSearch.Search(null, search.SearchCriteria, null);
                hrResults = hrResults.Concat(execution.Results.JobAdIds.Select(r => new KeyValuePair<Guid, int>(r, categoryId)));
            }

            return hrResults.ToLookup(p => p.Key, p => p.Value);
        }

        private class CategorySearchCriteria
        {
            public readonly int CategoryId;
            public readonly JobAdSearchCriteria SearchCriteria;

            public CategorySearchCriteria(int categoryId, JobAdSearchCriteria searchCriteria)
            {
                CategoryId = categoryId;
                SearchCriteria = searchCriteria;
            }
        }
    }
}
