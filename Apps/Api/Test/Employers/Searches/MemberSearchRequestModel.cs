using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Query.Search.Members;

namespace LinkMe.Apps.Api.Test.Employers.Searches
{
    public class MemberSearchRequestModel
        : JsonRequestModel
    {
        public string Name { get; set; }
        public bool IsAlert { get; set; }
        public MemberSearchCriteria Criteria { get; set; }
        public string DeviceToken { get; set; }
    }

    public class MemberSearchRequestModelConverter
        : Converter<MemberSearchRequestModel>
    {
        public override void Convert(MemberSearchRequestModel model, ISetValues values)
        {
            values.SetValue("Name", model.Name);
            values.SetValue("IsAlert", model.IsAlert);
            values.SetChildValue("Criteria", model.Criteria);
            values.SetValue("DeviceToken", model.DeviceToken);
        }

        public override MemberSearchRequestModel Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            return new MemberSearchRequestModel
            {
                Name = values.GetStringValue("Name"),
                IsAlert = values.GetBooleanValue("IsAlert").Value,
                Criteria = values.GetChildValue<MemberSearchCriteria>("Criteria"),
                DeviceToken = values.GetStringValue("DeviceToken"),
            };
        }
    }
}