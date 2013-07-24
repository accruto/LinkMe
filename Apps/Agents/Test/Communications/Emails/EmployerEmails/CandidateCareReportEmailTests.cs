using System;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails.EmployerClientEmails;
using LinkMe.Apps.Agents.Reports.Employers;
using LinkMe.Domain.Roles.Affiliations.Communities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.EmployerEmails
{
    [TestClass]
    public class CandidateCareReportEmailTests
        : EmailTests
    {
        public override TemplateEmail GeneratePreview(Community community)
        {
            var employer = CreateEmployer();
            return new CandidateCareReportEmail(employer, CreateAdministrator(), new CandidateCareReport(), employer.Organisation, DateTime.Now.Date.AddDays(-31), DateTime.Now.Date.AddDays(-1), "123");
        }
    }
}
