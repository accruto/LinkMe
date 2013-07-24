IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'linkme_mail_queue'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	ALTER TABLE linkme_owner.linkme_mail_queue
	DROP COLUMN lockedBy;
END 
GO

