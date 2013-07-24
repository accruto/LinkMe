EXEC sp_changeobjectowner 'linkme_owner.ServicePartner', dbo
GO

ALTER TABLE dbo.ServicePartner
ALTER COLUMN [name] CompanyName NOT NULL
GO
