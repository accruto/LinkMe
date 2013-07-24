IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GroupJoinInvitation') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GroupJoinInvitation;

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GroupMembership') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GroupMembership;

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GroupMembershipBanned') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GroupMembershipBanned;


IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GroupTag') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GroupTag;

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('Tag') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Tag;

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('Contributor') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Contributor;

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GroupAllowedEmailDomain') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GroupAllowedEmailDomain;

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('[Group]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Group];

