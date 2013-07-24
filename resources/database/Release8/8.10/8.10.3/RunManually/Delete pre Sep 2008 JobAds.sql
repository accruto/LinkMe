-- Drop the foreign keys and check constraints

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobApplication_JobAd]') AND parent_object_id = OBJECT_ID(N'[dbo].[JobApplication]'))
ALTER TABLE [dbo].[JobApplication] DROP CONSTRAINT [FK_JobApplication_JobAd]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobSearchResult_JobAd]') AND parent_object_id = OBJECT_ID(N'[dbo].[JobSearchResult]'))
ALTER TABLE [dbo].[JobSearchResult] DROP CONSTRAINT [FK_JobSearchResult_JobAd]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserEventActionedJobAd_JobAd]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserEventActionedJobAd]'))
ALTER TABLE [dbo].[UserEventActionedJobAd] DROP CONSTRAINT [FK_UserEventActionedJobAd_JobAd]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAdIndustry_JobAd]') AND parent_object_id = OBJECT_ID(N'[dbo].[JobAdIndustry]'))
ALTER TABLE [dbo].[JobAdIndustry] DROP CONSTRAINT [FK_JobAdIndustry_JobAd]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAdLocation_JobAd]') AND parent_object_id = OBJECT_ID(N'[dbo].[JobAdLocation]'))
ALTER TABLE [dbo].[JobAdLocation] DROP CONSTRAINT [FK_JobAdLocation_JobAd]

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
	WHERE CONSTRAINT_SCHEMA = 'dbo' AND TABLE_NAME = 'CandidateList'
	AND CONSTRAINT_NAME = 'CK_CandidateList_name')
ALTER TABLE [dbo].[CandidateList] DROP CONSTRAINT [CK_CandidateList_name]
GO

-- Create temporary indexes

CREATE INDEX IX_JobSearchResult_jobAdId
ON dbo.JobSearchResult (jobAdId)
GO

CREATE INDEX IX_UserEventActionedJobAd_jobAdId
ON dbo.UserEventActionedJobAd (jobAdId)
GO

CREATE INDEX IX_JobAdIndustry_jobAdId
ON dbo.JobAdIndustry (jobAdId)
GO

CREATE INDEX IX_JobAdLocation_jobAdId
ON dbo.JobAdLocation (jobAdId)
GO

-- Rename JobAd to _old_JobAd

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[_old_JobAd]') AND type in (N'U'))
DROP TABLE [dbo].[_old_JobAd]
GO

EXEC sp_rename 'dbo.JobAd', '_old_JobAd'
GO

-- Drop the FT catalog

IF  EXISTS (SELECT * FROM sys.fulltext_indexes fti WHERE fti.object_id = OBJECT_ID(N'[dbo].[_old_JobAd]'))
ALTER FULLTEXT INDEX ON [dbo].[_old_JobAd] DISABLE

IF  EXISTS (SELECT * FROM sys.fulltext_indexes fti WHERE fti.object_id = OBJECT_ID(N'[dbo].[_old_JobAd]'))
DROP FULLTEXT INDEX ON [dbo].[_old_JobAd]

IF  EXISTS (SELECT * FROM sysfulltextcatalogs ftc WHERE ftc.name = N'JobAdCatalog')
DROP FULLTEXT CATALOG [JobAdCatalog]

