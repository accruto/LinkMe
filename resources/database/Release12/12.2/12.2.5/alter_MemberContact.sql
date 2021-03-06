IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[MemberContact]') AND name = N'IX_MemberContact_employerId')
DROP INDEX [IX_MemberContact_employerId] ON [dbo].[MemberContact]
GO

CREATE CLUSTERED INDEX [IX_MemberContact_employerId] ON [dbo].[MemberContact] 
(
	employerId,
	time
)
GO