IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'content'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	ALTER TABLE linkme_owner.content
	ADD type varchar(20)
END
GO

IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'content'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	UPDATE linkme_owner.content SET type='RichText'
END
GO
