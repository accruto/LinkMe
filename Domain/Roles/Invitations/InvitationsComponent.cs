using System;
using System.Collections.Generic;
using LinkMe.Domain.Requests;

namespace LinkMe.Domain.Roles.Invitations
{
    public abstract class InvitationsComponent<TInvitation>
        where TInvitation : Invitation, new()
    {
        private readonly int _invitationAccessDays;
        protected readonly IInvitationsRepository<TInvitation> _repository;

        protected InvitationsComponent(IInvitationsRepository<TInvitation> repository, int invitationAccessDays)
        {
            _repository = repository;
            _invitationAccessDays = invitationAccessDays;
        }

        protected TInvitation GetInvitation(TInvitation invitation)
        {
            // Look for the latest corresponding invitation because there may be more than one.

            return invitation.InviteeId == null
                ? GetInvitation<TInvitation>(invitation.InviterId, invitation.InviteeEmailAddress)
                : GetInvitation<TInvitation>(invitation.InviterId, invitation.InviteeId.Value);
        }

        protected TDerivedInvitation GetInvitation<TDerivedInvitation>(Guid inviterId, Guid inviteeId)
            where TDerivedInvitation : TInvitation, new()
        {
            return GetInvitation(_repository.GetInvitations<TDerivedInvitation>(inviterId, inviteeId));
        }

        protected TDerivedInvitation GetInvitation<TDerivedInvitation>(Guid inviterId, string inviteeEmailAddress)
            where TDerivedInvitation : TInvitation, new()
        {
            return GetInvitation(_repository.GetInvitations<TDerivedInvitation>(inviterId, inviteeEmailAddress));
        }

        protected IList<TDerivedInvitation> GetInvitations<TDerivedInvitation>(Guid inviterId)
            where TDerivedInvitation : TInvitation, new()
        {
            var minLastSentTime = DateTime.Now.AddDays(-1 * _invitationAccessDays);
            return _repository.GetInvitations<TDerivedInvitation>(inviterId, minLastSentTime);
        }

        protected int GetInvitationCount(Guid inviterId, DateTimeRange dateTimeRange)
        {
            return _repository.GetInvitationCount(inviterId, dateTimeRange);
        }

        protected TDerivedInvitation GetInvitation<TDerivedInvitation>(Guid id)
            where TDerivedInvitation : TInvitation, new()
        {
            return _repository.GetInvitation<TDerivedInvitation>(id);
        }

        protected IList<TDerivedInvitation> GetInvitations<TDerivedInvitation>(Guid inviteeId, string inviteeEmailAddress)
            where TDerivedInvitation : TInvitation, new()
        {
            var minLastSentTime = DateTime.Now.AddDays(-1 * _invitationAccessDays);
            return _repository.GetInvitations<TDerivedInvitation>(inviteeId, inviteeEmailAddress, minLastSentTime);
        }

        protected static TDerivedInvitation GetInvitation<TDerivedInvitation>(IList<TDerivedInvitation> invitations)
            where TDerivedInvitation : TInvitation
        {
            if (invitations.Count == 0)
                return null;

            // Find the first invitation.

            TDerivedInvitation foundInvitation = null;
            DateTime? foundSentTime = null;

            foreach (var existingInvitation in invitations)
            {
                if (existingInvitation.Status == RequestStatus.Pending || existingInvitation.Status == RequestStatus.Declined)
                {
                    if (foundInvitation == null)
                    {
                        // First one found so use it.

                        foundInvitation = existingInvitation;
                        foundSentTime = foundInvitation.LastSentTime ?? foundInvitation.FirstSentTime;
                    }
                    else
                    {
                        // Need to compare;

                        var dt = existingInvitation.LastSentTime ?? existingInvitation.FirstSentTime;
                        if (dt != null && dt > foundSentTime)
                        {
                            foundInvitation = existingInvitation;
                            foundSentTime = foundInvitation.LastSentTime ?? foundInvitation.FirstSentTime;
                        }
                    }
                }
            }

            return foundInvitation;
        }
    }
}
