IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networker_profile'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	ALTER TABLE linkme_owner.networker_profile
	ADD resumeInLens BIT NULL

END
GO



IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networker_profile'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	ALTER TABLE linkme_owner.networker_profile
	ADD changesPending BIT NULL
END
GO



IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networker_profile'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	UPDATE networker_profile set resumeinLens = 1 where resumeId IS NOT NULL
	UPDATE networker_profile set changesPending = 0	
END
GO

IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networker_profile'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	ALTER TABLE linkme_owner.networker_profile
	DROP COLUMN resumeId 

END
GO