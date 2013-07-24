IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'account_transaction_line_item'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN	
create table linkme_owner.account_transaction_line_item
(
	
	Id		varchar(100) PRIMARY KEY NOT NULL,
	accTransactionId varchar(50) NULL, 
	productType	varchar(50) NULL,
	productUniqueId	varchar(50) NULL,
	quantity	integer NULL,
	pricePerItem money NULL

)

END
GO