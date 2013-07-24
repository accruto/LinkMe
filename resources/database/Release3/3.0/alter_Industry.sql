EXEC sp_changeobjectowner 'linkme_owner.Industry', dbo
GO

ALTER TABLE dbo.Industry
ALTER COLUMN normalisedName VARCHAR(50) NOT NULL
GO