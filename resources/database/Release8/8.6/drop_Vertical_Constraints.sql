IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('VerticalMember') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN

	ALTER TABLE VerticalMember DROP CONSTRAINT FK_primaryVerticalId
	ALTER TABLE VerticalMember DROP CONSTRAINT PK_VerticalMember

END

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('VerticalAssociation') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN

	ALTER TABLE VerticalAssociation DROP CONSTRAINT FK_VerticalAssociation_VerticalOrganisationalUnit
	ALTER TABLE VerticalAssociation DROP CONSTRAINT FK_VerticalAssociation_Vertical
	ALTER TABLE VerticalAssociation DROP CONSTRAINT PK_VerticalAssociation

END

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('VerticalOrganisationalUnit') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN

	ALTER TABLE VerticalOrganisationalUnit DROP CONSTRAINT PK_VerticalOrganisationalUnit

END

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('Vertical') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN

	ALTER TABLE Vertical DROP CONSTRAINT PK_Vertical
	ALTER TABLE Vertical DROP CONSTRAINT UQ_Vertical_displayName
	ALTER TABLE Vertical DROP CONSTRAINT UQ_Vertical_url

END

