IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FileReference_FileData]') AND parent_object_id = OBJECT_ID(N'[dbo].[FileReference]'))
ALTER TABLE [dbo].[FileReference] DROP CONSTRAINT [FK_FileReference_FileData]

-- Turn the primary key into a non-clustered index.

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[FileData]') AND name = N'PK_FileData')
ALTER TABLE [dbo].[FileData] DROP CONSTRAINT [PK_FileData]
GO

ALTER TABLE [dbo].[FileData]
ADD CONSTRAINT [PK_FileData] PRIMARY KEY NONCLUSTERED
(
	[id]
)
GO

-- Add index for context and size

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[FileData]') AND name = N'IX_FileData_size_context')
DROP INDEX [IX_FileData_size_context] ON [dbo].[FileData]
GO

CREATE CLUSTERED INDEX [IX_FileData_size_context] ON [dbo].[FileData]
(
	[sizeBytes],
	[context]
)
GO

ALTER TABLE dbo.[FileReference]
ADD CONSTRAINT [FK_FileReference_FileData] FOREIGN KEY (dataId)
REFERENCES dbo.FileData (id)
