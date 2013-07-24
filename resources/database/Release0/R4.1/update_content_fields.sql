IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'content'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	UPDATE linkme_owner.content
	SET contentValue = 'LinkMe is a new and innovative service that translates normal "real life" business networks into an online utility. It''s simple and easy to use.'
	WHERE (contentKey = 'homepage.notice')
	
END

GO