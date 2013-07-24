using System;
using LinkMe.Domain.Roles.Contenders;

namespace LinkMe.Domain.Users.Employers.Applicants
{
    internal enum ApplicantListType
    {
        Standard = 6,
    }

    public class ApplicantList
        : ContenderList
    {
        public Guid PosterId
        {
            get { return OwnerId; }
            set { OwnerId = value; }
        }

        internal ApplicantListType ApplicantListType
        {
            get { return (ApplicantListType)ListType; }
            set { ListType = (int)value; }
        }
    }

    public class ApplicantListEntry
        : ContenderListEntry
    {
        public new Guid ListId
        {
            get { return base.ListId; }
            set { base.ListId = value; }
        }

        public Guid ApplicantId
        {
            get { return ContenderId; }
            set { ContenderId = value; }
        }

        internal new Guid? ApplicationId
        {
            get { return base.ApplicationId; }
            set { base.ApplicationId = value; }
        }

        public new ApplicantStatus ApplicantStatus 
        { 
            get { return base.ApplicantStatus ?? ApplicantStatus.NotSubmitted; } 
            set { base.ApplicantStatus = value; }
        }
    }
}