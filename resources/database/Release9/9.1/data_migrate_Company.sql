-- Each Company becomes an Organisation. A verified one also
-- becomes an OrganisatonalUnit.
-- To make the migration really simple just keep the existing Company IDs.

INSERT INTO dbo.Organisation ([id], displayName)
SELECT [id], [name]
FROM dbo.Company

-- Default account manager to "verified by"
INSERT INTO dbo.OrganisationalUnit ([id], verifiedById, accountManagerId)
SELECT [id], verifiedById, verifiedById
FROM dbo.Company
WHERE verifiedById IS NOT NULL

GO

ALTER TABLE dbo.Employer
ADD organisationId UNIQUEIDENTIFIER NULL
GO

UPDATE dbo.Employer
SET organisationId = companyId
GO

ALTER TABLE dbo.Employer
ALTER COLUMN organisationId UNIQUEIDENTIFIER NOT NULL

ALTER TABLE dbo.Employer
ADD CONSTRAINT FK_Employer_Organisation
FOREIGN KEY (organisationId) REFERENCES dbo.Organisation ([id])

GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employer_Company]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employer]'))
ALTER TABLE [dbo].[Employer] DROP CONSTRAINT [FK_Employer_Company]

ALTER TABLE dbo.Employer
DROP COLUMN companyId

GO
