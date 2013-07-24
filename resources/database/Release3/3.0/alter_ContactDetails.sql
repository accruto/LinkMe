EXEC sp_changeobjectowner 'linkme_owner.ContactDetails', dbo
GO

ALTER TABLE dbo.ContactDetails
ALTER COLUMN email EmailAddress NULL

ALTER TABLE dbo.ContactDetails
ALTER COLUMN phoneNumber PhoneNumber NULL

ALTER TABLE dbo.ContactDetails
ALTER COLUMN faxNumber PhoneNumber NULL

GO
