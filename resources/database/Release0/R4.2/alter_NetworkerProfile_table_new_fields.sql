IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'Networker_Profile'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	ALTER TABLE Networker_Profile
	ADD resumeLastUpdatedDate DATETIME NULL
	
END
GO