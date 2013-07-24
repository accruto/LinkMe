namespace LinkMe.Query.Reports.Users.Employers
{
    public class EmployerMemberAccessReport
    {
        public int TotalAccesses { get; set; }
        public int DistinctAccesses { get; set; }
        public int MessagesSent { get; set; }
        public int PhoneNumbersViewed { get; set; }
        public int ResumesDownloaded { get; set; }
        public int ResumesSent { get; set; }
        public int Unlockings { get; set; }
    }
}