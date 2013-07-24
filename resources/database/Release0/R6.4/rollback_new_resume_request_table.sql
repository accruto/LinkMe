IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'user_profile_promotions'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN

DROP table linkme_owner.user_profile_promotions
	
END 
GO

