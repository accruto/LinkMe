IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'DisabilityFlags' AND ss.name = N'dbo')
DROP TYPE dbo.DisabilityFlags

GO

CREATE TYPE dbo.DisabilityFlags FROM int NOT NULL

GO

IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('Member') AND NAME = 'disabilityFlags')
BEGIN

	ALTER TABLE dbo.Member
	ADD disabilityFlags DisabilityFlags NULL

END
	
GO
