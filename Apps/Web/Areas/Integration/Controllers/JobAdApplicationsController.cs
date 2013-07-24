using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Apps.Services.Security;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Framework.Instrumentation;
using LinkMe.Web.Areas.Integration.Models.JobAds;

namespace LinkMe.Web.Areas.Integration.Controllers
{
    public class JobAdApplicationsController
        : IntegrationController
    {
        private static readonly EventSource EventSource = new EventSource<JobAdApplicationsController>();
        private readonly IJobAdApplicationsManager _jobAdApplicationsManager;
        private readonly IServiceAuthenticationManager _serviceAuthenticationManager;

        public JobAdApplicationsController(IJobAdApplicationsManager jobAdApplicationsManager, IServiceAuthenticationManager serviceAuthenticationManager)
        {
            _jobAdApplicationsManager = jobAdApplicationsManager;
            _serviceAuthenticationManager = serviceAuthenticationManager;
        }

        [EnsureHttps]
        public ActionResult Application(Guid applicationId)
        {
            const string method = "Application";

            try
            {
                // Authenticate.

                var integratorUser = _serviceAuthenticationManager.AuthenticateRequest(HttpContext, IntegratorPermissions.GetJobApplication);

                // Process.

                var errors = new List<string>();
                var xml = _jobAdApplicationsManager.GetJobApplication(integratorUser, applicationId, errors);
                return Xml(new GetJobApplicationResponse(xml, errors.ToArray()));
            }
            catch (Exception ex)
            {
                EventSource.Raise(Event.Error, method, "Cannot process the request.", ex, new StandardErrorHandler());
                return Xml(new GetJobApplicationResponse(ex));
            }
        }

        [HttpPost]
        public ActionResult SetApplicationStatuses()
        {
            const string method = "SetApplicationStatuses";

            try
            {
                // Authenticate.

                var integratorUser = _serviceAuthenticationManager.AuthenticateRequest(HttpContext, IntegratorPermissions.PostJobAds);

                // Process.

                var errors = new List<string>();
                var xml = _jobAdApplicationsManager.SetApplicationStatuses(integratorUser, GetRequestXml(), errors);
                return Xml(new SetApplicationStatusesResponse(xml, errors.ToArray()));
            }
            catch (Exception ex)
            {
                EventSource.Raise(Event.Error, method, "Cannot process the request.", ex, new StandardErrorHandler());
                return Xml(new SetApplicationStatusesResponse(ex));
            }
        }
    }
}
