IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('VerticalOrganisationalUnit') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE VerticalOrganisationalUnit
GO

IF EXISTS (SELECT * FROM dbo.systypes WHERE name = 'VerticalOrganisationalUnitFlags')
EXEC sp_droptype 'VerticalOrganisationalUnitFlags'
GO

EXEC sp_addtype 'VerticalOrganisationalUnitFlags', 'tinyint', 'not null'
GO


CREATE TABLE VerticalOrganisationalUnit ( 
	id UNIQUEIDENTIFIER NOT NULL,
	flags VerticalOrganisationalUnitFlags NOT NULL
)
GO

ALTER TABLE VerticalOrganisationalUnit ADD CONSTRAINT PK_VerticalOrganisationalUnit
	PRIMARY KEY CLUSTERED (id)
GO

