ALTER TABLE dbo.Vertical
ADD externalLoginUrl NVARCHAR(100) NULL
GO

ALTER TABLE dbo.Vertical
ADD requiresExternalLogin BIT NULL
GO

UPDATE
	dbo.Vertical
SET
	requiresExternalLogin = 0
GO

ALTER TABLE dbo.Vertical
ALTER COLUMN requiresExternalLogin BIT NOT NULL
GO
