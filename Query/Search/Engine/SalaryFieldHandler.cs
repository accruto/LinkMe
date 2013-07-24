using System;
using System.Collections.Generic;
using com.browseengine.bobo.api;
using LinkMe.Domain;
using org.apache.lucene.document;
using org.apache.lucene.misc;
using org.apache.lucene.search;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine
{
    internal class SalaryFieldHandler
    {
        private readonly string _minFieldName;
        private readonly string _maxFieldName;
        private readonly IBooster _booster;
        private readonly LuceneFilter _nullSalaryFilter;
        private readonly BooleanFilter _nonNullSalaryFilter;

        private const decimal MinToMaxConversion = 1.25m;
        private const decimal MaxToMinConversion = 0.8m;

        protected SalaryFieldHandler(string minFieldName, string maxFieldName, IBooster booster)
        {
            _minFieldName = minFieldName;
            _maxFieldName = maxFieldName;
            _booster = booster;

            _nullSalaryFilter = NumericRangeFilter.newIntRange(_maxFieldName, new java.lang.Integer(0), new java.lang.Integer(0), true, true);

            // nonNullSalary <=> NOT nullSalary

            _nonNullSalaryFilter = new BooleanFilter();
            _nonNullSalaryFilter.add(new FilterClause(_nullSalaryFilter, BooleanClause.Occur.MUST_NOT));
        }

        protected void AddContent(Document document, decimal? contentMinSalary, decimal? contentMaxSalary, bool useStrictValues)
        {
            int minSalary, maxSalary;
            if (useStrictValues)
                GetStrictSalaryFieldValues(contentMinSalary, contentMaxSalary, out minSalary, out maxSalary);
            else
                GetSalaryFieldValues(contentMinSalary, contentMaxSalary, out minSalary, out maxSalary);

            var field = new NumericField(_minFieldName).setIntValue(minSalary);
            _booster.SetBoost(field);
            document.add(field);
            field = new NumericField(_maxFieldName).setIntValue(maxSalary);
            _booster.SetBoost(field);
            document.add(field);
        }

        protected LuceneFilter GetFilter(Salary salary, bool excludeNoSalary)
        {
            int minSalary, maxSalary;

            //Ensure salary is yearly before filtering
            if (salary != null)
                salary = salary.ToRate(SalaryRate.Year);

            if (salary == null || !GetFilterFieldValues(salary.LowerBound, salary.UpperBound, out minSalary, out maxSalary) || (minSalary == 0 && maxSalary == int.MaxValue))
            {
                return excludeNoSalary
                    ? _nonNullSalaryFilter
                    : null;
            }

            var boundaryFilters = new List<LuceneFilter>(2);

            if (minSalary != 0)
                boundaryFilters.Add(FieldCacheRangeFilter.newIntRange(_maxFieldName, new java.lang.Integer(minSalary), null, true, true));

            if (maxSalary != int.MaxValue)
                boundaryFilters.Add(FieldCacheRangeFilter.newIntRange(_minFieldName, new java.lang.Integer(1), new java.lang.Integer(maxSalary), true, true));

            var salaryFilter = new ChainedFilter(boundaryFilters.ToArray(), ChainedFilter.AND);
            return !excludeNoSalary
                ? new ChainedFilter(new[] { salaryFilter, _nullSalaryFilter }, ChainedFilter.OR)
                : salaryFilter;
        }

        protected Sort GetSort(bool reverse)
        {
            return new Sort(new[] 
            {
                new BoboCustomSortField(_minFieldName, !reverse, new ZeroLastIntComparatorSource(FieldCache.__Fields.NUMERIC_UTILS_INT_PARSER, _minFieldName, !reverse)), SortField.FIELD_SCORE
            });
        }

        private static void GetSalaryFieldValues(decimal? minSalary, decimal? maxSalary, out int minSalaryValue, out int maxSalaryValue)
        {
            if (!minSalary.HasValue && !maxSalary.HasValue)
            {
                minSalaryValue = maxSalaryValue = 0;
                return;
            }

            if (minSalary.HasValue)
            {
                // If there is a min value then use it and 1.25 times it for the max, ignoring any specific value passed in.

                minSalaryValue = (int)Math.Min(minSalary.Value, int.MaxValue);
                maxSalaryValue = (int)Math.Min(minSalaryValue * MinToMaxConversion, int.MaxValue);
            }
            else
            {
                // Only the max is specified, so set the min to 0.8 times it.

                maxSalaryValue = (int)Math.Min(maxSalary.Value, int.MaxValue);
                minSalaryValue = (int)Math.Min(maxSalaryValue * MaxToMinConversion, int.MaxValue);
            }
        }

        private static void GetStrictSalaryFieldValues(decimal? minSalary, decimal? maxSalary, out int minSalaryValue, out int maxSalaryValue)
        {
            if (!minSalary.HasValue && !maxSalary.HasValue)
            {
                minSalaryValue = maxSalaryValue = 0;
                return;
            }

            // only use the conversions when no value exists; otherwise use the actual values

            minSalaryValue = minSalary.HasValue ? (int)Math.Min(minSalary.Value, int.MaxValue) : (int)Math.Min(maxSalary.Value * MaxToMinConversion, int.MaxValue);
            maxSalaryValue = maxSalary.HasValue ? (int)Math.Min(maxSalary.Value, int.MaxValue) : (int)Math.Min(minSalary.Value * MinToMaxConversion, int.MaxValue);
        }

        private static bool GetFilterFieldValues(decimal? minSalary, decimal? maxSalary, out int minSalaryValue, out int maxSalaryValue)
        {
            if (!minSalary.HasValue && !maxSalary.HasValue)
            {
                minSalaryValue = maxSalaryValue = 0;
                return false;
            }

            minSalaryValue = minSalary.HasValue
                ? (int)Math.Min(minSalary.Value, int.MaxValue)
                : 0;

            maxSalaryValue = maxSalary.HasValue
                ? (int)Math.Min(maxSalary.Value, int.MaxValue)
                : int.MaxValue;

            return true;
        }
    }
}