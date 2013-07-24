using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using LinkMe.Apps.Presentation.Converters;

namespace LinkMe.Apps.Api.Areas.Employers.Models.Search
{
    public class MemberSearchModelJavaScriptConverter
        : JavaScriptConverter
    {
        private static readonly Type[] Types = new[] { typeof(MemberSearchModel) };

        public override object Deserialize(IDictionary<string, object> values, Type type, JavaScriptSerializer serializer)
        {
            if (type == typeof(MemberSearchModel))
                return new MemberSearchModelConverter().Deconvert(new JavaScriptValues(values, serializer), null);
            return null;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var values = new Dictionary<string, object>();
            if (obj is MemberSearchModel)
                new MemberSearchModelConverter().Convert((MemberSearchModel)obj, new JavaScriptValues(values, serializer));
            return values;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return Types; }
        }
    }
}
