IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'EthnicFlags' AND ss.name = N'dbo')
DROP TYPE dbo.EthnicFlags

GO

CREATE TYPE dbo.EthnicFlags FROM int NOT NULL

GO

IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('Member') AND NAME = 'ethnicFlags')
BEGIN

	ALTER TABLE dbo.Member
	ADD ethnicFlags EthnicFlags NULL

END
	
GO
