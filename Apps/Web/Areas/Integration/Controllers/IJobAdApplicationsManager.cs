using System;
using System.Collections.Generic;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Integration;

namespace LinkMe.Web.Areas.Integration.Controllers
{
    public interface IJobAdApplicationsManager
    {
        string GetJobApplication(IntegratorUser integratorUser, Guid applicationId, List<string> errors);
        string SetApplicationStatuses(IntegratorUser integratorUser, string requestXml, List<string> errors);
        DocFile GetResume(Member member);
    }
}
