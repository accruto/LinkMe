IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'endorsement'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	DROP TABLE linkme_owner.endorsement
END
GO

IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'endorsement_relationship'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	DROP TABLE linkme_owner.endorsement_relationship
END
GO


