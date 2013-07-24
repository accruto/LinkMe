IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'searches'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	ALTER TABLE SEARCHES
	ADD lastRunDate DateTime NULL,
		lastViewedDate DateTime NULL,
		minimumScore integer NULL,
		status BIT NULL
	
END
GO