using System;
using System.Linq;
using System.Net;
using System.Text;
using LinkMe.Apps.Api.Areas.Employers.Models.Candidates;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Presentation.Domain.Users.Members;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Candidates
{
    [TestClass]
    public class GetCandidateTests
        : CandidateTests
    {
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly IMemberStatusQuery _memberStatusQuery = Resolve<IMemberStatusQuery>();

        [TestMethod]
        public void TestNotFoundCandidate()
        {
            var model = Deserialize<JsonResponseModel>(Get(HttpStatusCode.NotFound, GetCandidateUrl(Guid.NewGuid())));
            AssertJsonError(model, null, "400", "The candidate cannot be found.");
        }

        [TestMethod]
        public void TestAnonymousCandidate()
        {
            var member = CreateMember(0);

            // Call.

            var response = Get(GetCandidateUrl(member.Id));

            // Assert.

            var view = GetView(null, member);
            AssertResponse(response, view);

            var model = Deserialize<CandidateResponseModel>(response, new CandidateModelJavaScriptConverter());
            AssertCandidate(model, view);
        }

        [TestMethod]
        public void TestDesiredJobTypes()
        {
            // No desired job types.

            var member = CreateMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.DesiredJobTypes = JobTypes.None;
            _candidatesCommand.UpdateCandidate(candidate);

            var response = Get(GetCandidateUrl(member.Id));
            var view = GetView(null, member);
            AssertResponse(response, view);
            var model = Deserialize<CandidateResponseModel>(response, new CandidateModelJavaScriptConverter());
            AssertCandidate(model, view);

            // A desired job type.

            candidate.DesiredJobTypes = JobTypes.FullTime;
            _candidatesCommand.UpdateCandidate(candidate);

            response = Get(GetCandidateUrl(member.Id));
            view = GetView(null, member);
            AssertResponse(response, view);
            model = Deserialize<CandidateResponseModel>(response, new CandidateModelJavaScriptConverter());
            AssertCandidate(model, view);

            // Multiple desired job types.

            candidate.DesiredJobTypes = JobTypes.FullTime | JobTypes.PartTime | JobTypes.Contract;
            _candidatesCommand.UpdateCandidate(candidate);

            response = Get(GetCandidateUrl(member.Id));
            view = GetView(null, member);
            AssertResponse(response, view);
            model = Deserialize<CandidateResponseModel>(response, new CandidateModelJavaScriptConverter());
            AssertCandidate(model, view);
        }

        private void AssertResponse(string response, EmployerMemberView view)
        {
            var sb = new StringBuilder();
            sb.Append("{\"Candidate\":{")
                .Append("\"Id\":\"").Append(view.Id.ToString()).Append("\"")
                .Append(",\"CanContact\":\"").Append(view.CanContact()).Append("\"")
                .Append(",\"CanContactByPhone\":\"").Append(view.CanContactByPhone()).Append("\"")
                .Append(",\"HasBeenViewed\":").Append(view.HasBeenViewed ? "true" : "false")
                .Append(",\"HasBeenAccessed\":").Append(view.HasBeenAccessed ? "true" : "false")
                .Append(",\"IsInMobileFolder\":").Append(view.IsInMobileFolder ? "true" : "false")
                .Append(",\"FullName\":").Append(view.GetCandidateTitle() == null ? "null" : "\"" + view.GetCandidateTitle() + "\"")
                .Append(",\"PhoneNumbers\":").Append(view.PhoneNumbers == null || view.PhoneNumbers.All(p => string.IsNullOrEmpty(p.Number)) ? "null" : "\"" + view.PhoneNumbers + "\"")
                .Append(",\"Status\":\"").Append(view.Status).Append("\"")
                .Append(",\"LastUpdatedTime\":\"").Append(_memberStatusQuery.GetLastUpdatedTime(view, view, view.Resume).ToUniversalTime().ToString("o")).Append("\"")
                .Append(",\"Location\":").Append(view.Address.Location.ToString() == null ? "null" : "\"" + view.Address.Location + "\"")
                .Append(",\"DesiredJobTitle\":").Append(view.DesiredJobTitle == null ? "null" : "\"" + view.DesiredJobTitle + "\"");

            if (view.DesiredJobTypes.IsFlagSet(JobTypes.FullTime))
                sb.Append(",\"FullTime\":true");
            if (view.DesiredJobTypes.IsFlagSet(JobTypes.PartTime))
                sb.Append(",\"PartTime\":true");
            if (view.DesiredJobTypes.IsFlagSet(JobTypes.Contract))
                sb.Append(",\"Contract\":true");
            if (view.DesiredJobTypes.IsFlagSet(JobTypes.JobShare))
                sb.Append(",\"JobShare\":true");
            if (view.DesiredJobTypes.IsFlagSet(JobTypes.Temp))
                sb.Append(",\"Temp\":true");

            sb.Append(",\"Summary\":").Append(view.Resume.Summary == null ? "null" : "\"" + view.Resume.Summary.Replace("\r\n", "\\r\\n").Replace("\t", "\\t").Replace("'", "\\u0027") + "\"")
                .Append(",\"Jobs\":[");
            for (var index = 0; index < view.Resume.Jobs.Count; ++index)
            {
                var job = view.Resume.Jobs[index];
                if (index != 0)
                    sb.Append(",");
                sb.Append("{\"Title\":").Append(job.Title == null ? "null" : "\"" + job.Title + "\"")
                    .Append(",\"IsCurrent\":").Append(job.IsCurrent ? "true" : "false")
                    .Append(",\"StartDate\":").Append(job.Dates == null || job.Dates.Start == null ? "null" : string.Format("\"{0:D4}-{1:D2}\"", job.Dates.Start.Value.Year, job.Dates.Start.Value.Month))
                    .Append(",\"EndDate\":").Append(job.Dates == null || job.Dates.End == null ? "null" : string.Format("\"{0:D4}-{1:D2}\"", job.Dates.End.Value.Year, job.Dates.End.Value.Month))
                    .Append(",\"Company\":").Append(job.Company == null ? "null" : "\"" + job.Company + "\"")
                    .Append("}");
            }
            sb.Append("]").Append(",\"DesiredSalary\":").Append(view.DesiredSalary == null ? "null" : "\"" + view.DesiredSalary + "\"");
            sb.Append("},\"Success\":true,\"Errors\":null}");
            Assert.AreEqual(sb.ToString(), response);
        }
    }
}