-- Drop the keys and indexes

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[_old_JobAd]') AND name = N'PK_JobAd')
ALTER TABLE [dbo].[_old_JobAd] DROP CONSTRAINT [PK_JobAd]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAd_BrandingLogoFile]') AND parent_object_id = OBJECT_ID(N'[dbo].[_old_JobAd]'))
ALTER TABLE [dbo].[_old_JobAd] DROP CONSTRAINT [FK_JobAd_BrandingLogoFile]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAd_CandidateList]') AND parent_object_id = OBJECT_ID(N'[dbo].[_old_JobAd]'))
ALTER TABLE [dbo].[_old_JobAd] DROP CONSTRAINT [FK_JobAd_CandidateList]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAd_ContactDetails]') AND parent_object_id = OBJECT_ID(N'[dbo].[_old_JobAd]'))
ALTER TABLE [dbo].[_old_JobAd] DROP CONSTRAINT [FK_JobAd_ContactDetails]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAd_IntegratorUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[_old_JobAd]'))
ALTER TABLE [dbo].[_old_JobAd] DROP CONSTRAINT [FK_JobAd_IntegratorUser]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAd_JobPoster]') AND parent_object_id = OBJECT_ID(N'[dbo].[_old_JobAd]'))
ALTER TABLE [dbo].[_old_JobAd] DROP CONSTRAINT [FK_JobAd_JobPoster]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAd_Person]') AND parent_object_id = OBJECT_ID(N'[dbo].[_old_JobAd]'))
ALTER TABLE [dbo].[_old_JobAd] DROP CONSTRAINT [FK_JobAd_Person]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[_old_JobAd]') AND name = N'IX_JobAd_candidateListId')
DROP INDEX [IX_JobAd_candidateListId] ON [dbo].[_old_JobAd] WITH ( ONLINE = OFF )

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[_old_JobAd]') AND name = N'IX_JobAd_id_createdTime_jobTypes_status')
DROP INDEX [IX_JobAd_id_createdTime_jobTypes_status] ON [dbo].[_old_JobAd] WITH ( ONLINE = OFF )

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[_old_JobAd]') AND name = N'IX_JobAd_jobPosterId')
DROP INDEX [IX_JobAd_jobPosterId] ON [dbo].[_old_JobAd] WITH ( ONLINE = OFF )

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[_old_JobAd]') AND name = N'IX_JobAd_status_id_createdTime')
DROP INDEX [IX_JobAd_status_id_createdTime] ON [dbo].[_old_JobAd] WITH ( ONLINE = OFF )

GO

-- Create the new JobAd table

CREATE TABLE [dbo].[JobAd](
	[id] [uniqueidentifier] NOT NULL,
	[bulletPoints] [nvarchar](767) NULL,
	[content] [ntext] NOT NULL,
	[employerCompanyName] [dbo].[CompanyName] NULL,
	[expiryTime] [datetime] NOT NULL,
	[externalReferenceId] [varchar](50) NULL,
	[internalReferenceId] [int] NOT NULL,
	[jobTypes] [dbo].[JobTypes] NOT NULL,
	[lastUpdatedTime] [datetime] NOT NULL,
	[location] [nvarchar](200) NULL,
	[maxSalary] [dbo].[Salary] NULL,
	[minSalary] [dbo].[Salary] NULL,
	[packageDetails] [nvarchar](200) NULL,
	[postcode] [dbo].[Postcode] NULL,
	[residencyRequired] [bit] NOT NULL,
	[status] [dbo].[JobAdStatus] NOT NULL,
	[summary] [nvarchar](300) NULL,
	[title] [nvarchar](200) NOT NULL,
	[brandingLogoImageId] [uniqueidentifier] NULL,
	[candidateListId] [uniqueidentifier] NULL,
	[contactPersonId] [uniqueidentifier] NULL,
	[contactDetailsId] [uniqueidentifier] NOT NULL,
	[positionTitle] [nvarchar](200) NOT NULL,
	[createdTime] [datetime] NOT NULL,
	[externalApplyUrl] [dbo].[Url] NULL,
	[integratorUserId] [uniqueidentifier] NULL,
	[cssFilename] [dbo].[Filename] NULL,
	[salaryRateType] [dbo].[SalaryRateType] NOT NULL,
	[jobPosterId] [uniqueidentifier] NOT NULL,
	[jobg8ApplyForm] [text] NULL,

	CONSTRAINT [PK_JobAd] PRIMARY KEY NONCLUSTERED ([id])
)
GO

SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Note that this is the AD title, not the JOB title (which would be a separate field, but is currently not captured).' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JobAd', @level2type=N'COLUMN',@level2name=N'title'
GO

