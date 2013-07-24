IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networker_profile'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN 
alter table linkme_owner.networker_profile
add suburb VARCHAR(80) NULL;
alter table linkme_owner.networker_profile
add state VARCHAR(80) NULL;
END
GO