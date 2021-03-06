IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.LinkedInProfileIndustry') AND type in (N'U'))
DROP TABLE dbo.LinkedInProfileIndustry
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.LinkedInProfile') AND type in (N'U'))
DROP TABLE dbo.LinkedInProfile
GO

CREATE TABLE dbo.LinkedInProfile
(
	linkedInId NVARCHAR(100) NOT NULL,
	userId UNIQUEIDENTIFIER NOT NULL,
	createdTime DATETIME NOT NULL,
	lastUpdatedTime DATETIME NOT NULL,
	firstName NVARCHAR(30) NULL,
	lastName NVARCHAR(30) NULL,
	organisationName NVARCHAR(100) NULL,
	locationReferenceId UNIQUEIDENTIFIER NULL
)
GO

ALTER TABLE dbo.LinkedInProfile
ADD CONSTRAINT PK_LinkedInProfile PRIMARY KEY NONCLUSTERED
(
	linkedInId
)
GO

CREATE UNIQUE INDEX [IX_LinkedInProfile_userId] ON [dbo].[LinkedInProfile]
(
	userId
)
GO

ALTER TABLE dbo.LinkedInProfile
ADD CONSTRAINT FK_LinkedInProfile_LocationReference
FOREIGN KEY (locationReferenceId)
REFERENCES dbo.LocationReference (id)
GO

CREATE TABLE dbo.LinkedInProfileIndustry
(
	linkedInId NVARCHAR(100) NOT NULL,
	industryId UNIQUEIDENTIFIER NOT NULL
)
GO

ALTER TABLE dbo.LinkedInProfileIndustry
ADD CONSTRAINT PK_LinkedInProfileUndistry PRIMARY KEY CLUSTERED
(
	linkedInId,
	industryId
)
GO

ALTER TABLE dbo.LinkedInProfileIndustry
ADD CONSTRAINT FK_LinkedInProfileIndustry_LinkedInProfile
FOREIGN KEY (linkedInId)
REFERENCES dbo.LinkedInProfile (linkedInId)
GO

ALTER TABLE dbo.LinkedInProfileIndustry
ADD CONSTRAINT FK_LinkedInProfileIndustry_Industry
FOREIGN KEY (industryId)
REFERENCES dbo.Industry (id)
GO
