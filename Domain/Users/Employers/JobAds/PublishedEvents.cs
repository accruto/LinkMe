using System;

namespace LinkMe.Domain.Users.Employers.JobAds
{
    public static class PublishedEvents
    {
        public const string ApplicationSubmitted = "LinkMe.Domain.Users.Employers.JobAds.ApplicationSubmitted";
    }

    public class ApplicationSubmittedEventArgs
        : EventArgs
    {
        public Guid ApplicationId { get; private set; }

        public ApplicationSubmittedEventArgs(Guid applicationId)
        {
            ApplicationId = applicationId;
        }
    }
}