using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using LinkMe.Domain;
using LinkMe.Framework.Utility;

namespace LinkMe.Web.Areas.Members.Models.Profiles
{
    public class ProfileJavaScriptConverter
        : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            return null;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var result = new Dictionary<string, object>();
            foreach (var property in obj.GetType().GetProperties())
            {
                var value = property.GetValue(obj, null);
                if (value == null)
                {
                    result.Add(property.Name, "");
                }
                else if (value.GetType() == typeof(bool))
                {
                    result.Add(property.Name, (bool) value);
                }
                else if (value.GetType() == typeof(EthnicStatus))
                {
                    result.Add(property.Name, value);
                    var ethnicStatus = (EthnicStatus) value;
                    result.Add(ContactDetailsKeys.Aboriginal, ethnicStatus.IsFlagSet(EthnicStatus.Aboriginal));
                    result.Add(ContactDetailsKeys.TorresIslander, ethnicStatus.IsFlagSet(EthnicStatus.TorresIslander));
                }
                else if (value.GetType() == typeof(JobTypes))
                {
                    result.Add(property.Name, value);
                    var jobTypes = (JobTypes) value;
                    result.Add(DesiredJobKeys.Contract, jobTypes.IsFlagSet(JobTypes.Contract));
                    result.Add(DesiredJobKeys.FullTime, jobTypes.IsFlagSet(JobTypes.FullTime));
                    result.Add(DesiredJobKeys.JobShare, jobTypes.IsFlagSet(JobTypes.JobShare));
                    result.Add(DesiredJobKeys.PartTime, jobTypes.IsFlagSet(JobTypes.PartTime));
                    result.Add(DesiredJobKeys.Temp, jobTypes.IsFlagSet(JobTypes.Temp));
                }
                else
                {
                    result.Add(property.Name, value.ToString());
                }

                switch (property.Name)
                {
                    case ContactDetailsKeys.DateOfBirth:
                        result.Add(ContactDetailsKeys.DateOfBirthMonth, value == null ? "" : ((PartialDate)value).Month.ToString());
                        result.Add(ContactDetailsKeys.DateOfBirthYear, value == null ? "" : ((PartialDate)value).Year.ToString());
                        break;

                    case EmploymentHistoryKeys.IndustryIds:
                        var ids = (IList<Guid>)value;
                        if (ids != null)
                            result[EmploymentHistoryKeys.IndustryIds] = ids.ToArray();
                        break;

                    case EmploymentHistoryKeys.StartDate:
                        result.Add(EmploymentHistoryKeys.StartDateMonth, value == null || ((PartialDate)value).Month == null ? null : ((PartialDate)value).Month.Value.ToString());
                        result.Add(EmploymentHistoryKeys.StartDateYear, value == null ? null : ((PartialDate)value).Year.ToString());
                        break;

                    case EmploymentHistoryKeys.EndDate:
                        result.Add(EmploymentHistoryKeys.EndDateMonth, value == null || ((PartialDate)value).Month == null ? null : ((PartialDate)value).Month.Value.ToString());
                        result.Add(EmploymentHistoryKeys.EndDateYear, value == null ? null : ((PartialDate)value).Year.ToString());
                        break;

                    case DesiredJobKeys.RelocationCountryIds:
                        var relocationCountryIds = (IList<int>)value;
                        if (relocationCountryIds != null)
                            result[DesiredJobKeys.RelocationCountryIds] = relocationCountryIds.ToArray();
                        break;

                    case DesiredJobKeys.RelocationCountryLocationIds:
                        var relocationCountryLocationIds = (IList<int>)value;
                        if (relocationCountryLocationIds != null)
                            result[DesiredJobKeys.RelocationCountryLocationIds] = relocationCountryLocationIds.ToArray();
                        break;

                    case EmploymentHistoryKeys.Jobs:
                        var jobs = (IList<JobModel>)value;
                        if (jobs != null)
                            result[EmploymentHistoryKeys.Jobs] = jobs.ToArray();
                        break;

                    case EducationKeys.Schools:
                        var schools = (IList<SchoolModel>)value;
                        if (schools != null)
                            result[EducationKeys.Schools] = schools.ToArray();

                        break;
                }
            }
            return result;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return new[] { typeof(ContactDetailsMemberModel), typeof(DesiredJobMemberModel), typeof(CareerObjectivesMemberModel), typeof(EmploymentHistoryMemberModel), typeof(JobModel), typeof(EducationMemberModel), typeof(SchoolModel), typeof(OtherMemberModel) }; }
        }
    }

    public class ProfileJavaScriptSerializer
    {
        public string Serialize(ContactDetailsMemberModel model)
        {
            return Serialize((object)model);
        }

        public string Serialize(DesiredJobMemberModel model)
        {
            return Serialize((object)model);
        }

        public string Serialize(CareerObjectivesMemberModel model)
        {
            return Serialize((object)model);
        }

        public string Serialize(EducationMemberModel model)
        {
            return Serialize((object)model);
        }

        public string Serialize(EmploymentHistoryMemberModel model)
        {
            return Serialize((object)model);
        }

        public string Serialize(OtherMemberModel model)
        {
            return Serialize((object)model);
        }

        public string Serialize(MemberStatusModel model)
        {
            return Serialize((object)model);
        }

        public string Serialize(VisibilityModel model)
        {
            return Serialize((object)model);
        }

        private static string Serialize(object model)
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new ProfileJavaScriptConverter() });
            return serializer.Serialize(model);
        }
    }
}
