IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('Organisation') AND NAME = 'addressId')
BEGIN

	ALTER TABLE dbo.Organisation
	ADD addressId UNIQUEIDENTIFIER NULL

	ALTER TABLE dbo.Organisation
	ADD CONSTRAINT FK_Organisation_Address FOREIGN KEY (addressId)
	REFERENCES dbo.Address (id)


END
	
GO
