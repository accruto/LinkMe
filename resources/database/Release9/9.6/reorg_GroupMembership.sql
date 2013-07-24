-- Temporary drop constarints

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GroupMembership]') AND name = N'PK_GroupMembership')
ALTER TABLE [dbo].[GroupMembership] DROP CONSTRAINT [PK_GroupMembership]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GroupMembership]') AND name = N'IX_GroupMembership_contributorId_groupId')
DROP INDEX [IX_GroupMembership_contributorId_groupId] ON [dbo].[GroupMembership]
GO

-- Recreate constraints

ALTER TABLE [dbo].[GroupMembership] ADD CONSTRAINT [PK_GroupMembership] PRIMARY KEY NONCLUSTERED
(
	[id]
)

CREATE CLUSTERED INDEX IX_GroupMembership_contributorId_groupId ON dbo.GroupMembership 
(
	contributorId,
	groupId
)

