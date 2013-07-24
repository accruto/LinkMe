using System;
using LinkMe.Apps.Agents.Users.Members;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Candidates;

namespace LinkMe.Web.Areas.Members.Models.Profiles
{
    public class DesiredJobMemberModelConverter
        : Converter<DesiredJobMemberModel>
    {
        public override void Convert(DesiredJobMemberModel obj, ISetValues values)
        {
            throw new NotImplementedException();
        }

        public override DesiredJobMemberModel Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            return new DesiredJobMemberModel
            {
                DesiredJobTitle = values.GetStringValue(DesiredJobKeys.DesiredJobTitle),
                DesiredJobTypes = values.GetFlagsValue<JobTypes>() ?? Defaults.DesiredJobTypes,
                Status = values.GetValue<CandidateStatus>(DesiredJobKeys.Status) ?? Defaults.CandidateStatus,
                DesiredSalaryLowerBound = values.GetDecimalValue(DesiredJobKeys.DesiredSalaryLowerBound),
                DesiredSalaryRate = values.GetValue<SalaryRate>(DesiredJobKeys.DesiredSalaryRate) ?? Defaults.DesiredSalaryRate,
                IsSalaryNotVisible = values.GetBooleanValue(DesiredJobKeys.IsSalaryNotVisible) ?? !Defaults.SalaryVisibility,
                RelocationPreference = values.GetValue<RelocationPreference>(DesiredJobKeys.RelocationPreference) ?? Defaults.RelocationPreference,
                RelocationCountryIds = values.GetIntArrayValue(DesiredJobKeys.RelocationCountryIds),
                RelocationCountryLocationIds = values.GetIntArrayValue(DesiredJobKeys.RelocationCountryLocationIds),
                SendSuggestedJobs = values.GetBooleanValue(DesiredJobKeys.SendSuggestedJobs) ?? Defaults.SendSuggestedJobs,
            };
        }
    }
}