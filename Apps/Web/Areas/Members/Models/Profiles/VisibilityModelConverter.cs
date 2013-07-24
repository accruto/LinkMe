using System;
using LinkMe.Apps.Presentation.Converters;

namespace LinkMe.Web.Areas.Members.Models.Profiles
{
    public class VisibilityModelConverter
        : Converter<VisibilityModel>
    {
        public override void Convert(VisibilityModel obj, ISetValues values)
        {
            throw new NotImplementedException();
        }

        public override VisibilityModel Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            return new VisibilityModel
            {
                ShowResume = values.GetBooleanValue(VisibilityKeys.ShowResume) ?? false,
                ShowRecentEmployers = values.GetBooleanValue(VisibilityKeys.ShowRecentEmployers) ?? false,
                ShowName = values.GetBooleanValue(VisibilityKeys.ShowName) ?? false,
                ShowPhoneNumbers = values.GetBooleanValue(VisibilityKeys.ShowPhoneNumbers) ?? false,
                ShowProfilePhoto = values.GetBooleanValue(VisibilityKeys.ShowProfilePhoto) ?? false,
            };
        }
    }
}