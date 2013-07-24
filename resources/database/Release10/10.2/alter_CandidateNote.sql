-- Update first.

UPDATE
	dbo.CandidateNote
SET
	ownedById = NULL
WHERE
	isShared = 0

-- Update the columns.

IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('dbo.CandidateNote') AND NAME = 'isShared')
	ALTER TABLE dbo.CandidateNote
	DROP COLUMN isShared
GO

EXEC sp_rename 'dbo.CandidateNote.ownedById', 'sharedWithId'
GO

ALTER TABLE [dbo].[CandidateNote] DROP CONSTRAINT [FK_CandidateNote_Candidate]
GO

ALTER TABLE [dbo].[CandidateNote] DROP CONSTRAINT [FK_CandidateNote_CandidateSearcher]
GO

