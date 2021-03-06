IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ResumeSearch_SavedResumeSearch]') AND parent_object_id = OBJECT_ID(N'[dbo].[ResumeSearch]'))
ALTER TABLE [dbo].[ResumeSearch] DROP CONSTRAINT [FK_ResumeSearch_SavedResumeSearch]

-- Turn the primary key into a non-clustered index.

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SavedResumeSearch]') AND name = N'PK_SavedResumeSearch')
ALTER TABLE [dbo].[SavedResumeSearch] DROP CONSTRAINT [PK_SavedResumeSearch]
GO

ALTER TABLE [dbo].[SavedResumeSearch]
ADD CONSTRAINT [PK_SavedResumeSearch] PRIMARY KEY NONCLUSTERED
(
	[id]
)
GO

-- Add index for ownerId

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SavedResumeSearch]') AND name = N'IX_SavedResumeSearch_ownerId_name')
DROP INDEX [IX_SavedResumeSearch_ownerId_name] ON [dbo].[SavedResumeSearch]
GO

CREATE UNIQUE CLUSTERED INDEX [IX_SavedResumeSearch_ownerId_name] ON [dbo].[SavedResumeSearch]
(
	[ownerId],
	[name]
)
GO

