using System;
using LinkMe.Apps.Presentation.Converters;

namespace LinkMe.Web.Areas.Members.Models.Profiles
{
    public class OtherMemberModelConverter
        : Converter<OtherMemberModel>
    {
        public override void Convert(OtherMemberModel obj, ISetValues values)
        {
            throw new NotImplementedException();
        }

        public override OtherMemberModel Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            return new OtherMemberModel
            {
                Affiliations = values.GetStringValue(OtherKeys.Affiliations),
                Awards = values.GetStringValue(OtherKeys.Awards),
                Professional = values.GetStringValue(OtherKeys.Professional),
                Courses = values.GetStringValue(OtherKeys.Courses),
                Interests = values.GetStringValue(OtherKeys.Interests),
                Other = values.GetStringValue(OtherKeys.Other),
                Referees = values.GetStringValue(OtherKeys.Referees),
            };
        }
    }
}