IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'invoice_sequence'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN	
create table linkme_owner.invoice_sequence
(
	id		integer IDENTITY(100505,1) NOT NULL
)

END
GO