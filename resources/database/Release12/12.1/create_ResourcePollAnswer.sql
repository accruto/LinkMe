CREATE TABLE [dbo].[ResourcePollAnswer](
	[id] [uniqueidentifier] NOT NULL,
	[pollName] [nvarchar](50) NOT NULL,
	[answerNumber] [tinyint] NOT NULL,
	[voteCount] [int] NOT NULL,
 CONSTRAINT [PK_ResourcePoll] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)
