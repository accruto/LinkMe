EXEC sp_changeobjectowner 'linkme_owner.Company', dbo
GO

ALTER TABLE dbo.Company
ALTER COLUMN [name] CompanyName NOT NULL
GO

ALTER TABLE dbo.Company
ADD verifiedById UNIQUEIDENTIFIER NULL
GO

ALTER TABLE Company ADD CONSTRAINT FK_Company_VerifiedByAdministrator 
FOREIGN KEY (verifiedById) REFERENCES Administrator ([id])
GO
