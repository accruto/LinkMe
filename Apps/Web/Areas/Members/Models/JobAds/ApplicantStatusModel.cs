using System;

namespace LinkMe.Web.Areas.Members.Models.JobAds
{
    public class ResumeFileModel
    {
        public Guid FileReferenceId { get; set; }
        public string FileName { get; set; }
    }

    public class ApplicantModel
    {
        public bool HasResume { get; set; }
        public ResumeFileModel LastUsedResumeFile { get; set; }
        public int ProfileCompletePercent { get; set; }
    }
}
