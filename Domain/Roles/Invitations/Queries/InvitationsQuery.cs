using LinkMe.Domain.Requests;

namespace LinkMe.Domain.Roles.Invitations.Queries
{
    public abstract class InvitationsQuery<TInvitation>
        : InvitationsComponent<TInvitation>
        where TInvitation : Invitation, new()
    {
        protected InvitationsQuery(IInvitationsRepository<TInvitation> repository, int invitationAccessDays)
            : base(repository, invitationAccessDays)
        {
        }
    }
}
