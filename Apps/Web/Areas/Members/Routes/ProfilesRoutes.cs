using System;
using System.Web;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain;
using LinkMe.Web.Areas.Members.Controllers.Profiles;
using LinkMe.Web.Areas.Members.Models.Profiles;

namespace LinkMe.Web.Areas.Members.Routes
{
    public static class ProfilesRoutes
    {
        public static RouteReference Profile { get; private set; }
        public static RouteReference UpdateStatus { get; private set; }

        public static RouteReference Photo { get; private set; }
        public static RouteReference Download { get; private set; }
        public static RouteReference DownloadResume { get; private set; }

        public static RouteReference ApiUploadResume { get; private set; }
        public static RouteReference ApiParseResume { get; private set; }
        public static RouteReference ApiUploadPhoto { get; private set; }
        public static RouteReference ApiRemovePhoto { get; private set; }
        public static RouteReference ApiSetCurrent { get; private set; }
        public static RouteReference ApiSendResume { get; private set; }

        public static RouteReference ApiVisibility { get; private set; }
        public static RouteReference ApiContactDetails { get; private set; }
        public static RouteReference ApiDesiredJob { get; private set; }
        public static RouteReference ApiCareerObjectives { get; private set; }
        public static RouteReference ApiEmploymentHistory { get; private set; }
        public static RouteReference ApiEducation { get; private set; }
        public static RouteReference ApiOther { get; private set; }
        public static RouteReference ApiRemoveJob { get; private set; }
        public static RouteReference ApiRemoveSchool { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Profile = context.MapAreaRoute<ProfilesController>("members/profile", c => c.Profile);
            context.MapAreaRoute<ProfilesController>("members/profile/contactdetails", c => c.ContactDetails);
            context.MapAreaRoute<ProfilesController>("members/profile/desiredjob", c => c.DesiredJob);
            context.MapAreaRoute<ProfilesController>("members/profile/careerobjectives", c => c.CareerObjectives);
            context.MapAreaRoute<ProfilesController>("members/profile/employmenthistory", c => c.EmploymentHistory);
            context.MapAreaRoute<ProfilesController>("members/profile/education", c => c.Education);
            context.MapAreaRoute<ProfilesController>("members/profile/other", c => c.Other);

            Photo = context.MapAreaRoute<ProfileFilesController>("members/profile/photo", c => c.Photo);
            Download = context.MapAreaRoute<ProfileFilesController>("members/profile/download", c => c.Download);
            DownloadResume = context.MapAreaRoute<ProfileFilesController, Guid>("members/profile/resumes/{fileReferenceId}/download", c => c.DownloadResume);

            UpdateStatus = context.MapAreaRoute<StatusController, CandidateStatus?>("members/profile/status/update", c => c.UpdateStatus);
            context.MapRedirectRoute("members/profile/status/updateavailablenow", UpdateStatus, new { status = CandidateStatus.AvailableNow.ToString() });

            ApiUploadResume = context.MapAreaRoute<ProfilesApiController, HttpPostedFileBase>("members/profile/api/upload", c => c.Upload);
            ApiParseResume = context.MapAreaRoute<ProfilesApiController, Guid>("members/profile/api/parse", c => c.Parse);
            ApiUploadPhoto = context.MapAreaRoute<ProfilesApiController, HttpPostedFileBase>("members/profile/api/uploadphoto", c => c.UploadPhoto);
            ApiRemovePhoto = context.MapAreaRoute<ProfilesApiController>("members/profile/api/removephoto", c => c.RemovePhoto);
            ApiSetCurrent = context.MapAreaRoute<ProfilesApiController>("members/profile/api/setcurrent", c => c.SetCurrent);
            ApiSendResume = context.MapAreaRoute<ProfilesApiController>("members/profile/api/sendresume", c => c.SendResume);

            ApiVisibility = context.MapAreaRoute<ProfilesApiController, VisibilityModel>("members/profile/api/visibility", c => c.Visibility);
            ApiContactDetails = context.MapAreaRoute<ProfilesApiController, ContactDetailsMemberModel>("members/profile/api/contactdetails", c => c.ContactDetails);
            ApiDesiredJob = context.MapAreaRoute<ProfilesApiController, DesiredJobMemberModel>("members/profile/api/desiredjob", c => c.DesiredJob);
            ApiCareerObjectives = context.MapAreaRoute<ProfilesApiController, CareerObjectivesMemberModel>("members/profile/api/careerobjectives", c => c.CareerObjectives);
            ApiEmploymentHistory = context.MapAreaRoute<ProfilesApiController, EmploymentHistoryUpdateModel>("members/profile/api/employmenthistory", c => c.EmploymentHistory);
            ApiEducation = context.MapAreaRoute<ProfilesApiController, EducationUpdateModel>("members/profile/api/education", c => c.Education);
            ApiOther = context.MapAreaRoute<ProfilesApiController, OtherMemberModel>("members/profile/api/other", c => c.Other);
            ApiRemoveJob = context.MapAreaRoute<ProfilesApiController, Guid>("members/profile/api/employmenthistory/removejob", c => c.RemoveJob);
            ApiRemoveSchool = context.MapAreaRoute<ProfilesApiController, Guid>("members/profile/api/education/removeschool", c => c.RemoveSchool);

            // Old pages.

            context.MapRedirectRoute("ui/registered/networkers/NetworkerMyResumeForm.aspx", Profile);
            context.MapRedirectRoute("ui/registered/networkers/AboutMe.aspx", Profile);
            context.MapRedirectRoute("ui/registered/networkers/employmentdetails.aspx", Profile);
        }
    }
}
