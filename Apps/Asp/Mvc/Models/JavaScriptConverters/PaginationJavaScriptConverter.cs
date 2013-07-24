using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using LinkMe.Apps.Asp.Mvc.Models.Converters;
using LinkMe.Apps.Presentation.Converters;

namespace LinkMe.Apps.Asp.Mvc.Models.JavaScriptConverters
{
    public class PaginationJavaScriptConverter
        : JavaScriptConverter
    {
        private readonly int? _defaultItems;

        public PaginationJavaScriptConverter(int? defaultItems)
        {
            _defaultItems = defaultItems;
        }

        public override object Deserialize(IDictionary<string, object> values, Type type, JavaScriptSerializer serializer)
        {
            if (type != typeof(Pagination))
                return null;
            return new PaginationConverter(_defaultItems).Deconvert(new JavaScriptValues(values, serializer), null);
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var values = new Dictionary<string, object>();
            var criteria = obj as Pagination;
            if (criteria == null)
                return values;
            new PaginationConverter(_defaultItems).Convert(criteria, new JavaScriptValues(values, serializer));
            return values;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return new[] { typeof(Pagination) }; }
        }
    }
}