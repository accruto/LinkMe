using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LinkMe.Apps.Agents.Security;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;

namespace LinkMe.Common.Domain.Users.Members
{
/*    public interface IMembersCommand
    {
        void CreateMember(Member member);
        void CreateMember(Member member, LoginCredentials credentials);

        /// Does not fire events so do not use in production code until properly fixed.
        void UpdateMember(Member member);

        // Use these instead for now.
        void UpdateMember(Member member, string firstName, string lastName, string emailAddress);
        void UpdateMember(Member member, string firstName, string lastName, int countryId, string location, DateTime? dateOfBirth, Gender gender, EthnicStatus ethnicStatus, DisabilityStatus disabilityStatus);
        void UpdateMember(Member member, string firstName, string lastName, int countryId, string location, string homePhoneNumber, string mobilePhoneNumber, string workPhoneNumber);
        void UpdateMember(Member member, string emailAddress, string homePhoneNumber, string mobilePhoneNumber, string workPhoneNumber);

        void SetProfilePhoto(Member member, FileReference fileReference);
        void SetEmailBounced(Member member, string emailAddress, string reason);

        Member GetMember(Guid id);
        Member GetMember(string emailAddress);
        ReadOnlyCollection<Member> GetMembers(IEnumerable<Guid> ids);
    }
*/
}