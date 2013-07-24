IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networker_invites'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	ALTER TABLE linkme_owner.networker_invites
	ADD preliminaryInviteAccepted BIT NULL
END
GO
