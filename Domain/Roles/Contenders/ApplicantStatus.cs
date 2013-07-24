namespace LinkMe.Domain.Roles.Contenders
{
    public enum ApplicantStatus
        : byte
    {
        /// <summary>
        /// The application has not yet been submitted to the job poster.
        /// </summary>
        NotSubmitted = 0,
        /// <summary>
        /// The application has been submitted by the candidate, but has not been actioned by the job poster.
        /// </summary>
        New = 1,
        /// <summary>
        /// The job poster has shortlisted the application.
        /// </summary>
        Shortlisted = 2,
        /// <summary>
        /// The job poster has rejected the application.
        /// </summary>
        Rejected = 3,
        /// <summary>
        /// The job poster has removed the application from the job
        /// </summary>
        Removed = 8
    }
}