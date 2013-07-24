EXEC sp_changeobjectowner 'linkme_owner.AtsIntegrator', dbo
GO

ALTER TABLE dbo.AtsIntegrator
ALTER COLUMN [name] CompanyName NOT NULL
GO
