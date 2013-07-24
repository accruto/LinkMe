using System;
using LinkMe.Apps.Asp.Mvc.Models.Converters;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain;

namespace LinkMe.Web.Areas.Members.Models.Profiles
{
    public class EmploymentHistoryUpdateModelConverter
        : Converter<EmploymentHistoryUpdateModel>
    {
        public override void Convert(EmploymentHistoryUpdateModel obj, ISetValues values)
        {
            throw new NotImplementedException();
        }

        public override EmploymentHistoryUpdateModel Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            return new EmploymentHistoryUpdateModel
            {
                RecentProfession = values.GetValue<Profession>(EmploymentHistoryKeys.RecentProfession),
                RecentSeniority = values.GetValue<Seniority>(EmploymentHistoryKeys.RecentSeniority),
                IndustryIds = values.GetGuidArrayValue(EmploymentHistoryKeys.IndustryIds),
                Job = new JobModel
                {
                    Id = values.GetGuidValue(EmploymentHistoryKeys.Id),
                    Company = values.GetStringValue(EmploymentHistoryKeys.Company),
                    Title = values.GetStringValue(EmploymentHistoryKeys.Title),
                    Description = values.GetStringValue(EmploymentHistoryKeys.Description),
                    StartDate = new PartialDateConverter(false).Deconvert(EmploymentHistoryKeys.StartDate, values),
                    EndDate = new PartialDateConverter(false).Deconvert(EmploymentHistoryKeys.EndDate, values),
                    IsCurrent = values.GetBooleanValue(EmploymentHistoryKeys.IsCurrent),
                },
            };
        }
    }
}