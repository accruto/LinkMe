-- Drop keys on CandidateListEntry

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAd_CandidateList]') AND parent_object_id = OBJECT_ID(N'[dbo].[JobAd]'))
ALTER TABLE [dbo].[JobAd] DROP CONSTRAINT [FK_JobAd_CandidateList]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CandidateListEntry]') AND name = N'PK_CandidateListEntry')
ALTER TABLE [dbo].[CandidateListEntry] DROP CONSTRAINT [PK_CandidateListEntry]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CandidateListEntry_CandidateList]') AND parent_object_id = OBJECT_ID(N'[dbo].[CandidateListEntry]'))
ALTER TABLE [dbo].[CandidateListEntry] DROP CONSTRAINT [FK_CandidateListEntry_CandidateList]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CandidateListEntry_CreatedByEmployer]') AND parent_object_id = OBJECT_ID(N'[dbo].[CandidateListEntry]'))
ALTER TABLE [dbo].[CandidateListEntry] DROP CONSTRAINT [FK_CandidateListEntry_CreatedByEmployer]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CandidateListEntry_JobApplication]') AND parent_object_id = OBJECT_ID(N'[dbo].[CandidateListEntry]'))
ALTER TABLE [dbo].[CandidateListEntry] DROP CONSTRAINT [FK_CandidateListEntry_JobApplication]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CandidateListEntry]') AND name = N'IX_CandidateListEntry_jobApplicationId')
DROP INDEX [IX_CandidateListEntry_jobApplicationId] ON [dbo].[CandidateListEntry]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CandidateList]') AND name = N'PK_CandidateList')
ALTER TABLE [dbo].[CandidateList] DROP CONSTRAINT [PK_CandidateList]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CandidateList]') AND name = N'IX_CandidateList_ownerId')
DROP INDEX [IX_CandidateList_ownerId] ON [dbo].[CandidateList]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CandidateList]') AND name = N'IX_CandidateList_sharedWithId')
DROP INDEX [IX_CandidateList_sharedWithId] ON [dbo].[CandidateList]

-- Update

UPDATE
	dbo.CandidateListEntry
SET
	candidateListId = j.id
FROM
	dbo.CandidateListEntry AS e
INNER JOIN
	dbo.JobAd AS j ON j.candidateListId = e.candidateListId

DECLARE @temp AS TABLE (id UNIQUEIDENTIFIER)
DECLARE @count INT
DECLARE @total INT
SET @count = 1

WHILE @count > 0
BEGIN
	INSERT
		@temp
	SELECT
		TOP 100 l.id
	FROM
		dbo.CandidateList AS l
	WHERE
		l.id IN (SELECT candidateListId FROM dbo.JobAd)

	UPDATE
		dbo.CandidateList
	SET
		id = (SELECT j.id FROM JobAd AS j WHERE j.candidateListId = l.id)
	FROM
		dbo.CandidateList AS l
	WHERE
		l.id IN (SELECT id FROM @temp)

	DELETE @temp

	SELECT @total = COUNT(*) FROM dbo.CandidateList WHERE id IN (SELECT candidateListId FROM JobAd)
	PRINT @total

	SET @count = @count - 1
END
GO

-- Recreate keys on CandidateList

ALTER TABLE [dbo].[CandidateList]
ADD CONSTRAINT [PK_CandidateList] PRIMARY KEY NONCLUSTERED 
(
	[id] ASC
)

CREATE CLUSTERED INDEX [IX_CandidateList_ownerId] ON [dbo].[CandidateList] 
(
	[ownerId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CandidateList_sharedWithId] ON [dbo].[CandidateList] 
(
	[sharedWithId] ASC
)

ALTER TABLE [dbo].[CandidateListEntry]
ADD CONSTRAINT [PK_CandidateListEntry] PRIMARY KEY CLUSTERED 
(
	[candidateListId] ASC,
	[candidateId] ASC
)
GO

ALTER TABLE [dbo].[CandidateListEntry]
ADD CONSTRAINT [FK_CandidateListEntry_CandidateList] FOREIGN KEY([candidateListId])
REFERENCES [dbo].[CandidateList] ([id])
GO

ALTER TABLE [dbo].[CandidateListEntry]
ADD CONSTRAINT [FK_CandidateListEntry_JobApplication] FOREIGN KEY([jobApplicationId])
REFERENCES [dbo].[JobApplication] ([id])
GO

CREATE NONCLUSTERED INDEX [IX_CandidateListEntry_jobApplicationId] ON [dbo].[CandidateListEntry] 
(
	[jobApplicationId] ASC
)


