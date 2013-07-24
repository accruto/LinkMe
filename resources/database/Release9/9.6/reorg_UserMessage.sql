-- Temporary drop constarints

ALTER TABLE [dbo].[UserMessage] DROP CONSTRAINT [FK_UserMessage_MessageThread]

ALTER TABLE [dbo].[UserMessage] DROP CONSTRAINT [FK_UserMessage_SenderUser]

ALTER TABLE [dbo].[UserMessageAttachment] DROP CONSTRAINT [FK_UserMessageAttachment_UserMessage]

ALTER TABLE [dbo].[UserMessage] DROP CONSTRAINT [PK_UserMessage]

GO

-- Recreate constraints

ALTER TABLE [dbo].[UserMessage] ADD  CONSTRAINT [PK_UserMessage] PRIMARY KEY NONCLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]


ALTER TABLE [dbo].[UserMessage]  WITH NOCHECK ADD  CONSTRAINT [FK_UserMessage_MessageThread] FOREIGN KEY([threadId])
REFERENCES [dbo].[MessageThread] ([id])

ALTER TABLE [dbo].[UserMessage] NOCHECK CONSTRAINT [FK_UserMessage_MessageThread]


ALTER TABLE [dbo].[UserMessage]  WITH NOCHECK ADD  CONSTRAINT [FK_UserMessage_SenderUser] FOREIGN KEY([senderId])
REFERENCES [dbo].[RegisteredUser] ([id])

ALTER TABLE [dbo].[UserMessage] NOCHECK CONSTRAINT [FK_UserMessage_SenderUser]


ALTER TABLE [dbo].[UserMessageAttachment]  WITH NOCHECK ADD  CONSTRAINT [FK_UserMessageAttachment_UserMessage] FOREIGN KEY([messageId])
REFERENCES [dbo].[UserMessage] ([id])

ALTER TABLE [dbo].[UserMessageAttachment] NOCHECK CONSTRAINT [FK_UserMessageAttachment_UserMessage]

GO

-- Create indexes

CREATE CLUSTERED INDEX IX_UserMessage_threadId ON dbo.UserMessage 
(
	threadId ASC
)

CREATE INDEX IX_UserMessage_threadId_senderId ON dbo.UserMessage 
(
	threadId ASC,
	senderId ASC
)