IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'content'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	CREATE TABLE linkme_owner.content (
		contentKey VARCHAR(255),
		contentValue VARCHAR(2000)
		
		CONSTRAINT pk_content PRIMARY KEY (contentKey)
	)
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
	INSERT INTO linkme_owner.content VALUES ('homepage.notice', '')
	INSERT INTO linkme_owner.content VALUES ('employer.notice', 'You can upload a resume of someone and LinkMe will return a list of Contacts ranked according to how well they match the provided resume. This is perfect for when you need to find a replacement for someone who is leaving their job!')
	INSERT INTO linkme_owner.content VALUES ('networker.notice', 'You can upload a resume of someone and LinkMe will return a list of Contacts ranked according to how well they match the provided resume. This is perfect for when you need to find a replacement for someone who is leaving their job!')
END

GO