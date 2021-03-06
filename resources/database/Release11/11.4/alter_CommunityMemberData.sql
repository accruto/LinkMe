IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CommunityMemberData]') AND name = N'PK_CommunityMemberData')
ALTER TABLE [dbo].[CommunityMemberData] DROP CONSTRAINT [PK_CommunityMemberData]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CommunityMemberData]') AND name = N'IX_CommunityMemberData')
DROP INDEX [IX_CommunityMemberData] ON [dbo].[CommunityMemberData]
GO

ALTER TABLE [dbo].[CommunityMemberData] ADD CONSTRAINT [PK_CommunityMemberData] PRIMARY KEY CLUSTERED 
(
	memberId,
	id,
	name
)

UPDATE
	dbo.CommunityMemberData
SET
	id = (SELECT primaryCommunityId FROM dbo.CommunityMember WHERE id = d.memberId)
FROM
	dbo.CommunityMemberData AS d

UPDATE
	dbo.CommunityMemberData
SET
	name = 'FinsiaMemberId'
WHERE
	id = '1AD1D2EC-2442-4360-9E10-F07512281FC9'
	AND name = 'MemberId'

UPDATE
	dbo.CommunityMemberData
SET
	name = 'ItcraLinkMemberStatus'
WHERE
	id = '6F8E9378-D3C8-416D-A05F-319BA4A10EDA'
	AND name = 'Status'

UPDATE
	dbo.CommunityMemberData
SET
	value = 'Certified'
WHERE
	id = '6F8E9378-D3C8-416D-A05F-319BA4A10EDA'
	AND name = 'ItcraLinkMemberStatus'
	AND value = 0

UPDATE
	dbo.CommunityMemberData
SET
	name = 'AimeMemberStatus'
WHERE
	id = '7088F0A9-E627-4D72-AA06-2305846EA5D1'
	AND name = 'Status'

UPDATE
	dbo.CommunityMemberData
SET
	value = 'BecomeMentor'
WHERE
	id = '7088F0A9-E627-4D72-AA06-2305846EA5D1'
	AND name = 'AimeMemberStatus'

