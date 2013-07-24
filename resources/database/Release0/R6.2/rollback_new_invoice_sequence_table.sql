IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'invoice_sequence'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN	

DROP table linkme_owner.invoice_sequence


END
GO