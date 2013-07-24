using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Presentation.Converters
{
    public abstract class BaseQueryStringGenerator
    {
        public abstract void GenerateQueryString(object model, QueryString queryString);

        public QueryString GenerateQueryString(object model)
        {
            var queryString = new QueryString();
            GenerateQueryString(model, queryString);
            return queryString;
        }
    }

    public class QueryStringGenerator
        : BaseQueryStringGenerator
    {
        private readonly IConverter _converter;

        public QueryStringGenerator(IConverter converter)
        {
            _converter = converter;
        }

        public override void GenerateQueryString(object obj, QueryString queryString)
        {
            _converter.Convert(obj, new QueryStringSetValues(queryString));
        }
    }
}