using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;

namespace LinkMe.Domain.Users.Members.Status.Queries
{
    public class MemberStatusQuery
        : IMemberStatusQuery
    {
        #region UpdateNeeded Constants
        private const int UpdateIntervalAvailableNow = 30;
        private const int UpdateIntervalActivelyLooking = 30;
        private const int UpdateIntervalOpenToOffers = 90;
        private const int UpdateIntervalNotLooking = 180;
        private const int UpdateIntervalUnspecified = 90;
        #endregion

        private class MemberStatusItemData
        {
            public int CompletionPercentage { get; set; }
            public Func<IMember, ICandidate, IResume, bool> IsComplete { get; set; }
        }

        private static readonly IDictionary<MemberItem, MemberStatusItemData> Items = new Dictionary<MemberItem, MemberStatusItemData>
        {
            {
                MemberItem.Name,
                new MemberStatusItemData { CompletionPercentage = 0, IsComplete = (m, c, r) => !string.IsNullOrEmpty(m.FirstName) && !string.IsNullOrEmpty(m.LastName) }
            },
            {
                MemberItem.DesiredJob,
                new MemberStatusItemData { CompletionPercentage = 5, IsComplete = (m, c, r) => !string.IsNullOrEmpty(c.DesiredJobTitle) }
            },
            {
                MemberItem.DesiredSalary,
                new MemberStatusItemData { CompletionPercentage = 10, IsComplete = (m, c, r) => c.DesiredSalary != null && c.DesiredSalary.LowerBound != 0 }
            },
            {
                MemberItem.Address,
                new MemberStatusItemData { CompletionPercentage = 10, IsComplete = (m, c, r) => m.Address != null && !m.Address.IsEmpty }
            },
            {
                MemberItem.EmailAddress,
                new MemberStatusItemData { CompletionPercentage = 5, IsComplete = (m, c, r) => m.EmailAddresses != null && m.EmailAddresses.Count > 0 }
            },
            {
                MemberItem.PhoneNumber,
                new MemberStatusItemData { CompletionPercentage = 10, IsComplete = (m, c, r) => m.PhoneNumbers != null && m.PhoneNumbers.Count > 0 }
            },
            {
                MemberItem.Status,
                new MemberStatusItemData { CompletionPercentage = 10, IsComplete = (m, c, r) => c.Status != CandidateStatus.Unspecified }
            },
            {
                MemberItem.Objective,
                new MemberStatusItemData { CompletionPercentage = 5, IsComplete = (m, c, r) => r != null && !string.IsNullOrEmpty(r.Objective) }
            },
            {
                MemberItem.Industries,
                new MemberStatusItemData { CompletionPercentage = 5, IsComplete = (m, c, r) => c.Industries != null && c.Industries.Count != 0 }
            },
            {
                MemberItem.Jobs,
                new MemberStatusItemData { CompletionPercentage = 10, IsComplete = (m, c, r) => r != null && r.Jobs != null && r.Jobs.Count != 0 }
            },
            {
                MemberItem.Schools,
                new MemberStatusItemData { CompletionPercentage = 10, IsComplete = (m, c, r) => r != null && r.Schools != null && r.Schools.Count != 0 }
            },
            {
                MemberItem.RecentProfession,
                new MemberStatusItemData { CompletionPercentage = 5, IsComplete = (m, c, r) => c.RecentProfession.HasValue }
            },
            {
                MemberItem.RecentSeniority,
                new MemberStatusItemData { CompletionPercentage = 5, IsComplete = (m, c, r) => c.RecentSeniority.HasValue }
            },
            {
                MemberItem.HighestEducationLevel,
                new MemberStatusItemData { CompletionPercentage = 5, IsComplete = (m, c, r) => c.HighestEducationLevel.HasValue }
            },
            {
                MemberItem.VisaStatus,
                new MemberStatusItemData { CompletionPercentage = 5, IsComplete = (m, c, r) => c.VisaStatus.HasValue }
            },
        };

        /// <summary>
        /// Determine whether an update is needed to the resume/profile based on candidate's status
        /// In the absence of a update date/time on the resume the member's creation date/time is used
        /// </summary>
        /// <param name="member"></param>
        /// <param name="candidate"></param>
        /// <param name="resume"></param>
        /// <returns>true is the profile needs to be updated; false otherwise</returns>
        bool IMemberStatusQuery.IsUpdateNeeded(IMember member, ICandidate candidate, IResume resume)
        {
            DateTime lastUpdatedBefore;

            switch (candidate.Status)
            {
                case CandidateStatus.AvailableNow:
                    lastUpdatedBefore = DateTime.Now.AddDays(-1 * UpdateIntervalAvailableNow);
                    break;

                case CandidateStatus.ActivelyLooking:
                    lastUpdatedBefore = DateTime.Now.AddDays(-1 * UpdateIntervalActivelyLooking);
                    break;

                case CandidateStatus.NotLooking:
                    lastUpdatedBefore = DateTime.Now.AddDays(-1 * UpdateIntervalNotLooking);
                    break;

                case CandidateStatus.OpenToOffers:
                    lastUpdatedBefore = DateTime.Now.AddDays(-1 * UpdateIntervalOpenToOffers);
                    break;

                default:
                    lastUpdatedBefore = DateTime.Now.AddDays(-1 * UpdateIntervalUnspecified);
                    break;
            }

            return GetLastUpdatedTime(member, candidate, resume) < lastUpdatedBefore;
        }

        /// <summary>
        /// The percentage complete the profile/resume is; various parts of the profile have various scores/weights
        /// </summary>
        /// <param name="member"></param>
        /// <param name="candidate"></param>
        /// <param name="resume"></param>
        /// <returns>A whole number indicating the percentage complete. 100 indicates a complete profile</returns>
        int IMemberStatusQuery.GetPercentComplete(IMember member, ICandidate candidate, IResume resume)
        {
            var percentComplete = 0;

            foreach (var profileItem in Items)
            {
                if (profileItem.Value.IsComplete(member, candidate, resume))
                    percentComplete += profileItem.Value.CompletionPercentage;
            }

            return percentComplete;
        }

        /// <summary>
        /// The last time this profile was updated.
        /// </summary>
        /// <param name="member"></param>
        /// <param name="candidate"></param>
        /// <param name="resume"></param>
        DateTime IMemberStatusQuery.GetLastUpdatedTime(IMember member, ICandidate candidate, IResume resume)
        {
            return GetLastUpdatedTime(member, candidate, resume);
        }

        MemberStatus IMemberStatusQuery.GetMemberStatus(IMember member, ICandidate candidate, IResume resume)
        {
            return new MemberStatus(Items.ToDictionary(i => i.Key, i => new MemberItemStatus { Item = i.Key, IsComplete = i.Value.IsComplete(member, candidate, resume) }));
        }

        private static DateTime GetLastUpdatedTime(IMember member, ICandidate candidate, IResume resume)
        {
            // Use the latest of the various times.

            return new[]
            {
                resume == null ? DateTime.MinValue : resume.LastUpdatedTime,
                candidate.LastUpdatedTime,
                member.LastUpdatedTime
            }.Max();
        }
    }
}