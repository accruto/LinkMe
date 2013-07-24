using System;
using LinkMe.Domain.Roles.Networking;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Domain.Roles.Networking.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Networking
{
    [TestClass]
    public class InvitationsTests
        : TestClass
    {
        const string EmailAddress = "test@test.linkme.net.au";
        private readonly INetworkingInvitationsCommand _networkingInvitationsCommand = Resolve<INetworkingInvitationsCommand>();
        private readonly INetworkingInvitationsQuery _networkingInvitationsQuery = Resolve<INetworkingInvitationsQuery>();

        [TestMethod]
        public void TestSoonerFirstSentTime()
        {
            // As part of the migration for 3.0 it was possible for there to be multiple
            // invitations.  This test checks that the latest one is returned.

            var dt = DateTime.Now;
            var inviterId = Guid.NewGuid();

            var invitation1 = new NetworkingInvitation
            {
                InviterId = inviterId,
                InviteeEmailAddress = EmailAddress,
                FirstSentTime = dt
            };
            _networkingInvitationsCommand.CreateInvitation(invitation1);

            var invitation2 = new NetworkingInvitation
            {
                InviterId = inviterId,
                InviteeEmailAddress = EmailAddress,
                FirstSentTime = dt.AddDays(-2)
            };
            _networkingInvitationsCommand.CreateInvitation(invitation2);

            var invitation = _networkingInvitationsQuery.GetInvitation<NetworkingInvitation>(inviterId, EmailAddress);
            Assert.AreEqual(invitation1.Id, invitation.Id);
        }

        [TestMethod]
        public void TestLaterFirstSentTime()
        {
            var dt = DateTime.Now;
            var inviterId = Guid.NewGuid();

            var invitation1 = new NetworkingInvitation
            {
                InviterId = inviterId,
                InviteeEmailAddress = EmailAddress,
                FirstSentTime = dt
            };
            _networkingInvitationsCommand.CreateInvitation(invitation1);

            var invitation2 = new NetworkingInvitation
            {
                InviterId = inviterId,
                InviteeEmailAddress = EmailAddress,
                FirstSentTime = dt.AddDays(+2)
            };
            _networkingInvitationsCommand.CreateInvitation(invitation2);

            var invitation = _networkingInvitationsQuery.GetInvitation<NetworkingInvitation>(inviterId, EmailAddress);
            Assert.AreEqual(invitation2.Id, invitation.Id);
        }

        [TestMethod]
        public void TestSoonerLastSentTime()
        {
            var dt = DateTime.Now;
            var inviterId = Guid.NewGuid();

            var invitation1 = new NetworkingInvitation
            {
                InviterId = inviterId,
                InviteeEmailAddress = EmailAddress,
                FirstSentTime = dt
            };
            _networkingInvitationsCommand.CreateInvitation(invitation1);

            var invitation2 = new NetworkingInvitation
            {
                InviterId = inviterId,
                InviteeEmailAddress = EmailAddress,
                LastSentTime = dt.AddDays(-2)
            };
            _networkingInvitationsCommand.CreateInvitation(invitation2);

            var invitation = _networkingInvitationsQuery.GetInvitation<NetworkingInvitation>(inviterId, EmailAddress);
            Assert.AreEqual(invitation1.Id, invitation.Id);
        }

        [TestMethod]
        public void TestLaterLastSentTime()
        {
            var dt = DateTime.Now;
            var inviterId = Guid.NewGuid();

            var invitation1 = new NetworkingInvitation
            {
                InviterId = inviterId,
                InviteeEmailAddress = EmailAddress,
                FirstSentTime = dt
            };
            _networkingInvitationsCommand.CreateInvitation(invitation1);

            var invitation2 = new NetworkingInvitation
            {
                InviterId = inviterId,
                InviteeEmailAddress = EmailAddress,
                LastSentTime = dt.AddDays(+2)
            };
            _networkingInvitationsCommand.CreateInvitation(invitation2);

            var invitation = _networkingInvitationsQuery.GetInvitation<NetworkingInvitation>(inviterId, EmailAddress);
            Assert.AreEqual(invitation2.Id, invitation.Id);
        }

        [TestMethod]
        public void TestLastSentTimeSetSoonerFirstSentTime()
        {
            var dt = DateTime.Now;
            var inviterId = Guid.NewGuid();

            var invitation1 = new NetworkingInvitation
            {
                InviterId = inviterId,
                InviteeEmailAddress = EmailAddress,
                FirstSentTime = dt,
                LastSentTime = dt.AddDays(+1)
            };
            _networkingInvitationsCommand.CreateInvitation(invitation1);

            var invitation2 = new NetworkingInvitation
            {
                InviterId = inviterId,
                                  InviteeEmailAddress = EmailAddress,
                                  FirstSentTime = dt.AddDays(-2)
                              };
            _networkingInvitationsCommand.CreateInvitation(invitation2);

            var invitation = _networkingInvitationsQuery.GetInvitation<NetworkingInvitation>(inviterId, EmailAddress);
            Assert.AreEqual(invitation1.Id, invitation.Id);
        }

        [TestMethod]
        public void TestLastSentTimeSetLaterFirstTimeSent()
        {
            var dt = DateTime.Now;
            var inviterId = Guid.NewGuid();

            var invitation1 = new NetworkingInvitation
            {
                InviterId = inviterId,
                InviteeEmailAddress = EmailAddress,
                FirstSentTime = dt,
                LastSentTime = dt.AddDays(+1)
            };
            _networkingInvitationsCommand.CreateInvitation(invitation1);

            var invitation2 = new NetworkingInvitation
            {
                InviterId = inviterId,
                InviteeEmailAddress = EmailAddress,
                FirstSentTime = dt.AddDays(+2)
            };
            _networkingInvitationsCommand.CreateInvitation(invitation2);

            var invitation = _networkingInvitationsQuery.GetInvitation<NetworkingInvitation>(inviterId, EmailAddress);
            Assert.AreEqual(invitation2.Id, invitation.Id);
        }

        [TestMethod]
        public void TestLastSentTimeSetSoonerLastTimeSent()
        {
            var dt = DateTime.Now;
            var inviterId = Guid.NewGuid();

            var invitation1 = new NetworkingInvitation
            {
                InviterId = inviterId,
                InviteeEmailAddress = EmailAddress,
                FirstSentTime = dt,
                LastSentTime = dt.AddDays(+1)
            };
            _networkingInvitationsCommand.CreateInvitation(invitation1);

            var invitation2 = new NetworkingInvitation
            {
                InviterId = inviterId,
                InviteeEmailAddress = EmailAddress,
                LastSentTime = dt.AddDays(-2)
            };
            _networkingInvitationsCommand.CreateInvitation(invitation2);

            var invitation = _networkingInvitationsQuery.GetInvitation<NetworkingInvitation>(inviterId, EmailAddress);
            Assert.AreEqual(invitation1.Id, invitation.Id);
        }

        [TestMethod]
        public void TestLastSentTimeSetLaterLastTimeSent()
        {
            var dt = DateTime.Now;
            var inviterId = Guid.NewGuid();

            var invitation1 = new NetworkingInvitation
            {
                InviterId = inviterId,
                InviteeEmailAddress = EmailAddress,
                FirstSentTime = dt,
                LastSentTime = dt.AddDays(+1)
            };
            _networkingInvitationsCommand.CreateInvitation(invitation1);

            var invitation2 = new NetworkingInvitation
            {
                InviterId = inviterId,
                InviteeEmailAddress = EmailAddress,
                LastSentTime = dt.AddDays(+2)
            };
            _networkingInvitationsCommand.CreateInvitation(invitation2);

            var invitation = _networkingInvitationsQuery.GetInvitation<NetworkingInvitation>(inviterId, EmailAddress);
            Assert.AreEqual(invitation2.Id, invitation.Id);
        }
    }
}
