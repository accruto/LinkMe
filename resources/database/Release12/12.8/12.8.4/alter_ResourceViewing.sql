IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ResourceViewing]') AND name = N'PK_ResourceViewing')
ALTER TABLE [dbo].[ResourceViewing] DROP CONSTRAINT [PK_ResourceViewing]
GO

ALTER TABLE dbo.ResourceViewing
ADD CONSTRAINT PK_ResourceViewing PRIMARY KEY NONCLUSTERED
(
	id
)
GO

CREATE CLUSTERED INDEX [IX_ResourceViewing] ON [dbo].[ResourceViewing]
(
	resourceId ASC
)
GO

