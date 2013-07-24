using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Api.Areas.Employers.Models.Candidates
{
    public class SalaryModelConverter
        : Converter<SalaryModel>
    {
        public override void Convert(SalaryModel salary, ISetValues values)
        {
            if (salary == null)
                return;
            values.SetValue("LowerBound", salary.LowerBound);
            values.SetValue("UpperBound", salary.UpperBound);
        }

        public override SalaryModel Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            var lowerBound = values.GetDecimalValue("LowerBound");
            var upperBound = values.GetDecimalValue("UpperBound");
            if (lowerBound == null && upperBound == null)
                return null;
            return new SalaryModel { LowerBound = lowerBound, UpperBound = upperBound };
        }
    }

    public class JobModelConverter
        : Converter<JobModel>
    {
        public override void Convert(JobModel job, ISetValues values)
        {
            if (job == null)
                return;
            values.SetValue("Title", job.Title);
            values.SetValue("IsCurrent", job.IsCurrent);
            values.SetValue("StartDate", job.StartDate);
            values.SetValue("EndDate", job.EndDate);
            values.SetValue("Company", job.Company);
        }

        public override JobModel Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            return new JobModel
            {
                Title = values.GetStringValue("Title"),
                IsCurrent = values.GetBooleanValue("IsCurrent") ?? false,
                StartDate = values.GetPartialDateValue("StartDate"),
                EndDate = values.GetPartialDateValue("EndDate"),
                Company = values.GetStringValue("Company"),
            };
        }
    }

    public class CandidateModelConverter
        : Converter<CandidateModel>
    {
        public override void Convert(CandidateModel model, ISetValues values)
        {
            values.SetValue("Id", model.Id);
            values.SetValue("CanContact", model.CanContact);
            values.SetValue("CanContactByPhone", model.CanContactByPhone);
            values.SetValue("HasBeenViewed", model.HasBeenViewed);
            values.SetValue("HasBeenAccessed", model.HasBeenAccessed);
            values.SetValue("IsInMobileFolder", model.IsInMobileFolder);
            values.SetValue("FullName", model.FullName);
            values.SetArrayValue("PhoneNumbers", model.PhoneNumbers);
            values.SetValue("Status", model.Status);
            values.SetValue("LastUpdatedTime", model.LastUpdatedTime);
            values.SetValue("Location", model.Location);
            values.SetValue("DesiredJobTitle", model.DesiredJobTitle);
            values.SetFlagsValue(model.DesiredJobTypes);
            values.SetValue("Summary", model.Summary);
            values.SetArrayValue("Jobs", model.Jobs);
            values.SetChildValue("DesiredSalary", model.DesiredSalary);
        }

        public override CandidateModel Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            return new CandidateModel
            {
                Id = values.GetGuidValue("Id").Value,
                CanContact = values.GetValue<CanContactStatus>("CanContact").Value,
                CanContactByPhone = values.GetValue<CanContactStatus>("CanContactByPhone").Value,
                HasBeenViewed = values.GetBooleanValue("HasBeenViewed").Value,
                HasBeenAccessed = values.GetBooleanValue("HasBeenAccessed").Value,
                IsInMobileFolder = values.GetBooleanValue("IsInMobileFolder").Value,
                FullName = values.GetStringValue("FullName"),
                PhoneNumbers = values.GetStringArrayValue("PhoneNumbers"),
                Status = values.GetValue<CandidateStatus>("Status").Value,
                LastUpdatedTime = values.GetDateTimeValue("LastUpdatedTime"),
                Location = values.GetStringValue("Location"),
                DesiredJobTitle = values.GetStringValue("DesiredJobTitle"),
                DesiredJobTypes = values.GetFlagsValue<JobTypes>() ?? JobTypes.None,
                Summary = values.GetStringValue("Summary"),
                DesiredSalary = values.GetChildValue<SalaryModel>("DesiredSalary"),
                Jobs = values.GetArrayValue<JobModel>("Jobs"),
            };
        }
    }
}
