using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Presentation.Converters
{
    public abstract class BaseQueryStringBinder
    {
        public abstract object BindQueryString(ReadOnlyQueryString queryString);
    }

    public class QueryStringBinder
        : BaseQueryStringBinder
    {
        private readonly IDeconverter _deconverter;

        public QueryStringBinder(IDeconverter deconverter)
        {
            _deconverter = deconverter;
        }

        public override object BindQueryString(ReadOnlyQueryString queryString)
        {
            return _deconverter.Deconvert(new QueryStringGetValues(queryString), null);
        }
    }
}