ALTER TABLE [dbo].[JobAd]  WITH NOCHECK ADD  CONSTRAINT [FK_JobAd_BrandingLogoFile] FOREIGN KEY([brandingLogoImageId])
REFERENCES [dbo].[FileReference] ([id])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[JobAd] CHECK CONSTRAINT [FK_JobAd_BrandingLogoFile]
GO
ALTER TABLE [dbo].[JobAd]  WITH NOCHECK ADD  CONSTRAINT [FK_JobAd_CandidateList] FOREIGN KEY([candidateListId])
REFERENCES [dbo].[CandidateList] ([id])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[JobAd] CHECK CONSTRAINT [FK_JobAd_CandidateList]
GO
ALTER TABLE [dbo].[JobAd]  WITH NOCHECK ADD  CONSTRAINT [FK_JobAd_ContactDetails] FOREIGN KEY([contactDetailsId])
REFERENCES [dbo].[ContactDetails] ([id])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[JobAd] CHECK CONSTRAINT [FK_JobAd_ContactDetails]
GO
ALTER TABLE [dbo].[JobAd]  WITH NOCHECK ADD  CONSTRAINT [FK_JobAd_IntegratorUser] FOREIGN KEY([integratorUserId])
REFERENCES [dbo].[IntegratorUser] ([id])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[JobAd] CHECK CONSTRAINT [FK_JobAd_IntegratorUser]
GO
ALTER TABLE [dbo].[JobAd]  WITH NOCHECK ADD  CONSTRAINT [FK_JobAd_JobPoster] FOREIGN KEY([jobPosterId])
REFERENCES [dbo].[JobPoster] ([id])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[JobAd] CHECK CONSTRAINT [FK_JobAd_JobPoster]
GO
ALTER TABLE [dbo].[JobAd]  WITH NOCHECK ADD  CONSTRAINT [FK_JobAd_Person] FOREIGN KEY([contactPersonId])
REFERENCES [dbo].[Person] ([id])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[JobAd] CHECK CONSTRAINT [FK_JobAd_Person]
GO

CREATE NONCLUSTERED INDEX [IX_JobAd_candidateListId] ON [dbo].[JobAd] 
( [candidateListId] ASC)

CREATE NONCLUSTERED INDEX [IX_JobAd_id_createdTime_jobTypes_status] ON [dbo].[JobAd] 
(
	[id] ASC,
	[createdTime] ASC,
	[jobTypes] ASC,
	[status] ASC
)

CREATE NONCLUSTERED INDEX [IX_JobAd_jobPosterId] ON [dbo].[JobAd] 
(
	[jobPosterId] ASC
)

CREATE NONCLUSTERED INDEX [IX_JobAd_status_id_createdTime] ON [dbo].[JobAd] 
(
	[status] ASC,
	[id] ASC,
	[createdTime] ASC
)

GO

-- Copy rows to keep to the new table

DECLARE @beforeDate DATETIME
SET @beforeDate = '1 Sep 2008'

declare @JobAd_Old table 
(
	id uniqueidentifier not null
);

insert into @JobAd_Old
	select id from dbo._old_JobAd
	where status = 3 and lastUpdatedTime < @beforeDate;
PRINT 'Got job ad IDs: ' + CONVERT(VARCHAR(100), GETDATE(), 14)

INSERT INTO dbo.JobAd
SELECT *
FROM dbo._old_JobAd
WHERE NOT (status = 3 and lastUpdatedTime < @beforeDate);

PRINT 'Copied rows to new JobAd table: ' + CONVERT(VARCHAR(100), GETDATE(), 14)

-- Drop the rows from tables other than JobAd

declare @JobApplication_Old table 
(
	id uniqueidentifier not null
);

insert into @JobApplication_Old
	select id from dbo.JobApplication
	where jobAdId in (select id from @JobAd_Old);
PRINT 'Got application IDs: ' + CONVERT(VARCHAR(100), GETDATE(), 14)

delete from dbo.CandidateListEntry where candidateListId in
(
	select candidateListId from dbo._old_JobAd
	where status = 3 and lastUpdatedTime < @beforeDate AND candidateListId IS NOT NULL
);
PRINT 'Done CandidateListEntry: ' + CONVERT(VARCHAR(100), GETDATE(), 14)

delete from dbo.JobApplication where id in (select id from @JobApplication_Old);
PRINT 'Done JobApplication: ' + CONVERT(VARCHAR(100), GETDATE(), 14)

delete from dbo.JobSearchResult where jobAdId in (select id from @JobAd_Old);
PRINT 'Done JobSearchResult: ' + CONVERT(VARCHAR(100), GETDATE(), 14)

delete from dbo.UserEventActionedJobAd where jobAdId in (select id from @JobAd_Old);
PRINT 'Done UserEventActionedJobAd: ' + CONVERT(VARCHAR(100), GETDATE(), 14)

delete from dbo.JobAdIndustry where jobAdId in (select id from @JobAd_Old);
PRINT 'Done JobAdIndustry: ' + CONVERT(VARCHAR(100), GETDATE(), 14)

