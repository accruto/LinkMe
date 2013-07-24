IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networker_invites'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN	

create table linkme_owner.networker_invites
(
	id			varchar(100) PRIMARY KEY NOT NULL,
	inviterId		varchar(100) NOT NULL,
	inviteEmailAddress	varchar(500) NOT NULL,
	inviteAccepted		BIT NULL,
	inviteCreatedDate	DATETIME NULL,
	inviteAcceptedDate	DATETIME NULL
)


CREATE NONCLUSTERED INDEX ix_networker_invites_inviterId ON linkme_owner.networker_invites (inviterId)
CREATE NONCLUSTERED INDEX ix_networker_invites_inviteCreatedDate	 ON linkme_owner.networker_invites (inviteCreatedDate)	

END
GO

