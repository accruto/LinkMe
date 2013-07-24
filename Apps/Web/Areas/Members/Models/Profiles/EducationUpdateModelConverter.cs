using System;
using LinkMe.Apps.Asp.Mvc.Models.Converters;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain;

namespace LinkMe.Web.Areas.Members.Models.Profiles
{
    public class EducationUpdateModelConverter
        : Converter<EducationUpdateModel>
    {
        public override void Convert(EducationUpdateModel obj, ISetValues values)
        {
            throw new NotImplementedException();
        }

        public override EducationUpdateModel Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            return new EducationUpdateModel
            {
                HighestEducationLevel = values.GetValue<EducationLevel>(EducationKeys.HighestEducationLevel),
                School = new SchoolModel
                {
                    Id = values.GetGuidValue(EducationKeys.Id),
                    City = values.GetStringValue(EducationKeys.City),
                    EndDate = new PartialDateConverter(false).Deconvert(EducationKeys.EndDate, values),
                    IsCurrent = values.GetBooleanValue(EducationKeys.IsCurrent),
                    Description = values.GetStringValue(EducationKeys.Description),
                    Institution = values.GetStringValue(EducationKeys.Institution),
                    Major = values.GetStringValue(EducationKeys.Major),
                    Degree = values.GetStringValue(EducationKeys.Degree),
                }
            };
        }
    }
}