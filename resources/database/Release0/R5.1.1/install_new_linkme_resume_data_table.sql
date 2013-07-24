IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networker_resume_data'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN	
create table linkme_owner.networker_resume_data
(
	
	Id		varchar(100) PRIMARY KEY NOT NULL,
	resumeXml	TEXT NULL
)

END
GO