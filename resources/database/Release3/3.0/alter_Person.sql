EXEC sp_changeobjectowner 'linkme_owner.Person', dbo
GO

ALTER TABLE dbo.Person
DROP COLUMN title

ALTER TABLE dbo.Person
ALTER COLUMN firstName PersonName NULL

ALTER TABLE dbo.Person
ALTER COLUMN lastName PersonName NULL

GO
