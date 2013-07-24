

IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'email_verification'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN

CREATE TABLE linkme_owner.email_verification
(
	userProfileId 		varchar(50) PRIMARY KEY,
	activationCode	 	varchar(50) NOT NULL,
	accountCreatedDate	datetime NOT NULL,
	activated		bit NOT NULL
)
CREATE UNIQUE NONCLUSTERED INDEX i_email_verification_activation_code ON linkme_owner.email_verification (activationCode)

END
GO
