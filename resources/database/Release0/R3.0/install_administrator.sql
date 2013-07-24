IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'administrator_profile'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	CREATE TABLE linkme_owner.administrator_profile (
		id VARCHAR(50),
		emailAddress VARCHAR(255) NULL,
	
		CONSTRAINT pk_administrator_profile PRIMARY KEY (id)
	)
END

GO

IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'administrator_profile'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	INSERT INTO linkme_owner.user_profile (id, userId, password, firstName, lastName, active, joinDate, preferredHtmlEmailFormat) VALUES (NEWID(), 'linkmeadmin', 'Zz2q1SMrgxYhirurIM7CRA==',
		'Tony', 'Mittlemark', 1, GETDATE(), 1)

	
	INSERT INTO linkme_owner.administrator_profile
		SELECT id, 'admin@linkme.com.au' FROM linkme_owner.user_profile
			WHERE userId = 'linkmeadmin'		
END

GO
