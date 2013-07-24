CREATE TABLE linkme_owner.user_profile_pending_invite (
	id VARCHAR(50) NOT NULL,
	inviterId VARCHAR(50) NOT NULL,

	CONSTRAINT pk_user_profile_pending_invite PRIMARY KEY (id, inviterId)
)


