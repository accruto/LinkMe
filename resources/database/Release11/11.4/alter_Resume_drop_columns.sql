ALTER TABLE dbo.Resume
DROP COLUMN version

ALTER TABLE dbo.Resume
DROP COLUMN sha1sum

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Resume]') AND name = N'IX_Resume_lensXmlIsNull')
DROP INDEX [IX_Resume_lensXmlIsNull] ON [dbo].[Resume] WITH ( ONLINE = OFF )
GO

ALTER TABLE dbo.Resume
DROP COLUMN lensXmlIsNull
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[OnResumeInsertUpdate]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
DROP TRIGGER [dbo].[OnResumeInsertUpdate]
GO

-- View does not seem to be used any more/

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ResumeViewsByCandidate]'))
DROP VIEW [dbo].[ResumeViewsByCandidate]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Resume]') AND name = N'IX_Resume_candidateId_id')
DROP INDEX [IX_Resume_candidateId_id] ON [dbo].[Resume] WITH ( ONLINE = OFF )
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resume_Candidate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resume]'))
ALTER TABLE [dbo].[Resume] DROP CONSTRAINT [FK_Resume_Candidate]
GO

ALTER TABLE dbo.Resume
DROP COLUMN candidateId

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resume_ParsedFromFile]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resume]'))
ALTER TABLE [dbo].[Resume] DROP CONSTRAINT [FK_Resume_ParsedFromFile]

ALTER TABLE dbo.Resume
DROP COLUMN parsedFromFileId
