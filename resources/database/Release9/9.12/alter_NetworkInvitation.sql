-- Temporary drop constraints

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NetworkInvitation]') AND name = N'PK_NetworkInvitation')
ALTER TABLE [dbo].[NetworkInvitation] DROP CONSTRAINT [PK_NetworkInvitation]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NetworkInvitation]') AND name = N'IX_NetworkInvitation_inviteeId')
DROP INDEX [IX_NetworkInvitation_inviteeId] ON [dbo].[NetworkInvitation]
GO

-- Recreate constraints

ALTER TABLE [dbo].[NetworkInvitation] ADD CONSTRAINT [PK_NetworkInvitation] PRIMARY KEY NONCLUSTERED
(
	[id]
)

CREATE CLUSTERED INDEX IX_NetworkInvitation_inviteeId ON dbo.NetworkInvitation 
(
	[inviteeId] ASC
)
GO

