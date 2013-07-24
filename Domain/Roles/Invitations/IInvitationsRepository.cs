using System;
using System.Collections.Generic;
using LinkMe.Domain.Requests;

namespace LinkMe.Domain.Roles.Invitations
{
    public interface IInvitationsRepository<TInvitation>
        where TInvitation : Invitation, new()
    {
        void CreateInvitation(TInvitation invitation);
        void UpdateInvitation(TInvitation invitation);

        TDerivedInvitation GetInvitation<TDerivedInvitation>(Guid id) where TDerivedInvitation : TInvitation, new();
        IList<TDerivedInvitation> GetInvitations<TDerivedInvitation>(Guid inviterId, string emailAddress) where TDerivedInvitation : TInvitation, new();
        IList<TDerivedInvitation> GetInvitations<TDerivedInvitation>(Guid inviterId, Guid inviteeId) where TDerivedInvitation : TInvitation, new();
        IList<TDerivedInvitation> GetInvitations<TDerivedInvitation>(Guid inviterId, DateTime minLastSentTime) where TDerivedInvitation : TInvitation, new();
        IList<TDerivedInvitation> GetInvitations<TDerivedInvitation>(Guid inviteeId, string inviteeEmailAddress, DateTime minLastSentTime) where TDerivedInvitation : TInvitation, new();
        int GetInvitationCount(Guid inviterId, DateTimeRange dateTimeRange);
    }
}
