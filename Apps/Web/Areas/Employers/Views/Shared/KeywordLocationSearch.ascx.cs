using System;
using System.Linq;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Framework.Utility.Expressions;
using LinkMe.Web.Areas.Employers.Models.Search;

namespace LinkMe.Web.Areas.Employers.Views.Shared
{
    public class KeywordLocationSearch
        : ViewUserControl<KeywordLocationSearchModel>
    {
        private string[] _anyKeywords;

        protected string AnyKeywords1
        {
            get { return AnyKeywords.Take(1).FirstOrDefault(); }
        }

        protected string AnyKeywords2
        {
            get { return AnyKeywords.Skip(1).Take(1).FirstOrDefault(); }
        }

        protected string AnyKeywords3
        {
            get { return string.Join(" ", AnyKeywords.Skip(2).ToArray()); }
        }

        private string[] AnyKeywords
        {
            get
            {
                if (_anyKeywords == null)
                {
                    if (string.IsNullOrEmpty(Model.Criteria.AnyKeywords))
                    {
                        _anyKeywords = new string[0];
                    }
                    else
                    {
                        var expression = Expression.Parse(Model.Criteria.AnyKeywords, BinaryOperator.Or) as CommutativeExpression;
                        _anyKeywords = expression == null
                            ? new string[0]
                            : (from t in expression.Terms select t.GetRawExpression()).ToArray();
                    }
                }

                return _anyKeywords;
            }
        }
    }
}