delete from dbo.JobAdLocation where jobAdId in (select id from @JobAd_Old);
PRINT 'Done JobAdLocation: ' + CONVERT(VARCHAR(100), GETDATE(), 14)

-- Fix the few remaining JobAds with empty CandidateLists so we can delete the lists, too.

UPDATE dbo.JobAd
SET candidateListId = NULL
FROM dbo.JobAd ja
WHERE ja.candidateListId IS NOT NULL AND NOT EXISTS
(
	SELECT *
	FROM dbo.CandidateListEntry cle
	WHERE cle.candidateListId = ja.candidateListId
)
PRINT 'Done updating JobAds with empty lists: ' + CONVERT(VARCHAR(100), GETDATE(), 14)

-- Delete all empty candidate lists

delete from dbo.CandidateList where id in
(
	select id from dbo.CandidateList as cl where 
		not exists
		(
			select * from dbo.CandidateListEntry as cle where cle.candidateListId = cl.id
		)
);
PRINT 'Done CandidateList: ' + CONVERT(VARCHAR(100), GETDATE(), 14)

GO

-- Drop the indexes

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UserEventActionedJobAd]') AND name = N'IX_UserEventActionedJobAd_jobAdId')
DROP INDEX [IX_UserEventActionedJobAd_jobAdId] ON [dbo].[UserEventActionedJobAd] WITH ( ONLINE = OFF )

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[JobSearchResult]') AND name = N'IX_JobSearchResult_jobAdId')
DROP INDEX [IX_JobSearchResult_jobAdId] ON [dbo].[JobSearchResult] WITH ( ONLINE = OFF )

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[JobAdIndustry]') AND name = N'IX_JobAdIndustry_jobAdId')
DROP INDEX [IX_JobAdIndustry_jobAdId] ON [dbo].[JobAdIndustry] WITH ( ONLINE = OFF )

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[JobAdLocation]') AND name = N'IX_JobAdLocation_jobAdId')
DROP INDEX [IX_JobAdLocation_jobAdId] ON [dbo].[JobAdLocation] WITH ( ONLINE = OFF )

GO

-- Re-create the foreign keys and check constraints

ALTER TABLE [dbo].[JobApplication]  WITH NOCHECK
ADD  CONSTRAINT [FK_JobApplication_JobAd] FOREIGN KEY([jobAdId])
REFERENCES [dbo].[JobAd] ([id])

ALTER TABLE [dbo].[JobSearchResult] WITH NOCHECK
ADD CONSTRAINT [FK_JobSearchResult_JobAd] FOREIGN KEY([jobAdId])
REFERENCES [dbo].[JobAd] ([id])

ALTER TABLE [dbo].[UserEventActionedJobAd]  WITH NOCHECK
ADD  CONSTRAINT [FK_UserEventActionedJobAd_JobAd] FOREIGN KEY([jobAdId])
REFERENCES [dbo].[JobAd] ([id])

ALTER TABLE [dbo].[JobAdIndustry]  WITH NOCHECK
ADD  CONSTRAINT [FK_JobAdIndustry_JobAd] FOREIGN KEY([jobAdId])
REFERENCES [dbo].[JobAd] ([id])

ALTER TABLE [dbo].[JobAdLocation]  WITH NOCHECK
ADD  CONSTRAINT [FK_JobAdLocation_JobAd] FOREIGN KEY([jobAdId])
REFERENCES [dbo].[JobAd] ([id])

ALTER TABLE dbo.CandidateList
WITH NOCHECK
ADD CONSTRAINT CK_CandidateList_name
CHECK (dbo.HaveDuplicateCandidateListNames() = 0)

GO

-- Check constraints

ALTER TABLE [dbo].[JobApplication] CHECK CONSTRAINT [FK_JobApplication_JobAd]
ALTER TABLE [dbo].[JobSearchResult] CHECK CONSTRAINT [FK_JobSearchResult_JobAd]
ALTER TABLE [dbo].[UserEventActionedJobAd] CHECK CONSTRAINT [FK_UserEventActionedJobAd_JobAd]
ALTER TABLE [dbo].[JobAdIndustry] CHECK CONSTRAINT [FK_JobAdIndustry_JobAd]
ALTER TABLE [dbo].[JobAdLocation] CHECK CONSTRAINT [FK_JobAdLocation_JobAd]

GO

if (dbo.HaveDuplicateCandidateListNames() = 1)
	RAISERROR('The CandidateList table contains name conflicts.', 16, 1)

GO
