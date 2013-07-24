-- Restore the userIds for deactivated users, but make sure active is set to false.

UPDATE linkme_owner.user_profile
SET active = 0, userId = di.userProfileUserId
FROM linkme_owner.user_profile up
LEFT OUTER JOIN linkme_owner.disabled_user_profile_info di
ON up.[id] = di.[id]
WHERE userId IS NULL OR userId LIKE 'DEACTIVATED_%'

-- Delete users with NULLs for id, password and name - these should only occur in UAT, hopefully.

DELETE linkme_owner.user_profile
WHERE userId IS NULL AND [password] IS NULL AND firstName IS NULL AND lastName IS NULL

IF EXISTS
(
	SELECT *
	FROM linkme_owner.user_profile up
	WHERE userId IS NULL OR userId LIKE 'DEACTIVATED_%'
)
	RAISERROR('Some user IDs for deactivated users were not fixed.', 16, 1)
