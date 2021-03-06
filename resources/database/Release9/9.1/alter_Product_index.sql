-- Turn the primary key into a non-clustered index.

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND name = N'PK_Product')
ALTER TABLE [dbo].[Product] DROP CONSTRAINT [PK_Product]
GO

ALTER TABLE [dbo].[Product]
ADD CONSTRAINT [PK_Product] PRIMARY KEY NONCLUSTERED
(
	[id]
)
GO

-- Add index for userId

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND name = N'IX_Product_userId')
DROP INDEX [IX_Product_userId] ON [dbo].[Product]
GO

CREATE CLUSTERED INDEX [IX_Product_userId] ON [dbo].[Product]
(
	[userId]
)
GO

