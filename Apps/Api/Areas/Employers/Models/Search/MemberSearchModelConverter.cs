using System;
using LinkMe.Apps.Presentation.Converters;

namespace LinkMe.Apps.Api.Areas.Employers.Models.Search
{
    public class MemberSearchModelConverter
        : Converter<MemberSearchModel>
    {
        public override void Convert(MemberSearchModel model, ISetValues values)
        {
            values.SetValue("Id", model.Id);
            values.SetValue("Name", model.Name);
            values.SetValue("IsAlert", model.IsAlert);
            values.SetChildValue("Criteria", model.Criteria);
        }

        public override MemberSearchModel Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            throw new NotImplementedException();
        }
    }
}
