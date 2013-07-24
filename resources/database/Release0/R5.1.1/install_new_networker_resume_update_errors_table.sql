IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networker_resume_update_errors'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN	

create table linkme_owner.networker_resume_update_errors
(
	id	varchar(100) PRIMARY KEY NOT NULL,
	networkerId	varchar(100) 	NULL,
	exception	varchar(8000) 	NULL,
	timeStamp	TIMESTAMP	NULL
)

END
GO