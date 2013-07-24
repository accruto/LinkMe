using System.Globalization;
using System.Web.Mvc;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Test.Mvc.Models
{
    public class ReadOnlyQueryStringValueProvider
        : IValueProvider
    {
        private readonly ReadOnlyQueryString _queryString;

        public ReadOnlyQueryStringValueProvider(ReadOnlyQueryString queryString)
        {
            _queryString = queryString;
        }

        bool IValueProvider.ContainsPrefix(string prefix)
        {
            return false;
        }

        ValueProviderResult IValueProvider.GetValue(string key)
        {
            var rawValue = _queryString.GetValues(key);
            var attemptedValue = _queryString[key];
            return new ValueProviderResult(rawValue, attemptedValue, CultureInfo.InvariantCulture);
        }
    }
}