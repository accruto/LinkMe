IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'account_transaction_line_item'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN	

DROP table linkme_owner.account_transaction_line_item


END
GO