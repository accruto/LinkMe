using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using LinkMe.Apps.Presentation.Converters;

namespace LinkMe.Apps.Api.Areas.Employers.Models.Candidates
{
    public class CandidateModelJavaScriptConverter
        : JavaScriptConverter
    {
        private static readonly Type[] Types = new[] { typeof(CandidateModel), typeof(SalaryModel), typeof(JobModel) };

        public override object Deserialize(IDictionary<string, object> values, Type type, JavaScriptSerializer serializer)
        {
            if (type == typeof(CandidateModel))
                return new CandidateModelConverter().Deconvert(new JavaScriptValues(values, serializer), null);
            if (type == typeof(SalaryModel))
                return new SalaryModelConverter().Deconvert(new JavaScriptValues(values, serializer), null);
            if (type == typeof(JobModel))
                return new JobModelConverter().Deconvert(new JavaScriptValues(values, serializer), null);
            return null;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var values = new Dictionary<string, object>();
            if (obj is CandidateModel)
                new CandidateModelConverter().Convert((CandidateModel) obj, new JavaScriptValues(values, serializer));
            else if (obj is SalaryModel)
                new SalaryModelConverter().Convert((SalaryModel)obj, new JavaScriptValues(values, serializer));
            else if (obj is JobModel)
                new JobModelConverter().Convert((JobModel)obj, new JavaScriptValues(values, serializer));
            return values;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return Types; }
        }
    }
}
