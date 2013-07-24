using System;
using LinkMe.Apps.Asp.Json.Models;

namespace LinkMe.Web.Areas.Members.Models.Profiles
{
    public class JsonProfileModel
        : JsonResponseModel
    {
        public MemberStatusModel Profile { get; set; }
    }

    public class JsonProfilePhotoModel
        : JsonProfileModel
    {
        public Guid PhotoId { get; set; }
    }

    public class JsonProfileSchoolModel
        : JsonProfileModel
    {
        public Guid? SchoolId { get; set; }
    }

    public class JsonProfileJobModel
        : JsonProfileModel
    {
        public Guid? JobId { get; set; }
    }
}
