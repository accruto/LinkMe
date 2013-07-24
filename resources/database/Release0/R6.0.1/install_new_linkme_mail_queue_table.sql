IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'linkme_mail_queue'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN	
create table linkme_owner.linkme_mail_queue
(
	
	Id		varchar(100) PRIMARY KEY NOT NULL,
	toAddress	varchar(500) NULL,
	fromAddress	varchar(500) NULL,
	subject		varchar(500) NULL,
	bodyFormat	Varchar(50) NULL,
	body		TEXT NULL,
	dateSent	DateTime NULL,
	messageStatus	varchar(20) NULL,
	errorCount	int NULL
	
)

END
GO