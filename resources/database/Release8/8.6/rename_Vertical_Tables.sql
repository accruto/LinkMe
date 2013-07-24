-- VerticalMember

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('VerticalMember') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN
	EXEC sp_rename 'VerticalMember', 'CommunityMember'
END

IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('CommunityMember') AND NAME = 'primaryVerticalId')
BEGIN
	EXEC sp_rename 'CommunityMember.primaryVerticalId', 'primaryCommunityId', 'COLUMN'
END

-- VerticalAssociation

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('VerticalAssociation') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN
	EXEC sp_rename 'VerticalAssociation', 'CommunityAssociation'
END

IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('CommunityAssociation') AND NAME = 'verticalId')
BEGIN
	EXEC sp_rename 'CommunityAssociation.verticalId', 'communityId', 'COLUMN'
END

-- VerticalOrganisationalUnit

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('VerticalOrganisationalUnit') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN
	EXEC sp_rename 'VerticalOrganisationalUnit', 'CommunityOrganisationalUnit'
END

-- Vertical

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('Vertical') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN
	EXEC sp_rename 'Vertical', 'Community'
END

IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('Community') AND NAME = 'displayName')
BEGIN
	EXEC sp_rename 'Community.displayName', 'name', 'COLUMN'
END

