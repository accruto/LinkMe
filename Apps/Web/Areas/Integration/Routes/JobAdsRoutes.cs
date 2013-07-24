using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Integration.Controllers;

namespace LinkMe.Web.Areas.Integration.Routes
{
    public static class JobAdsRoutes
    {
        public static RouteReference Application { get; private set; }
        public static RouteReference Resume { get; private set; }
        public static RouteReference ResumeFile { get; private set; }
        public static RouteReference ResumeFileName { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapAreaRoute<JobAdsController, string, string, string>("jobads", c => c.JobAds);
            context.MapAreaRoute<JobAdsController>("jobads/close", c => c.CloseJobAds);
            context.MapAreaRoute<JobAdsController>("jobadids", c => c.JobAdIds);

            context.MapAreaRoute<JobAdApplicationsController>("jobapplications/status", c => c.SetApplicationStatuses);
            Application = context.MapAreaRoute<JobAdApplicationsController, Guid>("jobapplication/{applicationId}", c => c.Application);

            Resume = context.MapAreaRoute<JobAdFilesController, Guid>("resume/{candidateId}/file/rtf", c => c.Resume);
            ResumeFile = context.MapAreaRoute<JobAdFilesController, Guid>("file/{fileId}", c => c.ResumeFile);
            ResumeFileName = context.MapAreaRoute<JobAdFilesController, Guid>("file/{fileId}/{fileName}", c => c.ResumeFileName);
        }
    }
}