using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Query.Search.Members;

namespace LinkMe.Apps.Asp.Mvc.Models.JavaScriptConverters
{
    public class MemberSearchCriteriaJavaScriptConverter
        : JavaScriptConverter
    {
        private readonly ILocationQuery _locationQuery;
        private readonly IIndustriesQuery _industriesQuery;

        public MemberSearchCriteriaJavaScriptConverter(ILocationQuery locationQuery, IIndustriesQuery industriesQuery)
        {
            _locationQuery = locationQuery;
            _industriesQuery = industriesQuery;
        }

        public override object Deserialize(IDictionary<string, object> values, Type type, JavaScriptSerializer serializer)
        {
            if (type != typeof(MemberSearchCriteria) && type != typeof(MemberSearchCriteria))
                return null;
            return new MemberSearchCriteriaConverter(_locationQuery, _industriesQuery).Deconvert(new JavaScriptValues(values, serializer), null);
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var values = new Dictionary<string, object>();
            var criteria = obj as MemberSearchCriteria;
            if (criteria == null)
                return values;
            new MemberSearchCriteriaConverter(_locationQuery, _industriesQuery).Convert(criteria, new JavaScriptValues(values, serializer));
            return values;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return new[] { typeof(MemberSearchCriteria) }; }
        }
    }
}