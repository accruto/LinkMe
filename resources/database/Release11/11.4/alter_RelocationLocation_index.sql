-- Drop useless index.

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RelocationLocation]') AND name = N'IX_RelocationLocation_data')
DROP INDEX [IX_RelocationLocation_data] ON [dbo].[RelocationLocation]
GO

-- Turn the primary key into a non-clustered index.

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RelocationLocation]') AND name = N'PK_RelocationLocation')
ALTER TABLE [dbo].[RelocationLocation] DROP CONSTRAINT [PK_RelocationLocation]
GO

ALTER TABLE [dbo].[RelocationLocation]
ADD CONSTRAINT [PK_RelocationLocation] PRIMARY KEY NONCLUSTERED
(
	[id]
)
GO

-- Add index for candidateId

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RelocationLocation]') AND name = N'IX_RelocationLocation_candidateId')
DROP INDEX [IX_RelocationLocation_candidateId] ON [dbo].[RelocationLocation]
GO

CREATE CLUSTERED INDEX [IX_RelocationLocation_candidateId] ON [dbo].[RelocationLocation]
(
	[candidateId],
	[locationReferenceId]
)
GO

