using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Results;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Apps.Services.Security;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Files;

namespace LinkMe.Web.Areas.Integration.Controllers
{
    public class JobAdFilesController
        : ViewController
    {
        private static readonly EventSource EventSource = new EventSource<JobAdFilesController>();
        private readonly IMembersQuery _membersQuery;
        private readonly IJobAdApplicationsManager _jobAdApplicationsManager;
        private readonly IServiceAuthenticationManager _serviceAuthenticationManager;
        private readonly IFilesQuery _filesQuery;

        public JobAdFilesController(IJobAdApplicationsManager jobAdApplicationsManager, IMembersQuery membersQuery, IServiceAuthenticationManager serviceAuthenticationManager, IFilesQuery filesQuery)
        {
            _jobAdApplicationsManager = jobAdApplicationsManager;
            _membersQuery = membersQuery;
            _serviceAuthenticationManager = serviceAuthenticationManager;
            _filesQuery = filesQuery;
        }

        public ActionResult Resume(Guid candidateId)
        {
            const string method = "Resume";

            try
            {
                // Authenticate.

                _serviceAuthenticationManager.AuthenticateRequest(HttpContext, IntegratorPermissions.GetJobApplication);

                var member = _membersQuery.GetMember(candidateId);
                if (member == null)
                    return HttpNotFound();
                
                return DocFile(_jobAdApplicationsManager.GetResume(member), MediaType.RichText);
            }
            catch (Exception ex)
            {
                EventSource.Raise(Event.Error, method, "Cannot process the request.", ex, new StandardErrorHandler());
                return new TextResult("Error: " + ex.Message);
            }
        }

        public ActionResult ResumeFile(Guid fileId)
        {
            return GetResumeFile(fileId);
        }

        public ActionResult ResumeFileName(Guid fileId)
        {
            return GetResumeFile(fileId);
        }

        private ActionResult GetResumeFile(Guid fileId)
        {
            const string method = "Resume";

            try
            {
                // Look for the file first.

                var file = _filesQuery.GetFileReference(fileId);
                if (file == null || file.FileData.FileType != FileType.Resume)
                {
                    return HttpNotFound();
                }

                // Authenticate.

                _serviceAuthenticationManager.AuthenticateRequest(HttpContext, IntegratorPermissions.GetJobApplication);

                return File(_filesQuery.OpenFile(file), file.MediaType, file.FileName);
            }
            catch (Exception ex)
            {
                EventSource.Raise(Event.Error, method, "Cannot process the request.", ex, new StandardErrorHandler());
                return new TextResult("Error: " + ex.Message);
            }
        }
    }
}
