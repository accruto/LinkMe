CREATE TABLE [dbo].[ResourcePollAnswerVote](
	[id] [uniqueidentifier] NOT NULL,
	[resourcePollAnswerId] [uniqueidentifier] NOT NULL,
	[userid] [uniqueidentifier] NOT NULL,
	[createdTime] [datetime] NOT NULL,
 CONSTRAINT [PK_resourcePollAnswer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)

GO
ALTER TABLE [dbo].[ResourcePollAnswerVote]  WITH CHECK ADD  CONSTRAINT [FK_resourcePollAnswer_ResourcePoll] FOREIGN KEY([resourcePollAnswerId])
REFERENCES [dbo].[ResourcePollAnswer] ([id])
GO
ALTER TABLE [dbo].[ResourcePollAnswerVote] CHECK CONSTRAINT [FK_resourcePollAnswer_ResourcePoll]