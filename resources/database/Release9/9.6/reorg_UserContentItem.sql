-- Temporary drop constarints

ALTER TABLE [dbo].[UserContentRemovalRequest] DROP CONSTRAINT [FK_UserContentRemovalRequest_UserContentItem]
GO

ALTER TABLE [dbo].[Discussion] DROP CONSTRAINT [FK_Discussion_UserContentItem]
GO

ALTER TABLE [dbo].[DiscussionPost] DROP CONSTRAINT [FK_DiscussionPost_UserContentItem]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UserContentItem]') AND name = N'PK_UserContentItem')
ALTER TABLE [dbo].[UserContentItem] DROP CONSTRAINT [PK_UserContentItem]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UserContentItem]') AND name = N'IX_UserContentItem_createdTime')
DROP INDEX [IX_UserContentItem_createdTime] ON [dbo].[UserContentItem]
GO

-- Recreate constraints

ALTER TABLE [dbo].[UserContentItem] ADD CONSTRAINT [PK_UserContentItem] PRIMARY KEY NONCLUSTERED
(
	[id]
)

CREATE CLUSTERED INDEX IX_UserContentItem_createdTime ON dbo.UserContentItem 
(
	createdTime
)
GO

ALTER TABLE [dbo].[DiscussionPost] WITH NOCHECK ADD CONSTRAINT [FK_DiscussionPost_UserContentItem] FOREIGN KEY([id])
REFERENCES [dbo].[UserContentItem] ([id])
GO

ALTER TABLE [dbo].[Discussion] WITH NOCHECK ADD CONSTRAINT [FK_Discussion_UserContentItem] FOREIGN KEY([id])
REFERENCES [dbo].[UserContentItem] ([id])
GO

ALTER TABLE [dbo].[UserContentRemovalRequest] WITH NOCHECK ADD CONSTRAINT [FK_UserContentRemovalRequest_UserContentItem] FOREIGN KEY([userContentItemId])
REFERENCES [dbo].[UserContentItem] ([id])
GO
