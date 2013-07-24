ALTER TABLE dbo.GroupMembership DROP CONSTRAINT FK_GroupMembership_Contributor
GO

ALTER TABLE dbo.GroupMembershipBanned DROP CONSTRAINT FK_GroupMembershipBanned_Contributor
GO

ALTER TABLE dbo.GroupMembershipBanned DROP CONSTRAINT FK_GroupMembershipBanned_ByContributor
GO

ALTER TABLE dbo.GroupEventCoordinator DROP CONSTRAINT FK_EventCoordinator_Contributor
GO

ALTER TABLE dbo.GroupEventAttendee DROP CONSTRAINT FK_GroupEventAttendee_Contributor
GO

ALTER TABLE dbo.GroupEventInvitation DROP CONSTRAINT FK_GroupEventInvitation_Contributor
GO

ALTER TABLE dbo.GroupEventInvitation DROP CONSTRAINT FK_GroupEventInvitation_InviterContributor
GO

ALTER TABLE dbo.GroupJoinInvitation DROP CONSTRAINT FK_GroupJoinInvitation_Invitee
GO

ALTER TABLE dbo.UserContentRemovalRequest DROP CONSTRAINT FK_UserContentRemovalRequest_Contributor
GO

ALTER TABLE dbo.UserContentItem DROP CONSTRAINT FK_UserContentItem_Contributor_Deleter
GO

ALTER TABLE dbo.UserContentItem DROP CONSTRAINT FK_UserContentItem_Contributor_Poster
GO

ALTER TABLE dbo.DiscussionSubscriber DROP CONSTRAINT FK_DiscussionSubscriber_Contributor
GO

ALTER TABLE dbo.DiscussionBoardSubscriber DROP CONSTRAINT FK_DiscussionBoardSubscriber_Contributor
GO

ALTER TABLE dbo.DiscussionBoardModerator DROP CONSTRAINT FK_DiscussionBoardModerator_Contributor
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Contributor') AND type in (N'U'))
DROP TABLE dbo.Contributor
GO

