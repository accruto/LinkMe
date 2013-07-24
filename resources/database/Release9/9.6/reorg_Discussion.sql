-- Temporary drop constarints

ALTER TABLE [dbo].[DiscussionPost] DROP CONSTRAINT [FK_DiscussionPost_Discussion]
GO

ALTER TABLE [dbo].[DiscussionSubscriber] DROP CONSTRAINT [FK_DiscussionSubscriber_Discussion]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Discussion]') AND name = N'PK_Discussion')
ALTER TABLE [dbo].[Discussion] DROP CONSTRAINT [PK_Discussion]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Discussion]') AND name = N'IX_Discussion_boardId')
DROP INDEX [IX_Discussion_boardId] ON [dbo].[Discussion]
GO

-- Recreate constraints

ALTER TABLE [dbo].[Discussion] ADD CONSTRAINT [PK_Discussion] PRIMARY KEY NONCLUSTERED
(
	[id]
)

CREATE CLUSTERED INDEX IX_Discussion_boardId ON dbo.Discussion 
(
	boardId
)
GO

ALTER TABLE [dbo].[DiscussionPost]  WITH NOCHECK ADD  CONSTRAINT [FK_DiscussionPost_Discussion] FOREIGN KEY([discussionId])
REFERENCES [dbo].[Discussion] ([id])
GO

ALTER TABLE [dbo].[DiscussionSubscriber]  WITH NOCHECK ADD  CONSTRAINT [FK_DiscussionSubscriber_Discussion] FOREIGN KEY([discussionId])
REFERENCES [dbo].[Discussion] ([id])
GO