-- Invitation status mapping:
-- networker_invites.inviteAccepted = 1 --> 2 (Accepted)
-- networker_invites.inviteAccepted = 0 and corresponding user_profile_pending_invite row exists --> 1 (Pending)
-- networker_invites.inviteAccepted = 0 and no corresponding user_profile_pending_invite row --> 3 (Declined)

-- Insert the UserToUserRequest row without differentiating between Pending and Declined status.

INSERT INTO dbo.UserToUserRequest([id], actionedTime, firstSentTime, flags, lastSentTime, messageText, status)
SELECT dbo.GuidFromString([id]), NULLIF(inviteAcceptedDate, '1753-01-01'), inviteCreatedDate, 0, NULL, NULL,
	CASE inviteAccepted WHEN 1 THEN 2 ELSE 1 END
FROM linkme_owner.networker_invites

-- Insert the NetworkInvitation row without an inviteeId (which is not in the current data).

INSERT INTO dbo.NetworkInvitation([id], inviteeEmailAddress, inviterId, inviteeId, donationRequestId)
SELECT dbo.GuidFromString([id]), inviteEmailAddress, dbo.GuidFromString(inviterId), NULL, donationRequestId
FROM linkme_owner.networker_invites

GO

-- Now get a bit more clever: replace inviteeEmailAddress with inviteeId if it matches the emailAddress of
-- an existing Member.

UPDATE dbo.NetworkInvitation
SET inviteeId = n.[id], inviteeEmailAddress = NULL
FROM dbo.NetworkInvitation ni
INNER JOIN dbo.RegisteredUser ru
ON ru.emailAddress = ni.inviteeEmailAddress
INNER JOIN dbo.Networker n
ON ru.[id] = n.[id]

GO

-- Set the status to Declined where it's Pending and there is no corresponding row in
-- user_profile_pending_invite (ie. the user clicked Remove for that invite - the old equivalent of Decline).
-- We don't know the time in this case, so just set it to the invite time.

UPDATE dbo.UserToUserRequest
SET status = 3, actionedTime = firstSentTime
FROM dbo.UserToUserRequest r
INNER JOIN dbo.NetworkInvitation ni
ON r.[id] = ni.[id]
WHERE status = 1 AND inviteeId IS NOT NULL AND NOT EXISTS
(
	SELECT *
	FROM linkme_owner.user_profile_pending_invite
	WHERE dbo.GuidFromString([id]) = ni.inviteeId AND dbo.GuidFromString([inviterId]) = ni.inviterId
)

GO
