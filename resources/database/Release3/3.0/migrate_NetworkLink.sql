-- Migrate user_profile_contacts with a dummy time.
-- Unfortunately the added time is not available for every link, so some will just have to be
-- stuck with this "unknown" time. All links added in the future should have a time set, however.

DECLARE @dummyTime DATETIME
SET @dummyTime = '1753-01-01'

INSERT INTO dbo.NetworkLink(fromNetworkerId, toNetworkerId, addedTime)
SELECT dbo.GuidFromString([id]), dbo.GuidFromString(contactId), @dummyTime
FROM linkme_owner.user_profile_contact

-- Set the addedTime from networker_invites. There may be multiple invites and we only want the latest one.

UPDATE dbo.NetworkLink
SET addedTime = inviteAcceptedDate
FROM dbo.NetworkLink link
INNER JOIN linkme_owner.user_profile invitee
ON link.toNetworkerId = dbo.GuidFromString(invitee.[id])
INNER JOIN linkme_owner.networker_invites ni
ON link.fromNetworkerId = dbo.GuidFromString(ni.inviterId) AND invitee.userId = ni.inviteEmailAddress
WHERE inviteAccepted = 1 AND inviteAcceptedDate > addedTime

-- Set the added times from the old event table (event type 1).

UPDATE dbo.NetworkLink
SET addedTime = e.[timestamp]
FROM dbo.NetworkLink nl
INNER JOIN linkme_owner.event e
ON nl.fromNetworkerId = dbo.GuidFromString(e.ownerId) AND nl.toNetworkerId = dbo.GuidFromString(e.data)
WHERE e.type = 1

-- Set the same time for the reverse link.

UPDATE link
SET addedTime = rev.addedTime
FROM dbo.NetworkLink link
INNER JOIN dbo.NetworkLink rev
ON link.fromNetworkerId = rev.toNetworkerId AND link.toNetworkerId = rev.fromNetworkerId
WHERE link.addedTime = @dummyTime

GO
