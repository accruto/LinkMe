using System;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Networking;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Query.Reports.Roles.Networking.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Reports.Test.Roles.Networking
{
    [TestClass]
    public class NetworkingReportsTests
        : TestClass
    {
        private readonly INetworkingInvitationsCommand _networkingInvitationsCommand = Resolve<INetworkingInvitationsCommand>();
        private readonly INetworkingReportsQuery _networkingReportsQuery = Resolve<INetworkingReportsQuery>();

        [TestMethod]
        public void TestGetInvitations()
        {
            var inviterId = Guid.NewGuid();
            var invitee1Id = Guid.NewGuid();
            var invitee2Id = Guid.NewGuid();

            // No invitations to begin with.

            Assert.AreEqual(0, _networkingReportsQuery.GetInvitationsSent(DayRange.Yesterday));
            Assert.AreEqual(0, _networkingReportsQuery.GetInvitationsAccepted(DayRange.Yesterday));

            // Send two.

            var invitation1 = SendInvitation(inviterId, invitee1Id, DateTime.Today.AddDays(-1));
            var invitation2 = SendInvitation(inviterId, invitee2Id, DateTime.Today.AddDays(-1));

            Assert.AreEqual(2, _networkingReportsQuery.GetInvitationsSent(DayRange.Yesterday));
            Assert.AreEqual(0, _networkingReportsQuery.GetInvitationsAccepted(DayRange.Yesterday));

            // Accept them.

            AcceptInvitation(invitee1Id, invitation1, DateTime.Today.AddDays(-1));
            AcceptInvitation(invitee2Id, invitation2, DateTime.Today.AddDays(-1));

            Assert.AreEqual(2, _networkingReportsQuery.GetInvitationsSent(DayRange.Yesterday));
            Assert.AreEqual(2, _networkingReportsQuery.GetInvitationsAccepted(DayRange.Yesterday));
        }

        private NetworkingInvitation SendInvitation(Guid inviterId, Guid inviteeId, DateTime time)
        {
            var invitation = new NetworkingInvitation { InviterId = inviterId, InviteeId = inviteeId, FirstSentTime = time };
            _networkingInvitationsCommand.CreateInvitation(invitation);
            _networkingInvitationsCommand.SendInvitation(invitation);
            return invitation;
        }

        private void AcceptInvitation(Guid inviteeId, NetworkingInvitation invitation, DateTime time)
        {
            _networkingInvitationsCommand.AcceptInvitation(inviteeId, invitation);
            invitation.ActionedTime = time;
            _networkingInvitationsCommand.UpdateInvitation(invitation);
        }
    }
}
