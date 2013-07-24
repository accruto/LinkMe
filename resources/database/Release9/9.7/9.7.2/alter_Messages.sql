ALTER TABLE dbo.UserMessage DROP CONSTRAINT FK_UserMessage_SenderUser
GO

ALTER TABLE dbo.MessageThreadParticipant DROP CONSTRAINT FK_MessageThreadParticipant_ParticipantUser
GO

DROP PROCEDURE [dbo].[GetNewMessageCount]
GO

-- Temporary drop constarints

ALTER TABLE [dbo].[MessageThreadParticipant] DROP CONSTRAINT [PK_MessageThreadParticipant]

GO

-- Recreate constraints

ALTER TABLE [dbo].[MessageThreadParticipant] ADD CONSTRAINT [PK_MessageThreadParticipant] PRIMARY KEY NONCLUSTERED
(
	[threadId],
	[userId]
)
GO

-- Create indexes

CREATE CLUSTERED INDEX IX_MessageThreadParticipant_threadId ON dbo.MessageThreadParticipant
(
	threadId ASC
)
GO

CREATE INDEX IX_MessageThreadParticipant_userId ON dbo.MessageThreadParticipant
(
	userId ASC
)
GO
