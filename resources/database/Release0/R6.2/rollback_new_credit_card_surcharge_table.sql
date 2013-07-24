IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'credit_card_surcharge'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN	

DROP table linkme_owner.credit_card_surcharge


END
GO