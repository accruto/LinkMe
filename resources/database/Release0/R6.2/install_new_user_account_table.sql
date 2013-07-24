IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'user_account'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN	
create table linkme_owner.user_account
(
	
	Id		varchar(100) PRIMARY KEY NOT NULL,
	abnNumber	varchar(11) NULL,
	status		varchar(40) NULL,
	noAbnReason	varchar(2500) NULL

)

END
GO