IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('VerticalAssociation') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE VerticalAssociation
GO

IF EXISTS (SELECT * FROM dbo.systypes WHERE name = 'VerticalAssociationFlags')
EXEC sp_droptype 'VerticalAssociationFlags'
GO

EXEC sp_addtype 'VerticalAssociationFlags', 'tinyint', 'not null'
GO

CREATE TABLE VerticalAssociation ( 
	organisationalUnitId UNIQUEIDENTIFIER NOT NULL,
	verticalId UNIQUEIDENTIFIER NOT NULL,
	flags VerticalAssociationFlags NOT NULL
)
GO

ALTER TABLE VerticalAssociation ADD CONSTRAINT PK_VerticalAssociation
	PRIMARY KEY CLUSTERED (organisationalUnitId, verticalId)
GO

ALTER TABLE VerticalAssociation ADD CONSTRAINT FK_VerticalAssociation_VerticalOrganisationalUnit
	FOREIGN KEY (organisationalUnitId) REFERENCES VerticalOrganisationalUnit (id)
GO

ALTER TABLE VerticalAssociation ADD CONSTRAINT FK_VerticalAssociation_Vertical
	FOREIGN KEY (verticalId) REFERENCES Vertical (id)
GO

