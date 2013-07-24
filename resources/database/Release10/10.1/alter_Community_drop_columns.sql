IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('dbo.CommunityOrganisationalUnit') AND NAME = 'flags')
BEGIN
	ALTER TABLE dbo.CommunityOrganisationalUnit
	DROP COLUMN flags
END
GO

IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('dbo.CommunityAssociation') AND NAME = 'flags')
BEGIN
	ALTER TABLE dbo.CommunityAssociation
	DROP COLUMN flags
END
GO
