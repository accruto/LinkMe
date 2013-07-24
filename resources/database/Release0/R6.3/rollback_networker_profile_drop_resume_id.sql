IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'temp_resumeIds'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	ALTER TABLE linkme_owner.networker_profile
	ADD resumeId varchar(150) NULL;
END 
GO


IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'temp_resumeIds'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN

	UPDATE    linkme_owner.networker_profile
	SET              resumeId  =
		(SELECT temp.resumeId 
		 FROM linkme_owner.temp_resumeIds temp where linkme_owner.networker_profile.id = temp.id)

END 
GO



