IF NOT EXISTS (SELECT * FROM syscolumns where id = OBJECT_ID('Vertical') AND NAME = 'host')
BEGIN
	ALTER TABLE dbo.Vertical
	ADD host  nvarchar(100)
END

GO


	ALTER TABLE dbo.Vertical
	ALTER COLUMN url nvarchar(100) null

GO