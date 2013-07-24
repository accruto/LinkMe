IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'content'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	DELETE linkme_owner.content WHERE contentKey='networker.benefits'
	DELETE linkme_owner.content WHERE contentKey='employer.benefits'
END

GO