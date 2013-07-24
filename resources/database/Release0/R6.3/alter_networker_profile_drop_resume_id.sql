IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'temp_resumeIds'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	DROP TABLE linkme_owner.temp_resumeIds;
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
	SELECT id, resumeId INTO linkme_owner.temp_resumeIds FROM linkme_owner.networker_profile ;
	ALTER TABLE linkme_owner.networker_profile DROP COLUMN resumeId;
END 
GO

