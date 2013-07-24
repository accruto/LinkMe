IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('VerticalNav') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE VerticalNav
GO

CREATE TABLE VerticalNav ( 
	verticalId UNIQUEIDENTIFIER NOT NULL,
	nav NVARCHAR(100) NOT NULL,
	newNav NVARCHAR(100) NOT NULL
)
GO

ALTER TABLE VerticalNav ADD CONSTRAINT PK_VerticalNav
	PRIMARY KEY CLUSTERED (verticalId, nav)
GO

