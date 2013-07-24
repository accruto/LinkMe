IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'account_transaction'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN	
create table linkme_owner.account_transaction
(
	id		varchar(100) PRIMARY KEY NOT NULL,
	verisignInvoice varchar(100) NULL,
	invoiceNumber	integer,
	userAccountId	varchar(50) NULL,
    	creatorId  varchar(50) NULL,
	abn		varchar(11) NULL,
	transactionType varchar(50) NULL,
	transactionDate	DateTime NULL,
	moneyComponent	money NULL,
	gstComponent	money NULL,	
	linkMeCreditsComponent	integer NULL,
	linkMeCreditsAfter integer NULL,
	allocationReason varchar(500) NULL
)

END
GO