using System;
using LinkMe.Domain.Requests;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Invitations.Commands
{
    public abstract class InvitationsCommand<TInvitation>
        : InvitationsComponent<TInvitation>
        where TInvitation : Invitation, new()
    {
        protected InvitationsCommand(IInvitationsRepository<TInvitation> repository, int invitationAccessDays)
            : base(repository, invitationAccessDays)
        {
        }

        protected void CreateInvitation(TInvitation invitation)
        {
            // There may already be a matching invitation, ie the same inviter and the same invitee.
            // If there is then resue that invitation rather than creating a whole new invitation.

            var existingInvitation = GetInvitation(invitation);
            if (existingInvitation == null)
            {
                // Create a new invitation.

                Prepare(invitation);
                Validate(invitation);
                _repository.CreateInvitation(invitation);
            }
            else
            {
                // A matching invitation exists, so copy over its details.

                invitation.Id = existingInvitation.Id;
                invitation.Status = existingInvitation.Status;
                invitation.FirstSentTime = existingInvitation.FirstSentTime;
                invitation.LastSentTime = existingInvitation.LastSentTime;
                invitation.ActionedTime = existingInvitation.ActionedTime;

                // If something has changed then update.

                if (invitation.InviteeId == null)
                {
                    if (existingInvitation.Text != invitation.Text)
                    {
                        Prepare(invitation);
                        _repository.UpdateInvitation(invitation);
                    }
                }
                else
                {
                    if (existingInvitation.Text != invitation.Text)
                    {
                        Prepare(invitation);
                        _repository.UpdateInvitation(invitation);
                    }
                }
            }
        }

        protected void UpdateInvitation(TInvitation invitation)
        {
            // Validate and update.

            Validate(invitation);
            _repository.UpdateInvitation(invitation);
        }

        protected void SendInvitation(TInvitation invitation)
        {
            // Make sure the invitation exists first.

            CreateInvitation(invitation);

            // Update it with its new status.

            invitation.Status = RequestStatus.Pending;
            invitation.LastSentTime = DateTime.Now;
            if (invitation.FirstSentTime == null)
                invitation.FirstSentTime = invitation.LastSentTime;
            _repository.UpdateInvitation(invitation);
        }

        protected void AcceptInvitation(Guid inviteeId, TInvitation invitation)
        {
            if (invitation.Status == RequestStatus.Pending)
            {
                // Update the invitation.

                invitation.ActionedTime = DateTime.Now;
                invitation.Status = RequestStatus.Accepted;
                invitation.InviteeId = inviteeId;
                _repository.UpdateInvitation(invitation);
            }
        }

        protected void RejectInvitation(TInvitation invitation)
        {
            if (invitation.Status == RequestStatus.Pending)
            {
                // Update the invitation.

                invitation.ActionedTime = DateTime.Now;
                invitation.Status = RequestStatus.Declined;
                _repository.UpdateInvitation(invitation);
            }
        }

        protected void RevokeInvitations(Guid inviterId)
        {
            var invitations = GetInvitations<TInvitation>(inviterId);
            foreach (var invitation in invitations)
            {
                if (invitation.Status == RequestStatus.Pending)
                {
                    // Update the invitation.

                    invitation.ActionedTime = DateTime.Now;
                    invitation.Status = RequestStatus.Revoked;
                    _repository.UpdateInvitation(invitation);
                }
            }
        }

        private static void Prepare(Invitation invitation)
        {
            invitation.Prepare();
            invitation.Status = RequestStatus.Pending;
        }

        private static void Validate(Invitation invitation)
        {
            invitation.Validate();

            // Check that one of the invitee fields is set.

            if (string.IsNullOrEmpty(invitation.InviteeEmailAddress) && invitation.InviteeId == null)
                throw new ValidationErrorsException(new RequiredValidationError("InviteeId"));

            // Make sure the inviter is different from the invitee.

            if (invitation.InviterId == invitation.InviteeId)
                throw new ValidationErrorsException(new SameValidationError("Inviter", "Invitee"));
        }
    }
}