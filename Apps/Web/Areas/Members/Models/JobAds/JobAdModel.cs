using System;
using LinkMe.Apps.Agents.Users;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using System.Collections.Generic;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Context;

namespace LinkMe.Web.Areas.Members.Models.JobAds
{
    public class JobAdModel
    {
        public MemberJobAdView JobAd { get; set; }
        public IEmployer JobPoster { get; set; }
        public int DistinctViewedCount { get; set; }
        public ApplicantModel Applicant { get; set; }
        public VisitorStatus VisitorStatus { get; set; }
        public IList<MemberJobAdView> SuggestedJobs { get; set; }
        public JobAdSearchNavigation CurrentSearch { get; set; }
        public int ContactedLastWeek { get; set; }
        public string IntegratorName { get; set; }
        public FoldersModel Folders { get; set; }
        public JobAdStatusModel Status { get; set; }
        public string OrganisationCssFile { get; set; }
    }

    public class JobAdStatusModel
    {
        public int CurrentIndex { get; set; }
        public int TotalCount { get; set; }
        public JobAd PreviousJobAd { get; set; }
        public JobAd NextJobAd { get; set; }
    }

    public class EmailToModel
    {
        [Required]
        public string ToName { get; set; }
        [Required, EmailAddress(true)]
        public string ToEmailAddress { get; set; }
    }

    public class EmailJobAdsModel
        : JsonRequestModel
    {
        [Required]
        public string FromName { get; set; }
        [Required, EmailAddress(true)]
        public string FromEmailAddress { get; set; }
        [Required, ArrayLength(1, int.MaxValue), Prepare, Validate]
        public EmailToModel[] Tos { get; set; }
        [Required, ArrayLength(1, int.MaxValue)]
        public IList<Guid> JobAdIds { get; set; }
    }

    public enum JoinOrLoginToApply
    {
        SignMeUp, HaveLinkMePassword
    }
}
