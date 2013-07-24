using System;
using System.IO;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.AdministratorEmails;
using LinkMe.Apps.Agents.Reports.Employers;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Files;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.InternalEmails
{
    [TestClass]
    public class AccountManagerReportEmailTests
        : EmailTests
    {
        public override TemplateEmail GeneratePreview(Community community)
        {
            var employer = CreateEmployer();
            var report = new ResumeSearchActivityReport
            {
                ClientId = employer.Organisation.Id,
                SendToAccountManager = true,
                SendToClient = true,
            };
            var email = new AccountManagerReportEmail(CreateAdministrator(), report, employer.Organisation as VerifiedOrganisation, DateTime.Now.Date.AddDays(-31), DateTime.Now.Date.AddDays(-1));

            var fileName = report.Name + " - " + employer.Organisation.FullName + ".pdf";
            var attachmentStream = new MemoryStream();
            email.AddAttachments(new[] { new ContentAttachment(attachmentStream, fileName, MediaType.Pdf) });

            return email;
        }
    }
}
