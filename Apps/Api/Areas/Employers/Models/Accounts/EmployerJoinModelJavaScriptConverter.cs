using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using LinkMe.Apps.Presentation.Converters;

namespace LinkMe.Apps.Api.Areas.Employers.Models.Accounts
{
    public class EmployerJoinModelJavaScriptConverter
        : JavaScriptConverter
    {
        private static readonly Type[] Types = new[] { typeof(EmployerJoinModel) };

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var values = new Dictionary<string, object>();
            if (!(obj is EmployerJoinModel))
                return values;
            new EmployerJoinModelConverter().Convert((EmployerJoinModel)obj, new JavaScriptValues(values, serializer));
            return values;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return Types; }
        }
    }
}
