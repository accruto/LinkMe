IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'credit_card_surcharge'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN	
create table linkme_owner.credit_card_surcharge
(
	
	Id			varchar(100) PRIMARY KEY NOT NULL,
	creditCardType		int NOT NULL,
	surchargeMethod		int NOT NULL,
	surchargePercentage	decimal(8,4)  NULL,
	absoluteAmount		money NULL
)

END
GO