ALTER TABLE dbo.OrganisationalUnit
ADD primaryContactId UNIQUEIDENTIFIER
GO

ALTER TABLE dbo.OrganisationalUnit
ADD CONSTRAINT FK_OrganisationalUnit_PrimaryContact
FOREIGN KEY (primaryContactId) REFERENCES dbo.ContactDetails ([id])
GO
