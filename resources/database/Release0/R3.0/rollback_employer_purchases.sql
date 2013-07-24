IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'employer_purchases'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	DROP TABLE linkme_owner.employer_purchases
END
GO