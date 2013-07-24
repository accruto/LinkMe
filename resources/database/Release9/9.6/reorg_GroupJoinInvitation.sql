-- Temporary drop constarints

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GroupJoinInvitation]') AND name = N'PK_GroupJoinInvitation')
ALTER TABLE [dbo].[GroupJoinInvitation] DROP CONSTRAINT [PK_GroupJoinInvitation]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GroupJoinInvitation]') AND name = N'IX_GroupJoinInvitation_groupId_inviteeContributorId')
DROP INDEX [IX_GroupJoinInvitation_groupId_inviteeContributorId] ON [dbo].[GroupJoinInvitation]
GO

-- Recreate constraints

ALTER TABLE [dbo].[GroupJoinInvitation] ADD CONSTRAINT [PK_GroupJoinInvitation] PRIMARY KEY NONCLUSTERED
(
	[id]
)

CREATE CLUSTERED INDEX IX_GroupJoinInvitation_groupId_inviteeContributorId ON dbo.GroupJoinInvitation 
(
	groupId,
	inviteeContributorId
)

