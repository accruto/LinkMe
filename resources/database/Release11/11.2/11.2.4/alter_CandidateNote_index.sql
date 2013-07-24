
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CandidateNote]') AND name = N'IX_CandidateNote_searcherId_candidateId')
DROP INDEX [IX_CandidateNote_searcherId_candidateId] ON [dbo].[CandidateNote] WITH ( ONLINE = OFF )

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CandidateNote]') AND name = N'IX_CandidateNote_Searcher_Candidate')
DROP INDEX [IX_CandidateNote_Searcher_Candidate] ON [dbo].[CandidateNote] WITH ( ONLINE = OFF )

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CandidateNote]') AND name = N'IX_CandidateNote_OwnedBy')
DROP INDEX [IX_CandidateNote_OwnedBy] ON [dbo].[CandidateNote] WITH ( ONLINE = OFF )

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CandidateNote]') AND name = N'IX_CandidateNote_sharedWithId_candidateId')
DROP INDEX [IX_CandidateNote_sharedWithId_candidateId] ON [dbo].[CandidateNote] WITH ( ONLINE = OFF )

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CandidateNote]') AND name = N'PK_CandidateNote')
ALTER TABLE [dbo].[CandidateNote] DROP CONSTRAINT [PK_CandidateNote]
GO

ALTER TABLE [dbo].[CandidateNote]
ADD CONSTRAINT [PK_CandidateNote] PRIMARY KEY NONCLUSTERED
(
	[id]
)
GO

CREATE NONCLUSTERED INDEX [IX_CandidateNote_sharedWithId_candidateId] ON [dbo].[CandidateNote] 
(
	[sharedWithId],
 	[candidateId]
)
GO

CREATE CLUSTERED INDEX [IX_CandidateNote_searcherId_candidateId] ON [dbo].[CandidateNote] 
(
	[searcherId],
 	[candidateId]
)
GO

