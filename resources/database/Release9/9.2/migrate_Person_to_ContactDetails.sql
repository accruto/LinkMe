-- Create firstName and lastName on ContactDetails

ALTER TABLE dbo.ContactDetails
ADD firstName PersonName NULL

ALTER TABLE dbo.ContactDetails
ADD lastName PersonName NULL
GO

-- Migrate the data

UPDATE dbo.ContactDetails
SET firstName = p.firstName, lastName = p.lastName
FROM dbo.ContactDetails cd
INNER JOIN dbo.JobAd ja
ON cd.[id] = ja.contactDetailsId
INNER JOIN dbo.Person p
ON ja.contactPersonId = p.[id]
GO

-- Drop Person

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAd_Person]') AND parent_object_id = OBJECT_ID(N'[dbo].[JobAd]'))
ALTER TABLE [dbo].[JobAd] DROP CONSTRAINT [FK_JobAd_Person]
GO

ALTER TABLE dbo.JobAd
DROP COLUMN contactPersonId
GO

DROP TABLE dbo.Person
GO
