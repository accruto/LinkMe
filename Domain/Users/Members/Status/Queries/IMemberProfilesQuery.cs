using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;

namespace LinkMe.Domain.Users.Members.Status.Queries
{
    public interface IMemberStatusQuery
    {
        /// <summary>
        /// Determine whether an update is needed to the resume/profile based on candidate's status
        /// In the absence of a update date/time on the resume the member's creation date/time is used
        /// </summary>
        /// <param name="member"></param>
        /// <param name="candidate"></param>
        /// <param name="resume"></param>
        /// <returns>true is the profile needs to be updated; false otherwise</returns>
        bool IsUpdateNeeded(IMember member, ICandidate candidate, IResume resume);

        /// <summary>
        /// The percentage complete the profile/resume is; various parts of the profile have various scores/weights
        /// </summary>
        /// <param name="member"></param>
        /// <param name="candidate"></param>
        /// <param name="resume"></param>
        /// <returns>A whole number indicating the percentage complete. 100 indicates a complete profile</returns>
        int GetPercentComplete(IMember member, ICandidate candidate, IResume resume);

        /// <summary>
        /// The age of the profile/resume in "months" (where a "month" is 30 days)
        /// The number of months is rounded down, so 45 days is one month old
        /// If the resume has no updated date/time the member's creation date/time is used
        /// </summary>
        /// <param name="member"></param>
        /// <param name="candidate"></param>
        /// <param name="resume"></param>
        /// <returns>The time since the profile was last updated</returns>
        DateTime GetLastUpdatedTime(IMember member, ICandidate candidate, IResume resume);

        MemberStatus GetMemberStatus(IMember member, ICandidate candidate, IResume resume);
    }
}