IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'user_profile_promotions'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	CREATE TABLE linkme_owner.user_profile_promotions
	(
		id	varchar(50) NOT NULL,
		userProfileId varchar(50) NOT NULL,
		code    varchar(255) NOT NULL,

		CONSTRAINT pk_user_profile_promotions_id PRIMARY KEY (id)
	)
	
	CREATE INDEX i_user_profile_promotions_id ON linkme_owner.user_profile_promotions (id)	
END 
GO

