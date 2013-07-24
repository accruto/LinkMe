EXEC sp_changeobjectowner 'linkme_owner.JobAd', dbo
GO

-- Change column types to user-defined types.

ALTER TABLE dbo.JobAd
ALTER COLUMN employerCompanyName CompanyName NULL

ALTER TABLE dbo.JobAd
ALTER COLUMN jobTypes JobTypes NOT NULL

ALTER TABLE dbo.JobAd
ALTER COLUMN maxSalary Salary NULL

ALTER TABLE dbo.JobAd
ALTER COLUMN minSalary Salary NULL

ALTER TABLE dbo.JobAd
ALTER COLUMN postcode Postcode NULL

ALTER TABLE dbo.JobAd
ALTER COLUMN status JobAdStatus NOT NULL

ALTER TABLE dbo.JobAd
ALTER COLUMN cssFilename [Filename] NULL

ALTER TABLE dbo.JobAd
ALTER COLUMN externalApplyUrl Url NULL

GO

-- Add salaryRateType and set all existing values to Year (1) or None (0) where there is no salary.

ALTER TABLE dbo.JobAd
ADD salaryRateType SalaryRateType NULL

GO

UPDATE dbo.JobAd
SET salaryRateType = 1
WHERE minSalary IS NOT NULL OR maxSalary IS NOT NULL

UPDATE dbo.JobAd
SET salaryRateType = 0
WHERE salaryRateType IS NULL

ALTER TABLE dbo.JobAd
ALTER COLUMN salaryRateType SalaryRateType NOT NULL

GO

-- Change jobPosterId to UNIQUEIDENTIFIER, updating constraints and indexes.

if exists (select * from dbo.sysindexes where name = N'IX_JobAd_JobPoster' and id = object_id(N'[dbo].[JobAd]'))
drop index [dbo].[JobAd].[IX_JobAd_JobPoster]

ALTER TABLE [dbo].[JobAd] DROP CONSTRAINT [FK_JobAd_employer_profile]

EXEC sp_rename 'dbo.JobAd.jobPosterId', '_jobPosterId', 'COLUMN'

ALTER TABLE dbo.JobAd
ADD jobPosterId UNIQUEIDENTIFIER NULL

GO

UPDATE dbo.JobAd
SET jobPosterId = dbo.GuidFromString(_jobPosterId)

GO

ALTER TABLE dbo.JobAd
ALTER COLUMN jobPosterId UNIQUEIDENTIFIER NOT NULL

ALTER TABLE dbo.JobAd
DROP COLUMN _jobPosterId

CREATE INDEX IX_JobAd_jobPosterId
ON dbo.JobAd (jobPosterId ASC)

ALTER TABLE dbo.JobAd
ADD CONSTRAINT FK_JobAd_JobPoster 
FOREIGN KEY (jobPosterId) REFERENCES dbo.JobPoster ([id])

GO

-- Change the BrandingLogo foreign key to point to FileReference

ALTER TABLE dbo.JobAd
DROP CONSTRAINT FK_JobAd_BrandingLogoFile

ALTER TABLE dbo.JobAd
ADD CONSTRAINT FK_JobAd_BrandingLogoFile
FOREIGN KEY (brandingLogoImageId) REFERENCES FileReference ([id])

GO

ALTER TABLE 
	[dbo].[jobad]
ALTER COLUMN
	[externalReferenceId] VARCHAR(50)
GO
